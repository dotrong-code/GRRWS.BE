using FluentValidation;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IImportService
    {
        Task<Result> ImportAsync<TEntity>(Stream excelStream, IGenericRepository<TEntity> repository) where TEntity : class, new();
    }
}
