using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Sparepart;
using GRRWS.Infrastructure.DTOs.SparePartUsage;
using GRRWS.Infrastructure.DTOs.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class SparePartUsageService : ISparePartUsageService
    {
        private readonly UnitOfWork _unitOfWork;

        public SparePartUsageService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> GetByIdAsync(Guid id)
        {
            var sparePartUsage = await _unitOfWork.SparePartUsageRepository.GetByIdAsync(id);
            if (sparePartUsage == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Spare part usage not found"));

            var dto = new SparePartUsageDto
            {
                Id = sparePartUsage.Id,
                RequestTakeSparePartUsageId = sparePartUsage.RequestTakeSparePartUsageId,
                SparePartId = sparePartUsage.SparePartId,
                QuantityUsed = sparePartUsage.QuantityUsed,
                IsTakenFromStock = sparePartUsage.IsTakenFromStock
            };

            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> GetAllRequestTakeSparePartUsagesAsync(int pageNumber, int pageSize, string assigneeName = null)
        {
            var pagedResult = await _unitOfWork.SparePartUsageRepository.GetAllRequestTakeSparePartUsagesAsync(pageNumber, pageSize, assigneeName);
            

            var dtos = pagedResult.Data.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                AssigneeId = r.AssigneeId,
                AssigneeName = r.Assignee?.FullName ?? string.Empty,
                Status = r.Status.ToString(),
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock,
                    Spareparts = s.SparePart != null
        ? new List<SparepartDto>
        {
            new SparepartDto
            {
                Id = s.SparePart.Id,
                SparepartCode = s.SparePart.SparepartCode,
                SparepartName = s.SparePart.SparepartName,
                Description = s.SparePart.Description,
                Specification = s.SparePart.Specification,
                StockQuantity = s.SparePart.StockQuantity,
                IsAvailable = s.SparePart.IsAvailable,
                Unit = s.SparePart.Unit,
                
                ExpectedAvailabilityDate = s.SparePart.ExpectedAvailabilityDate,
                SupplierId = s.SparePart.SupplierId,
                Category = s.SparePart.Category,
                ImgUrl = s.SparePart.ImgUrl
            }
        }
        : new List<SparepartDto>()
                }).ToList()
            }).ToList();

            var response = new PagedResponse<RequestTakeSparePartUsageDto>
            {
                Data = dtos,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusUnconfirmedAsync(int pageNumber, int pageSize, string assigneeName = null)
        {
            var pagedResult = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Unconfirmed, pageNumber, pageSize, assigneeName);
            

            var dtos = pagedResult.Data.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                AssigneeId = r.AssigneeId,
                AssigneeName = r.Assignee?.FullName ?? string.Empty,
                Status = r.Status.ToString(),
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock,
                    Spareparts = s.SparePart != null
        ? new List<SparepartDto>
        {
            new SparepartDto
            {
                Id = s.SparePart.Id,
                SparepartCode = s.SparePart.SparepartCode,
                SparepartName = s.SparePart.SparepartName,
                Description = s.SparePart.Description,
                Specification = s.SparePart.Specification,
                StockQuantity = s.SparePart.StockQuantity,
                IsAvailable = s.SparePart.IsAvailable,
                Unit = s.SparePart.Unit,

                ExpectedAvailabilityDate = s.SparePart.ExpectedAvailabilityDate,
                SupplierId = s.SparePart.SupplierId,
                Category = s.SparePart.Category,
                ImgUrl = s.SparePart.ImgUrl
            }
        }
        : new List<SparepartDto>()
                }).ToList()
            }).ToList();

            var response = new PagedResponse<RequestTakeSparePartUsageDto>
            {
                Data = dtos,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusConfirmedAsync(int pageNumber, int pageSize, string assigneeName = null)
        {
            var pagedResult = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Confirmed, pageNumber, pageSize, assigneeName);
            

            var dtos = pagedResult.Data.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                AssigneeId = r.AssigneeId,
                AssigneeName = r.Assignee?.FullName ?? string.Empty,
                Status = r.Status.ToString(),
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock,
                    Spareparts = s.SparePart != null
        ? new List<SparepartDto>
        {
            new SparepartDto
            {
                Id = s.SparePart.Id,
                SparepartCode = s.SparePart.SparepartCode,
                SparepartName = s.SparePart.SparepartName,
                Description = s.SparePart.Description,
                Specification = s.SparePart.Specification,
                StockQuantity = s.SparePart.StockQuantity,
                IsAvailable = s.SparePart.IsAvailable,
                Unit = s.SparePart.Unit,

                ExpectedAvailabilityDate = s.SparePart.ExpectedAvailabilityDate,
                SupplierId = s.SparePart.SupplierId,
                Category = s.SparePart.Category,
                ImgUrl = s.SparePart.ImgUrl
            }
        }
        : new List<SparepartDto>()
                }).ToList()
            }).ToList();

            var response = new PagedResponse<RequestTakeSparePartUsageDto>
            {
                Data = dtos,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusInsufficientAsync(int pageNumber, int pageSize, string assigneeName = null)
        {
            var pagedResult = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Insufficient, pageNumber, pageSize, assigneeName);
            
            var dtos = pagedResult.Data.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                AssigneeId = r.AssigneeId,
                AssigneeName = r.Assignee?.FullName ?? string.Empty,
                Status = r.Status.ToString(),
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock,
                    Spareparts = s.SparePart != null
        ? new List<SparepartDto>
        {
            new SparepartDto
            {
                Id = s.SparePart.Id,
                SparepartCode = s.SparePart.SparepartCode,
                SparepartName = s.SparePart.SparepartName,
                Description = s.SparePart.Description,
                Specification = s.SparePart.Specification,
                StockQuantity = s.SparePart.StockQuantity,
                IsAvailable = s.SparePart.IsAvailable,
                Unit = s.SparePart.Unit,

                ExpectedAvailabilityDate = s.SparePart.ExpectedAvailabilityDate,
                SupplierId = s.SparePart.SupplierId,
                Category = s.SparePart.Category,
                ImgUrl = s.SparePart.ImgUrl
            }
        }
        : new List<SparepartDto>()
                }).ToList()
            }).ToList();

            var response = new PagedResponse<RequestTakeSparePartUsageDto>
            {
                Data = dtos,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusDeliveredAsync(int pageNumber, int pageSize, string assigneeName = null)
        {
            var pagedResult = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Delivered, pageNumber, pageSize, assigneeName);
            

            var dtos = pagedResult.Data.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                AssigneeId = r.AssigneeId,
                AssigneeName = r.Assignee?.FullName ?? string.Empty,
                Status = r.Status.ToString(),
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock,
                    Spareparts = s.SparePart != null
        ? new List<SparepartDto>
        {
            new SparepartDto
            {
                Id = s.SparePart.Id,
                SparepartCode = s.SparePart.SparepartCode,
                SparepartName = s.SparePart.SparepartName,
                Description = s.SparePart.Description,
                Specification = s.SparePart.Specification,
                StockQuantity = s.SparePart.StockQuantity,
                IsAvailable = s.SparePart.IsAvailable,
                Unit = s.SparePart.Unit,

                ExpectedAvailabilityDate = s.SparePart.ExpectedAvailabilityDate,
                SupplierId = s.SparePart.SupplierId,
                Category = s.SparePart.Category,
                ImgUrl = s.SparePart.ImgUrl
            }
        }
        : new List<SparepartDto>()
                }).ToList()
            }).ToList();

            var response = new PagedResponse<RequestTakeSparePartUsageDto>
            {
                Data = dtos,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusCancelledAsync(int pageNumber, int pageSize, string assigneeName = null)
        {
            var pagedResult = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Cancelled, pageNumber, pageSize, assigneeName);
            

            var dtos = pagedResult.Data.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                AssigneeId = r.AssigneeId,
                AssigneeName = r.Assignee?.FullName ?? string.Empty,
                Status = r.Status.ToString(),
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock,
                    Spareparts = s.SparePart != null
        ? new List<SparepartDto>
        {
            new SparepartDto
            {
                Id = s.SparePart.Id,
                SparepartCode = s.SparePart.SparepartCode,
                SparepartName = s.SparePart.SparepartName,
                Description = s.SparePart.Description,
                Specification = s.SparePart.Specification,
                StockQuantity = s.SparePart.StockQuantity,
                IsAvailable = s.SparePart.IsAvailable,
                Unit = s.SparePart.Unit,

                ExpectedAvailabilityDate = s.SparePart.ExpectedAvailabilityDate,
                SupplierId = s.SparePart.SupplierId,
                Category = s.SparePart.Category,
                ImgUrl = s.SparePart.ImgUrl
            }
        }
        : new List<SparepartDto>()
                }).ToList()
            }).ToList();


            var response = new PagedResponse<RequestTakeSparePartUsageDto>
            {
                Data = dtos,
                TotalCount = pagedResult.TotalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return Result.SuccessWithObject(response);
        }

        public async Task<Result> GetRequestTakeSparePartUsageByIdAsync(Guid id)
        {
            var request = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsageByIdAsync(id);
            if (request == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Request take spare part usage with ID {id} not found"));

            var sparePartUsageDtos = new List<SparePartUsageDto>();

            foreach (var s in request.SparePartUsages)
            {
                var sparepartDto = await MapSparepartDto(s.SparePartId);

                sparePartUsageDtos.Add(new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock,
                    Spareparts = s.SparePart != null
        ? new List<SparepartDto>
        {
            new SparepartDto
            {
                Id = s.SparePart.Id,
                SparepartCode = s.SparePart.SparepartCode,
                SparepartName = s.SparePart.SparepartName,
                Description = s.SparePart.Description,
                Specification = s.SparePart.Specification,
                StockQuantity = s.SparePart.StockQuantity,
                IsAvailable = s.SparePart.IsAvailable,
                Unit = s.SparePart.Unit,

                ExpectedAvailabilityDate = s.SparePart.ExpectedAvailabilityDate,
                SupplierId = s.SparePart.SupplierId,
                Category = s.SparePart.Category,
                ImgUrl = s.SparePart.ImgUrl
            }
        }
        : new List<SparepartDto>()
                });
            }

            var dto = new RequestTakeSparePartUsageDto
            {
                Id = request.Id,
                RequestCode = request.RequestCode,
                RequestDate = request.RequestDate,
                RequestedById = request.RequestedById,
                AssigneeId = request.AssigneeId,
                AssigneeName = request.Assignee?.FullName ?? string.Empty,
                Status = request.Status.ToString(),
                ConfirmedDate = request.ConfirmedDate,
                ConfirmedById = request.ConfirmedById,
                Notes = request.Notes,
                SparePartUsages = sparePartUsageDtos
            };

            return Result.SuccessWithObject(dto);
        }

        private async Task<SparepartDto> MapSparepartDto(Guid sparePartId)
        {
            var sparepart = await _unitOfWork.SparepartRepository.GetByIdAsync(sparePartId);
            if (sparepart == null)
                return null;

            return new SparepartDto
            {
                Id = sparepart.Id,
                SparepartCode = sparepart.SparepartCode,
                SparepartName = sparepart.SparepartName,
                Description = sparepart.Description,
                Specification = sparepart.Specification,
                StockQuantity = sparepart.StockQuantity
            };
        }

        public async Task<Result> UpdateIsTakenFromStockAsync(UpdateIsTakenFromStockRequest request)
        {
            if (request.SparePartUsageIds == null || !request.SparePartUsageIds.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "At least one SparePartUsage ID is required"));

            var usageToUpdate = new List<SparePartUsage>();
            var requestIds = new HashSet<Guid>();
            var sparePartsToUpdate = new Dictionary<Guid, Sparepart>();

            foreach (var usageId in request.SparePartUsageIds)
            {
                var usage = await _unitOfWork.SparePartUsageRepository.GetByIdAsync(usageId);
                if (usage == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Spare part usage with ID {usageId} not found"));

                if (usage.IsTakenFromStock != request.IsTakenFromStock)
                {
                    usage.IsTakenFromStock = request.IsTakenFromStock;
                    usageToUpdate.Add(usage);

                    if (request.IsTakenFromStock)
                    {
                        var sparePart = await _unitOfWork.SparepartRepository.GetByIdAsync(usage.SparePartId);
                        if (sparePart == null)
                            return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Spare part with ID {usage.SparePartId} not found"));

                        if (sparePart.StockQuantity < usage.QuantityUsed)
                            return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Insufficient stock", $"Spare part {sparePart.SparepartCode} has insufficient stock quantity"));

                        sparePart.StockQuantity -= usage.QuantityUsed;
                        sparePart.IsAvailable = sparePart.StockQuantity > 0;
                        sparePartsToUpdate[sparePart.Id] = sparePart;
                    }

                    if (usage.RequestTakeSparePartUsageId.HasValue)
                        requestIds.Add(usage.RequestTakeSparePartUsageId.Value);
                }
            }

            foreach (var usage in usageToUpdate)
            {
                await _unitOfWork.SparePartUsageRepository.UpdateAsync(usage);
            }

            foreach (var sparePart in sparePartsToUpdate.Values)
            {
                await _unitOfWork.SparepartRepository.UpdateAsync(sparePart);
            }

            foreach (var requestId in requestIds)
            {
                var requestUsage = await _unitOfWork.RequestTakeSparePartUsageRepository.GetByIdAsync(requestId);
                if (requestUsage != null)
                {
                    var allUsages = await _unitOfWork.SparePartUsageRepository.GetByRequestTakeSparePartUsageIdAsync(requestId);
                    bool allTaken = allUsages.All(u => u.IsTakenFromStock);
                    if (allTaken && requestUsage.Status != SparePartRequestStatus.Cancelled)
                    {
                        requestUsage.Status = SparePartRequestStatus.Delivered;
                        requestUsage.ConfirmedDate = TimeHelper.GetHoChiMinhTime();
                        await _unitOfWork.RequestTakeSparePartUsageRepository.UpdateAsync(requestUsage);
                    }
                }
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Update successfully" });
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var sparePartUsage = await _unitOfWork.SparePartUsageRepository.GetByIdAsync(id);
            if (sparePartUsage == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "Spare part usage not found"));

            await _unitOfWork.SparePartUsageRepository.RemoveAsync(sparePartUsage);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Spare part usage deleted successfully!" });
        }

        



        public async Task<Result> UpdateRequestTakeSparePartUsageStatusAsync(UpdateRequestStatusRequest request)
        {
            if (request.RequestTakeSparePartUsageId == Guid.Empty)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "RequestTakeSparePartUsageId is required"));

            var requestUsage = await _unitOfWork.RequestTakeSparePartUsageRepository.GetByIdAsync(request.RequestTakeSparePartUsageId);
            if (requestUsage == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Request take spare part usage with ID {request.RequestTakeSparePartUsageId} not found"));

            if (!Enum.TryParse<SparePartRequestStatus>(request.Status, true, out var newStatus))
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "Invalid status value"));

            if (!IsValidStatusTransition(requestUsage.Status, newStatus))
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", $"Invalid transition from {requestUsage.Status} to {newStatus}"));

            requestUsage.Status = newStatus;

            if (newStatus == SparePartRequestStatus.Confirmed || newStatus == SparePartRequestStatus.Delivered)
            {
                requestUsage.ConfirmedDate = TimeHelper.GetHoChiMinhTime();
                requestUsage.ConfirmedById = request.ConfirmedById ?? requestUsage.ConfirmedById;
                if (newStatus == SparePartRequestStatus.Confirmed && requestUsage.AssigneeId == null)
                {
                    var mechanics = await _unitOfWork.UserRepository.GetMechanicsWithoutTask();
                    if (mechanics == null || !mechanics.Any())
                    {
                        // Cập nhật trạng thái mà không gán AssigneeId
                        requestUsage.Notes = request.Notes ?? requestUsage.Notes;
                        await _unitOfWork.RequestTakeSparePartUsageRepository.UpdateAsync(requestUsage);
                        await _unitOfWork.SaveChangesAsync();
                        return Result.SuccessWithObject(new
                        {
                            Message = "Không có nhân viên rảnh để assign, nhưng request đã được cập nhật trạng thái thành Confirmed.",
                            RequestTakeSparePartUsageId = requestUsage.Id
                        });
                    }

                    var primaryMechanic = mechanics.First().Id;
                    requestUsage.AssigneeId = primaryMechanic;
                    requestUsage.Notes = request.Notes ?? requestUsage.Notes;
                    // Update AssigneeId and Status for related Task
                    // Find related Task via ErrorDetail
                    var errorDetail = await _unitOfWork.ErrorDetailRepository.GetByRequestTakeSparePartUsageIdAsync(requestUsage.Id);
                    if (errorDetail != null && errorDetail.TaskId.HasValue)
                    {
                        var task = await _unitOfWork.TaskRepository.GetByIdAsync(errorDetail.TaskId.Value);
                        if (task != null)
                        {
                            task.AssigneeId = primaryMechanic;
                            task.Status = Status.Pending;
                            _unitOfWork.TaskRepository.Update(task);
                        }                        
                    }
                }
            }
            else
            {
                requestUsage.Notes = request.Notes ?? requestUsage.Notes;
            }

            requestUsage.Notes = request.Notes ?? requestUsage.Notes;

            await _unitOfWork.RequestTakeSparePartUsageRepository.UpdateAsync(requestUsage);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Status updated successfully" });
        }
        public async Task<Result> UpdateRequestTakeSparePartUsageInsufficientStatusAsync(UpdateRequestInsufficientStatusRequest request)
        {
            if (request.RequestTakeSparePartUsageId == Guid.Empty)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "RequestTakeSparePartUsageId is required"));

            if (request.SparePartIds == null || !request.SparePartIds.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "At least one SparePart ID is required"));

            if (request.ExpectedAvailabilityDate <= TimeHelper.GetHoChiMinhTime())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "ExpectedAvailabilityDate must be in the future"));

            var requestUsage = await _unitOfWork.RequestTakeSparePartUsageRepository.GetByIdIncludeSparePartUsagesAsync(request.RequestTakeSparePartUsageId);
            if (requestUsage == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Request take spare part usage with ID {request.RequestTakeSparePartUsageId} not found"));

            if (!IsValidStatusTransition(requestUsage.Status, SparePartRequestStatus.Insufficient))
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", $"Invalid transition from {requestUsage.Status} to Insufficient"));

            // Update status to Insufficient
            requestUsage.Status = SparePartRequestStatus.Insufficient;
            requestUsage.Notes = request.Notes ?? requestUsage.Notes;

            // Update ExpectedAvailabilityDate for specified spare parts
            foreach (var sparePartId in request.SparePartIds)
            {
                var sparePart = await _unitOfWork.SparepartRepository.GetByIdAsync(sparePartId);
                if (sparePart == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Spare part with ID {sparePartId} not found"));

                // Verify the spare part is part of the request
                var usage = requestUsage.SparePartUsages.FirstOrDefault(u => u.SparePartId == sparePartId);
                if (usage == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", $"Spare part with ID {sparePartId} is not associated with this request"));

                sparePart.ExpectedAvailabilityDate = request.ExpectedAvailabilityDate;
                await _unitOfWork.SparepartRepository.UpdateAsync(sparePart);
            }

            await _unitOfWork.RequestTakeSparePartUsageRepository.UpdateAsync(requestUsage);
            await _unitOfWork.SaveChangesAsync();

            return Result.SuccessWithObject(new { Message = "Insufficient status updated successfully with expected availability date for specified spare parts" });
        }
        private bool IsValidStatusTransition(SparePartRequestStatus currentStatus, SparePartRequestStatus newStatus)
        {
            switch (currentStatus)
            {
                case SparePartRequestStatus.Unconfirmed:
                    return newStatus == SparePartRequestStatus.Confirmed ||
                           newStatus == SparePartRequestStatus.Insufficient ||
                           newStatus == SparePartRequestStatus.Cancelled;
                case SparePartRequestStatus.Confirmed:
                    return newStatus == SparePartRequestStatus.Delivered ||
                           newStatus == SparePartRequestStatus.Insufficient ||
                           newStatus == SparePartRequestStatus.Cancelled;
                case SparePartRequestStatus.Insufficient:
                    return newStatus == SparePartRequestStatus.Confirmed ||
                           newStatus == SparePartRequestStatus.Cancelled;
                case SparePartRequestStatus.Delivered:
                case SparePartRequestStatus.Cancelled:
                    return false;
                default:
                    return false;
            }
        }
    }
}