using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IWorkerService : ICrud<WorkerModel>
    {
        Task<IEnumerable<WorkerModel>> GetWorkersByLeaderIdAsync(int leaderId);
        Task<WorkerModel> GetWorkerBySubordinateIdAsync(int subordinateId);
        Task<IEnumerable<WorkerModel>> GetWorkerAsPossibleLeadersBySubordinateIdAsync(int subordinateId);
    }
}
