using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class TechnicalSymptomReportConfiguration : IEntityTypeConfiguration<TechnicalSymptomReport>
    {
        public void Configure(EntityTypeBuilder<TechnicalSymptomReport> builder)
        {
            builder.HasKey(tsr => new { tsr.ReportId, tsr.TechnicalSymptomId });
        }
    }
}
