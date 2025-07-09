using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Common;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.DTOs.RequestMachineReplacement;
using GRRWS.Infrastructure.Interfaces;

namespace GRRWS.Application.Implement.Service
{
    public class RequestMachineReplacementService : IRequestMachineReplacementService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CheckIsExist _checkIsExist;
        public RequestMachineReplacementService(IUnitOfWork unitOfWork, CheckIsExist checkIsExist)
        {
            _unitOfWork = unitOfWork;
            _checkIsExist = checkIsExist;
        }

        public async Task<Result> ConfirmHadDevice(Guid requestMachineId, Guid userId)
        {
            var requestMachineCheck = await _checkIsExist.RequestMachine(requestMachineId);
            if (!requestMachineCheck.IsSuccess) return requestMachineCheck;
            var userCheck = await _checkIsExist.User(userId);
            if (!userCheck.IsSuccess) return userCheck;

            var requestMachine = await _unitOfWork.RequestMachineReplacementRepository.GetByIdAsync(requestMachineId);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            var mechanics = await _unitOfWork.UserRepository.GetMechanicsWithoutTask();
            var primaryMechanic = mechanics.FirstOrDefault().Id;
            requestMachine.Status = Domain.Enum.MachineReplacementStatus.InProgress;
            requestMachine.ModifiedBy = userId;
            requestMachine.AssigneeId = primaryMechanic;
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(requestMachine.TaskId ?? Guid.Empty);
            task.AssigneeId = primaryMechanic;
            task.Status = Status.Pending;
            await UpdateForInstalledDeviceInfor(requestMachine.OldDeviceId, (Guid)requestMachine.NewDeviceId);

            _unitOfWork.TaskRepository.Update(task);
            _unitOfWork.RequestMachineReplacementRepository.Update(requestMachine);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(new
            {

                Message = "Machine replacement request updated successfully!",
                RequestMachineId = requestMachine.Id,
                TaskId = requestMachine.TaskId,
            });

        }
        public async Task<Result> ConfirmTakenDevice(Guid requestMachineId, Guid userId)
        {
            var requestMachine = await _unitOfWork.RequestMachineReplacementRepository.GetByIdAsync(requestMachineId);
            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user.Role == 4)
            {
                requestMachine.StokkKeeperConfirm = true;
            }
            if (user.Role == 3)
            {
                requestMachine.AssigneeConfirm = true;
            }
            if (requestMachine.StokkKeeperConfirm && requestMachine.AssigneeConfirm)
            {
                requestMachine.Status = Domain.Enum.MachineReplacementStatus.Completed;
                requestMachine.CompletedDate = TimeHelper.GetHoChiMinhTime();


            }
            else
            {
                requestMachine.Status = Domain.Enum.MachineReplacementStatus.InProgress;
            }


            await _unitOfWork.RequestMachineReplacementRepository.UpdateAsync(requestMachine);
            await _unitOfWork.SaveChangesAsync();
            return Result.SuccessWithObject(new
            {
                RequestMachineId = requestMachine.Id,
                AssigneeConfirm = requestMachine.AssigneeConfirm,
                StokkKeeperConfirm = requestMachine.StokkKeeperConfirm,
                Status = requestMachine.Status.ToString(),
            });

        }
        public async Task<Result> CreateRequestMachineReplacementAsync(Guid requestId, Guid requestUserId)
        {
            try
            {
                var requestCheck = await _checkIsExist.Request(requestId);
                if (!requestCheck.IsSuccess) return requestCheck;
                var userCheck = await _checkIsExist.User(requestUserId);
                if (!userCheck.IsSuccess) return userCheck;

                var request = await _unitOfWork.RequestRepository.GetRequestByIdAsync(requestId);
                var device = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(request.DeviceId);
                var deviceByMachine = await _unitOfWork.DeviceRepository.GetDevicesByMachineIdAsync(device.MachineId ?? Guid.Empty);
                var replaceDeviceId = deviceByMachine.FirstOrDefault(d => d.Id != device.Id && d.Status == DeviceStatus.Active)?.Id;
                var deviceInfo = await _unitOfWork.TaskRepository.GetDeviceInfoAsync((Guid)replaceDeviceId);


                var requestMachineReplacement = new RequestMachineReplacement
                {
                    Id = Guid.NewGuid(),
                    CreatedDate = TimeHelper.GetHoChiMinhTime(),
                    ModifiedDate = TimeHelper.GetHoChiMinhTime(),
                    RequestedById = request.RequestedById,
                    OldDeviceId = device.Id,
                    MachineId = device.MachineId ?? null,
                    RequestCode = $"Yêu cầu lấy máy-{TimeHelper.GetHoChiMinhTime():yyyyMMddHHmmss}",
                    Status = MachineReplacementStatus.Pending,
                    NewDeviceId = replaceDeviceId,

                };
                if (requestMachineReplacement.MachineId == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Device does not have a machine model."));
                }
                await _unitOfWork.RequestMachineReplacementRepository.CreateAsync(requestMachineReplacement);
                await _unitOfWork.SaveChangesAsync();
                return Result.SuccessWithObject(new
                {
                    Message = "Machine replacement request created successfully!",
                    RequestMachineId = requestMachineReplacement.Id,

                });
            }
            catch (Exception e)
            {

                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("InternalServerError", $"An error occurred while creating the machine replacement request: {e.Message}"));
            }
        }
        public async Task<Result> GetAllAsync(
    int pageNumber,
    int pageSize,
    string? status = null,
    string? sortBy = null,
    bool isAscending = true)
        {
            var (items, totalCount) = await _unitOfWork.RequestMachineReplacementRepository.GetAllAsync(
                pageNumber,
                pageSize,
                status,
                sortBy,
                isAscending);
            var dtos = new List<Infrastructure.DTOs.RequestMachineReplacement.GetAll>();
            foreach (var r in items.Where(r => !r.IsDeleted).OrderByDescending(r => r.CreatedDate))
            {
                var dto = await MapRequestMachine(r);
                dtos.Add(dto);
            }
            var response = new PagedResponse<Infrastructure.DTOs.RequestMachineReplacement.GetAll>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }
        public async Task<Result> UpdateRequestMachineReplacement(UpdateRMR updateRMR)
        {
            try
            {
                var requestMachineCheck = await _checkIsExist.RequestMachine(updateRMR.RequestMachineId);
                if (!requestMachineCheck.IsSuccess) return requestMachineCheck;
                var requestMachine = await _unitOfWork.RequestMachineReplacementRepository.GetByIdAsync(updateRMR.RequestMachineId);
                var deviceCheck = await _checkIsExist.Device(updateRMR.DeviceId, true); // Allow null for deviceId
                if (!deviceCheck.IsSuccess) return deviceCheck;

                requestMachine.Reason = updateRMR.Reason;
                requestMachine.Notes = updateRMR.Notes;
                requestMachine.NewDeviceId = updateRMR.DeviceId;
                requestMachine.ModifiedDate = TimeHelper.GetHoChiMinhTime();
                await UpdateForInstalledDeviceInfor(requestMachine.OldDeviceId, (Guid)requestMachine.NewDeviceId);

                _unitOfWork.RequestMachineReplacementRepository.Update(requestMachine);
                _unitOfWork.SaveChangesAsync().Wait();
                return Result.SuccessWithObject(new
                {
                    Message = "Request machine replacement updated successfully!",
                    RequestMachineId = requestMachine.Id,
                }
                );
            }
            catch (Exception e)
            {
                return Result.Failure(Infrastructure.DTOs.Common.Error.Failure("InternalServerError", $"An error occurred while updating the request machine replacement: {e.Message}"));
            }
        }
        private async Task<Infrastructure.DTOs.RequestMachineReplacement.GetAll> MapRequestMachine(RequestMachineReplacement data)
        {
            return new Infrastructure.DTOs.RequestMachineReplacement.GetAll
            {
                Title = data.RequestCode,
                Description = data.Notes,
                Id = data.Id,
                RequestDate = data.RequestDate,
                AssigneeId = data.AssigneeId,
                AssigneeName = data.Assignee?.FullName,
                OldDeviceId = data.OldDeviceId,
                NewDeviceId = data.NewDeviceId,
                MachineId = data.MachineId,
                Status = data.Status.ToString()
            };
        }
        private async Task UpdateForInstalledDeviceInfor(Guid oldeDeviceId, Guid newDeviceId)
        {
            var newDevice = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(newDeviceId);
            var oldDevice = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(oldeDeviceId);
            newDevice.InstallationDate = TimeHelper.GetHoChiMinhTime();
            newDevice.Status = DeviceStatus.InUse;
            newDevice.PositionId = oldDevice.PositionId; // Giữ nguyên vị trí của thiết bị cũ
            _unitOfWork.DeviceRepository.Update(newDevice);
        }
    }
}
