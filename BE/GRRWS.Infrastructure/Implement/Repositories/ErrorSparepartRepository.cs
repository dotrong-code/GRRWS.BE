using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
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
    public class ErrorSparepartRepository : GenericRepository<ErrorSparepart>, IErrorSparepartRepository
    {
        public ErrorSparepartRepository(GRRWSContext context) : base(context) { }
        public async Task<List<ErrorSparepart>> GetByErrorIdsAsync(List<Guid> errorIds)
        {
            return await _context.ErrorSpareparts
                .Where(sp => errorIds.Contains(sp.ErrorId))
                .Include(sp => sp.Sparepart)
                .ToListAsync();
        }

    }
}
