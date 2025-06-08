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
    public class ErrorFixStepRepository : GenericRepository<ErrorFixStep>, IErrorFixStepRepository
    {
        public ErrorFixStepRepository(GRRWSContext context) : base(context) { }
        public async Task<List<ErrorFixStep>> GetByErrorGuidelineIdAsync(Guid errorGuidelineId)
        {
            return await _context.ErrorFixSteps
                .Where(efs => efs.ErrorGuidelineId == errorGuidelineId)
                .ToListAsync();
        }
    }
}
