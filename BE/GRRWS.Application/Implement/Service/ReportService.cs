using AutoMapper;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
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
            var report = _mapper.Map<Report>(dto);
            report.Id = Guid.NewGuid();
            report.Status = "InProgress";
            if (dto.ErrorIds != null && dto.ErrorIds.Any())
            {
                report.ErrorDetails = dto.ErrorIds.Select(errorId => new ErrorDetail
                {
                    ReportId = report.Id,
                    ErrorId = errorId
                }).ToList();
            }
            await _unit.ReportRepository.CreateAsync(report);
            var request = await _unit.RequestRepository.GetRequestByIdAsync((Guid)report.RequestId);
            request.ReportId = report.Id;
            request.Status = "Approved";
            await _unit.RequestRepository.UpdateAsync(request);
            return Result.SuccessWithObject(report.Id);
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
            return Result.SuccessWithObject(new { Message = "Successfully!" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var report = await _unit.ReportRepository.GetByIdAsync(id);
            if (report == null) return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Report not found.", 0));
            report.IsDeleted = true;
            report.ModifiedDate = DateTime.Now;
            await _unit.ReportRepository.UpdateAsync(report);
            return Result.Success();
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
