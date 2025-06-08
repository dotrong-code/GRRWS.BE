using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Sparepart;
using GRRWS.Infrastructure.DTOs.SparePartUsage;
using System;
using System.Collections.Generic;
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
                // Không cần Spareparts ở đây vì chỉ GetRequestTakeSparePartUsageById cần
            };

            return Result.SuccessWithObject(dto);
        }

        public async Task<Result> GetAllRequestTakeSparePartUsagesAsync()
        {
            var requests = await _unitOfWork.SparePartUsageRepository.GetAllRequestTakeSparePartUsagesAsync();
            if (requests == null || !requests.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No request take spare part usage found"));

            var dtos = requests.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                Status = r.Status,
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock
                }).ToList()
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }


        
        public async Task<Result> UpdateIsTakenFromStockAsync(UpdateIsTakenFromStockRequest request)
        {
            if (request.SparePartUsageIds == null || !request.SparePartUsageIds.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Bad Request", "At least one SparePartUsage ID is required"));

            var usageToUpdate = new List<SparePartUsage>();
            var requestIds = new HashSet<Guid>(); // Lưu các RequestTakeSparePartUsageId liên quan

            foreach (var usageId in request.SparePartUsageIds)
            {
                var usage = await _unitOfWork.SparePartUsageRepository.GetByIdAsync(usageId);
                if (usage == null)
                    return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Spare part usage with ID {usageId} not found"));
                usage.IsTakenFromStock = request.IsTakenFromStock;
                usageToUpdate.Add(usage);
                if (usage.RequestTakeSparePartUsageId.HasValue)
                    requestIds.Add(usage.RequestTakeSparePartUsageId.Value);
            }

            foreach (var usage in usageToUpdate)
            {
                await _unitOfWork.SparePartUsageRepository.UpdateAsync(usage);
            }

            // Kiểm tra và cập nhật trạng thái của RequestTakeSparePartUsage
            foreach (var requestId in requestIds)
            {
                var requestUsage = await _unitOfWork.RequestTakeSparePartUsageRepository.GetByIdAsync(requestId);
                if (requestUsage != null)
                {
                    var allUsages = await _unitOfWork.SparePartUsageRepository.GetByRequestTakeSparePartUsageIdAsync(requestId);
                    bool allTaken = allUsages.All(u => u.IsTakenFromStock);
                    if (allTaken && requestUsage.Status != SparePartRequestStatus.Cancelled) // Sử dụng enum thay vì chuỗi
                    {
                        requestUsage.Status = SparePartRequestStatus.Delivered; // Sử dụng enum
                        requestUsage.ConfirmedDate = DateTime.UtcNow; // Cập nhật ngày xác nhận
                        await _unitOfWork.RequestTakeSparePartUsageRepository.UpdateAsync(requestUsage);
                    }
                }
            }

            return Result.Success(); // Trả về thành công nếu không có lỗi
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


        public async Task<Result> GetRequestTakeSparePartUsagesByStatusUnconfirmedAsync()
        {
            var requests = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Unconfirmed);
            if (requests == null || !requests.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No unconfirmed request take spare part usage found"));

            var dtos = requests.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                Status = r.Status,
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock
                }).ToList()
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusConfirmedAsync()
        {
            var requests = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Confirmed);
            if (requests == null || !requests.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No confirmed request take spare part usage found"));

            var dtos = requests.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                Status = r.Status,
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock
                }).ToList()
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusInsufficientAsync()
        {
            var requests = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Insufficient);
            if (requests == null || !requests.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No insufficient request take spare part usage found"));

            var dtos = requests.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                Status = r.Status,
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock
                }).ToList()
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusDeliveredAsync()
        {
            var requests = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Delivered);
            if (requests == null || !requests.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No delivered request take spare part usage found"));

            var dtos = requests.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                Status = r.Status,
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock
                }).ToList()
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }

        public async Task<Result> GetRequestTakeSparePartUsagesByStatusCancelledAsync()
        {
            var requests = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsagesByStatusAsync(SparePartRequestStatus.Cancelled);
            if (requests == null || !requests.Any())
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", "No cancelled request take spare part usage found"));

            var dtos = requests.Select(r => new RequestTakeSparePartUsageDto
            {
                Id = r.Id,
                RequestCode = r.RequestCode,
                RequestDate = r.RequestDate,
                RequestedById = r.RequestedById,
                Status = r.Status,
                ConfirmedDate = r.ConfirmedDate,
                ConfirmedById = r.ConfirmedById,
                Notes = r.Notes,
                SparePartUsages = r.SparePartUsages.Select(s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock

                }).ToList()
            }).ToList();

            return Result.SuccessWithObject(dtos);
        }
        public async Task<Result> GetRequestTakeSparePartUsageByIdAsync(Guid id)
        {
            var request = await _unitOfWork.SparePartUsageRepository.GetRequestTakeSparePartUsageByIdAsync(id);
            if (request == null)
                return Result.Failure(Infrastructure.DTOs.Common.Error.NotFound("Not found", $"Request take spare part usage with ID {id} not found"));

            var dto = new RequestTakeSparePartUsageDto
            {
                Id = request.Id,
                RequestCode = request.RequestCode,
                RequestDate = request.RequestDate,
                RequestedById = request.RequestedById,
                Status = request.Status,
                ConfirmedDate = request.ConfirmedDate,
                ConfirmedById = request.ConfirmedById,
                Notes = request.Notes,
                SparePartUsages = (await Task.WhenAll(request.SparePartUsages.Select(async s => new SparePartUsageDto
                {
                    Id = s.Id,
                    RequestTakeSparePartUsageId = s.RequestTakeSparePartUsageId,
                    SparePartId = s.SparePartId,
                    QuantityUsed = s.QuantityUsed,
                    IsTakenFromStock = s.IsTakenFromStock,
                    Spareparts = new List<SparepartDto>
                {
                    await MapSparepartDto(s.SparePartId)
                }
                }))).ToList()
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

    }
}