using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Infrastructure.DTOs.SparePartUsage;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class SparePartUsageController : ControllerBase
{
    private readonly ISparePartUsageService _sparePartUsageService;

    public SparePartUsageController(ISparePartUsageService sparePartUsageService)
    {
        _sparePartUsageService = sparePartUsageService;
    }

    [HttpGet("{id}")]
    public async Task<IResult> GetById(Guid id)
    {
        var result = await _sparePartUsageService.GetByIdAsync(id);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved spare part usage")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpGet("requests")]
    public async Task<IResult> GetAllRequestTakeSparePartUsages([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string assigneeName = null)
    {
        var result = await _sparePartUsageService.GetAllRequestTakeSparePartUsagesAsync(pageNumber, pageSize, assigneeName);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved all request take spare part usages")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpGet("requests/unconfirmed")]
    public async Task<IResult> GetRequestTakeSparePartUsagesByStatusUnconfirmed([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string assigneeName = null)
    {
        var result = await _sparePartUsageService.GetRequestTakeSparePartUsagesByStatusUnconfirmedAsync(pageNumber, pageSize, assigneeName);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved unconfirmed request take spare part usages")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpGet("requests/confirmed")]
    public async Task<IResult> GetRequestTakeSparePartUsagesByStatusConfirmed([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string assigneeName = null)
    {
        var result = await _sparePartUsageService.GetRequestTakeSparePartUsagesByStatusConfirmedAsync(pageNumber, pageSize, assigneeName);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved confirmed request take spare part usages")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpGet("requests/insufficient")]
    public async Task<IResult> GetRequestTakeSparePartUsagesByStatusInsufficient([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string assigneeName = null)
    {
        var result = await _sparePartUsageService.GetRequestTakeSparePartUsagesByStatusInsufficientAsync(pageNumber, pageSize, assigneeName);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved insufficient request take spare part usages")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpGet("requests/delivered")]
    public async Task<IResult> GetRequestTakeSparePartUsagesByStatusDelivered([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string assigneeName = null)
    {
        var result = await _sparePartUsageService.GetRequestTakeSparePartUsagesByStatusDeliveredAsync(pageNumber, pageSize, assigneeName);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved delivered request take spare part usages")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpGet("requests/cancelled")]
    public async Task<IResult> GetRequestTakeSparePartUsagesByStatusCancelled([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10, [FromQuery] string assigneeName = null)
    {
        var result = await _sparePartUsageService.GetRequestTakeSparePartUsagesByStatusCancelledAsync(pageNumber, pageSize, assigneeName);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Successfully retrieved cancelled request take spare part usages")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpGet("requests/{id}")]
    public async Task<IResult> GetRequestTakeSparePartUsageById(Guid id)
    {
        var result = await _sparePartUsageService.GetRequestTakeSparePartUsageByIdAsync(id);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, $"Successfully retrieved request take spare part usage with ID {id}")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpPut("update-taken-from-stock")]
    public async Task<IResult> UpdateIsTakenFromStock([FromBody] UpdateIsTakenFromStockRequest request)
    {
        var result = await _sparePartUsageService.UpdateIsTakenFromStockAsync(request);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Spare part usage updated successfully")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpDelete("{id}")]
    public async Task<IResult> Delete(Guid id)
    {
        var result = await _sparePartUsageService.DeleteAsync(id);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, "Spare part usage deleted successfully")
            : ResultExtensions.ToProblemDetails(result);
    }

    [HttpPut("update-status")]
    public async Task<IResult> UpdateRequestTakeSparePartUsageStatus([FromBody] UpdateRequestStatusRequest request)
    {
        var result = await _sparePartUsageService.UpdateRequestTakeSparePartUsageStatusAsync(request);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, $"Successfully updated status of request take spare part usage with ID {request.RequestTakeSparePartUsageId}")
            : ResultExtensions.ToProblemDetails(result);
    }
    [HttpPut("update-insufficient-status")]
    public async Task<IResult> UpdateRequestTakeSparePartUsageInsufficientStatus([FromBody] UpdateRequestInsufficientStatusRequest request)
    {
        var result = await _sparePartUsageService.UpdateRequestTakeSparePartUsageInsufficientStatusAsync(request);
        return result.IsSuccess
            ? ResultExtensions.ToSuccessDetails(result, $"Successfully updated request take spare part usage with ID {request.RequestTakeSparePartUsageId} to Insufficient status")
            : ResultExtensions.ToProblemDetails(result);
    }
}