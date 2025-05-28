using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.WarrantyDetail;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using GRRWS.Infrastructure.Interfaces;

namespace GRRWS.Application.Implement.Service
{
    public class WarrantyDetailService : IWarrantyDetailService
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public WarrantyDetailService( IUnitOfWork unitOfWork)
        {
            
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateAsync(CreateWarrantyDetailDTO dto)
        {
            // Kiểm tra Request tồn tại
            var request = await _unitOfWork.RequestRepository.GetRequestByIdAsync(dto.RequestId);
            if (request == null)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "Request not found.", 0));
            }

            // Kiểm tra Issues tồn tại
            var issues = new List<Issue>();
            foreach (var issueId in dto.IssueIds)
            {
                var issue = await _unitOfWork.IssueRepository.GetByIdAsync(issueId);
                if (issue == null)
                {
                    return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", $"Issue {issueId} not found.", 0));
                }
                issues.Add(issue);
            }

            // Tạo Report
            var report = new Report
            {
                Id = Guid.NewGuid(),
                RequestId = dto.RequestId,
                Priority = 2, // Medium
                Location = dto.Location,
                Status = "Pending",
                CreatedDate = DateTime.UtcNow
            };

            // Tạo WarrantyDetail
            var warrantyDetail = new WarrantyDetail
            {
                Id = Guid.NewGuid(),
                WarrantyNotes = dto.WarrantyNotes,
                ReportId = report.Id
            };
            // Tạo Report và WarrantyDetail trong DB
            await _unitOfWork.ReportRepository.CreateAsync(report);
            await _unitOfWork.WarrantyDetailRepository.CreateAsync(warrantyDetail);
            await _unitOfWork.SaveChangesAsync();
            // Liên kết Issues với WarrantyDetail
            foreach (var issue in issues)
            {
                issue.WarrantyDetailId = warrantyDetail.Id;
                await _unitOfWork.IssueRepository.UpdateAsync(issue);
            }


            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(new { Message = "WarrantyDetail and Report created successfully!" });
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var warrantyDetail = await _unitOfWork.WarrantyDetailRepository.GetByIdAsync(id);
            if (warrantyDetail == null)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "WarrantyDetail not found.", 0));
            }

            var dto = new WarrantyDetailDTO
            {
                Id = warrantyDetail.Id,
                ReportId = warrantyDetail.ReportId,
                TaskId = warrantyDetail.TaskId,
                WarrantyNotes = warrantyDetail.WarrantyNotes,
                IssueIds = warrantyDetail.Issues?.Select(i => i.Id).ToList() ?? new List<Guid>()
            };

            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> GetAllAsync()
        {
            var warrantyDetails = await _unitOfWork.WarrantyDetailRepository.GetAllAsync();
            var dtos = warrantyDetails.Select(wd => new WarrantyDetailDTO
            {
                Id = wd.Id,
                ReportId = wd.ReportId,
                TaskId = wd.TaskId,
                WarrantyNotes = wd.WarrantyNotes,
                IssueIds = wd.Issues?.Select(i => i.Id).ToList() ?? new List<Guid>()
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> UpdateAsync(Guid id, UpdateWarrantyDetailDTO dto)
        {
            var warrantyDetail = await _unitOfWork.WarrantyDetailRepository.GetByIdAsync(id);
            if (warrantyDetail == null)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "WarrantyDetail not found.", 0));
            }

            warrantyDetail.WarrantyNotes = dto.WarrantyNotes ?? warrantyDetail.WarrantyNotes;
            warrantyDetail.TaskId = dto.TaskId ?? warrantyDetail.TaskId;

            await _unitOfWork.WarrantyDetailRepository.UpdateAsync(warrantyDetail);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "WarrantyDetail updated successfully!" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var warrantyDetail = await _unitOfWork.WarrantyDetailRepository.GetByIdAsync(id);
            if (warrantyDetail == null)
            {
                return Result.Failure(new Infrastructure.DTOs.Common.Error("Error", "WarrantyDetail not found.", 0));
            }

            // Xóa liên kết trong Issues
            if (warrantyDetail.Issues != null)
            {
                foreach (var issue in warrantyDetail.Issues)
                {
                    issue.WarrantyDetailId = null;
                    await _unitOfWork.IssueRepository.UpdateAsync(issue);
                }
            }

            await _unitOfWork.WarrantyDetailRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "WarrantyDetail deleted successfully!" });
        }
    }
}
