﻿using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IRequestService
    {
        Task<Result> GetAllAsync();
        Task<Result> GetByIdAsync(Guid id);
        Task<Result> CreateAsync(CreateRequestDTO dto);
        Task<Result> UpdateAsync(UpdateRequestDTO dto, Guid id);
        Task<Result> DeleteAsync(Guid id);
    }
}
