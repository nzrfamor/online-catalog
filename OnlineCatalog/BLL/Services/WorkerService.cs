using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class WorkerService : IWorkerService
    {
        readonly IUnitOfWork unitOfWork;
        readonly IMapper mapper;
        public WorkerService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            mapper = _mapper;
        }
        public async Task AddAsync(WorkerModel model)
        {
            if (model == null)
            {
                throw new CatalogException("WorkerModel object cannot be null!");
            }
            if (model.Name == null || model.Name.Length == 0)
            {
                throw new CatalogException("Name cannot be empty!");
            }
            if (model.Surname == null || model.Surname.Length == 0)
            {
                throw new CatalogException("Surname cannot be empty!");
            }
            if (model.HireDate >= DateTime.Now || model.HireDate < DateTime.Parse("1991-1-1"))
            {
                throw new CatalogException("Incorrect HireDate value!");
            }
            if(model.Salary < 5000m)
            {
                throw new CatalogException("Salary cannot be less than 5000!");
            }
            var worker = mapper.Map<Worker>(model);
            await unitOfWork.WorkerRepository.AddAsync(worker);
            await unitOfWork.SaveAsync();
        }

        public async Task DeleteAsync(int modelId)
        {
            await unitOfWork.WorkerRepository.DeleteByIdAsync(modelId);
            await unitOfWork.SaveAsync();
        }

        public async Task<IEnumerable<WorkerModel>> GetAllAsync()
        {
            var workers = await unitOfWork.WorkerRepository.GetAllWithDetailsAsync();
            return mapper.Map<IEnumerable<WorkerModel>>(workers);
        }

        public async Task<WorkerModel> GetByIdAsync(int id)
        {
            var worker = await unitOfWork.WorkerRepository.GetByIdWithDetailsAsync(id);
            return mapper.Map<WorkerModel>(worker);
        }

        public async Task<IEnumerable<WorkerModel>> GetWorkerAsPossibleLeadersBySubordinateIdAsync(int subordinateId)
        {
            IEnumerable<Worker> possibleLeaders = new List<Worker>();
            var workers = await unitOfWork.WorkerRepository.GetAllWithDetailsAsync();
            var worker = workers.Where(w => w.WorkerAsLeader != null)
                                .FirstOrDefault(w => w.WorkerAsLeader.Subordinates.Select(s => s.Id)
                                                                                  .Contains(subordinateId));
            worker = workers.Where(w => w.WorkerAsLeader != null)
                            .FirstOrDefault(w => worker != null &&worker.WorkerAsSubordinate != null && w.WorkerAsLeader.Subordinates.Select(s => s.Id)
                                                                                                                    .Contains(worker.WorkerAsSubordinate.Id));
           
            return mapper.Map<IEnumerable<WorkerModel>>(workers.Where(w => worker != null && worker.WorkerAsLeader.Subordinates.Select(s => s.WorkerId).Contains(w.Id)));
        }

        public async Task<IEnumerable<WorkerModel>> GetWorkersByLeaderIdAsync(int leaderId)
        {
            var workers = await unitOfWork.WorkerRepository.GetAllWithDetailsAsync();
            return mapper.Map<IEnumerable<WorkerModel>>(workers.Where(w => w.WorkerAsSubordinate != null && w.WorkerAsSubordinate.LeaderId == leaderId));
        }

        public async Task<WorkerModel> GetWorkerBySubordinateIdAsync(int subordinateId)
        {
            var workers = await unitOfWork.WorkerRepository.GetAllWithDetailsAsync();
            return mapper.Map<WorkerModel>(workers.Where(w => w.WorkerAsLeader != null && w.WorkerAsLeader.Subordinates.Select(s => s.Id)
                                                                                           .Contains(subordinateId)));
        }

        public async Task UpdateAsync(WorkerModel model)
        {
            if (model == null)
            {
                throw new CatalogException("WorkerModel object cannot be null!");
            }
            if (model.Name == null || model.Name.Length == 0)
            {
                throw new CatalogException("Name cannot be empty!");
            }
            if (model.Surname == null || model.Surname.Length == 0)
            {
                throw new CatalogException("Surname cannot be empty!");
            }
            if (model.HireDate >= DateTime.Now || model.HireDate < DateTime.Parse("1991-1-1"))
            {
                throw new CatalogException("Incorrect HireDate value!");
            }
            if (model.Salary < 3500m)
            {
                throw new CatalogException("Salary cannot be less than 5000!");
            }
            unitOfWork.WorkerRepository.Update(mapper.Map<Worker>(model));
            await unitOfWork.SaveAsync();
        }
    }
}
