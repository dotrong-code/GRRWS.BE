using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorActionConfiguration : IEntityTypeConfiguration<ErrorAction>
    {
        public void Configure(EntityTypeBuilder<ErrorAction> builder)
        {
        }
    }
}
