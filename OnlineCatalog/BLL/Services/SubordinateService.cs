using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SubordinateService : ISubordinateService
    {
        readonly IUnitOfWork unitOfWork;
        readonly IMapper mapper;
        public SubordinateService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task AddAsync(SubordinateModel model)
        {
            if (model == null)
            {
                throw new CatalogException("SubordinateModel object cannot be null!");
            }
            var subordinate = mapper.Map<Subordinate>(model);
            await unitOfWork.SubordinateRepository.AddAsync(subordinate);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await unitOfWork.SubordinateRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<SubordinateModel>> GetAllAsync()
        {
            return mapper.Map<IEnumerable<SubordinateModel>>(await unitOfWork.SubordinateRepository.GetAllAsync());
        }

        public async Task<SubordinateModel> GetByIdAsync(int id)
        {
            return mapper.Map<SubordinateModel>(await unitOfWork.SubordinateRepository.GetByIdAsync(id));
        }

        public async Task UpdateAsync(SubordinateModel model)
        {
            if (model == null)
            {
                throw new CatalogException("SubordinateModel object cannot be null!");
            }

            unitOfWork.SubordinateRepository.Update(mapper.Map<Subordinate>(model));

            await unitOfWork.SaveAsync();
        }
    }
}
