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

        public async Task<Result> Request(Guid requestId) // Fixed 'Common.Result' to 'Result'
        {
            var exists = await _unitOfWork.RequestRepository.IsExistAsync(requestId);
            return exists
                ? Result.Success()
                : Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Request does not exist."));
        }

        public async Task<Result> User(Guid userId)
        {
            var exists = await _unitOfWork.UserRepository.IdExistsAsync(userId);
            return exists
                ? Result.Success()
                : Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "User does not exist."));
        }

        public async Task<Result> TaskGroup(Guid taskGroupId)
        {
            var exists = await _unitOfWork.TaskGroupRepository.GetByIdAsync(taskGroupId) != null;
            return exists
                ? Result.Success()
                : Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Task group does not exist."));
        }

        // Add more methods here if needed (e.g., Device, SparePart, etc.)
    }
}
