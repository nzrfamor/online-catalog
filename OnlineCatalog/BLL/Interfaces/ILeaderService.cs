using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ILeaderService : ICrud<LeaderModel>
    {
        public Task<LeaderModel> GetByWorkerIdAsync(int id);
    }
}
