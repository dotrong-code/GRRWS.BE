using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.ErrorDetail;

namespace GRRWS.Application.Implement.Service
{
    public class ErrorDetailService : IErrorDetailService
    {
        private readonly UnitOfWork _unitOfWork;

        public ErrorDetailService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> CreateErrorDetailAsync(CreateErrorDetailRequest request)
        {
            // Validate Request exists
            var requestExists = await _unitOfWork.RequestRepository.GetByIdAsync(request.RequestId) != null;
            if (!requestExists)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request not found"));

            // Validate Error exists
            // var errorExists = await _unitOfWork.ErrorRepository.GetByIdAsync(request.ErrorId) != null;
            // if (!errorExists)
            //     return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Error not found"));

            // Get ReportId from RequestId
            var reportId = await _unitOfWork.ErrorDetailRepository.GetReportIdByRequestIdAsync(request.RequestId);
            if (reportId == Guid.Empty)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Report not found for this request"));

            // Check if ErrorDetail already exists for this Error and Report
            // var exists = await _unitOfWork.ErrorDetailRepository.ErrorDetailExistsAsync(request.ErrorId, reportId);
            // if (exists)
            //     return Result.Failure(Infrastructure.DTOs.Common.Error.BadRequest("Bad Request", "ErrorDetail already exists for this Error and Report"));

            var errorDetail = new ErrorDetail
            {
                // Id = Guid.NewGuid(),
                ErrorId = request.ErrorId,
                ReportId = reportId,
                // DetectedDate = DateTime.UtcNow,
                // IsResolved = false,
                // CreatedDate = DateTime.UtcNow,
                // IsDeleted = false
            };

            await _unitOfWork.ErrorDetailRepository.CreateAsync(errorDetail);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "ErrorDetail created successfully!"/*, ErrorDetailId = errorDetail.Id*/ });
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var errorDetail = await _unitOfWork.ErrorDetailRepository.GetByIdAsync(id);
            if (errorDetail == null /*|| errorDetail.IsDeleted*/)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorDetail not found"));

            return Result.SuccessWithObject(errorDetail);
        }

        public async Task<Result> GetByRequestIdAsync(Guid requestId)
        {
            var requestExists = await _unitOfWork.RequestRepository.GetByIdAsync(requestId) != null;
            if (!requestExists)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request not found"));

            var errorDetails = await _unitOfWork.ErrorDetailRepository.GetByRequestIdAsync(requestId);
            return Result.SuccessWithObject(errorDetails);
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var errorDetail = await _unitOfWork.ErrorDetailRepository.GetByIdAsync(id);
            if (errorDetail == null /*||errorDetail.IsDeleted*/)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "ErrorDetail not found"));

            // errorDetail.IsDeleted = true;
            // errorDetail.ModifiedDate = DateTime.UtcNow;

            await _unitOfWork.ErrorDetailRepository.UpdateAsync(errorDetail);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "ErrorDetail deleted successfully!" });
        }
    }
}