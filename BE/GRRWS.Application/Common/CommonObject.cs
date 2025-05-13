using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace GRRWS.Application.Common
{
    public class CommonObject
    {
        public static string Domain { get; private set; } = "https://localhost:7179"; // Default

        public static void Initialize(IConfiguration configuration)
        {
            Domain = configuration["Domain:Global"] ?? Domain;
        }
    }
}
