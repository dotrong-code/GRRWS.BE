﻿using AutoMapper;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Device.ErrorHistory;
using GRRWS.Infrastructure.DTOs.Device.History;
using GRRWS.Infrastructure.DTOs.Device.IssueHistory;
using GRRWS.Infrastructure.DTOs.Report;

namespace GRRWS.Infrastructure.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Report
            CreateMap<Report, ReportViewDTO>();
            CreateMap<ReportCreateDTO, Report>();
            CreateMap<ReportWarrantyCreateDTO, Report>();
            CreateMap<ReportUpdateDTO, Report>();
            CreateMap<Report, ReportViewDTO>()
           .ForMember(dest => dest.Errors, opt => opt.MapFrom(src =>
            src.ErrorDetails.Select(ed => new ErrorSummaryDTO
            {
                ErrorId = ed.Error.Id,
                Name = ed.Error.Name
            }).ToList()
            ))
            .ForMember(dest => dest.TechnicalSymptoms, opt => opt.MapFrom(src =>
            src.TechnicalSymptomReports.Select(ed => new SymtomsSummaryDTO
            {
                TechnicalSymtomId = ed.TechnicalSymptom.Id,
                Name = ed.TechnicalSymptom.Name
            }).ToList()
            ));

            //DeviceHistory
            CreateMap<DeviceHistory, DeviceHistoryDTO>();

            //DeviceIssueHistory
            CreateMap<DeviceIssueHistory, DeviceIssueHistoryDTO>();

            //DeviceErrorHistory
            CreateMap<DeviceErrorHistory, DeviceErrorHistoryDTO>();
        }
    }
}
