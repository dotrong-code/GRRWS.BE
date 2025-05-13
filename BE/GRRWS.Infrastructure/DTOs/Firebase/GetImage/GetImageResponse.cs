using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Infrastructure.DTOs.Common;

namespace GRRWS.Infrastructure.DTOs.Firebase.GetImage
{
    public class GetImageResponse
    {
        public string ImageUrl { get; set; }  // The image in memory
        public string ContentType { get; set; }        // Optional: MIME type of the image (e.g., image/jpeg)
        public bool Success { get; set; }              // Indicates whether the operation was successful
        public Error Error { get; set; }
    }
}
