using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.DB.Configuration;
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

        public DbSet<ErrorSparepart> ErrorSpareparts { get; set; }
        public DbSet<TechnicalSymptom> TechnicalSymptoms { get; set; }
        public DbSet<IssueTechnicalSymptom> IssueTechnicalSymptoms { get; set; }
        public DbSet<TechnicalSymptomReport> TechnicalSymptomReports { get; set; }

        public DbSet<ErrorAction> ErrorActions { get; set; }
        public DbSet<TaskAction> TaskActions { get; set; }



        public DbSet<MachineIssueHistory> MachineIssueHistories { get; set; }
        public DbSet<MachineErrorHistory> MachineErrorHistories { get; set; }
        public DbSet<DeviceIssueHistory> DeviceIssueHistories { get; set; }
        public DbSet<DeviceErrorHistory> DeviceErrorHistories { get; set; }
        public DbSet<WarrantyDetail> WarrantyDetails { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region Data Configuration
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new IssueConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorConfiguration());
            modelBuilder.ApplyConfiguration(new SparepartConfiguration());

            //modelBuilder.ApplyConfiguration(new SparepartConfiguration());

            modelBuilder.ApplyConfiguration(new IssueErrorConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorSparepartConfiguration());
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new ZoneConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceErrorHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceIssueHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceWarrantyConfiguration());
            modelBuilder.ApplyConfiguration(new MachineErrorHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new MachineIssueHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new TasksConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new RequestIssueConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorDetailConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceWarrantyHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new TechnicalSymptomConfiguration());
            modelBuilder.ApplyConfiguration(new IssueTechnicalSymptomConfiguration());
            modelBuilder.ApplyConfiguration(new TechnicalSymptomReportConfiguration());

            modelBuilder.ApplyConfiguration(new ErrorActionConfiguration());
            modelBuilder.ApplyConfiguration(new TaskActionConfiguration());


            #endregion


            #region Entity Configurations

            // Area
            modelBuilder.Entity<Area>()
                .Property(a => a.AreaName)
                .IsRequired();

            modelBuilder.Entity<User>()
               .HasOne(u => u.Area)
               .WithMany(a => a.Users)
               .HasForeignKey(u => u.AreaId)
               .OnDelete(DeleteBehavior.Restrict);

            // Zone
            modelBuilder.Entity<Zone>()
                .Property(z => z.ZoneName)
                .IsRequired();

            modelBuilder.Entity<Zone>()
                .HasOne(z => z.Area)
                .WithMany(a => a.Zones)
                .HasForeignKey(z => z.AreaId)
                .OnDelete(DeleteBehavior.Restrict);

            // Position
            modelBuilder.Entity<Position>()
                .Property(p => p.Index)
                .IsRequired();

            // Device
            modelBuilder.Entity<Device>(entity =>
            {
                entity.Property(d => d.DeviceName).IsRequired();
                entity.Property(d => d.DeviceCode).IsRequired();
                entity.Property(d => d.Status)
                    .IsRequired()
                    .HasConversion<string>();
                entity.HasIndex(d => d.DeviceCode).IsUnique();
            });

            // Device - Position (One-to-One)
            modelBuilder.Entity<Device>()
                .HasOne(d => d.Position)
                .WithOne(p => p.Device)
                .HasForeignKey<Position>(p => p.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            // User - Feedback (One-to-One)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Feedback)
                .WithOne(f => f.User)
                .HasForeignKey<Feedback>(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Request - Report (One-to-One)
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Report)
                .WithOne(rep => rep.Request)
                .HasForeignKey<Report>(rep => rep.RequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .Property(r => r.Status)
                .HasConversion<string>();

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(r => r.DeviceId).IsRequired();
                entity.Property(r => r.RequestedById).IsRequired();
                entity.Property(r => r.Status)
                    .HasConversion<string>();
                entity.Property(r => r.Priority)
                    .HasConversion<string>();
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
                entity.Property(dwh => dwh.Status)
                    .IsRequired()
                    .HasConversion<string>();
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
            // MachineIssueHistory
            modelBuilder.Entity<MachineIssueHistory>(entity =>
            {
                entity.Property(mih => mih.MachineId).IsRequired();
                entity.Property(mih => mih.IssueId).IsRequired();
                entity.Property(mih => mih.LastOccurredDate).IsRequired();
                entity.Property(mih => mih.OccurrenceCount).IsRequired().HasDefaultValue(1);
                entity.HasIndex(mih => new { mih.MachineId, mih.IssueId });
            });

            // MachineErrorHistory
            modelBuilder.Entity<MachineErrorHistory>(entity =>
            {
                entity.Property(meh => meh.MachineId).IsRequired();
                entity.Property(meh => meh.ErrorId).IsRequired();
                entity.Property(meh => meh.LastOccurredDate).IsRequired();
                entity.Property(meh => meh.OccurrenceCount).IsRequired().HasDefaultValue(1);
                entity.HasIndex(meh => new { meh.MachineId, meh.ErrorId });
            });

            // DeviceIssueHistory
            modelBuilder.Entity<DeviceIssueHistory>(entity =>
            {
                entity.Property(dih => dih.DeviceId).IsRequired();
                entity.Property(dih => dih.IssueId).IsRequired();
                entity.Property(dih => dih.LastOccurredDate).IsRequired();
                entity.Property(dih => dih.OccurrenceCount).IsRequired().HasDefaultValue(1);
                entity.HasIndex(dih => new { dih.DeviceId, dih.IssueId });
            });

            // DeviceErrorHistory
            modelBuilder.Entity<DeviceErrorHistory>(entity =>
            {
                entity.Property(deh => deh.DeviceId).IsRequired();
                entity.Property(deh => deh.ErrorId).IsRequired();
                entity.Property(deh => deh.LastOccurredDate).IsRequired();
                entity.Property(deh => deh.OccurrenceCount).IsRequired().HasDefaultValue(1);
                entity.HasIndex(deh => new { deh.DeviceId, deh.ErrorId });
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

            // ErrorSparepart
            modelBuilder.Entity<ErrorSparepart>()
                .HasKey(es => new { es.ErrorId, es.SparepartId }); // Composite Key

            // Report
            modelBuilder.Entity<Report>(entity =>
            {
                entity.Property(r => r.Location).IsRequired();
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

                entity.Property(s => s.StockQuantity).IsRequired().HasDefaultValue(0);
                entity.HasIndex(s => s.SparepartCode).IsUnique();
                entity.Property(s => s.ExpectedAvailabilityDate); // Cấu hình trường mới

            });


            // Tasks
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

            // Machine - MachineIssueHistory (One-to-Many)
            modelBuilder.Entity<MachineIssueHistory>()
                .HasOne(mih => mih.Machine)
                .WithMany()
                .HasForeignKey(mih => mih.MachineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MachineIssueHistory>()
                .HasOne(mih => mih.Issue)
                .WithMany()
                .HasForeignKey(mih => mih.IssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Machine - MachineErrorHistory (One-to-Many)
            modelBuilder.Entity<MachineErrorHistory>()
                .HasOne(meh => meh.Machine)
                .WithMany()
                .HasForeignKey(meh => meh.MachineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MachineErrorHistory>()
                .HasOne(meh => meh.Error)
                .WithMany()
                .HasForeignKey(meh => meh.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Device - DeviceIssueHistory (One-to-Many)
            modelBuilder.Entity<DeviceIssueHistory>()
                .HasOne(dih => dih.Device)
                .WithMany()
                .HasForeignKey(dih => dih.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeviceIssueHistory>()
                .HasOne(dih => dih.Issue)
                .WithMany()
                .HasForeignKey(dih => dih.IssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Device - DeviceErrorHistory (One-to-Many)
            modelBuilder.Entity<DeviceErrorHistory>()
                .HasOne(deh => deh.Device)
                .WithMany()
                .HasForeignKey(deh => deh.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeviceErrorHistory>()
                .HasOne(deh => deh.Error)
                .WithMany()
                .HasForeignKey(deh => deh.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Error - ErrorSparepart (One-to-Many)
            modelBuilder.Entity<ErrorSparepart>()
                .HasOne(es => es.Error)
                .WithMany(e => e.ErrorSpareparts)
                .HasForeignKey(es => es.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sparepart - ErrorSparepart (One-to-Many)
            modelBuilder.Entity<ErrorSparepart>()
                .HasOne(es => es.Sparepart)
                .WithMany(s => s.ErrorSpareparts)
                .HasForeignKey(es => es.SparepartId)
                .OnDelete(DeleteBehavior.Restrict);

            // Issue - IssueError (One-to-Many)
            modelBuilder.Entity<IssueError>()
                .HasOne(ie => ie.Issue)
                .WithMany(i => i.IssueErrors)
                .HasForeignKey(ie => ie.IssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Error - IssueError (One-to-Many)
            modelBuilder.Entity<IssueError>()
                .HasOne(ie => ie.Error)
                .WithMany(e => e.IssueErrors)
                .HasForeignKey(ie => ie.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeviceHistory>()
                .HasOne(dh => dh.Device)
                .WithMany(d => d.Histories)
                .HasForeignKey(dh => dh.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DeviceWarranty>()
                .HasOne(dw => dw.Device)
                .WithMany(d => d.Warranties)
                .HasForeignKey(dw => dw.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);
            // Add this relationship configuration
            modelBuilder.Entity<DeviceWarrantyHistory>()
                .HasOne(dwh => dwh.Device)
                .WithMany()  // No navigation property in Device for DeviceWarrantyHistories
                .HasForeignKey(dwh => dwh.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            // Add this relationship configuration
            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Request)
                .WithMany(r => r.RequestIssues)
                .HasForeignKey(ri => ri.RequestId)
                .OnDelete(DeleteBehavior.Restrict);

            // And fix the typo in the SerderId relationship
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Sender)
                .WithMany(u => u.Requests)
                .HasForeignKey(r => r.SerderId)  // This should be SenderId in your entity
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Request>()
                .HasOne(r => r.Device)
                .WithMany(d => d.Requests)
                .HasForeignKey(r => r.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Issue)
                .WithMany(i => i.RequestIssues)
                .HasForeignKey(ri => ri.IssueId)
                .OnDelete(DeleteBehavior.Restrict);
            // Update your existing Image-RequestIssue relationship to use the collection
            modelBuilder.Entity<Image>()
                .HasOne(i => i.RequestIssue)
                .WithMany(ri => ri.Images)
                .HasForeignKey(i => i.RequestIssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Add these relationship configurations to your OnModelCreating method
            modelBuilder.Entity<ErrorDetail>()
                .HasOne(ed => ed.Report)
                .WithMany(r => r.ErrorDetails)
                .HasForeignKey(ed => ed.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ErrorDetail>()
                .HasOne(ed => ed.Error)
                .WithMany(e => e.ErrorDetails)
                .HasForeignKey(ed => ed.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            // If you have a Task relationship in ErrorDetail
            modelBuilder.Entity<ErrorDetail>()
                .HasOne(ed => ed.Task)
                .WithMany(t => t.ErrorDetails)
                .HasForeignKey(ed => ed.TaskId)
                .OnDelete(DeleteBehavior.Restrict);
            // Cấu hình cho WarrantyDetail
            modelBuilder.Entity<WarrantyDetail>(entity =>
            {
                entity.Property(wd => wd.ReportId).IsRequired();
                entity.Property(wd => wd.WarrantyNotes).HasMaxLength(1000);
            });

            // WarrantyDetail - Report (One-to-Many)
            modelBuilder.Entity<WarrantyDetail>()
                .HasOne(wd => wd.Report)
                .WithMany(r => r.WarrantyDetails)
                .HasForeignKey(wd => wd.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            // WarrantyDetail - Tasks (One-to-One)
            modelBuilder.Entity<WarrantyDetail>()
                .HasOne(wd => wd.Task)
                .WithMany() // Không cần navigation property trong Tasks
                .HasForeignKey(wd => wd.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            // WarrantyDetail - Issue (One-to-Many)
            modelBuilder.Entity<Issue>()
                .HasOne(i => i.WarrantyDetail)
                .WithMany(wd => wd.Issues)
                .HasForeignKey(i => i.WarrantyDetailId)
                .OnDelete(DeleteBehavior.Restrict);

            // IssueTechnicalSymptom
            modelBuilder.Entity<IssueTechnicalSymptom>()
                .HasOne(its => its.Issue)
                .WithMany(i => i.IssueTechnicalSymptoms)
                .HasForeignKey(its => its.IssueId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<IssueTechnicalSymptom>()
                .HasOne(its => its.TechnicalSymptom)
                .WithMany(ts => ts.IssueTechnicalSymptoms)
                .HasForeignKey(its => its.TechnicalSymptomId)
                .OnDelete(DeleteBehavior.Restrict);

            // TechnicalSymptomReport 

            modelBuilder.Entity<TechnicalSymptomReport>()
                .HasOne(tsr => tsr.Report)
                .WithMany(r => r.TechnicalSymptomReports)
                .HasForeignKey(tsr => tsr.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TechnicalSymptomReport>()
                .HasOne(tsr => tsr.TechnicalSymptom)
                .WithMany(ts => ts.TechnicalSymptomReports)
                .HasForeignKey(tsr => tsr.TechnicalSymptomId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TechnicalSymptomReport>()
                .HasOne(tsr => tsr.Task)
                .WithMany(t => t.TechnicalSymptomReports)
                .HasForeignKey(tsr => tsr.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            //ErrorAction
            modelBuilder.Entity<ErrorAction>()
                .HasOne(ea => ea.Error)
                .WithMany(e => e.ErrorActions)
                .HasForeignKey(ea => ea.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            //TaskAction
            modelBuilder.Entity<TaskAction>()
                .HasOne(ta => ta.Task)
                .WithMany(t => t.TaskActions)
                .HasForeignKey(ta => ta.TaskId)
                .OnDelete(DeleteBehavior.Restrict);

            //TaskAction → ErrorAction (Many-to-One):
            modelBuilder.Entity<TaskAction>()
                .HasOne(ta => ta.ErrorAction)
                .WithMany(ea => ea.TaskActions)
                .HasForeignKey(ta => ta.ErrorActionId)
                .OnDelete(DeleteBehavior.Restrict);

            //TaskAction → User(Many - to - One):
            modelBuilder.Entity<TaskAction>()
                .HasOne(ta => ta.PerformedBy)
                .WithMany(u => u.TaskActions)
                .HasForeignKey(ta => ta.PerformedById)
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
            modelBuilder.Entity<WarrantyDetail>().ToTable("WarrantyDetails");
            modelBuilder.Entity<TechnicalSymptom>().ToTable("TechnicalSymptoms");
            modelBuilder.Entity<IssueTechnicalSymptom>().ToTable("IssueTechnicalSymptoms");
            modelBuilder.Entity<TechnicalSymptomReport>().ToTable("TechnicalSymptomReports");

            modelBuilder.Entity<ErrorAction>().ToTable("ErrorActions");
            modelBuilder.Entity<TaskAction>().ToTable("TaskActions");

            #endregion
        }
    }
}