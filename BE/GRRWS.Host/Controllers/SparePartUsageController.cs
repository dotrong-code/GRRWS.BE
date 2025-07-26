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

    
}