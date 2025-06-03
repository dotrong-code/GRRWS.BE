using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace GRRWS.Infrastructure.DB.Configuration
{
    public class TaskActionConfiguration : IEntityTypeConfiguration<TaskAction>
    {
        public void Configure(EntityTypeBuilder<TaskAction> builder)
        {
        }
    }
}