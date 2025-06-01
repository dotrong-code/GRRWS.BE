using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class TasksConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.HasData(
                // Original 10 tasks (unchanged)
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0001-0001-0001-100000000001"),
                    TaskName = "Repair Juki DDL-8700 Unit 1",
                    TaskDescription = "Sửa lỗi đứt chỉ do mỏ trói chỉ lỏng trên máy Juki DDL-8700 Unit 1 (DEV001-JUKI-DDL8700-01).",
                    TaskType = "Warranty",
                    Priority = 2, // Medium
                    Status = "Completed",
                    StartTime = new DateTime(2025, 4, 15, 10, 30, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 4, 16, 10, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 4, 15, 16, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 4, 15, 16, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy hoạt động bình thường sau khi thay mỏ trói chỉ.",
                    ReportNotes = "Đã kiểm tra và thay mỏ trói chỉ mới, vận hành thử ổn định.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333333") // Staff Member
                    // Additional mechanics: Staff Member 2 (43333333-3333-3333-3333-333333333334)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0002-0002-0002-100000000002"),
                    TaskName = "Fix Motor Issue on Juki DDL-8700 Unit 3",
                    TaskDescription = "Sửa lỗi máy không chạy do động cơ cháy trên máy Juki DDL-8700 Unit 3 (DEV003-JUKI-DDL8700-03).",
                    TaskType = "Repair",
                    Priority = 3, // High
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 1, 14, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 4, 14, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                    // Additional mechanics: Staff Member 3 (43333333-3333-3333-3333-333333333335)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0003-0003-0003-100000000003"),
                    TaskName = "Repair Juki DDL-8700 Unit 4",
                    TaskDescription = "Sửa lỗi máy ngừng hoạt động do chập điện trên máy Juki DDL-8700 Unit 4 (DEV004-JUKI-DDL8700-04).",
                    TaskType = "Repair",
                    Priority = 3, // High
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 10, 9, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 13, 9, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333335") // Staff Member 3
                    // Additional mechanics: Staff Member 4 (43333333-3333-3333-3333-333333333336)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0004-0004-0004-100000000004"),
                    TaskName = "Replace Needle on Juki DDL-8700 Unit 7",
                    TaskDescription = "Thay kim gãy do lệch tâm trên máy Juki DDL-8700 Unit 7 (DEV007-JUKI-DDL8700-07).",
                    TaskType = "Replace",
                    Priority = 2, // Medium
                    Status = "Completed",
                    StartTime = new DateTime(2025, 3, 20, 11, 15, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 3, 21, 11, 15, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 3, 20, 15, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 3, 20, 15, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy hoạt động tốt sau khi thay kim mới.",
                    ReportNotes = "Đã thay kim mới và căn chỉnh trục kim, kiểm tra vận hành ổn.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333336") // Staff Member 4
                    // Additional mechanics: Staff Member 5 (43333333-3333-3333-3333-333333333337)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0005-0005-0005-100000000005"),
                    TaskName = "Repair Noise Issue on Juki DDL-8700 Unit 8",
                    TaskDescription = "Sửa tiếng ồn lớn do bánh răng mòn trên máy Juki DDL-8700 Unit 8 (DEV008-JUKI-DDL8700-08).",
                    TaskType = "Repair",
                    Priority = 1, // Low
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 4, 25, 13, 45, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 4, 27, 13, 45, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333337") // Staff Member 5
                    // Additional mechanics: Staff Member 6 (43333333-3333-3333-3333-333333333338)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0006-0006-0006-100000000006"),
                    TaskName = "Repair Error Light on Juki DDL-9000C Unit 1",
                    TaskDescription = "Sửa lỗi đèn báo lỗi do mạch điều khiển trên máy Juki DDL-9000C Unit 1 (DEV011-JUKI-DDL9000C-01).",
                    TaskType = "Repair",
                    Priority = 3, // High
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 5, 8, 20, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 8, 8, 20, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333338") // Staff Member 6
                    // Additional mechanics: Staff Member 7 (43333333-3333-3333-3333-333333333339)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0007-0007-0007-100000000007"),
                    TaskName = "Adjust Stitch on Juki DDL-9000C Unit 3",
                    TaskDescription = "Sửa lỗi chỉ không đều do bulong lỏng trên máy Juki DDL-9000C Unit 3 (DEV013-JUKI-DDL9000C-03).",
                    TaskType = "Warranty",
                    Priority = 2, // Medium
                    Status = "Completed",
                    StartTime = new DateTime(2025, 4, 10, 16, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 4, 11, 16, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 4, 10, 18, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 4, 10, 18, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy may đều sau khi siết lại bulong.",
                    ReportNotes = "Đã siết chặt bulong bộ truyền, kiểm tra đường chỉ ổn định.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333339") // Staff Member 7
                    // Additional mechanics: Staff Member 8 (43333333-3333-3333-3333-333333333340)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0008-0008-0008-100000000008"),
                    TaskName = "Repair Thread Issue on Brother B957 Unit 1",
                    TaskDescription = "Sửa lỗi chỉ dưới không kéo lên trên máy Brother B957 Unit 1 (DEV015-BROTHER-B957-01).",
                    TaskType = "Repair",
                    Priority = 2, // Medium
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 15, 12, 10, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 17, 12, 10, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333340") // Staff Member 8
                    // Additional mechanics: Staff Member 9 (43333333-3333-3333-3333-333333333341)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0009-0009-0009-100000000009"),
                    TaskName = "Replace Motor on Singer 4452 Unit 1",
                    TaskDescription = "Thay mô tơ cháy trên máy Singer 4452 Unit 1 (DEV018-SINGER-4452-01).",
                    TaskType = "Replace",
                    Priority = 3, // High
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 18, 10, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 21, 10, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333341") // Staff Member 9
                    // Additional mechanics: Staff Member 10 (43333333-3333-3333-3333-333333333342)
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0010-0010-0010-100000000010"),
                    TaskName = "Adjust Fabric Feed on Singer 4452 Unit 3",
                    TaskDescription = "Sửa lỗi vải bị nhăn do bộ cấp vải không đều trên máy Singer 4452 Unit 3 (DEV020-SINGER-4452-03).",
                    TaskType = "Warranty",
                    Priority = 2, // Medium
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 20, 15, 30, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 22, 15, 30, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333342") // Staff Member 10
                    // Additional mechanics: Staff Member 1 (43333333-3333-3333-3333-333333333333)
                },
                // New tasks for Staff Member 2 (9 tasks)
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0011-0011-0011-100000000011"),
                    TaskName = "Replace Thread Tensioner on Juki DDL-8700 Unit 5",
                    TaskDescription = "Thay bộ điều chỉnh căng chỉ bị mòn trên máy Juki DDL-8700 Unit 5 (DEV005-JUKI-DDL8700-05).",
                    TaskType = "Replace",
                    Priority = 2, // Medium
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 26, 8, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 27, 8, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0012-0012-0012-100000000012"),
                    TaskName = "Repair Motor on Juki DDL-9000C Unit 4",
                    TaskDescription = "Sửa lỗi động cơ yếu trên máy Juki DDL-9000C Unit 4 (DEV014-JUKI-DDL9000C-04).",
                    TaskType = "Repair",
                    Priority = 3, // High
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 25, 10, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 28, 10, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0013-0013-0013-100000000013"),
                    TaskName = "Adjust Stitch Length on Brother B957 Unit 2",
                    TaskDescription = "Cân chỉnh độ dài mũi may không đều trên máy Brother B957 Unit 2 (DEV016-BROTHER-B957-02).",
                    TaskType = "Warranty",
                    Priority = 2, // Medium
                    Status = "Completed",
                    StartTime = new DateTime(2025, 5, 20, 9, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 21, 9, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 5, 20, 14, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 5, 20, 14, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy hoạt động ổn định sau khi điều chỉnh.",
                    ReportNotes = "Đã căn chỉnh bộ điều chỉnh mũi may, kiểm tra ổn.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0014-0014-0014-100000000014"),
                    TaskName = "Replace Needle Bar on Juki MO-6714S Unit 1",
                    TaskDescription = "Thay thanh kim bị cong trên máy Juki MO-6714S Unit 1 (DEV021-JUKI-MO6714S-01).",
                    TaskType = "Replace",
                    Priority = 2, // Medium
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 27, 11, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 28, 11, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0015-0015-0015-100000000015"),
                    TaskName = "Repair Fabric Feed on Singer 4452 Unit 2",
                    TaskDescription = "Sửa lỗi bộ cấp vải không hoạt động trên máy Singer 4452 Unit 2 (DEV019-SINGER-4452-02).",
                    TaskType = "Repair",
                    Priority = 3, // High
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 24, 13, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 27, 13, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0016-0016-0016-100000000016"),
                    TaskName = "Adjust Tension on Brother S-7200C Unit 1",
                    TaskDescription = "Cân chỉnh độ căng chỉ không ổn định trên máy Brother S-7200C Unit 1 (DEV023-BROTHER-S7200C-01).",
                    TaskType = "Warranty",
                    Priority = 1, // Low
                    Status = "Completed",
                    StartTime = new DateTime(2025, 5, 22, 15, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 23, 15, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 5, 22, 17, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 5, 22, 17, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy hoạt động tốt sau khi điều chỉnh.",
                    ReportNotes = "Đã điều chỉnh bộ căng chỉ, kiểm tra ổn định.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0017-0017-0017-100000000017"),
                    TaskName = "Replace Foot Pedal on Juki DDL-8700 Unit 9",
                    TaskDescription = "Thay bàn đạp chân bị hỏng trên máy Juki DDL-8700 Unit 9 (DEV009-JUKI-DDL8700-09).",
                    TaskType = "Replace",
                    Priority = 2, // Medium
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 28, 9, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 29, 9, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0018-0018-0018-100000000018"),
                    TaskName = "Repair Circuit Board on Juki DDL-8700 Unit 10",
                    TaskDescription = "Sửa lỗi mạch điều khiển trên máy Juki DDL-8700 Unit 10 (DEV010-JUKI-DDL8700-10).",
                    TaskType = "Repair",
                    Priority = 3, // High
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 23, 14, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 26, 14, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0019-0019-0019-100000000019"),
                    TaskName = "Replace Motor on Juki DDL-9000C Unit 3",
                    TaskDescription = "Thay mô tơ bị cháy trên máy Juki DDL-9000C Unit 3 (DEV013-JUKI-DDL9000C-03).",
                    TaskType = "Replace",
                    Priority = 3, // High
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 29, 10, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 6, 1, 10, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333334") // Staff Member 2
                },
                // New tasks for Staff Member 4 (6 tasks)
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0020-0020-0020-100000000020"),
                    TaskName = "Replace Gear on Juki DDL-8700 Unit 8",
                    TaskDescription = "Thay bánh răng mòn gây tiếng ồn trên máy Juki DDL-8700 Unit 8 (DEV008-JUKI-DDL8700-08).",
                    TaskType = "Replace",
                    Priority = 1, // Low
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 26, 14, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 27, 14, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333336") // Staff Member 4
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0021-0021-0021-100000000021"),
                    TaskName = "Repair Control Panel on Juki DDL-9000C Unit 2",
                    TaskDescription = "Sửa lỗi bảng điều khiển không phản hồi trên máy Juki DDL-9000C Unit 2 (DEV012-JUKI-DDL9000C-02).",
                    TaskType = "Repair",
                    Priority = 3, // High
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 24, 11, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 27, 11, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333336") // Staff Member 4
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0022-0022-0022-100000000022"),
                    TaskName = "Adjust Cutting Mechanism on Brother S-7200C Unit 2",
                    TaskDescription = "Cân chỉnh cơ chế cắt chỉ tự động trên máy Brother S-7200C Unit 2 (DEV024-BROTHER-S7200C-02).",
                    TaskType = "Warranty",
                    Priority = 2, // Medium
                    Status = "Completed",
                    StartTime = new DateTime(2025, 5, 21, 10, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 22, 10, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 5, 21, 15, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 5, 21, 15, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Cơ chế cắt chỉ hoạt động bình thường.",
                    ReportNotes = "Đã điều chỉnh cơ chế cắt, kiểm tra ổn định.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333336") // Staff Member 4
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0023-0023-0023-100000000023"),
                    TaskName = "Replace Needle on Juki LH-3568S Unit 1",
                    TaskDescription = "Thay kim gãy trên máy Juki LH-3568S Unit 1 (DEV027-JUKI-LH3568S-01).",
                    TaskType = "Replace",
                    Priority = 2, // Medium
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 28, 13, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 29, 13, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333336") // Staff Member 4
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0024-0024-0024-100000000024"),
                    TaskName = "Repair Thread Tension on Brother B735 Unit 1",
                    TaskDescription = "Sửa lỗi căng chỉ không đều trên máy Brother B735 Unit 1 (DEV029-BROTHER-B735-01).",
                    TaskType = "Repair",
                    Priority = 2, // Medium
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 25, 15, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 27, 15, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333336") // Staff Member 4
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0025-0025-0025-100000000025"),
                    TaskName = "Adjust Stitch on Singer 4423 Unit 1",
                    TaskDescription = "Cân chỉnh mũi may không đều trên máy Singer 4423 Unit 1 (DEV025-SINGER-4423-01).",
                    TaskType = "Warranty",
                    Priority = 1, // Low
                    Status = "Completed",
                    StartTime = new DateTime(2025, 5, 23, 12, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 24, 12, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 5, 23, 16, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 5, 23, 16, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy may ổn định sau khi điều chỉnh.",
                    ReportNotes = "Đã điều chỉnh bộ điều chỉnh mũi may, kiểm tra ổn.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333336") // Staff Member 4
                }
            );
        }
    }
}