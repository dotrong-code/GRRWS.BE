using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.ErrorFixProgress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class ErrorFixProgressService : IErrorFixProgressService
    {
        private readonly UnitOfWork _unitOfWork;

        public ErrorFixProgressService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var errorFixProgress = await _unitOfWork.ErrorFixProgressRepository.GetByIdAsync(id);
            if (errorFixProgress == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Error fix progress not found"));

            return Result.SuccessWithObject(errorFixProgress);
        }

        public async Task<Result> GetAllByErrorDetailIdAsync(Guid errorDetailId)
        {
            var errorFixProgressList = await _unitOfWork.ErrorFixProgressRepository.GetAllByErrorDetailIdAsync(errorDetailId);
            if (errorFixProgressList == null || !errorFixProgressList.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No error fix progress found for the given error detail ID"));

            return Result.SuccessWithObject(errorFixProgressList);
        }

        public async Task<Result> UpdateIsCompletedAsync(UpdateIsCompletedRequest request)
        {
            if (request.ErrorFixProgressIds == null || !request.ErrorFixProgressIds.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "At least one ErrorFixProgress ID is required"));

            var progressToUpdate = new List<ErrorFixProgress>();
            foreach (var progressId in request.ErrorFixProgressIds)
            {
                var progress = await _unitOfWork.ErrorFixProgressRepository.GetByIdAsync(progressId);
                if (progress == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Error fix progress with ID {progressId} not found"));
                progress.IsCompleted = request.IsCompleted;
                if (request.IsCompleted)
                    progress.CompletedAt = TimeHelper.GetHoChiMinhTime();
                progressToUpdate.Add(progress);
            }

            foreach (var progress in progressToUpdate)
            {
                await _unitOfWork.ErrorFixProgressRepository.UpdateAsync(progress);
            }
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Error fix progress updated successfully!" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var errorFixProgress = await _unitOfWork.ErrorFixProgressRepository.GetByIdAsync(id);
            if (errorFixProgress == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Error fix progress not found"));

            await _unitOfWork.ErrorFixProgressRepository.RemoveAsync(errorFixProgress);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Error fix progress deleted successfully!" });
        }
    }
}