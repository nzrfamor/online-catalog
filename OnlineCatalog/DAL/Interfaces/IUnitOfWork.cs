using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IPersonRepository PersonRepository { get; }
        IWorkerRepository WorkerRepository { get; }
        ILeaderRepository LeaderRepository { get; }
        ISubordinateRepository SubordinateRepository { get; }

        Task SaveAsync();
    }
}
