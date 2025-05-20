using AutoMapper;
using GRRWS.Application.Common.Result;
using GRRWS.Application.Interface.IService;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DTOs.Device.History;
using GRRWS.Infrastructure.DTOs.Device.IssueHistory;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Implement.Service
{
    public class DeviceIssueHistoryService : IDeviceIssueHistoryService
    {
        private readonly IUnitOfWork _unit;
        private readonly IMapper _mapper;
        public DeviceIssueHistoryService(IUnitOfWork unit, IMapper mapper)
        {
            _unit = unit;
            _mapper = mapper;
        }
        public async Task<Result> GetDeviceIssueHistoryByDeviceIdAsync(Guid id)
        {
            var deviceIssueHistories = await _unit.DeviceIssueHistoryRepository.GetDeviceIssueHistoryByDeviceIdAsync(id);
            var dtos = _mapper.Map<List<DeviceIssueHistoryDTO>>(deviceIssueHistories).Cast<object>().ToList();
            return Result.SuccessWithObject(dtos);
        }
    }
}
