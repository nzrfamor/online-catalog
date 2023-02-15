using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class LeaderService : ILeaderService
    {
        readonly IUnitOfWork unitOfWork;
        readonly IMapper mapper;
        public LeaderService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task AddAsync(LeaderModel model)
        {
            var leader = mapper.Map<Leader>(model);
            await unitOfWork.LeaderRepository.AddAsync(leader);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {

            await unitOfWork.LeaderRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<LeaderModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<LeaderModel>>(await unitOfWork.LeaderRepository.GetAllWithDetailsAsync());
        }

        public async Task<LeaderModel> GetByIdAsync(int id)
        {
            return mapper.Map<LeaderModel>(await unitOfWork.LeaderRepository.GetByIdWithDetailsAsync(id));
        }
        public async Task<LeaderModel> GetByWorkerIdAsync(int id)
        {
            var leaders = await unitOfWork.LeaderRepository.GetAllWithDetailsAsync();
            return mapper.Map<LeaderModel>(leaders.FirstOrDefault(l => l.WorkerId == id));
        }

        public async Task UpdateAsync(LeaderModel model)
        {
            var leader = mapper.Map<Leader>(model);
            unitOfWork.LeaderRepository.Update(leader);
            await unitOfWork.SaveAsync();

        }
    }
}
