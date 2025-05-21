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
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0001-0001-0001-100000000001"),
                    TaskName = "Repair Juki DDL-8700 Unit 1",
                    TaskDescription = "Sửa lỗi đứt chỉ do mỏ trói chỉ lỏng trên máy Juki DDL-8700 Unit 1 (MC001-JUKI-DDL8700-01).",
                    TaskType = "Maintenance",
                    Priority = 2, // Medium
                    Status = "Completed",
                    StartTime = new DateTime(2025, 4, 15, 10, 30, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 4, 16, 10, 30, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 4, 15, 16, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 4, 15, 16, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy hoạt động bình thường sau khi thay mỏ trói chỉ.",
                    ReportNotes = "Đã kiểm tra và thay mỏ trói chỉ mới, vận hành thử ổn định.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333333") // Staff Member
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0002-0002-0002-100000000002"),
                    TaskName = "Fix Motor Issue on Juki DDL-8700 Unit 3",
                    TaskDescription = "Sửa lỗi máy không chạy do động cơ cháy trên máy Juki DDL-8700 Unit 3 (MC003-JUKI-DDL8700-03).",
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
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0003-0003-0003-100000000003"),
                    TaskName = "Repair Juki DDL-8700 Unit 4",
                    TaskDescription = "Sửa lỗi máy ngừng hoạt động do chập điện trên máy Juki DDL-8700 Unit 4 (MC004-JUKI-DDL8700-04).",
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
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0004-0004-0004-100000000004"),
                    TaskName = "Fix Needle Issue on Juki DDL-8700 Unit 7",
                    TaskDescription = "Sửa lỗi kim gãy do lệch tâm trên máy Juki DDL-8700 Unit 7 (MC007-JUKI-DDL8700-07).",
                    TaskType = "Maintenance",
                    Priority = 2, // Medium
                    Status = "Completed",
                    StartTime = new DateTime(2025, 3, 20, 11, 15, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 3, 21, 11, 15, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 3, 20, 15, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 3, 20, 15, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy hoạt động tốt sau khi căn chỉnh trục kim.",
                    ReportNotes = "Đã thay kim mới và căn chỉnh trục kim, kiểm tra vận hành ổn.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333336") // Staff Member 4
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0005-0005-0005-100000000005"),
                    TaskName = "Reduce Noise on Juki DDL-8700 Unit 8",
                    TaskDescription = "Bảo trì tiếng ồn lớn do bánh răng mòn trên máy Juki DDL-8700 Unit 8 (MC008-JUKI-DDL8700-08).",
                    TaskType = "Maintenance",
                    Priority = 1, // Low
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 4, 25, 13, 45, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 4, 27, 13, 45, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333337") // Staff Member 5
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0006-0006-0006-100000000006"),
                    TaskName = "Fix Error Light on Juki DDL-9000C Unit 1",
                    TaskDescription = "Sửa lỗi đèn báo lỗi do mạch điều khiển trên máy Juki DDL-9000C Unit 1 (MC011-JUKI-DDL9000C-01).",
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
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0007-0007-0007-100000000007"),
                    TaskName = "Adjust Stitch on Juki DDL-9000C Unit 3",
                    TaskDescription = "Sửa lỗi chỉ không đều do bulong lỏng trên máy Juki DDL-9000C Unit 3 (MC013-JUKI-DDL9000C-03).",
                    TaskType = "Maintenance",
                    Priority = 2, // Medium
                    Status = "Completed",
                    StartTime = new DateTime(2025, 4, 10, 16, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 4, 11, 16, 00, 0, DateTimeKind.Utc),
                    EndTime = new DateTime(2025, 4, 10, 18, 00, 0, DateTimeKind.Utc),
                    DeviceReturnTime = new DateTime(2025, 4, 10, 18, 30, 0, DateTimeKind.Utc),
                    DeviceCondition = "Máy may đều sau khi siết lại bulong.",
                    ReportNotes = "Đã siết chặt bulong bộ truyền, kiểm tra đường chỉ ổn định.",
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333339") // Staff Member 7
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0008-0008-0008-100000000008"),
                    TaskName = "Fix Thread Issue on Brother B957 Unit 1",
                    TaskDescription = "Sửa lỗi chỉ dưới không kéo lên trên máy Brother B957 Unit 1 (MC015-BROTHER-B957-01).",
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
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0009-0009-0009-100000000009"),
                    TaskName = "Repair Singer 4452 Unit 1",
                    TaskDescription = "Sửa lỗi máy không chạy do mô tơ cháy trên máy Singer 4452 Unit 1 (MC018-SINGER-4452-01).",
                    TaskType = "Repair",
                    Priority = 3, // High
                    Status = "Pending",
                    StartTime = new DateTime(2025, 5, 18, 10, 00, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 21, 10, 00, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333341") // Staff Member 9
                },
                new Tasks
                {
                    Id = Guid.Parse("b1c2d3e4-0010-0010-0010-100000000010"),
                    TaskName = "Adjust Fabric Feed on Singer 4452 Unit 3",
                    TaskDescription = "Sửa lỗi vải bị nhăn do bộ cấp vải không đều trên máy Singer 4452 Unit 3 (MC020-SINGER-4452-03).",
                    TaskType = "Maintenance",
                    Priority = 2, // Medium
                    Status = "InProgress",
                    StartTime = new DateTime(2025, 5, 20, 15, 30, 0, DateTimeKind.Utc),
                    ExpectedTime = new DateTime(2025, 5, 22, 15, 30, 0, DateTimeKind.Utc),
                    EndTime = null,
                    DeviceReturnTime = null,
                    DeviceCondition = null,
                    ReportNotes = null,
                    AssigneeId = Guid.Parse("43333333-3333-3333-3333-333333333342") // Staff Member 10
                }
            );
        }
    }
}