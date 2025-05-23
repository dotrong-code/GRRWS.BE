﻿using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IRequestRepository : IGenericRepository<Request>
    {
        Task<Request> GetRequestByIdAsync(Guid id);
        Task<List<Request>> GetAllRequestAsync();
        Task UpdateRequestAsync(Request request, List<Guid> newIssueIds);
    }
}
