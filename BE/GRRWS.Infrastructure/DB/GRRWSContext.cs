using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.DB
{
    public class GRRWSContext : DbContext
    {
        public GRRWSContext() { }
        public GRRWSContext(DbContextOptions<GRRWSContext> options) : base(options) { }

        #region DBSet
        public DbSet<User> Users { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity Configurations
            #endregion

            #region Table Mappings
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Area>().ToTable("Area");
            modelBuilder.Entity<EmailTemplate>().ToTable("EmailTemplate");
            #endregion


            #region Relationships and Additional Configuration
            modelBuilder.Entity<Area>()
                .HasOne(a => a.User)
                .WithMany(u => u.ManagedAreas)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            #endregion
        }
    }
}
