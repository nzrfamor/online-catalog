using DAL.Interfaces;
using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        readonly ProjectDbContext db;
        private IWorkerRepository workerRepository;
        private IPersonRepository personRepository;
        private ILeaderRepository leaderRepository;
        private ISubordinateRepository subordinateRepository;
        public UnitOfWork(ProjectDbContext _db)
        {
            db = _db;
        }

        public IWorkerRepository WorkerRepository => workerRepository ??= new WorkerRepository(db);

        public IPersonRepository PersonRepository => personRepository ??= new PersonRepository(db);

        public ILeaderRepository LeaderRepository => leaderRepository ??= new LeaderRepository(db);

        public ISubordinateRepository SubordinateRepository => subordinateRepository ??= new SubordinateRepository(db);

        public async Task SaveAsync()
        {
            await db.SaveChangesAsync();
        }
    }
}
