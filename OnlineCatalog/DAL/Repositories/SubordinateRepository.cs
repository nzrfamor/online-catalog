using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class SubordinateRepository : ISubordinateRepository
    {
        readonly ProjectDbContext db;

        public SubordinateRepository(ProjectDbContext _db)
        {
            db = _db;
        }

        public async Task AddAsync(Subordinate entity)
        {
            await db.Subordinates.AddAsync(entity);
        }

        public void Delete(Subordinate entity)
        {
            db.Subordinates.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            db.Subordinates.Remove(await db.Subordinates.FirstOrDefaultAsync(s => s.Id == id));
        }

        public async Task<IEnumerable<Subordinate>> GetAllAsync()
        {
            return await db.Subordinates.ToListAsync();
        }

        public async Task<IEnumerable<Subordinate>> GetAllWithDetailsAsync()
        {
            return await db.Subordinates
                           .Include(s => s.Leader)
                           .Include(l => l.Worker)
                           .ToListAsync();
        }

        public async Task<Subordinate> GetByIdAsync(int id)
        {
            return await db.Subordinates.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<Subordinate> GetByIdWithDetailsAsync(int id)
        {
            return await db.Subordinates
                           .Include(s => s.Leader)
                           .Include(l => l.Worker)
                           .FirstOrDefaultAsync(s => s.Id == id);
        }

        public void Update(Subordinate entity)
        {
            db.Subordinates.Update(entity);
        }
    }
}
