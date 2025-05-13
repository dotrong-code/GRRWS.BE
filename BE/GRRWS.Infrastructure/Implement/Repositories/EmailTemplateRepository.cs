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
    public class EmailTemplateRepository : GenericRepository<EmailTemplate>, IEmailTemplateRepository
    {
        public EmailTemplateRepository(GRRWSContext context) : base(context) { }
        public async Task<EmailTemplate> GetEmailTemplateByTypeAsync(string type)
        {
            return await _context.EmailTemplates.FirstOrDefaultAsync(et => et.Type == type);
        }


        public async Task AddEmailTemplateAsync(EmailTemplate emailTemplate)
        {
            await _context.EmailTemplates.AddAsync(emailTemplate);
            await _context.SaveChangesAsync();
        }
    }
}
