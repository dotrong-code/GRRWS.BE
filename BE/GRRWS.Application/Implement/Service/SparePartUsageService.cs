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

       
    }
}