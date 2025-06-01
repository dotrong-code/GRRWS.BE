using AutoMapper;
using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Report;
using GRRWS.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;

        public ReportService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }

        public async Task<Result> CreateAsync(ReportCreateDTO dto)
        {
            if (dto.RequestId == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "RequestId is required.", 0));

            if (dto.ErrorIds != null && dto.ErrorIds.Any())
                if (dto.ErrorIds.Any(errorId => errorId == Guid.Empty))
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "ErrorIds cannot contain empty GUIDs.", 0));

            if (dto.Priority.GetType() != typeof(int))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be an integer.", 0));

            if (dto.Priority < 0 || dto.Priority > 5)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be between 0 and 5.", 0));

            var request = await _unit.RequestRepository.GetRequestByIdAsync((Guid)dto.RequestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Request not found for the provided RequestId."
                ));
            }
            if (request.ReportId != null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict(
                    "Conflict", "Request already has an associated report."
                ));
            }

            var missingErrors = await _unit.ErrorRepository.GetNotFoundErrorDisplayNamesAsync(dto.ErrorIds);
            if (missingErrors.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Some errors do not exist: " + string.Join(", ", missingErrors.Select(x => x.Id))
                ));
            }

            var createLocation = "";
            try
            {
                createLocation = TitleHelper.GenerateReportTitle(request.Device.Position.Zone.Area.AreaCode, request.Device.Position.Zone.ZoneCode, request.Device.Position.Index, request.Device.DeviceCode);
            }
            catch (Exception)
            {
                createLocation = "Create title fail";
            }

            var report = _mapper.Map<Report>(dto);
            report.Id = Guid.NewGuid();
            report.Status = "InProgress";
            report.CreatedDate = DateTime.Now;
            report.Location = createLocation;

            if (dto.ErrorIds != null && dto.ErrorIds.Any())
            {
                report.ErrorDetails = dto.ErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId
                }).ToList();
            }
            else
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "ErrorIds is null!.", 0));
            }
            await _unit.ReportRepository.CreateAsync(report);
            var getRequest = await _unit.RequestRepository.GetRequestByIdAsync((Guid)report.RequestId);
            getRequest.ReportId = report.Id;
            getRequest.Status = "Approved";
            await _unit.RequestRepository.UpdateAsync(getRequest);
            return Result.SuccessWithObject(new { Message = "Report created successfully!", ReportId = report.Id });
        }
        public async Task<Result> CreateWarrantyReportAsync(ReportWarrantyCreateDTO dto)
        {
            if (dto.RequestId == null)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "RequestId is required.", 0));

            if (dto.TechnicalSymtomIds != null && dto.TechnicalSymtomIds.Any())
                if (dto.TechnicalSymtomIds.Any(technicalSymtomId => technicalSymtomId == Guid.Empty))
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "TechnicalSymtomIds cannot contain empty GUIDs.", 0));

            if (dto.Priority.GetType() != typeof(int))
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be an integer.", 0));

            if (dto.Priority < 0 || dto.Priority > 5)
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Priority must be between 0 and 5.", 0));

            var request = await _unit.RequestRepository.GetRequestByIdAsync((Guid)dto.RequestId);
            if (request == null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Request not found for the provided RequestId."
                ));
            }
            if (request.ReportId != null)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Conflict(
                    "Conflict", "Request already has an associated report."
                ));
            }

            var missingSymtoms = await _unit.TechnicalSymtomRepository.GetNotFoundTechnicalSymtomDisplayNamesAsync(dto.TechnicalSymtomIds);
            if (missingSymtoms.Any())
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound(
                    "NotFound", "Some symtoms do not exist: " + string.Join(", ", missingSymtoms.Select(x => x.Id))
                ));
            }

            var createLocation = "";
            try
            {
                createLocation = TitleHelper.GenerateReportTitle(request.Device.Position.Zone.Area.AreaCode, request.Device.Position.Zone.ZoneCode, request.Device.Position.Index, request.Device.DeviceCode);
            }
            catch (Exception)
            {
                createLocation = "Create title fail";
            }

            var report = _mapper.Map<Report>(dto);
            report.Id = Guid.NewGuid();
            report.Status = "InProgress";
            report.CreatedDate = DateTime.Now;
            report.Location = createLocation;

            if (dto.TechnicalSymtomIds != null && dto.TechnicalSymtomIds.Any())
            {
                report.TechnicalSymptomReports = dto.TechnicalSymtomIds.Select(symtomId => new TechnicalSymptomReport
                {
                    ReportId = report.Id,
                    TechnicalSymptomId = symtomId
                }).ToList();
            }
            else
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "TechnicalSymptomIds is null!.", 0));
            }
            await _unit.ReportRepository.CreateAsync(report);
            var getRequest = await _unit.RequestRepository.GetRequestByIdAsync((Guid)report.RequestId);
            getRequest.ReportId = report.Id;
            getRequest.Status = "Approved";
            await _unit.RequestRepository.UpdateAsync(getRequest);
            return Result.SuccessWithObject(new { Message = "Report created successfully!", ReportId = report.Id });
        }
        public async Task<Result> UpdateAsync(ReportUpdateDTO dto)
        {
            var report = await _unit.ReportRepository.GetByIdAsync(dto.Id);
            if (report == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));
            _mapper.Map(dto, report);
            if (dto.ErrorIds != null)
            {
                report.ErrorDetails = dto.ErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId
                }).ToList();
            }
            await _unit.ReportRepository.UpdateReportAsync(report, dto.ErrorIds ?? new List<Guid>());
            return Result.SuccessWithObject(new { Message = "Report updated successfully!" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var report = await _unit.ReportRepository.GetByIdAsync(id);
            if (report == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));
            report.IsDeleted = true;
            report.ModifiedDate = DateTime.Now;
            await _unit.ReportRepository.UpdateAsync(report);
            return Result.SuccessWithObject(new { Message = "Report canceled successfully!" });
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var report = await _unit.ReportRepository.GetReportWithRequestAsync(id);
            if (report == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));

            var dto = _mapper.Map<ReportViewDTO>(report);
            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> GetAllAsync()
        {
            var reports = await _unit.ReportRepository.GetReportsWithRequestAsync();
            var dtos = _mapper.Map<List<ReportViewDTO>>(reports).Cast<object>().ToList();
            return Result.SuccessWithObject(dtos);
        }
    }

}
