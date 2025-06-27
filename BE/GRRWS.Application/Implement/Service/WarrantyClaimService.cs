using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.WarrantyClaim;
using Microsoft.Extensions.Logging;

namespace GRRWS.Application.Implement.Service
{
    public class WarrantyClaimService : IWarrantyClaimService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly ILogger<WarrantyClaimService> _logger;

        public WarrantyClaimService(UnitOfWork unitOfWork, ILogger<WarrantyClaimService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task<Result> GetWarrantyClaimWithTasksAsync(Guid warrantyClaimId)
        {
            try
            {
                var warrantyClaimWithTasks = await _unitOfWork.WarrantyClaimRepository.GetWarrantyClaimWithTasksAsync(warrantyClaimId);
                
                if (warrantyClaimWithTasks == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Warranty claim not found."));
                }
                
                return Result.SuccessWithObject(warrantyClaimWithTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving warranty claim with tasks for ID: {WarrantyClaimId}", warrantyClaimId);
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", ex.Message));
            }
        }

        public async Task<Result> GetWarrantyClaimsWithTasksAsync(int pageNumber, int pageSize)
        {
            if (pageNumber <= 0)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Page number must be greater than 0"));
            }

            if (pageSize <= 0 || pageSize > 100)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Page size must be between 1 and 100"));
            }
            
            try
            {
                var warrantyClaimsWithTasks = await _unitOfWork.WarrantyClaimRepository.GetWarrantyClaimsWithTasksAsync(pageNumber, pageSize);
                var totalCount = await _unitOfWork.WarrantyClaimRepository.GetTotalCountAsync();
                
                var response = new PagedResponse<WarrantyClaimWithTasksDTO>
                {
                    Data = warrantyClaimsWithTasks,
                    TotalCount = totalCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
                
                return Result.SuccessWithObject(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving warranty claims with tasks");
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", ex.Message));
            }
        }

        public async Task<Result> GetWarrantyClaimByTaskIdAsync(Guid taskId, string taskType)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(taskType))
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Task type must be specified (Submission or Return)"));
                }

                bool isSubmissionTask;
                if (taskType.Equals("Submission", StringComparison.OrdinalIgnoreCase))
                {
                    isSubmissionTask = true;
                }
                else if (taskType.Equals("Return", StringComparison.OrdinalIgnoreCase))
                {
                    isSubmissionTask = false;
                }
                else
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Task type must be either 'Submission' or 'Return'"));
                }

                // Verify the task exists first
                var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
                if (task == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Task not found."));
                }

                // Get warranty claim with tasks
                var warrantyClaimWithTasks = await _unitOfWork.WarrantyClaimRepository.GetWarrantyClaimByTaskIdAsync(taskId, isSubmissionTask);
                
                if (warrantyClaimWithTasks == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", 
                        $"No warranty claim found with the specified {taskType.ToLower()} task ID."));
                }
                
                // Process documents to fetch their actual URLs
                if (warrantyClaimWithTasks.Documents != null && warrantyClaimWithTasks.Documents.Any())
                {
                    foreach (var document in warrantyClaimWithTasks.Documents)
                    {
                        if (!string.IsNullOrEmpty(document.DocumentUrl))
                        {
                            var getDocumentRequest = new GetImageRequest(document.DocumentUrl);
                            var documentResult = await _unitOfWork.FirebaseRepository.GetImageAsync(getDocumentRequest);
                            if (documentResult.Success && !string.IsNullOrEmpty(documentResult.ImageUrl))
                            {
                                document.DocumentUrl = documentResult.ImageUrl;
                            }
                        }
                    }
                }
                
                _logger.LogInformation("Retrieved warranty claim with {DocumentCount} documents for task ID: {TaskId}, type: {TaskType}", 
                    warrantyClaimWithTasks.Documents?.Count ?? 0, taskId, taskType);
                
                return Result.SuccessWithObject(warrantyClaimWithTasks);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving warranty claim by task ID: {TaskId}, type: {TaskType}", taskId, taskType);
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("Error", ex.Message));
            }
        }
    }
}