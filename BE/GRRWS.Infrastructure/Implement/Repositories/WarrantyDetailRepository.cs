using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class WarrantyDetailRepository : GenericRepository<WarrantyDetail>, IWarrantyDetailRepository
    {
        public WarrantyDetailRepository(GRRWSContext context) : base(context) { }

        public async Task<WarrantyDetail> GetByIdAsync(Guid id)
        {
            return await _context.WarrantyDetails
                .Include(wd => wd.Issues)
                .Include(wd => wd.Report)
                .Include(wd => wd.Task)
                .FirstOrDefaultAsync(wd => wd.Id == id);
        }

        public async Task<IEnumerable<WarrantyDetail>> GetAllAsync()
        {
            return await _context.WarrantyDetails
                .Include(wd => wd.Issues)
                .Include(wd => wd.Report)
                .Include(wd => wd.Task)
                .ToListAsync();
        }

        public async Task CreateAsync(WarrantyDetail warrantyDetail)
        {
            await _context.WarrantyDetails.AddAsync(warrantyDetail);
        }

        public async Task UpdateAsync(WarrantyDetail warrantyDetail)
        {
            _context.WarrantyDetails.Update(warrantyDetail);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var warrantyDetail = await _context.WarrantyDetails.FindAsync(id);
            if (warrantyDetail != null)
            {
                _context.WarrantyDetails.Remove(warrantyDetail);
            }
        }
    }
}
