using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class LeaderRepository : ILeaderRepository
    {
        readonly ProjectDbContext db;
        public LeaderRepository(ProjectDbContext _db)
        {
            db = _db;
        }

        public async Task AddAsync(Leader entity)
        {
            await db.Leaders.AddAsync(entity);
        }

        public void Delete(Leader entity)
        {
            db.Leaders.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            db.Leaders.Remove(await db.Leaders.FirstOrDefaultAsync(l => l.Id == id));
        }

        public async Task<IEnumerable<Leader>> GetAllAsync()
        {
            return await db.Leaders.ToListAsync();
        }

        public async Task<IEnumerable<Leader>> GetAllWithDetailsAsync()
        {
            return await db.Leaders
                           .Include(l => l.Subordinates)
                           .Include(l => l.Worker)
                           .ToListAsync();
        }

        public async Task<Leader> GetByIdAsync(int id)
        {
            return await db.Leaders.FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Leader> GetByIdWithDetailsAsync(int id)
        {
            return await db.Leaders
                           .Include(l => l.Subordinates)
                           .Include(l => l.Worker)
                           .FirstOrDefaultAsync(l => l.Id == id);
        }

        public void Update(Leader entity)
        {
            db.Leaders.Update(entity);
        }
    }
}
