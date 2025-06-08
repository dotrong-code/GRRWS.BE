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
        public DbSet<MachineIssueHistory> MachineIssueHistories { get; set; }
        public DbSet<MachineErrorHistory> MachineErrorHistories { get; set; }
        public DbSet<DeviceIssueHistory> DeviceIssueHistories { get; set; }
        public DbSet<DeviceErrorHistory> DeviceErrorHistories { get; set; }
        public DbSet<WarrantyDetail> WarrantyDetails { get; set; }
        public DbSet<ErrorGuideline> ErrorGuidelines { get; set; }
        public DbSet<ErrorFixStep> ErrorFixSteps { get; set; }
        public DbSet<ErrorFixProgress> ErrorFixProgresses { get; set; }
        public DbSet<SparePartUsage> SparePartUsages { get; set; }
        public DbSet<RequestTakeSparePartUsage> RequestTakeSparePartUsages { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<MachineSparepart> MachineSpareparts { get; set; }
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
            modelBuilder.ApplyConfiguration(new IssueErrorConfiguration());
            modelBuilder.ApplyConfiguration(new MachineConfiguration());
            modelBuilder.ApplyConfiguration(new RequestConfiguration());
            modelBuilder.ApplyConfiguration(new ZoneConfiguration());
            modelBuilder.ApplyConfiguration(new AreaConfiguration());
            modelBuilder.ApplyConfiguration(new PositionConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceWarrantyConfiguration());
            modelBuilder.ApplyConfiguration(new MachineErrorHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new MachineIssueHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new TasksConfiguration());
            modelBuilder.ApplyConfiguration(new ReportConfiguration());
            modelBuilder.ApplyConfiguration(new RequestIssueConfiguration());
            modelBuilder.ApplyConfiguration(new RequestTakeSparePartUsageConfiguration());
            modelBuilder.ApplyConfiguration(new SparePartUsageConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorDetailConfiguration());
            modelBuilder.ApplyConfiguration(new DeviceWarrantyHistoryConfiguration());
            modelBuilder.ApplyConfiguration(new TechnicalSymptomConfiguration());
            modelBuilder.ApplyConfiguration(new IssueTechnicalSymptomConfiguration());
            modelBuilder.ApplyConfiguration(new TechnicalSymptomReportConfiguration());

            modelBuilder.ApplyConfiguration(new ErrorGuidelineConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorFixStepConfiguration());
            modelBuilder.ApplyConfiguration(new ErrorSparepartConfiguration());
            modelBuilder.ApplyConfiguration(new SupplierConfiguration());
            modelBuilder.ApplyConfiguration(new MachineSparepartConfiguration());

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

            modelBuilder.Entity<Request>(entity =>
            {
                entity.Property(r => r.DeviceId).IsRequired();
                entity.Property(r => r.RequestedById).IsRequired();
                entity.Property(r => r.Status)
                    .HasConversion<string>();
                entity.Property(r => r.Priority)
                    .HasConversion<string>();
                entity.HasOne(r => r.Device)
                    .WithMany(d => d.Requests)
                    .HasForeignKey(r => r.DeviceId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
            // ErrorSparepart
            modelBuilder.Entity<ErrorSparepart>()
                .HasKey(es => new { es.ErrorGuidelineId, es.SparepartId }); // Định nghĩa composite key


            modelBuilder.Entity<ErrorSparepart>()
                .HasOne(es => es.ErrorGuideline)
                .WithMany(eg => eg.ErrorSpareparts)
                .HasForeignKey(es => es.ErrorGuidelineId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ErrorSparepart>()
                .HasOne(es => es.Sparepart)
                .WithMany(s => s.ErrorSpareparts)
                .HasForeignKey(es => es.SparepartId)
                .OnDelete(DeleteBehavior.Restrict);

            // Request - Device (Many-to-One)
            modelBuilder.Entity<Request>()
                .HasOne(r => r.Device)
                .WithMany(d => d.Requests)
                .HasForeignKey(r => r.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);


            // DeviceHistory
            modelBuilder.Entity<DeviceHistory>()
                .Property(dh => dh.EventDate)
                .IsRequired();

            modelBuilder.Entity<DeviceHistory>()
                .HasOne(dh => dh.Device)
                .WithMany(d => d.Histories)
                .HasForeignKey(dh => dh.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            // DeviceWarranty
            modelBuilder.Entity<DeviceWarranty>()
                .Property(dw => dw.DeviceId)
                .IsRequired();

            modelBuilder.Entity<DeviceWarranty>()
                .HasOne(dw => dw.Device)
                .WithMany(d => d.Warranties)
                .HasForeignKey(dw => dw.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

            // DeviceWarrantyHistory
            modelBuilder.Entity<DeviceWarrantyHistory>(entity =>
            {
                entity.Property(dwh => dwh.DeviceId).IsRequired();
                entity.Property(dwh => dwh.DeviceDescription).IsRequired();
                entity.Property(dwh => dwh.Status)
                    .IsRequired()
                    .HasConversion<string>();
            });

            modelBuilder.Entity<DeviceWarrantyHistory>()
                .HasOne(dwh => dwh.Device)
                .WithMany()
                .HasForeignKey(dwh => dwh.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);

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

            // ErrorGuideline
            modelBuilder.Entity<ErrorGuideline>()
                .HasKey(eg => eg.Id);

            modelBuilder.Entity<ErrorGuideline>()
                .Property(eg => eg.Title)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<ErrorGuideline>()
                .HasOne(eg => eg.Error)
                .WithMany(e => e.ErrorGuidelines)
                .HasForeignKey(eg => eg.ErrorId)
                .OnDelete(DeleteBehavior.Restrict);

            // ErrorDetail
            modelBuilder.Entity<ErrorDetail>()
                .HasKey(ed => ed.Id);

            modelBuilder.Entity<ErrorDetail>()
                .HasIndex(ed => new { ed.ReportId, ed.ErrorId })
                .IsUnique();

            //.HasKey(ed => new { ed.ReportId, ed.ErrorId });


            modelBuilder.Entity<ErrorDetail>()
                .Property(ed => ed.ErrorId)
                .IsRequired();

            modelBuilder.Entity<ErrorDetail>()
                .Property(ed => ed.ErrorGuideLineId);

            modelBuilder.Entity<ErrorDetail>()
                .Property(ed => ed.RequestTakeSparePartUsageId); // Thêm cột cho quan hệ 1-1

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

            modelBuilder.Entity<ErrorDetail>()
                .HasOne(ed => ed.ErrorGuideline)
                .WithMany(eg => eg.ErrorDetails)
                .HasForeignKey(ed => ed.ErrorGuideLineId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<ErrorDetail>()
                .HasOne(ed => ed.Task)
                .WithMany(t => t.ErrorDetails)
                .HasForeignKey(ed => ed.TaskId)
                .OnDelete(DeleteBehavior.SetNull);

            // Xóa cấu hình liên kết với SparePartUsage trực tiếp

            // RequestTakeSparePartUsage
            modelBuilder.Entity<RequestTakeSparePartUsage>()
                .HasKey(rtspu => rtspu.Id);

            modelBuilder.Entity<RequestTakeSparePartUsage>()
                .Property(rtspu => rtspu.RequestCode)
                .IsRequired();

            modelBuilder.Entity<RequestTakeSparePartUsage>()
                .Property(rtspu => rtspu.Status)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<RequestTakeSparePartUsage>()
                .HasOne(rtspu => rtspu.ErrorDetail)
                .WithOne(ed => ed.RequestTakeSparePartUsage)
                .HasForeignKey<ErrorDetail>(ed => ed.RequestTakeSparePartUsageId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<RequestTakeSparePartUsage>()
                .HasMany(rtspu => rtspu.SparePartUsages)
                .WithOne(spu => spu.RequestTakeSparePartUsage)
                .HasForeignKey(spu => spu.RequestTakeSparePartUsageId)
                .OnDelete(DeleteBehavior.Cascade);

            // SparePartUsage
            modelBuilder.Entity<SparePartUsage>()
                .HasKey(spu => spu.Id);

            modelBuilder.Entity<SparePartUsage>()
                .HasOne(spu => spu.SparePart)
                .WithMany()
                .HasForeignKey(spu => spu.SparePartId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SparePartUsage>()
                .Property(spu => spu.QuantityUsed)
                .IsRequired();

            modelBuilder.Entity<SparePartUsage>()
                .Property(spu => spu.IsTakenFromStock)
                .IsRequired();

            modelBuilder.Entity<SparePartUsage>()
                .HasOne(spu => spu.RequestTakeSparePartUsage)
                .WithMany(rtspu => rtspu.SparePartUsages)
                .HasForeignKey(spu => spu.RequestTakeSparePartUsageId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            // Xóa cấu hình liên kết với ErrorDetail trong SparePartUsage

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

            // MachineErrorHistory
            modelBuilder.Entity<MachineErrorHistory>(entity =>
            {
                entity.Property(meh => meh.MachineId).IsRequired();
                entity.Property(meh => meh.ErrorId).IsRequired();
                entity.Property(meh => meh.LastOccurredDate).IsRequired();
                entity.Property(meh => meh.OccurrenceCount).IsRequired().HasDefaultValue(1);
                entity.HasIndex(meh => new { meh.MachineId, meh.ErrorId });
            });

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

            // DeviceIssueHistory
            modelBuilder.Entity<DeviceIssueHistory>(entity =>
            {
                entity.Property(dih => dih.DeviceId).IsRequired();
                entity.Property(dih => dih.IssueId).IsRequired();
                entity.Property(dih => dih.LastOccurredDate).IsRequired();
                entity.Property(dih => dih.OccurrenceCount).IsRequired().HasDefaultValue(1);
                entity.HasIndex(dih => new { dih.DeviceId, dih.IssueId });
            });

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

            // DeviceErrorHistory
            modelBuilder.Entity<DeviceErrorHistory>(entity =>
            {
                entity.Property(deh => deh.DeviceId).IsRequired();
                entity.Property(deh => deh.ErrorId).IsRequired();
                entity.Property(deh => deh.LastOccurredDate).IsRequired();
                entity.Property(deh => deh.OccurrenceCount).IsRequired().HasDefaultValue(1);
                entity.HasIndex(deh => new { deh.DeviceId, deh.ErrorId });
            });

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

            // Notification
            modelBuilder.Entity<Notification>(entity =>
            {
                entity.Property(n => n.SenderId).IsRequired();
                entity.Property(n => n.ReceiverId).IsRequired();
            });

            // RepairSparepart
            modelBuilder.Entity<RepairSparepart>()
                .HasKey(rs => new { rs.SpareId, rs.TaskId });

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

            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Request)
                .WithMany(r => r.RequestIssues)
                .HasForeignKey(ri => ri.RequestId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RequestIssue>()
                .HasOne(ri => ri.Issue)
                .WithMany(i => i.RequestIssues)
                .HasForeignKey(ri => ri.IssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Image
            modelBuilder.Entity<Image>()
                .HasOne(i => i.RequestIssue)
                .WithMany(ri => ri.Images)
                .HasForeignKey(i => i.RequestIssueId)
                .OnDelete(DeleteBehavior.Restrict);

            // Sparepart
            modelBuilder.Entity<Sparepart>(entity =>
            {
                entity.Property(s => s.SparepartCode).IsRequired();
                entity.HasIndex(s => s.SparepartCode).IsUnique();
                entity.Property(s => s.SparepartName).IsRequired();
                entity.Property(s => s.StockQuantity).IsRequired().HasDefaultValue(0);
                entity.Property(s => s.ExpectedAvailabilityDate);
            });

            // Tasks
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Assignee)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssigneeId)
                .OnDelete(DeleteBehavior.Restrict);

            // WarrantyDetail
            modelBuilder.Entity<WarrantyDetail>(entity =>
            {
                entity.Property(wd => wd.ReportId).IsRequired();
                entity.Property(wd => wd.WarrantyNotes).HasMaxLength(1000);
            });

            modelBuilder.Entity<WarrantyDetail>()
                .HasOne(wd => wd.Report)
                .WithMany(r => r.WarrantyDetails)
                .HasForeignKey(wd => wd.ReportId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<WarrantyDetail>()
                .HasOne(wd => wd.Task)
                .WithMany()
                .HasForeignKey(wd => wd.TaskId)
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
                .HasKey(tsr => new { tsr.ReportId, tsr.TechnicalSymptomId });

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
                .OnDelete(DeleteBehavior.SetNull);
            // ErrorFixProgress
            modelBuilder.Entity<ErrorFixProgress>()
                .HasKey(efp => efp.Id);

            modelBuilder.Entity<ErrorFixProgress>()
                .HasOne(efp => efp.ErrorDetail)
                .WithMany(ed => ed.ProgressRecords)
                .HasForeignKey(efp => efp.ErrorDetailId)
                .OnDelete(DeleteBehavior.Cascade); // Giữ cascade cho ErrorDetail

            modelBuilder.Entity<ErrorFixProgress>()
                .HasOne(efp => efp.ErrorFixStep)
                .WithMany() // Không cần WithMany nếu ErrorFixStep không có collection
                .HasForeignKey(efp => efp.ErrorFixStepId)
                .OnDelete(DeleteBehavior.Restrict); // Thay bằng Restrict để tránh vòng lặp

            modelBuilder.Entity<ErrorFixProgress>()
                .Property(efp => efp.IsCompleted)
                .IsRequired();
            modelBuilder.Entity<Sparepart>()
    .HasOne(s => s.Supplier)
    .WithMany(sp => sp.Spareparts)
    .HasForeignKey(s => s.SupplierId)
    .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Sparepart>()
    .Property(s => s.SupplierId)
    .IsRequired(false);

            modelBuilder.Entity<MachineSparepart>()
    .HasKey(ms => new { ms.MachineId, ms.SparepartId }); // Composite key

            modelBuilder.Entity<MachineSparepart>()
    .HasOne(ms => ms.Machine)
    .WithMany(m => m.MachineSpareparts)
    .HasForeignKey(ms => ms.MachineId)
    .IsRequired() // Đảm bảo MachineId không NULL
    .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MachineSparepart>()
    .HasOne(ms => ms.Sparepart)
    .WithMany(s => s.MachineSpareparts)
    .HasForeignKey(ms => ms.SparepartId)
    .IsRequired() // Đảm bảo SparepartId không NULL
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
            modelBuilder.Entity<ErrorGuideline>().ToTable("ErrorGuidelines");
            modelBuilder.Entity<ErrorFixStep>().ToTable("ErrorFixSteps");
            modelBuilder.Entity<ErrorFixProgress>().ToTable("ErrorFixProgresses");
            modelBuilder.Entity<SparePartUsage>().ToTable("SparePartUsages");
            modelBuilder.Entity<RequestTakeSparePartUsage>().ToTable("RequestTakeSparePartUsages");
            modelBuilder.Entity<MachineSparepart>().ToTable("MachineSpareparts");
            modelBuilder.Entity<Supplier>().ToTable("Suppliers");

            #endregion
        }
    }
}