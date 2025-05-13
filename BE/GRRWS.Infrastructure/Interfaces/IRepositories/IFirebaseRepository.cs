using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Infrastructure.DTOs.Firebase.AddImage;
using GRRWS.Infrastructure.DTOs.Firebase.GetImage;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IFirebaseRepository
    {
        Task<AddImageResponse> UploadImageAsync(AddImageRequest request);
        Task<GetImageResponse> GetImageAsync(GetImageRequest request);
    }
}
