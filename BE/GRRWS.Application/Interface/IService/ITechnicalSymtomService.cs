using GRRWS.Application.Common;
using GRRWS.Application.Common.Result;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.DTOs.RequestDTO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Application.Interface.IService
{
    public interface ITechnicalSymtomService
    {
        Task<Result> GetSymtomSuggestionsAsync(string query, int maxResults);
        Task<Result> GetRecommendedSymtomsAsync(IssueIdsRequestDTO dto);
        Task<Result> ImportTechnicalSymtomsAsync(IFormFile file);
    }
}
