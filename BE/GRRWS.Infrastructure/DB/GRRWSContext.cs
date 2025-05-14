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
        public DbSet<Request> Requests { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<RequestIssue> RequestIssues { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueError> IssueErrors { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ReportError> ReportErrors { get; set; }
        #endregion
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity Configurations
            #endregion

            #region Table Mappings
            modelBuilder.Entity<User>().ToTable("User");
            modelBuilder.Entity<Area>().ToTable("Area");
            modelBuilder.Entity<EmailTemplate>().ToTable("EmailTemplate");
            modelBuilder.Entity<Request>().ToTable("Request");
            modelBuilder.Entity<Report>().ToTable("Report");
            modelBuilder.Entity<RequestIssue>().ToTable("RequestIssue");
            modelBuilder.Entity<Issue>().ToTable("Issue");
            modelBuilder.Entity<IssueError>().ToTable("IssueError");
            modelBuilder.Entity<Error>().ToTable("Error");
            modelBuilder.Entity<ReportError>().ToTable("ReportError");
            #endregion


            #region Relationships and Additional Configuration
            modelBuilder.Entity<Area>()
                .HasOne(a => a.User)
                .WithMany(u => u.ManagedAreas)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Sender)
                .WithMany(u => u.RequestsSents)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Receiver)
                .WithMany(u => u.RequestsReceiveds)
                .HasForeignKey(r => r.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);

            // Request - Report (1-1)
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Report)
                .WithOne(rp => rp.Request)
                .HasForeignKey<Report>(rp => rp.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // Request - RequestIssue (1-N)
            

            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Request)
                .WithMany(r => r.RequestIssues)
                .HasForeignKey(ri => ri.RequestId);

            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Issue)
                .WithMany(i => i.RequestIssues)
                .HasForeignKey(ri => ri.IssueId);

            // Issue - IssueError (many-to-many)
            

            modelBuilder.Entity<IssueError>()
                .HasOne(ie => ie.Issue)
                .WithMany(i => i.IssueErrors)
                .HasForeignKey(ie => ie.IssueId);

            modelBuilder.Entity<IssueError>()
                .HasOne(ie => ie.Error)
                .WithMany(e => e.IssueErrors)
                .HasForeignKey(ie => ie.ErrorId);

            // Report - ReportError (many-to-many)
            

            modelBuilder.Entity<ReportError>()
                .HasOne(re => re.Report)
                .WithMany(r => r.ReportErrors)
                .HasForeignKey(re => re.ReportId);

            modelBuilder.Entity<ReportError>()
                .HasOne(re => re.Error)
                .WithMany(e => e.ReportErrors)
                .HasForeignKey(re => re.ErrorId);

            modelBuilder.Entity<Zone>()
    .HasOne(z => z.Area)
    .WithMany(a => a.Zones)
    .HasForeignKey(z => z.AreaId);

            modelBuilder.Entity<Position>()
                .HasOne(p => p.Zone)
                .WithMany(z => z.Positions)
                .HasForeignKey(p => p.ZoneId);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Machine)
                .WithMany(m => m.Devices)
                .HasForeignKey(d => d.MachineId);

            modelBuilder.Entity<Device>()
                .HasOne(d => d.Position)
                .WithMany(p => p.Devices)
                .HasForeignKey(d => d.PositionId);

            modelBuilder.Entity<DeviceWarranty>()
                .HasOne(dw => dw.Device)
                .WithMany(d => d.Warranties)
                .HasForeignKey(dw => dw.DeviceId);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Device)
                .WithMany(d => d.Requests)
                .HasForeignKey(r => r.DeviceId);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Sender)
                .WithMany(u => u.RequestsSents)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Receiver)
                .WithMany(u => u.RequestsReceiveds)
                .HasForeignKey(r => r.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);



            #endregion
        }
    }
}
