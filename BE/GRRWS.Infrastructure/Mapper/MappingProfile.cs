using AutoMapper;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Device.ErrorHistory;
using GRRWS.Infrastructure.DTOs.Device.History;
using GRRWS.Infrastructure.DTOs.Device.IssueHistory;
using GRRWS.Infrastructure.DTOs.MechanicShift;
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

            //MechanicShift
            CreateMap<MechanicShift, MechanicShiftDTO>();
            CreateMap<MechanicShift, MechanicShiftResponseDTO>()
                            .ForMember(dest => dest.MechanicShiftId, opt => opt.MapFrom(src => src.Id))
                            .ForMember(dest => dest.MechanicId, opt => opt.MapFrom(src => src.MechanicId ?? Guid.Empty))
                            .ForMember(dest => dest.MechanicName, opt => opt.MapFrom(src => src.Mechanic != null ? src.Mechanic.FullName : "Unknown"))
                            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.TaskId ?? Guid.Empty))
                            .ForMember(dest => dest.ShiftName, opt => opt.MapFrom(src => src.Shift != null ? src.Shift.ShiftName : "Unnamed Shift"))
                            .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.StartTime))
                            .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.EndTime))
                            .ForMember(dest => dest.IsAvailable, opt => opt.MapFrom(src => src.IsAvailable));
        }
    }
}
