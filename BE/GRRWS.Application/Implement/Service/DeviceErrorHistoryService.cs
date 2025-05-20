using AutoMapper;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Device.ErrorHistory;
using GRRWS.Infrastructure.DTOs.Device.History;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class DeviceErrorHistoryService : IDeviceErrorHistoryService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public DeviceErrorHistoryService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<Result> GetDeviceErrorHistoryByDeviceIdAsync(Guid id)
        {
            var deviceErrorHistories = await _unit.DeviceErrorHistoryRepository.GetDeviceErrorHistoryByDeviceIdAsync(id);
            var dtos = _mapper.Map<List<DeviceErrorHistoryDTO>>(deviceErrorHistories).Cast<object>().ToList();
            return Result.SuccessWithObject(dtos);
        }
    }
}
