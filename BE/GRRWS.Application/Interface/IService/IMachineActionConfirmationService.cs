using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.MachineActionConfirmation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IMachineActionConfirmationService
    {

        // Stock out
        Task<Result> StockkeeperConfirmHadDevice(Guid confirmationId, Guid userId, Guid deviceId);
        Task<Result> StockkeeperConfirmTakenDevice(ConfirmDoneRequest confirmationRequest, Guid userId);       
        Task<Result> MechanicVerifyDeviceAsync(Guid confirmationId, Guid deviceId, Guid mechanicId);
        //Stock In
        Task<Result> MechanicVerifyStockInAsync(Guid confirmationId, Guid deviceId, Guid mechanicId); 
        Task<Result> StockkeeperConfirmStockIn(ConfirmDoneRequest confirmationRequest, Guid userId);
        // Installation
        Task<Result> HODConfirmTaskInstall(ConfirmTaskRequest request, Guid hodId);
        Task<Result> MechanicConfirmInstallation(Guid taskId, Guid mechanicId, Guid newDevice);
        Task<Result> MechanicVerifyInstallationAsync(Guid confirmationId, Guid deviceId, Guid mechanicId, string? reason, string? deviceCondition);


        //WarrantySubmission

        Task<Result> MechanicVerifyWarrantySubmissionAsync(Guid confirmationId, Guid deviceId, Guid mechanicId);
        Task<Result> StockkeeperConfirmWarrantySubmission(ConfirmDoneRequest confirmationRequest, Guid userId);

        //Get
        Task<Result> GetAllAsync(
            int pageNumber,
            int pageSize,
            string? status = null,
            string? sortBy = null,
            bool isAscending = true);
        Task<Result> GetByIdAsync(Guid confirmationId);
        
        
    }
}
