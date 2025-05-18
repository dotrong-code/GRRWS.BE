using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace GRRWS.Infrastructure.DB
{
    public class GRRWSContext : DbContext
    {
        public GRRWSContext() { }
        public GRRWSContext(DbContextOptions<GRRWSContext> options) : base(options) { }

        #region DbSet
        public DbSet<Area> Areas { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<DeviceHistory> DeviceHistories { get; set; }
        public DbSet<DeviceWarranty> DeviceWarranties { get; set; }
        public DbSet<DeviceWarrantyHistory> DeviceWarrantyHistories { get; set; }
        public DbSet<EmailTemplate> EmailTemplates { get; set; }
        public DbSet<Error> Errors { get; set; }
        public DbSet<ErrorDetail> ErrorDetails { get; set; }
        public DbSet<Feedback> Feedbacks { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Issue> Issues { get; set; }
        public DbSet<IssueError> IssueErrors { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<RepairSparepart> RepairSpareparts { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Request> Requests { get; set; }
        public DbSet<RequestIssue> RequestIssues { get; set; }
        public DbSet<Sparepart> Spareparts { get; set; }
        public DbSet<Tasks> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            #region Entity Configurations

            // Area
            modelBuilder.Entity<Area>()
                .Property(a => a.AreaName)
                .IsRequired();

            // Zone
            modelBuilder.Entity<Zone>()
                .Property(z => z.ZoneName)
                .IsRequired();

            // Position
            modelBuilder.Entity<Position>()
                .Property(p => p.Index)
                .IsRequired();

            // Device
            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(d => d.DeviceName).IsRequired();
                entity.Property(d => d.DeviceCode).IsRequired();
                entity.Property(d => d.Status).IsRequired();
                entity.HasIndex(d => d.DeviceCode).IsUnique();
            });

            // DeviceHistory
            modelBuilder.Entity<DeviceHistory>()
                .Property(dh => dh.EventDate)
                .IsRequired();

            // DeviceWarranty
            modelBuilder.Entity<DeviceWarranty>()
                .Property(dw => dw.DeviceId)
                .IsRequired();

            // DeviceWarrantyHistory
            modelBuilder.Entity<DeviceWarrantyHistory>(entity =>
            {
                entity.Property(dwh => dwh.DeviceId).IsRequired();
                entity.Property(dwh => dwh.DeviceDescription).IsRequired();
                entity.Property(dwh => dwh.Status).IsRequired();
            });

            // EmailTemplate
            modelBuilder.Entity<EmailTemplate>(entity =>
            {
                entity.Property(e => e.Subject).IsRequired();
                entity.Property(e => e.Body).IsRequired();
                entity.Property(e => e.Type).IsRequired();
                entity.Property(e => e.CreateBy).IsRequired();
                entity.Property(e => e.UpdateBy).IsRequired();
                entity.Property(e => e.ImageMappingsJson).IsRequired();
            });

            // Error
            modelBuilder.Entity<Error>(entity =>
            {
                entity.Property(e => e.ErrorCode).IsRequired();
                entity.HasIndex(e => e.ErrorCode).IsUnique();
            });

            // ErrorDetail
            modelBuilder.Entity<ErrorDetail>()
                .HasKey(ed => new { ed.ReportId, ed.ErrorId });

            // Feedback
            modelBuilder.Entity<Feedback>()
                .Property(f => f.UserId)
                .IsRequired();

            // Image
            modelBuilder.Entity<Image>()
                .Property(i => i.RequestIssueId)
                .IsRequired();

            // Issue
            modelBuilder.Entity<Issue>(entity =>
            {
                entity.Property(i => i.IssueKey).IsRequired();
                entity.HasIndex(i => i.IssueKey).IsUnique();
            });

            // IssueError
            modelBuilder.Entity<IssueError>()
                .HasKey(ie => new { ie.IssueId, ie.ErrorId });

            // Machine
            modelBuilder.Entity<Machine>(entity =>
            {
                entity.Property(m => m.MachineCode).IsRequired();
                entity.HasIndex(m => m.MachineCode).IsUnique();
            });

            // Notification
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(n => n.SenderId).IsRequired();
                entity.Property(n => n.ReceiverId).IsRequired();
            });

            // RepairSparepart
            modelBuilder.Entity<RepairSparepart>()
                .HasKey(rs => new { rs.SpareId, rs.TaskId });

            // Report
            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(r => r.Location).IsRequired();
                entity.Property(r => r.Status).IsRequired();
                entity.Property(r => r.Priority).IsRequired();
            });

            // Request
            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(r => r.DeviceId).IsRequired();
                entity.Property(r => r.RequestedById).IsRequired();
            });

            // RequestIssue
            modelBuilder.Entity<RequestIssue>(entity =>
            {
                entity.HasKey(ri => ri.Id);
                entity.HasIndex(ri => new { ri.RequestId, ri.IssueId }).IsUnique();
            });

            // Sparepart
            modelBuilder.Entity<Sparepart>(entity =>
            {
                entity.Property(s => s.SparepartName).IsRequired();
                entity.Property(s => s.Available).IsRequired();
            });

            // Tasks
            modelBuilder.Entity<Tasks>()
                .Property(t => t.AssigneeId)
                .IsRequired();

            // User
            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(u => u.Email).IsRequired();
                entity.Property(u => u.UserName).IsRequired();
                entity.HasIndex(u => u.Email).IsUnique();
                entity.HasIndex(u => u.UserName).IsUnique();
                entity.HasIndex(u => u.StaffID).IsUnique();
            });
            #endregion

            #region Table Mappings
            modelBuilder.Entity<Area>().ToTable("Areas");
            modelBuilder.Entity<Zone>().ToTable("Zones");
            modelBuilder.Entity<Position>().ToTable("Positions");
            modelBuilder.Entity<Device>().ToTable("Devices");
            modelBuilder.Entity<DeviceHistory>().ToTable("DeviceHistories");
            modelBuilder.Entity<DeviceWarranty>().ToTable("DeviceWarranties");
            modelBuilder.Entity<DeviceWarrantyHistory>().ToTable("DeviceWarrantyHistories");
            modelBuilder.Entity<EmailTemplate>().ToTable("EmailTemplates");
            modelBuilder.Entity<Error>().ToTable("Errors");
            modelBuilder.Entity<ErrorDetail>().ToTable("ErrorDetails");
            modelBuilder.Entity<Feedback>().ToTable("Feedbacks");
            modelBuilder.Entity<Image>().ToTable("Images");
            modelBuilder.Entity<Issue>().ToTable("Issues");
            modelBuilder.Entity<IssueError>().ToTable("IssueErrors");
            modelBuilder.Entity<Machine>().ToTable("Machines");
            modelBuilder.Entity<Notification>().ToTable("Notifications");
            modelBuilder.Entity<RepairSparepart>().ToTable("RepairSpareparts");
            modelBuilder.Entity<Report>().ToTable("Reports");
            modelBuilder.Entity<Request>().ToTable("Requests");
            modelBuilder.Entity<RequestIssue>().ToTable("RequestIssues");
            modelBuilder.Entity<Sparepart>().ToTable("Spareparts");
            modelBuilder.Entity<Tasks>().ToTable("Tasks");
            modelBuilder.Entity<User>().ToTable("Users");
            #endregion

            #region Relationships
            // Area - Zone (One-to-Many)
            modelBuilder.Entity<Zone>()
                .HasOne(z => z.Area)
                .WithMany(a => a.Zones)
                .HasForeignKey(z => z.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Zone - Position (One-to-Many)
            modelBuilder.Entity<Position>()
                .HasOne(p => p.Zone)
                .WithMany(z => z.Positions)
                .HasForeignKey(p => p.ZoneId)
                .OnDelete(DeleteBehavior.Restrict);

            // Position - Device (One-to-One)
            modelBuilder.Entity<Position>()
                .HasOne(p => p.Device)
                .WithOne(d => d.Position)
                .HasForeignKey<Position>(p => p.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Device - Machine (Many-to-One)
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Machine)
                .WithMany(m => m.Devices)
                .HasForeignKey(d => d.MachineId)
                .OnDelete(DeleteBehavior.Restrict);


            // Device - DeviceHistory (One-to-Many)
            modelBuilder.Entity<DeviceHistory>()
                .HasOne(dh => dh.Device)
                .WithMany(d => d.Histories)
                .HasForeignKey(dh => dh.DeviceId)
                .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict to avoid cascade path issues


            // Device - DeviceWarranty (One-to-Many)
            modelBuilder.Entity<DeviceWarranty>()
                .HasOne(dw => dw.Device)
                .WithMany(d => d.Warranties)
                .HasForeignKey(dw => dw.DeviceId)
                .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict to further avoid cascade issues

            // Device - DeviceWarrantyHistory (One-to-Many)
            modelBuilder.Entity<DeviceWarrantyHistory>()
                .HasOne(dwh => dwh.Device)
                .WithMany() // No navigation property in Device
                .HasForeignKey(dwh => dwh.DeviceId)
                .OnDelete(DeleteBehavior.Restrict); // Changed to Restrict to further avoid cascade issues

            // Device - Request (One-to-Many)

            // WarrantyTask - Device
            //modelBuilder.Entity<WarrantyTask>()
            //    .HasOne(wt => wt.Device)
            //    .WithMany()
            //    .HasForeignKey(wt => wt.DeviceId)
            //    .OnDelete(DeleteBehavior.Cascade);

            //// WarrantyTask - User (AssignedStaff)
            //modelBuilder.Entity<WarrantyTask>()
            //    .HasOne(wt => wt.AssignedStaff)
            //    .WithMany()
            //    .HasForeignKey(wt => wt.AssignedStaffId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //// WarrantyTask - DeviceWarranty
            //modelBuilder.Entity<WarrantyTask>()
            //    .HasMany(wt => wt.RelatedWarranties)
            //    .WithOne()
            //    .HasForeignKey("RelatedTaskId")
            //    .OnDelete(DeleteBehavior.NoAction);

            //// DeviceHistory - WarrantyTask
            //modelBuilder.Entity<DeviceHistory>()
            //    .HasOne(dh => dh.RelatedTask)
            //    .WithMany()
            //    .HasForeignKey(dh => dh.RelatedTaskId)
            //    .OnDelete(DeleteBehavior.NoAction);

            // Request - Device
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Device)
                .WithMany(d => d.Requests)
                .HasForeignKey(r => r.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Request (One-to-Many)
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Sender)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.RequestedById)
                .OnDelete(DeleteBehavior.Restrict);

            // Request - Report (One-to-One)
            modelBuilder.Entity<Report>()
                .HasOne(rp => rp.Request)
                .WithOne(r => r.Report)
                .HasForeignKey<Report>(rp => rp.RequestId)
                .OnDelete(DeleteBehavior.Restrict);

            // Request - RequestIssue (One-to-Many)
            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Request)
                .WithMany(r => r.RequestIssues)
                .HasForeignKey(ri => ri.RequestId)
                .OnDelete(DeleteBehavior.Cascade);

            // Issue - RequestIssue (One-to-Many)
            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Issue)
                .WithMany(i => i.RequestIssues)
                .HasForeignKey(ri => ri.IssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Issue - IssueError (Many-to-Many)
            modelBuilder.Entity<IssueError>()
                .HasOne(ie => ie.Issue)
                .WithMany(i => i.IssueErrors)
                .HasForeignKey(ie => ie.IssueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueError>()
                .HasOne(ie => ie.Error)
                .WithMany(e => e.IssueErrors)
                .HasForeignKey(ie => ie.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Error - ErrorDetail (One-to-Many)
            modelBuilder.Entity<ErrorDetail>()
                .HasOne(ed => ed.Error)
                .WithMany(e => e.ErrorDetails)
                .HasForeignKey(ed => ed.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Report - ErrorDetail (One-to-Many)
            modelBuilder.Entity<ErrorDetail>()
                .HasOne(ed => ed.Report)
                .WithMany(r => r.ErrorDetails)
                .HasForeignKey(ed => ed.ReportId)
                .OnDelete(DeleteBehavior.Cascade);

            // Tasks - ErrorDetail (One-to-Many)
            modelBuilder.Entity<ErrorDetail>()
                .HasOne(ed => ed.Task)
                .WithMany(t => t.ErrorDetails)
                .HasForeignKey(ed => ed.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Feedback (One-to-One)
            modelBuilder.Entity<Feedback>()
                .HasOne(f => f.User)
                .WithOne(u => u.Feedback)
                .HasForeignKey<Feedback>(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Tasks (One-to-Many)
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Assignee)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Tasks - RepairSparepart (Many-to-Many)
            modelBuilder.Entity<RepairSparepart>()
                .HasOne(rs => rs.Task)
                .WithMany(t => t.RepairSpareparts)
                .HasForeignKey(rs => rs.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RepairSparepart>()
                .HasOne(rs => rs.Sparepart)
                .WithMany(s => s.RepairSpareparts)
                .HasForeignKey(rs => rs.SpareId)
                .OnDelete(DeleteBehavior.Restrict);

            // Image - RequestIssue (Many-to-One)
            modelBuilder.Entity<Image>()
                .HasOne(i => i.RequestIssue)
                .WithMany() // No navigation property in RequestIssue
                .HasForeignKey(i => i.RequestIssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // BaseEntity Defaults
            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.Id)
                .HasDefaultValueSql("NEWID()");

            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.CreatedDate)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.Entity<BaseEntity>()
                .Property(b => b.IsDeleted)
                .HasDefaultValue(false);

            #endregion
        }
    }
}