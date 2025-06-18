using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.ErrorDetail;
using GRRWS.Infrastructure.DTOs.ErrorGuideline;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class ErrorGuidelineService : IErrorGuidelineService
    {
        private readonly UnitOfWork _unitOfWork;

        public ErrorGuidelineService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateErrorGuidelineAsync(CreateErrorGuidelineRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Title))
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "Title is required"));

            if (request.ErrorId == Guid.Empty)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "ErrorId is required"));

            if (request.ErrorFixSteps == null || !request.ErrorFixSteps.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "At least one ErrorFixStep is required"));

            var errorExists = await _unitOfWork.ErrorRepository.GetByIdAsync(request.ErrorId) != null;
            if (!errorExists)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Error not found"));

            var errorGuideline = new ErrorGuideline
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                ErrorId = request.ErrorId
            };

            // Tạo mới ErrorFixSteps
            var errorFixSteps = new List<ErrorFixStep>();
            foreach (var stepRequest in request.ErrorFixSteps)
            {
                var errorFixStep = new ErrorFixStep
                {
                    Id = Guid.NewGuid(),
                    ErrorGuidelineId = errorGuideline.Id,
                    StepDescription = stepRequest.StepDescription,
                    StepOrder = stepRequest.StepOrder
                };
                errorFixSteps.Add(errorFixStep);
            }
            errorGuideline.ErrorFixSteps = errorFixSteps;

            // Thêm ErrorSpareparts (tùy chọn)
            if (request.ErrorSparepartIds != null && request.ErrorSparepartIds.Any())
            {
                var errorSpareparts = new List<ErrorSparepart>();
                foreach (var sparepartId in request.ErrorSparepartIds)
                {
                    var sparepart = await _unitOfWork.SparepartRepository.GetByIdAsync(sparepartId); // Giả định có repository này
                    if (sparepart == null)
                        return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Sparepart with ID {sparepartId} not found"));
                    var errorSparepart = new ErrorSparepart
                    {
                        ErrorGuidelineId = errorGuideline.Id,
                        SparepartId = sparepartId,
                        QuantityNeeded = 1 // Giá trị mặc định, có thể điều chỉnh
                    };
                    errorSpareparts.Add(errorSparepart);
                }
                errorGuideline.ErrorSpareparts = errorSpareparts;
            }

            await _unitOfWork.ErrorGuidelineRepository.CreateAsync(errorGuideline);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "ErrorGuideline created successfully!", ErrorGuidelineId = errorGuideline.Id });
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var errorGuideline = await _unitOfWork.ErrorGuidelineRepository.GetByIdInclueErrorFixStepErrorSparepartssAsync(id);
            if (errorGuideline == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorGuideline not found"));

            var dto = new ErrorGuidelineDto
            {
                Id = errorGuideline.Id,
                Title = errorGuideline.Title,
                EstimatedRepairTime = errorGuideline.EstimatedRepairTime,
                Priority = errorGuideline.Priority,
                ErrorFixSteps = errorGuideline.ErrorFixSteps?.Select(s => new ErrorFixStepDto
                {
                    Id = s.Id,
                    StepDescription = s.StepDescription,
                    StepOrder = s.StepOrder
                }).ToList(),
                ErrorSpareparts = errorGuideline.ErrorSpareparts?.Select(s => new ErrorSparepartDto
                {
                    SparepartId = s.SparepartId,
                    QuantityNeeded = s.QuantityNeeded
                }).ToList()
            };

            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> GetAllByErrorIdAsync(Guid errorId)
        {
            var errorExists = await _unitOfWork.ErrorRepository.GetByIdAsync(errorId) != null;
            if (!errorExists)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Error not found"));

            var errorGuidelines = await _unitOfWork.ErrorGuidelineRepository.GetAllByErrorIdAsync(errorId);
            var dtos = errorGuidelines.Select(eg => new ErrorGuidelineDto
            {
                Id = eg.Id,
                ErrorId = eg.ErrorId,
                Title = eg.Title,
                EstimatedRepairTime = eg.EstimatedRepairTime,
                Priority = eg.Priority,
                ErrorFixSteps = eg.ErrorFixSteps?.Select(s => new ErrorFixStepDto
                {
                    Id = s.Id,
                    StepDescription = s.StepDescription,
                    StepOrder = s.StepOrder
                }).ToList(),
                ErrorSpareparts = eg.ErrorSpareparts?.Select(s => new ErrorSparepartDto
                {
                    SparepartId = s.SparepartId,
                    QuantityNeeded = s.QuantityNeeded
                }).ToList()
            }).ToList();
            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> UpdateErrorGuidelineAsync(Guid id, UpdateErrorGuidelineRequests request)
        {
            var errorGuideline = await _unitOfWork.ErrorGuidelineRepository.GetByIdAsync(id);
            if (errorGuideline == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorGuideline not found"));

            if (string.IsNullOrWhiteSpace(request.Title))
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "Title is required"));

            errorGuideline.Title = request.Title;
            errorGuideline.ErrorId = request.ErrorId ?? errorGuideline.ErrorId;

            // Cập nhật ErrorFixSteps
            if (request.ErrorFixStepIds != null && request.ErrorFixStepIds.Any())
            {
                var errorFixSteps = new List<ErrorFixStep>();
                foreach (var stepId in request.ErrorFixStepIds)
                {
                    var step = await _unitOfWork.ErrorFixStepRepository.GetByIdAsync(stepId);
                    if (step == null)
                        return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"ErrorFixStep with ID {stepId} not found"));
                    errorFixSteps.Add(step);
                }
                errorGuideline.ErrorFixSteps = errorFixSteps;
            }
            else
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound ("Bad Request", "At least one ErrorFixStep is required"));
            }

            // Cập nhật ErrorSpareparts (tùy chọn)
            if (request.ErrorSparepartIds != null && request.ErrorSparepartIds.Any())
            {
                var errorSpareparts = new List<ErrorSparepart>();
                foreach (var sparepartId in request.ErrorSparepartIds)
                {
                    var sparepart = await _unitOfWork.ErrorSparepartRepository.GetByIdAsync(sparepartId);
                    if (sparepart == null)
                        return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"ErrorSparepart with ID {sparepartId} not found"));
                    errorSpareparts.Add(sparepart);
                }
                errorGuideline.ErrorSpareparts = errorSpareparts;
            }
            else
            {
                errorGuideline.ErrorSpareparts = null; // Xóa nếu không có spareparts
            }

            await _unitOfWork.ErrorGuidelineRepository.UpdateAsync(errorGuideline);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "ErrorGuideline updated successfully!" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var errorGuideline = await _unitOfWork.ErrorGuidelineRepository.GetByIdAsync(id);
            if (errorGuideline == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorGuideline not found"));

            await _unitOfWork.ErrorGuidelineRepository.RemoveAsync(errorGuideline);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "ErrorGuideline deleted successfully!" });
        }
    }
}