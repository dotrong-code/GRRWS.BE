using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Common;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common.StringHelper;
using GRRWS.Infrastructure.DTOs.MachineActionConfirmation;
using GRRWS.Infrastructure.DTOs.Paging;
using GRRWS.Infrastructure.Interfaces;

namespace GRRWS.Application.Implement.Service
{
    public class MachineActionConfirmationService : IMachineActionConfirmationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CheckIsExist _checkIsExist;

        public MachineActionConfirmationService(IUnitOfWork unitOfWork, CheckIsExist checkIsExist)
        {
            _unitOfWork = unitOfWork;
            _checkIsExist = checkIsExist;
        }
        
        public async Task<Result> StockkeeperConfirmHadDevice(Guid confirmationId, Guid userId, Guid Deviceid)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdAsync(confirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Confirmation not found."));

            if (confirmation.ActionType != MachineActionType.StockOut)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Only StockOut confirmations can be confirmed by stockkeeper."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user.Role != 4) // Stockkeeper role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only stockkeepers can confirm device availability."));

            var mechanics = await _unitOfWork.UserRepository.GetMechanicsWithoutTask();
            if (!mechanics.Any())
            {
                confirmation.DeviceId = Deviceid;
                confirmation.StockkeeperConfirm = true;
                confirmation.ApprovedById = userId;
                confirmation.ApprovedDate = TimeHelper.GetHoChiMinhTime();
                confirmation.Status = MachineActionStatus.Approved;
                confirmation.Notes = "Stockkeeper confirmed availability, but no mechanics available.";
                confirmation.StartDate = TimeHelper.GetHoChiMinhTime();
                await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
                await _unitOfWork.SaveChangesAsync();
                return Result.SuccessWithObject(new
                {
                    Message = "Stockkeeper confirmed availability, but no mechanics available.",
                    ConfirmationId = confirmation.Id,
                    TaskId = confirmation.TaskId
                });
            }

            var mechanicId = mechanics.First().Id;
            confirmation.DeviceId = Deviceid;
            confirmation.StockkeeperConfirm = true;
            confirmation.ApprovedById = userId;
            confirmation.ApprovedDate = TimeHelper.GetHoChiMinhTime();
            confirmation.AssigneeId = mechanicId;
            confirmation.Status = MachineActionStatus.InProgress;
            confirmation.Notes = "Stockkeeper confirmed availability, mechanic assigned.";
            confirmation.StartDate = TimeHelper.GetHoChiMinhTime();

            // Assign mechanic to associated StockIn confirmation
            var stockInConfirmation = await _unitOfWork.MachineActionConfirmationRepository
                .GetByTaskIdAndTypeAsync(confirmation.TaskId, MachineActionType.StockIn);
            if (stockInConfirmation != null)
            {
                stockInConfirmation.AssigneeId = mechanicId;
                stockInConfirmation.Status = MachineActionStatus.Pending;
                await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(stockInConfirmation);
            }

            // Update task
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(confirmation.TaskId);
            if (task != null)
            {
                task.AssigneeId = mechanicId;
                task.Status = Status.Pending;
                await _unitOfWork.TaskRepository.UpdateAsync(task);
            }

            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new
            {
                Message = "Stockkeeper confirmed availability and mechanic assigned.",
                ConfirmationId = confirmation.Id,
                TaskId = confirmation.TaskId,
                MechanicId = mechanicId
            });
        }
        
        public async Task<Result> StockkeeperConfirmTakenDevice(ConfirmDoneRequest confirmationRequest, Guid userId)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdAsync(confirmationRequest.ConfirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Confirmation not found."));

            if (confirmation.ActionType != MachineActionType.StockOut)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Only StockOut confirmations can be confirmed by stockkeeper."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user.Role != 4) // Stockkeeper role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only stockkeepers can confirm device handover."));

            if (!confirmation.MechanicConfirm)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Mechanic must verify device before stockkeeper confirmation."));

            if (confirmation.DeviceId != confirmationRequest.DeviceId)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Confirmed device does not match the assigned device."));

            if (confirmation.VerificationToken != confirmationRequest.VerificationToken)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Invalid verification token."));

            if (confirmation.TokenExpiration < TimeHelper.GetHoChiMinhTime())
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Verification token has expired."));

            var task = await _unitOfWork.TaskRepository.GetByIdAsync(confirmation.TaskId);
            if (task == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Task not found."));

            confirmation.Status = MachineActionStatus.Completed;
            confirmation.CompletedDate = TimeHelper.GetHoChiMinhTime();
            confirmation.SignerId = userId;
            confirmation.SignerRole = "Stockkeeper";
            confirmation.SignatureBase64 = confirmationRequest.SignatureBase64;
            confirmation.Notes = $"Stockkeeper confirmed device {confirmationRequest.DeviceId} handed to mechanic at {TimeHelper.GetHoChiMinhTime()}";
            confirmation.StockkeeperConfirm = true;
            confirmation.ModifiedDate = TimeHelper.GetHoChiMinhTime();
            confirmation.VerificationToken = null; // Clear token after use
            confirmation.TokenExpiration = null; // Clear expiration after use

            task.StartTime = TimeHelper.GetHoChiMinhTime();
            task.Status = Status.InProgress;

            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new
            {
                ConfirmationId = confirmation.Id,
                MechanicConfirm = confirmation.MechanicConfirm,
                StockkeeperConfirm = confirmation.StockkeeperConfirm,
                Status = confirmation.Status.ToString(),
                TaskId = confirmation.TaskId
            });
        }
        public async Task<Result> MechanicVerifyDeviceAsync(Guid confirmationId, Guid deviceId, Guid mechanicId)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdAsync(confirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Confirmation not found."));

            if (confirmation.ActionType != MachineActionType.StockOut)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Only StockOut confirmations can be verified by mechanic."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(mechanicId);
            if (user.Role != 3) // Mechanic role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can verify devices."));

            if (confirmation.DeviceId != deviceId)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Scanned device does not match the assigned device."));

            // Generate QR code data (e.g., a JSON payload with ConfirmationId and a unique token)
            // Generate QR code data with a unique token
            var token = Guid.NewGuid();
            var qrCodePayload = new
            {
                ConfirmationId = confirmationId,
                DeviceId = deviceId,
                Timestamp = TimeHelper.GetHoChiMinhTime(),
                VerificationToken = token
            };
            var qrCodeData = System.Text.Json.JsonSerializer.Serialize(qrCodePayload);
            // In a real implementation, convert qrCodeData to a QR code image (base64 or URL) using a library like QRCoder
            // For simplicity, return the JSON string here; the frontend can generate the QR code

            //SetupToken
            confirmation.VerificationToken = token;
            confirmation.TokenExpiration = TimeHelper.GetHoChiMinhTime().AddMinutes(10); // Token expires in 5 minutes

            confirmation.MechanicConfirm = true; // Mark that mechanic has verified the device
            confirmation.Notes = $"Mechanic verified device {deviceId} at {TimeHelper.GetHoChiMinhTime()}";
            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new MechanicDeviceVerificationResponse
            {
                ConfirmationId = confirmationId,
                DeviceId = deviceId,
                QRCodeData = qrCodeData, // Frontend will convert this to a QR code
                Message = "Device verified successfully. Present QR code to stockkeeper for confirmation."
            });
        }

        public async Task<Result> MechanicVerifyInstallationAsync(Guid confirmationId, Guid deviceId, Guid mechanicId, string? reason, string? deviceCondition)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdAsync(confirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Installation confirmation not found."));

            if (confirmation.ActionType != MachineActionType.Installation)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Only Installation confirmations can be verified by mechanic."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(mechanicId);
            if (user.Role != 3) // Mechanic role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can verify installations."));

            if (confirmation.DeviceId != deviceId)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Scanned device does not match the assigned device."));

            // Generate QR code data with a unique token
            var token = Guid.NewGuid();
            var qrCodePayload = new
            {
                ConfirmationId = confirmationId,
                DeviceId = deviceId,
                Timestamp = TimeHelper.GetHoChiMinhTime(),
                VerificationToken = token
            };
            var qrCodeData = System.Text.Json.JsonSerializer.Serialize(qrCodePayload);

            // Store token and expiration
            if(confirmation.DeviceId != deviceId)
            {
                confirmation.Reason = reason;
                confirmation.DeviceId = deviceId;
                confirmation.DeviceCondition = deviceCondition;
            }
            else
            {
                confirmation.DeviceCondition = "New Device works well";
            }
            confirmation.MechanicConfirm = true;
            confirmation.VerificationToken = token;
            confirmation.TokenExpiration = TimeHelper.GetHoChiMinhTime().AddMinutes(10); // Token expires in 10 minutes
            confirmation.Notes = $"Mechanic verified installation of device {deviceId} at {TimeHelper.GetHoChiMinhTime()}";
            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new MechanicDeviceVerificationResponse
            {
                ConfirmationId = confirmationId,
                DeviceId = deviceId,
                QRCodeData = qrCodeData,
                Message = "Installation verified successfully. Present QR code to HOD for confirmation."
            });
        }
        public async Task<Result> MechanicConfirmInstallation(Guid taskId, Guid mechanicId, Guid NewDeviceId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(taskId);
            if (task == null || task.TaskType != TaskType.Installation)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Installation task not found."));

            if (task.AssigneeId != mechanicId)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only the assigned mechanic can confirm installation."));

            var request = await _unitOfWork.RequestRepository.GetByTaskIdAsync(taskId);
            if (request == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Request not found."));

            // Create Installation confirmation
            var installationConfirmation = new MachineActionConfirmation
            {
                Id = Guid.NewGuid(),
                ConfirmationCode = RequestReplaceMachineString.InstallationConfirmation("Unknown"),
                StartDate = TimeHelper.GetHoChiMinhTime(),
                RequestedById = mechanicId,
                DeviceId = NewDeviceId,
                TaskId = taskId,
                Reason = "Confirm successful installation of replacement device",
                Status = MachineActionStatus.Pending,
                ActionType = MachineActionType.Installation,
                Notes = "Awaiting HOD signature"
            };

            task.Status = Status.InProgress;
            request.IsNeedSign = true;

            await _unitOfWork.MachineActionConfirmationRepository.CreateAsync(installationConfirmation);
            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.RequestRepository.UpdateAsync(request);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new
            {
                Message = "Installation confirmed, awaiting HOD signature.",
                TaskId = taskId,
                ConfirmationId = installationConfirmation.Id
            });
        }

        //Done
        public async Task<Result> HODConfirmTaskInstall(ConfirmTaskRequest request, Guid hodId)
        {
            var task = await _unitOfWork.TaskRepository.GetByIdAsync(request.TaskId);
            if (task == null || task.TaskType != TaskType.Installation)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Installation task not found."));
            //Lấy phiếu lắp đặt máy để confirm kí hoàn thành lặp đặt 
            var installationConfirmation = await _unitOfWork.MachineActionConfirmationRepository
                .GetByTaskIdAndTypeAsync(request.TaskId, MachineActionType.Installation);
            if (installationConfirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Installation confirmation not found."));
            //Lấy phiếu trả máy để gửi request cho Stockkeeper biết yêu cầu lấy máy 
            var stockInConfirmation = await _unitOfWork.MachineActionConfirmationRepository
                .GetByTaskIdAndTypeAsync(request.TaskId, MachineActionType.StockIn);
            if (stockInConfirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "StockIn confirmation not found."));
            //Lấy thông tin máy mới được lắp đặt
            var newDevice = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(installationConfirmation.DeviceId);
            if (newDevice == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Replacement device not found."));

            var requestt = await _unitOfWork.RequestRepository.GetByTaskIdAsync(request.TaskId);
            if (requestt == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Request not found."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(hodId);
            if (user.Role != 1) // HOD role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only HOD can confirm tasks."));

            if (!installationConfirmation.MechanicConfirm)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Mechanic must verify installation before HOD confirmation."));

            if (installationConfirmation.VerificationToken != request.VerificationToken)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Invalid verification token."));

            if (installationConfirmation.TokenExpiration < TimeHelper.GetHoChiMinhTime())
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Verification token has expired."));

            // Update installation confirmation
            installationConfirmation.Status = MachineActionStatus.Completed;
            installationConfirmation.CompletedDate = TimeHelper.GetHoChiMinhTime();
            installationConfirmation.SignerId = hodId;
            installationConfirmation.SignerRole = "HOD";
            installationConfirmation.SignatureBase64 = request.SignatureBase64;
            installationConfirmation.DeviceCondition = request.DeviceCondition;
            installationConfirmation.Notes = "HOD confirmed successful installation";
            installationConfirmation.ModifiedDate = TimeHelper.GetHoChiMinhTime();
            installationConfirmation.VerificationToken = null; // Clear token
            installationConfirmation.TokenExpiration = null; // Clear expiration
            installationConfirmation.StartDate = TimeHelper.GetHoChiMinhTime();
            // Update StockIn confirmation
            stockInConfirmation.StartDate = TimeHelper.GetHoChiMinhTime(); // Update start date for return
            stockInConfirmation.Status = MachineActionStatus.InProgress;
            stockInConfirmation.Notes = "HOD confirmed installation, mechanic can return faulty device";
            stockInConfirmation.ModifiedDate = TimeHelper.GetHoChiMinhTime();

            // Update task
            task.Status = Status.Completed;
            task.EndTime = TimeHelper.GetHoChiMinhTime();
            task.IsSigned = true;
            task.IsInstall = true;
            

            //Update for newDevice
            newDevice.Status = DeviceStatus.InUse;
            newDevice.InUsed = true;
            newDevice.InstallationDate = TimeHelper.GetHoChiMinhTime();
            newDevice.PositionId = (await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(stockInConfirmation.DeviceId)).PositionId;

            //Update for oldDevice
            var oldDevice = stockInConfirmation.Device;
            oldDevice.Status = DeviceStatus.InWarranty;
            oldDevice.InstallationDate = null;
            oldDevice.PositionId = null;
            newDevice.InUsed = false;

            //Update request 
            requestt.IsNeedSign = false;
            requestt.IsSovled = true;

            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(installationConfirmation);
            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(stockInConfirmation);
            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.RequestRepository.UpdateAsync(requestt);
            await _unitOfWork.DeviceRepository.UpdateAsync(newDevice);
            await _unitOfWork.DeviceRepository.UpdateAsync(oldDevice);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new
            {
                Message = "Task confirmed, installation completed, and StockIn action started.",
                TaskId = task.Id,
                InstallationConfirmationId = installationConfirmation.Id,
                StockInConfirmationId = stockInConfirmation.Id
            });
        }
        //Stock in

        public async Task<Result> MechanicVerifyStockInAsync(Guid confirmationId, Guid deviceId, Guid mechanicId)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdAsync(confirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "StockIn confirmation not found."));

            if (confirmation.ActionType != MachineActionType.StockIn)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Only StockIn confirmations can be verified by mechanic."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(mechanicId);
            if (user.Role != 3) // Mechanic role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can verify stock-in devices."));

            if (confirmation.DeviceId != deviceId)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Scanned device does not match the assigned device."));

            // Generate QR code data with a unique token
            var token = Guid.NewGuid();
            var qrCodePayload = new
            {
                ConfirmationId = confirmationId,
                DeviceId = deviceId,
                Timestamp = TimeHelper.GetHoChiMinhTime(),
                VerificationToken = token
            };
            var qrCodeData = System.Text.Json.JsonSerializer.Serialize(qrCodePayload);

            // Store token and expiration
            confirmation.MechanicConfirm = true;
            confirmation.VerificationToken = token;
            confirmation.TokenExpiration = TimeHelper.GetHoChiMinhTime().AddMinutes(10); // Token expires in 10 minutes
            confirmation.Notes = $"Mechanic verified stock-in of device {deviceId} at {TimeHelper.GetHoChiMinhTime()}";
            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new MechanicDeviceVerificationResponse
            {
                ConfirmationId = confirmationId,
                DeviceId = deviceId,
                QRCodeData = qrCodeData,
                Message = "Stock-in verified successfully. Present QR code to stockkeeper for confirmation."
            });
        }

        public async Task<Result> StockkeeperConfirmStockIn(ConfirmDoneRequest confirmationRequest, Guid userId)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdAsync(confirmationRequest.ConfirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "StockIn confirmation not found."));

            if (confirmation.ActionType != MachineActionType.StockIn)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Only StockIn confirmations can be confirmed by stockkeeper."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user.Role != 4) // Stockkeeper role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only stockkeepers can confirm stock-in."));

            if (!confirmation.MechanicConfirm)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Mechanic must verify stock-in before stockkeeper confirmation."));

            if (confirmation.DeviceId != confirmationRequest.DeviceId)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Confirmed device does not match the assigned device."));

            if (confirmation.VerificationToken != confirmationRequest.VerificationToken)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Invalid verification token."));

            if (confirmation.TokenExpiration < TimeHelper.GetHoChiMinhTime())
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Verification token has expired."));

            var task = await _unitOfWork.TaskRepository.GetByIdAsync(confirmation.TaskId);
            if (task == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Task not found."));

            confirmation.Status = MachineActionStatus.Completed;
            confirmation.CompletedDate = TimeHelper.GetHoChiMinhTime();
            confirmation.SignerId = userId;
            confirmation.SignerRole = "Stockkeeper";
            confirmation.SignatureBase64 = confirmationRequest.SignatureBase64;
            confirmation.Notes = $"Stockkeeper confirmed stock-in of device {confirmationRequest.DeviceId} at {TimeHelper.GetHoChiMinhTime()}";
            confirmation.StockkeeperConfirm = true;
            confirmation.ModifiedDate = TimeHelper.GetHoChiMinhTime();
            confirmation.VerificationToken = null; // Clear token
            confirmation.TokenExpiration = null; // Clear expiration

            // Update device status to indicate it is back in stock
            var device = await _unitOfWork.DeviceRepository.GetDeviceByIdAsync(confirmationRequest.DeviceId);
            if (device == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Device not found."));

            device.Status = DeviceStatus.Inactive; // Update to reflect device is back in stock
            device.InUsed = false;
            device.InstallationDate = null;
            device.PositionId = null;

            // Update task status
            task.Status = Status.Completed;
            task.EndTime = TimeHelper.GetHoChiMinhTime();
            task.IsUninstall = false;

            // Find and activate WarrantySubmission task in the same TaskGroup
            var taskGroupId = task.TaskGroupId;
            if (!taskGroupId.HasValue)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Task group not found for the current task."));

            var warrantyTask = await _unitOfWork.TaskRepository.GetByTaskGroupIdAndTypeAsync(taskGroupId.Value, TaskType.WarrantySubmission);
            if (warrantyTask == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "WarrantySubmission task not found in the task group."));


            var mechanics = await _unitOfWork.UserRepository.GetMechanicsWithoutTask();
            if (!mechanics.Any())
            {
                // Update warranty task without mechanic assignment
                warrantyTask.Status = Status.InProgress;
                warrantyTask.StartTime = TimeHelper.GetHoChiMinhTime();
                warrantyTask.ModifiedDate = TimeHelper.GetHoChiMinhTime();
                warrantyTask.TaskDescription = "WarrantySubmission task activated, but no mechanics available.";


                // Update warranty confirmation without mechanic assignment
                var warrantyConfirmation = await _unitOfWork.MachineActionConfirmationRepository
                .GetByTaskIdAndTypeAsync(warrantyTask.Id, MachineActionType.WarrantySubmission);
                
                if (warrantyConfirmation == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "WarrantySubmission task not found in the task group."));
                }
                else
                {
                    warrantyConfirmation.Status = MachineActionStatus.Pending;
                    warrantyConfirmation.StartDate = TimeHelper.GetHoChiMinhTime();
                    warrantyConfirmation.Notes = $"Warranty submission confirmation activated at {TimeHelper.GetHoChiMinhTime()}";
                    warrantyConfirmation.ModifiedDate = TimeHelper.GetHoChiMinhTime();
                    await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(warrantyConfirmation);
                    
                }

            }
            else
            {
                // Update warranty task without mechanic assignment
                var mechanicId = mechanics.First().Id;
                warrantyTask.AssigneeId = mechanicId;
                warrantyTask.Status = Status.InProgress;
                warrantyTask.StartTime = TimeHelper.GetHoChiMinhTime();
                warrantyTask.ModifiedDate = TimeHelper.GetHoChiMinhTime();
                warrantyTask.TaskDescription = $"WarrantySubmission task activated and assigned to mechanic {mechanicId} at {TimeHelper.GetHoChiMinhTime()}";



                // Update warranty confirmation without mechanic assignment
                var warrantyConfirmation = await _unitOfWork.MachineActionConfirmationRepository
                .GetByTaskIdAndTypeAsync(warrantyTask.Id, MachineActionType.WarrantySubmission);
                
                if (warrantyConfirmation == null)
                {
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "WarrantySubmission task not found in the task group."));
                }
                else
                {
                    warrantyConfirmation.AssigneeId = mechanicId;
                    warrantyConfirmation.Status = MachineActionStatus.Pending;
                    warrantyConfirmation.StartDate = TimeHelper.GetHoChiMinhTime();
                    warrantyConfirmation.Notes = $"Warranty submission confirmation activated at {TimeHelper.GetHoChiMinhTime()}";
                    warrantyConfirmation.ModifiedDate = TimeHelper.GetHoChiMinhTime();
                    await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(warrantyConfirmation);
                    
                }
            }
            


            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
            await _unitOfWork.DeviceRepository.UpdateAsync(device);
            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.TaskRepository.UpdateAsync(warrantyTask);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new
            {
                ConfirmationId = confirmation.Id,
                MechanicConfirm = confirmation.MechanicConfirm,
                StockkeeperConfirm = confirmation.StockkeeperConfirm,
                Status = confirmation.Status.ToString(),
                TaskId = confirmation.TaskId,
                DeviceId = confirmationRequest.DeviceId,
                WarrantyTaskId = warrantyTask.Id,
                Message = "Stock-in confirmed successfully, WarrantySubmission task and confirmation activated."

            });
        }

        public async Task<Result> MechanicVerifyWarrantySubmissionAsync(Guid confirmationId, Guid deviceId, Guid mechanicId)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdAsync(confirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "WarrantySubmission confirmation not found."));

            if (confirmation.ActionType != MachineActionType.WarrantySubmission)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Only WarrantySubmission confirmations can be verified by mechanic."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(mechanicId);
            if (user.Role != 3) // Mechanic role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only mechanics can verify warranty submissions."));

            if (confirmation.DeviceId != deviceId)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Scanned device does not match the assigned device."));

            var token = Guid.NewGuid();
            var qrCodePayload = new
            {
                ConfirmationId = confirmationId,
                DeviceId = deviceId,
                Timestamp = TimeHelper.GetHoChiMinhTime(),
                VerificationToken = token
            };
            var qrCodeData = System.Text.Json.JsonSerializer.Serialize(qrCodePayload);

            confirmation.MechanicConfirm = true;
            confirmation.VerificationToken = token;
            confirmation.TokenExpiration = TimeHelper.GetHoChiMinhTime().AddMinutes(10);
            confirmation.Notes = $"Mechanic verified warranty submission for device {deviceId} at {TimeHelper.GetHoChiMinhTime()}";
            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new MechanicDeviceVerificationResponse
            {
                ConfirmationId = confirmationId,
                DeviceId = deviceId,
                QRCodeData = qrCodeData,
                Message = "Warranty submission verified successfully. Present QR code to stockkeeper for confirmation."
            });
        }

        public async Task<Result> StockkeeperConfirmWarrantySubmission(ConfirmDoneRequest confirmationRequest, Guid userId)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdAsync(confirmationRequest.ConfirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "WarrantySubmission confirmation not found."));

            if (confirmation.ActionType != MachineActionType.WarrantySubmission)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Only WarrantySubmission confirmations can be confirmed by stockkeeper."));

            var user = await _unitOfWork.UserRepository.GetByIdAsync(userId);
            if (user.Role != 4) // Stockkeeper role
                return Result.Failure(Infrastructure.DTOs.Common.Error.Unauthorized("Unauthorized", "Only stockkeepers can confirm warranty submission handover."));

            if (!confirmation.MechanicConfirm)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Mechanic must verify warranty submission before stockkeeper confirmation."));

            if (confirmation.DeviceId != confirmationRequest.DeviceId)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Confirmed device does not match the assigned device."));

            if (confirmation.VerificationToken != confirmationRequest.VerificationToken)
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Invalid verification token."));

            if (confirmation.TokenExpiration < TimeHelper.GetHoChiMinhTime())
                return Result.Failure(Infrastructure.DTOs.Common.Error.Validation("ValidationError", "Verification token has expired."));

            var task = await _unitOfWork.TaskRepository.GetByIdAsync(confirmation.TaskId);
            if (task == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Task not found."));

            confirmation.Status = MachineActionStatus.Completed;
            confirmation.CompletedDate = TimeHelper.GetHoChiMinhTime();
            confirmation.SignerId = userId;
            confirmation.SignerRole = "Stockkeeper";
            confirmation.SignatureBase64 = confirmationRequest.SignatureBase64;
            confirmation.Notes = $"Stockkeeper confirmed warranty submission handover of device {confirmationRequest.DeviceId} to mechanic at {TimeHelper.GetHoChiMinhTime()}";
            confirmation.StockkeeperConfirm = true;
            confirmation.ModifiedDate = TimeHelper.GetHoChiMinhTime();
            confirmation.VerificationToken = null;
            confirmation.TokenExpiration = null;

            task.Status = Status.Completed;
            task.EndTime = TimeHelper.GetHoChiMinhTime();

            await _unitOfWork.MachineActionConfirmationRepository.UpdateAsync(confirmation);
            await _unitOfWork.TaskRepository.UpdateAsync(task);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new
            {
                ConfirmationId = confirmation.Id,
                MechanicConfirm = confirmation.MechanicConfirm,
                StockkeeperConfirm = confirmation.StockkeeperConfirm,
                Status = confirmation.Status.ToString(),
                TaskId = confirmation.TaskId,
                Message = "Warranty submission handover confirmed successfully."
            });
        }





        public async Task<Result> GetAllAsync(
            int pageNumber,
            int pageSize,
            string? status = null,
            string? sortBy = null,
            bool isAscending = true)
        {
            var (items, totalCount) = await _unitOfWork.MachineActionConfirmationRepository.GetAllAsync(
                pageNumber, pageSize, status, sortBy, isAscending);
            var dtos = new List<Infrastructure.DTOs.MachineActionConfirmation.GetAll>();
            foreach (var item in items.Where(r => !r.IsDeleted).OrderByDescending(r => r.CreatedDate))
            {
                dtos.Add(new Infrastructure.DTOs.MachineActionConfirmation.GetAll
                {
                    Id = item.Id,
                    ConfirmationCode = item.ConfirmationCode,
                    StartDate = item.StartDate,
                    RequestedById = item.RequestedById,
                    AssigneeId = item.AssigneeId,
                    AssigneeName = item.Assignee?.FullName,
                    DeviceId = item.DeviceId ?? Guid.Empty,
                    MachineId = item.MachineId,
                    Status = item.Status.ToString(),
                    ActionType = item.ActionType.ToString(),
                    MechanicConfirm = item.MechanicConfirm,
                    StockkeeperConfirm = item.StockkeeperConfirm,
                    Notes = item.Notes
                });
            }

            var response = new PagedResponse<Infrastructure.DTOs.MachineActionConfirmation.GetAll>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetByIdAsync(Guid confirmationId)
        {
            var confirmation = await _unitOfWork.MachineActionConfirmationRepository.GetByIdWithDetailsAsync(confirmationId);
            if (confirmation == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("NotFound", "Confirmation not found."));

            var dto = new MachineActionConfirmationDTO
            {
                Id = confirmation.Id,
                ConfirmationCode = confirmation.ConfirmationCode,
                StartDate = confirmation.StartDate,
                RequestedById = confirmation.RequestedById,
                DeviceId = confirmation.DeviceId ?? Guid.Empty,
                TaskId = confirmation.TaskId,
                Status = confirmation.Status,
                ActionType = confirmation.ActionType,
                CompletedDate = confirmation.CompletedDate,
                Reason = confirmation.Reason,
                VerificationToken = confirmation.VerificationToken,
                TokenExpiration = confirmation.TokenExpiration,
                SignerId = confirmation.SignerId,
                SignerRole = confirmation.SignerRole,
                SignatureBase64 = confirmation.SignatureBase64,
                AssigneeId = confirmation.AssigneeId,
                MechanicConfirm = confirmation.MechanicConfirm,
                ApprovedById = confirmation.ApprovedById,
                StockkeeperConfirm = confirmation.StockkeeperConfirm,
                ApprovedDate = confirmation.ApprovedDate,
                MachineId = confirmation.MachineId,
                Notes = confirmation.Notes,
                DeviceCondition = confirmation.DeviceCondition,
                RequestedByName = confirmation.RequestedBy?.FullName,
                AssigneeName = confirmation.Assignee?.FullName,
                DeviceName = confirmation.Device?.DeviceName,
                MachineName = confirmation.Machine?.MachineName
            };
            return Result.SuccessWithObject(dto);
        }
    }
}