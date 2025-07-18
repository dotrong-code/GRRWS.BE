﻿using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IMachineRepository : IGenericRepository<Machine>
    {
        Task<(List<Machine> Items, int TotalCount)> GetAllActiveMachinesAsync(int pageNumber, int pageSize);
    }
}

