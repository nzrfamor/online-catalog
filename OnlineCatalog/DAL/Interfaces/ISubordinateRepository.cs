using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface ISubordinateRepository : IRepository<Subordinate>
    {
        Task<IEnumerable<Subordinate>> GetAllWithDetailsAsync();
        Task<Subordinate> GetByIdWithDetailsAsync(int id);
    }
}
