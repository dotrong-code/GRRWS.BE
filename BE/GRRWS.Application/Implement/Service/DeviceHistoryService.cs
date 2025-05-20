using AutoMapper;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Device.History;
using GRRWS.Infrastructure.DTOs.Report;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class DeviceHistoryService : IDeviceHistoryService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public DeviceHistoryService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<Result> GetDeviceHistoryByDeviceIdAsync(Guid id)
        {
            var deviceHistories = await _unit.DeviceHistoryRepository.GetDeviceHistoryByDeviceIdAsync(id);
            var dtos = _mapper.Map<List<DeviceHistoryDTO>>(deviceHistories).Cast<object>().ToList();
            return Result.SuccessWithObject(dtos);
        }
    }
}
