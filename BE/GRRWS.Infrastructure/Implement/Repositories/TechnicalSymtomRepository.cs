using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.DTOs.Common;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class TechnicalSymtomRepository : GenericRepository<TechnicalSymptom>, ITechnicalSymtomRepository
    {
        public TechnicalSymtomRepository(GRRWSContext context) : base(context)
        {
        }
        public async Task<List<SuggestObject>> GetNotFoundTechnicalSymtomDisplayNamesAsync(IEnumerable<Guid> technicalSymtomIds)
        {
            var idSet = technicalSymtomIds.ToHashSet();

            var existingTechnicalSymtoms = await _context.TechnicalSymptoms
                .AsNoTracking()
                .Where(i => idSet.Contains(i.Id) && !i.IsDeleted)
                .Select(i => new { i.Id, i.Name })
                .ToListAsync();

            var foundIds = existingTechnicalSymtoms.Select(i => i.Id).ToHashSet();

            var notFoundIds = idSet.Except(foundIds);

            return notFoundIds
                .Select(id => new SuggestObject
                {
                    Id = id,
                    Name = "(Not found)"
                })
                .ToList();
        } 
    }
}
