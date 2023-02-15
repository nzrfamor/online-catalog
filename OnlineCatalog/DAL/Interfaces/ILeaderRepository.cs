using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ILeaderRepository : IRepository<Leader>
    {
        Task<IEnumerable<Leader>> GetAllWithDetailsAsync();
        Task<Leader> GetByIdWithDetailsAsync(int id);
    }
}
