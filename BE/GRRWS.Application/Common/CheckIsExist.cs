using GRRWS.Infrastructure.Interfaces;

namespace GRRWS.Application.Common
{
    public class CheckIsExist
    {
        private readonly IUnitOfWork _unitOfWork;

        public CheckIsExist(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Common.Result.Result> Request(Guid? requestId, bool allowNull = false)
        {
            if (!requestId.HasValue)
                return allowNull
                    ? Common.Result.Result.Success()
                    : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Invalid", "Request ID is required."));

            var exists = await _unitOfWork.RequestRepository.IsExistAsync(requestId.Value);
            return exists
                ? Common.Result.Result.Success()
                : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request does not exist."));
        }

        public async Task<Common.Result.Result> User(Guid? userId, bool allowNull = false)
        {
            if (!userId.HasValue)
                return allowNull
                    ? Common.Result.Result.Success()
                    : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Invalid", "User ID is required."));

            var exists = await _unitOfWork.UserRepository.IsExistAsync(userId.Value);
            return exists
                ? Common.Result.Result.Success()
                : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "User does not exist."));
        }

        public async Task<Common.Result.Result> TaskGroup(Guid? taskGroupId, bool allowNull = false)
        {
            if (!taskGroupId.HasValue)
                return allowNull
                    ? Common.Result.Result.Success()
                    : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Invalid", "Task group ID is required."));

            var exists = await _unitOfWork.TaskGroupRepository.IsExistAsync(taskGroupId.Value);
            return exists
                ? Common.Result.Result.Success()
                : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Task group does not exist."));
        }
        public async Task<Common.Result.Result> Device(Guid? deviceId, bool allowNull = false)
        {
            if (!deviceId.HasValue)
                return allowNull
                    ? Common.Result.Result.Success()
                    : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Invalid", "Device ID is required."));

            var exists = await _unitOfWork.DeviceRepository.IsExistAsync(deviceId.Value);
            return exists
                ? Common.Result.Result.Success()
                : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Device does not exist."));
        }

        public async Task<Common.Result.Result> Task(Guid? taskId, bool allowNull = false)
        {
            if (!taskId.HasValue)
                return allowNull
                    ? Common.Result.Result.Success()
                    : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Invalid", "Task ID is required."));

            var exists = await _unitOfWork.TaskRepository.IsExistAsync(taskId);
            return exists
                ? Common.Result.Result.Success()
                : Common.Result.Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Task does not exist."));
        }

        // Add more methods here if needed (e.g., Device, SparePart, etc.)
    }
}
