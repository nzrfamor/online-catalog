using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class WorkerRepository : IWorkerRepository
    {
        readonly ProjectDbContext db;
        public WorkerRepository(ProjectDbContext _db)
        {
            db = _db;
        }
        public async Task AddAsync(Worker entity)
        {
            await db.Workers.AddAsync(entity);
        }

        public void Delete(Worker entity)
        {
            db.Workers.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            db.Workers.Remove(await db.Workers.FirstOrDefaultAsync(w => w.Id == id));
        }

        public async Task<IEnumerable<Worker>> GetAllAsync()
        {
            return await db.Workers.ToListAsync();
        }

        public async Task<IEnumerable<Worker>> GetAllWithDetailsAsync()
        {
            return await db.Workers.Include(w => w.Person)
                                   .Include(w => w.WorkerAsSubordinate)
                                   .ThenInclude(s => s.Leader)
                                   .Include(w => w.WorkerAsLeader)
                                   .ThenInclude(l => l.Subordinates)
                                   .ToListAsync();
        }

        public Task<Worker> GetByIdAsync(int id)
        {
            return db.Workers.FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<Worker> GetByIdWithDetailsAsync(int id)
        {
            return await db.Workers.Include(w => w.Person)
                                   .Include(w => w.WorkerAsSubordinate)
                                   .ThenInclude(s => s.Leader)
                                   .Include(w => w.WorkerAsLeader)
                                   .ThenInclude(l => l.Subordinates)
                                   .FirstOrDefaultAsync(w => w.Id == id);
        }

        public void Update(Worker entity)
        {
            db.Workers.Update(entity);
        }
    }
}
