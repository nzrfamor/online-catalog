using AutoMapper;
using BLL.Models;
using DAL.Entities;
using System;
using System.Linq;

namespace BLL
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Worker, WorkerModel>()
                .ForMember(wm => wm.Id, w => w.MapFrom(x => x.PersonId))
                .ForMember(wm => wm.Id, w => w.MapFrom(x => x.Person.Id))
                .ForMember(wm => wm.Name, w => w.MapFrom(x => x.Person.Name))
                .ForMember(wm => wm.Surname, w => w.MapFrom(x => x.Person.Surname))
                .ForMember(wm => wm.LeaderId, w => w.MapFrom(x => x.WorkerAsSubordinate.LeaderId))
                .ForMember(wm => wm.WorkerAsLeaderId, w => w.MapFrom(x => x.WorkerAsLeader.Id))
                .ForMember(wm => wm.WorkerAsSubordinateId, w => w.MapFrom(x => x.WorkerAsSubordinate.Id))
                .ForMember(wm => wm.SubordinatesIds, w => w.MapFrom(x => x.WorkerAsLeader.Subordinates.Select(s => s.Id)))
                .ReverseMap();

            CreateMap<Leader, LeaderModel>()
                .ForMember(lm => lm.SubordinatesIds, l => l.MapFrom(x => x.Subordinates.Select(s => s.Id)))
                .ReverseMap();

            CreateMap<Subordinate, SubordinateModel>()
                .ReverseMap();
;
        }
    }
}
