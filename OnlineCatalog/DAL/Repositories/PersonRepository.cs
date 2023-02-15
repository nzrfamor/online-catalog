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
    public class PersonRepository : IPersonRepository
    {
        readonly ProjectDbContext db;
        public PersonRepository(ProjectDbContext _db)
        {
            db = _db;
        }
        public async Task AddAsync(Person entity)
        {
            await db.Persons.AddAsync(entity);
        }

        public void Delete(Person entity)
        {
            db.Persons.Remove(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            db.Persons.Remove(await db.Persons.FirstOrDefaultAsync(p => p.Id == id));
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await db.Persons.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(int id)
        {
            return await db.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }

        public void Update(Person entity)
        {
            db.Persons.Update(entity);
        }
    }
}
