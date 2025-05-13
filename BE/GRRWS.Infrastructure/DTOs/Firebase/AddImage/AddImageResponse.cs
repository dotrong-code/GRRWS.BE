using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Infrastructure.DTOs.Common;

namespace GRRWS.Infrastructure.DTOs.Firebase.AddImage
{
    public class AddImageResponse
    {
        public string FilePath { get; set; }
        public bool Success { get; set; }
        public Error Error { get; set; }

        public AddImageResponse(string filePath, bool success, Error error)
        {
            FilePath = filePath;
            Success = success;
            Error = error;
        }
    }
}
