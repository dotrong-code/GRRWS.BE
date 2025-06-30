using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Common;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.Interfaces;

namespace GRRWS.Application.Implement.Service
{
    public class RequestMachineReplacementService : IRequestMachineReplacementService
    {
        private readonly IUnitOfWork _unitOfWork;
        public RequestMachineReplacementService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                requestMachine.Status = Domain.Enum.MachineReplacementStatus.Pending;
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


        private async Task<Infrastructure.DTOs.RequestMachineReplacement.GetAll> MapRequestMachine(RequestMachineReplacement data)
        {
            return new Infrastructure.DTOs.RequestMachineReplacement.GetAll
            {
                Title = $"Yêu cầu máy thuộc {data.Machine.MachineName}",
                Description = $"Hệ thống gợi ý thiết bị {data.NewDevice.DeviceName}",
                Id = data.Id,
                RequestDate = data.RequestDate,
                AssigneeId = data.AssigneeId,
                AssigneeName = data.Assignee.FullName,
                OldDeviceId = data.OldDeviceId,
                NewDeviceId = data.NewDeviceId,
                MachineId = data.MachineId,
                Status = data.Status.ToString()
            };
        }

    }
}
