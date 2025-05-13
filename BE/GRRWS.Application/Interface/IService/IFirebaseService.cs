using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;
using Microsoft.AspNetCore.Http;

namespace GRRWS.Application.Interface.IService
{
    public interface IFirebaseService
    {
        Task<Result> UploadImageAsync(IFormFile file, string folder);
        Task<Result> GetImageAsync(GetImageRequest request);
    }
}
