using GRRWS.Application.Common.Result;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface IMachineService
    {
        
        Task<Result> ImportMachinesAsync(IFormFile file);
    }
}
