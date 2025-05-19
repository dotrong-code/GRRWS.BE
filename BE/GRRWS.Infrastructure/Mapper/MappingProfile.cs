using AutoMapper;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Report, ReportViewDTO>();
            CreateMap<ReportCreateDTO, Report>();
            CreateMap<ReportUpdateDTO, Report>();
            CreateMap<Report, ReportViewDTO>()
    .ForMember(dest => dest.Errors, opt => opt.MapFrom(src =>
        src.ErrorDetails.Select(ed => new ErrorSummaryDTO
        {
            ErrorId = ed.Error.Id,
            Name = ed.Error.Name
        }).ToList()
    ));

        }
    }
}
