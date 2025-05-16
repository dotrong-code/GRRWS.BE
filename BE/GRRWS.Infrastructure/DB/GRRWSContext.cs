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
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceWarranty> DeviceWarranties { get; set; }
        public DbSet<DeviceHistory> DeviceHistories { get; set; }
        public DbSet<WarrantyTask> WarrantyTasks { get; set; }
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
            modelBuilder.Entity<Zone>().ToTable("Zone");
            modelBuilder.Entity<Position>().ToTable("Position");
            modelBuilder.Entity<Machine>().ToTable("Machine");
            modelBuilder.Entity<Device>().ToTable("Device");
            modelBuilder.Entity<DeviceWarranty>().ToTable("DeviceWarranty");
            modelBuilder.Entity<DeviceHistory>().ToTable("DeviceHistory");
            modelBuilder.Entity<WarrantyTask>().ToTable("WarrantyTask");
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
            // Area - User
            modelBuilder.Entity<Area>()
                .HasOne(a => a.User)
                .WithMany(u => u.ManagedAreas)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.NoAction);

            // Zone - Area
            modelBuilder.Entity<Zone>()
                .HasOne(z => z.Area)
                .WithMany(a => a.Zones)
                .HasForeignKey(z => z.AreaId)
                .OnDelete(DeleteBehavior.Cascade);

            // Position - Zone
            modelBuilder.Entity<Position>()
                .HasOne(p => p.Zone)
                .WithMany(z => z.Positions)
                .HasForeignKey(p => p.ZoneId)
                .OnDelete(DeleteBehavior.NoAction); // Changed to avoid cascade conflicts

            // Device - Machine
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Machine)
                .WithMany(m => m.Devices)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.NoAction);

            // Device - Position
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Position)
                .WithMany(p => p.Devices)
                .HasForeignKey(d => d.PositionId)
                .OnDelete(DeleteBehavior.NoAction);

            

            // DeviceWarranty - Device
            modelBuilder.Entity<DeviceWarranty>()
                .HasOne(dw => dw.Device)
                .WithMany(d => d.Warranties)
                .HasForeignKey(dw => dw.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            // DeviceHistory - Device
            modelBuilder.Entity<DeviceHistory>()
                .HasOne(dh => dh.Device)
                .WithMany(d => d.Histories)
                .HasForeignKey(dh => dh.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            // WarrantyTask - Device
            modelBuilder.Entity<WarrantyTask>()
                .HasOne(wt => wt.Device)
                .WithMany()
                .HasForeignKey(wt => wt.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            // WarrantyTask - User (AssignedStaff)
            modelBuilder.Entity<WarrantyTask>()
                .HasOne(wt => wt.AssignedStaff)
                .WithMany()
                .HasForeignKey(wt => wt.AssignedStaffId)
                .OnDelete(DeleteBehavior.NoAction);

            // WarrantyTask - DeviceWarranty
            modelBuilder.Entity<WarrantyTask>()
                .HasMany(wt => wt.RelatedWarranties)
                .WithOne()
                .HasForeignKey("RelatedTaskId")
                .OnDelete(DeleteBehavior.NoAction);

            // DeviceHistory - WarrantyTask
            modelBuilder.Entity<DeviceHistory>()
                .HasOne(dh => dh.RelatedTask)
                .WithMany()
                .HasForeignKey(dh => dh.RelatedTaskId)
                .OnDelete(DeleteBehavior.NoAction);

            // Request - Device
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Device)
                .WithMany(d => d.Requests)
                .HasForeignKey(r => r.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Request - Sender
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Sender)
                .WithMany(u => u.RequestsSents)
                .HasForeignKey(r => r.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Request - Receiver
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

            // RequestIssue - Request
            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Request)
                .WithMany(r => r.RequestIssues)
                .HasForeignKey(ri => ri.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // RequestIssue - Issue
            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Issue)
                .WithMany(i => i.RequestIssues)
                .HasForeignKey(ri => ri.IssueId)
                .OnDelete(DeleteBehavior.Cascade);

            // Issue - Machine
            modelBuilder.Entity<Issue>()
                .HasOne(i => i.Machine)
                .WithMany()
                .HasForeignKey(i => i.MachineId)
                .OnDelete(DeleteBehavior.NoAction);

            // Error - Machine
            modelBuilder.Entity<Error>()
                .HasOne(e => e.Machine)
                .WithMany()
                .HasForeignKey(e => e.MachineId)
                .OnDelete(DeleteBehavior.NoAction);

            // IssueError - Issue
            modelBuilder.Entity<IssueError>()
                .HasOne(ie => ie.Issue)
                .WithMany(i => i.IssueErrors)
                .HasForeignKey(ie => ie.IssueId)
                .OnDelete(DeleteBehavior.Cascade);

            // IssueError - Error
            modelBuilder.Entity<IssueError>()
                .HasOne(ie => ie.Error)
                .WithMany(e => e.IssueErrors)
                .HasForeignKey(ie => ie.ErrorId)
                .OnDelete(DeleteBehavior.Cascade);

            // ReportError - Report
            modelBuilder.Entity<ReportError>()
                .HasOne(re => re.Report)
                .WithMany(r => r.ReportErrors)
                .HasForeignKey(re => re.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // ReportError - Error
            modelBuilder.Entity<ReportError>()
                .HasOne(re => re.Error)
                .WithMany(e => e.ReportErrors)
                .HasForeignKey(re => re.ErrorId)
                .OnDelete(DeleteBehavior.Cascade);
            #endregion
        }
    }
}