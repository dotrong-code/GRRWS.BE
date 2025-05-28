using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.Implement.Repositories
{
    public class WarrantyDetailRepository : IWarrantyDetailRepository
    {
        private readonly DbContext _context;
        private readonly DbSet<WarrantyDetail> _warrantyDetails;

        public WarrantyDetailRepository(DbContext context)
        {
            _context = context;
            _warrantyDetails = context.Set<WarrantyDetail>();
        }

        public async Task<WarrantyDetail> GetByIdAsync(Guid id)
        {
            return await _warrantyDetails
                .Include(wd => wd.Issues)
                .Include(wd => wd.Report)
                .Include(wd => wd.Task)
                .FirstOrDefaultAsync(wd => wd.Id == id);
        }

        public async Task<IEnumerable<WarrantyDetail>> GetAllAsync()
        {
            return await _warrantyDetails
                .Include(wd => wd.Issues)
                .Include(wd => wd.Report)
                .Include(wd => wd.Task)
                .ToListAsync();
        }

        public async Task CreateAsync(WarrantyDetail warrantyDetail)
        {
            await _warrantyDetails.AddAsync(warrantyDetail);
        }

        public async Task UpdateAsync(WarrantyDetail warrantyDetail)
        {
            _warrantyDetails.Update(warrantyDetail);
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(Guid id)
        {
            var warrantyDetail = await _warrantyDetails.FindAsync(id);
            if (warrantyDetail != null)
            {
                _warrantyDetails.Remove(warrantyDetail);
            }
        }
    }
}
