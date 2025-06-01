using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.Interfaces.IRepositories
{
    public interface IIssueTechnicalSymptomRepository : IGenericRepository<IssueTechnicalSymptom>
    {
        Task CreateRangeAsync(IEnumerable<IssueTechnicalSymptom> issueTechnicalSymptoms);
        Task<List<IssueTechnicalSymptom>> GetByIssueIdAsync(Guid issueId);
        Task<List<IssueTechnicalSymptom>> GetBySymptomIdAsync(Guid symptomId);
        Task DeleteRangeAsync(IEnumerable<IssueTechnicalSymptom> issueTechnicalSymptoms);
    }
}
