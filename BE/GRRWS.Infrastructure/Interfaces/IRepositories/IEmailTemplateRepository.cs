using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IEmailTemplateRepository : IGenericRepository<EmailTemplate>
    {
        public Task<EmailTemplate> GetEmailTemplateByTypeAsync(string type);
        public Task AddEmailTemplateAsync(EmailTemplate emailTemplate);
    }
}
