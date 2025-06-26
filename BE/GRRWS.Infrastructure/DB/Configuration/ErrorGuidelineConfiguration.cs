using GRRWS.Domain.Entities;
using GRRWS.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorGuidelineConfiguration : IEntityTypeConfiguration<ErrorGuideline>
    {
        public void Configure(EntityTypeBuilder<ErrorGuideline> builder)
        {
            builder.HasIndex(e => e.Title).IsUnique();

            builder.HasData(
                // Existing guidelines
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    ErrorId = Guid.Parse("e1d1a111-0001-0001-0001-000000000001"), // Hỏng Bàn Đạp
                    Title = "Hướng dẫn sửa chữa bàn đạp không phản hồi",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    ErrorId = Guid.Parse("e1d1a222-0002-0002-0002-000000000002"), //  Dây Curoa Mòn
                    Title = "Xử lý dây chuyền trượt do mòn",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000003"),
                    ErrorId = Guid.Parse("e1d1a333-0003-0003-0003-000000000003"), // Máy Chạy Luôn Lượt
                    Title = "Sửa lỗi bo điều khiển máy chạy luôn lượt",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000004"),
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // Cháy Motor
                    Title = "Thay thế motor bị cháy do quá tải",
                    EstimatedRepairTime = TimeSpan.FromMinutes(180),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000005"),
                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005"), // Khóa Kim Hỏng
                    Title = "Cân chỉnh cơ chế khóa kim",
                    EstimatedRepairTime = TimeSpan.FromMinutes(40),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000006"),
                    ErrorId = Guid.Parse("e1d1a666-0006-0006-0006-000000000006"), // Gioăng Dầu Bị Rò
                    Title = "Xử lý gioăng dầu bị rò",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000007"),
                    ErrorId = Guid.Parse("e1d1a777-0007-0007-0007-000000000007"), // Cảm Biến Lệch
                    Title = "Căn chỉnh cảm biến vị trí bị lệch",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Low
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000008"),
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008"), // Lỗi Mạch Điều Khiển
                    Title = "Sửa chữa mạch điều khiển bị lỗi",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000009"),
                    ErrorId = Guid.Parse("e1d1a999-0009-0009-0009-000000000009"), // Chống Trôi Không Hoạt Động
                    Title = "Sửa chữa cơ chế chống trôi",
                    EstimatedRepairTime = TimeSpan.FromMinutes(50),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000010"),
                    ErrorId = Guid.Parse("e1d1abbb-0010-0010-0010-000000000010"), // Chốt Vải Kẹt
                    Title = "Giải quyết tình trạng chốt vải kẹt",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    Priority = Priority.Low
                },
                // New guidelines for remaining 50 errors
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000011"),
                    ErrorId = Guid.Parse("e1d1addd-0011-0011-0011-000000000011"), // Vòng Bạc Mòn
                    Title = "Thay thế vòng bạc bị mòn gây rung lắc",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000012"),
                    ErrorId = Guid.Parse("e1d1aeee-0012-0012-0012-000000000012"), // Dao Cắt Không Sắc
                    Title = "Mài sắc hoặc thay dao cắt không bén",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    Priority = Priority.Low
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000013"),
                    ErrorId = Guid.Parse("e1d1afff-0013-0013-0013-000000000013"), // Cảm Biến Vải Không Nhận
                    Title = "Kiểm tra và sửa cảm biến vải không nhận diện",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000014"),
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014"), // Kim Lỗi Tâm
                    Title = "Căn chỉnh kim đúng trục tâm",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000015"),
                    ErrorId = Guid.Parse("e1d1a123-0015-0015-0015-000000000015"), // Lỗi Quạt Gió
                    Title = "Sửa chữa quạt gió không hoạt động",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Low
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000016"),
                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016"), // Trục Chính Lệch
                    Title = "Căn chỉnh trục chính quay đồng tâm",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000017"),
                    ErrorId = Guid.Parse("e1d1a125-0017-0017-0017-000000000017"), // Đuôi Đèn Cháy
                    Title = "Thay thế đuôi đèn bị cháy",
                    EstimatedRepairTime = TimeSpan.FromMinutes(20),
                    Priority = Priority.Low
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000018"),
                    ErrorId = Guid.Parse("e1d1a126-0018-0018-0018-000000000018"), // Mất Bộ Nhớ Lưu Thông Số
                    Title = "Khôi phục bộ nhớ điều khiển lưu thông số",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000019"),
                    ErrorId = Guid.Parse("e1d1a127-0019-0019-0019-000000000019"), // Cảm Biến Áp Lực Lỗi
                    Title = "Kiểm tra và sửa cảm biến áp lực sai số",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000020"),
                    ErrorId = Guid.Parse("e1d1a128-0020-0020-0020-000000000020"), // Rong Không Đủ Siêu
                    Title = "Sửa bộ cấp vải để kéo đều",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000021"),
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021"), // Mỏ Trói Chỉ Bị Lỏng
                    Title = "Căn chỉnh mỏ trói chỉ để siết chặt",
                    EstimatedRepairTime = TimeSpan.FromMinutes(45),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000022"),
                    ErrorId = Guid.Parse("e1d1a130-0022-0022-0022-000000000022"), // Bánh Răng Mòn
                    Title = "Thay thế bánh răng truyền động bị mòn",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000023"),
                    ErrorId = Guid.Parse("e1d1a131-0023-0023-0023-000000000023"), // Cần Tay Không Ăn Khớp
                    Title = "Sửa cần tay điều khiển không ăn khớp",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Low
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000024"),
                    ErrorId = Guid.Parse("e1d1a132-0024-0024-0024-000000000024"), // Kim Chạm Vải
                    Title = "Căn chỉnh kim để tránh va chạm vải",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    Priority = Priority.Low
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000025"),
                    ErrorId = Guid.Parse("e1d1a133-0025-0025-0025-000000000025"), // Dây Khởi Động Lỗi
                    Title = "Sửa dây khởi động bị hở hoặc đứt",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000026"),
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026"), // Bu Lông Lỏng
                    Title = "Siết chặt bu lông cố định cụm máy",
                    EstimatedRepairTime = TimeSpan.FromMinutes(40),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000027"),
                    ErrorId = Guid.Parse("e1d1a135-0027-0027-0027-000000000027"), // Mạch Đèn Lỗi
                    Title = "Sửa mạch điện đèn chiếu sáng",
                    EstimatedRepairTime = TimeSpan.FromMinutes(30),
                    Priority = Priority.Low
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000028"),
                    ErrorId = Guid.Parse("e1d1a136-0028-0028-0028-000000000028"), // Dầu Bôi Trơn Nhiều
                    Title = "Điều chỉnh lượng dầu bôi trơn để tránh loang",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000029"),
                    ErrorId = Guid.Parse("e1d1a137-0029-0029-0029-000000000029"), // Quạt Thông Gió Yếu
                    Title = "Sửa quạt thông gió để làm mát hiệu quả",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000030"),
                    ErrorId = Guid.Parse("e1d1a138-0030-0030-0030-000000000030"), // Cửa Kim Lệch
                    Title = "Căn chỉnh cửa kim thẳng hàng với trục",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000031"),
                    ErrorId = Guid.Parse("e1d1a139-0031-0031-0031-000000000031"), // Ống Dẫn Chỉ Hỏng
                    Title = "Thay thế ống dẫn chỉ bị mòn hoặc gãy",
                    EstimatedRepairTime = TimeSpan.FromMinutes(45),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000032"),
                    ErrorId = Guid.Parse("e1d1a140-0032-0032-0032-000000000032"), // Suốt Chỉ Không Quay
                    Title = "Sửa suốt chỉ dưới để quay bình thường",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000033"),
                    ErrorId = Guid.Parse("e1d1a141-0033-0033-0033-000000000033"), // Chân Vịt Không Đồng Bộ
                    Title = "Đồng bộ chân vịt với kim để may đúng",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000034"),
                    ErrorId = Guid.Parse("e1d1a142-0034-0034-0034-000000000034"), // Dây Điện Nguồn Hở
                    Title = "Sửa dây nguồn bị hở để tránh chập điện",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000035"),
                    ErrorId = Guid.Parse("e1d1a143-0035-0035-0035-000000000035"), // Băng Tải Hỏng
                    Title = "Sửa hoặc thay băng tải truyền động",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000036"),
                    ErrorId = Guid.Parse("e1d1a144-0036-0036-0036-000000000036"), // Trục Kim Gãy
                    Title = "Thay thế trục kim bị gãy",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000037"),
                    ErrorId = Guid.Parse("e1d1a145-0037-0037-0037-000000000037"), // Cảm Biến Nhiệt Độ Lỗi
                    Title = "Sửa cảm biến nhiệt độ để cảnh báo chính xác",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000038"),
                    ErrorId = Guid.Parse("e1d1a146-0038-0038-0038-000000000038"), // Mô Hộp Số Không Khớp
                    Title = "Căn chỉnh hộp số truyền động",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000039"),
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039"), // Răng Cưa Mòn
                    Title = "Thay thế răng cưa kéo vải bị mòn",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000040"),
                    ErrorId = Guid.Parse("e1d1a148-0040-0040-0040-000000000040"), // Cáp Nguồn Yếu
                    Title = "Thay cáp nguồn để cung cấp điện ổn định",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000041"),
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041"), // Mô Căng Chỉ Lỗi
                    Title = "Sửa bộ căng chỉ để chỉ đều",
                    EstimatedRepairTime = TimeSpan.FromMinutes(45),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000042"),
                    ErrorId = Guid.Parse("e1d1a150-0042-0042-0042-000000000042"), // Bộ Nhớ Điều Khiển Hỏng
                    Title = "Thay thế bộ nhớ điều khiển bị lỗi",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000043"),
                    ErrorId = Guid.Parse("e1d1a151-0043-0043-0043-000000000043"), // Trục Chuyển Động Kẹt
                    Title = "Làm sạch và bôi trơn trục chuyển động",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000044"),
                    ErrorId = Guid.Parse("e1d1a152-0044-0044-0044-000000000044"), // Cảm Biến Vận Tốc Lỗi
                    Title = "Sửa cảm biến vận tốc để ổn định tốc độ",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000045"),
                    ErrorId = Guid.Parse("e1d1a153-0045-0045-0045-000000000045"), // Pháo Chỉ Hỏng
                    Title = "Thay pháo chỉ dưới bị mòn",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000046"),
                    ErrorId = Guid.Parse("e1d1a154-0046-0046-0046-000000000046"), // Bộ Câu Chỉ Lỗi
                    Title = "Sửa bộ câu chỉ để kéo chỉ dưới",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000047"),
                    ErrorId = Guid.Parse("e1d1a155-0047-0047-0047-000000000047"), // Ống Dầu Bị Tắc
                    Title = "Thông ống dẫn dầu bôi trơn",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000048"),
                    ErrorId = Guid.Parse("e1d1a156-0048-0048-0048-000000000048"), // Bộ Truyền Động Yếu
                    Title = "Sửa bộ truyền động để tăng lực",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000049"),
                    ErrorId = Guid.Parse("e1d1a157-0049-0049-0049-000000000049"), // Chân Vịt Không Nâng
                    Title = "Sửa cơ cấu nâng chân vịt",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000050"),
                    ErrorId = Guid.Parse("e1d1a158-0050-0050-0050-000000000050"), // Cảm Biến Kim Lỗi
                    Title = "Sửa cảm biến kim để nhận diện vị trí",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000051"),
                    ErrorId = Guid.Parse("e1d1a159-0051-0051-0051-000000000051"), // Mô Dẫn Vải Hỏng
                    Title = "Thay mô dẫn vải để kéo đều",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000052"),
                    ErrorId = Guid.Parse("e1d1a160-0052-0052-0052-000000000052"), // Bộ Điều Khiển Quá Nhiệt
                    Title = "Làm mát và sửa bo mạch điều khiển",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000053"),
                    ErrorId = Guid.Parse("e1d1a161-0053-0053-0053-000000000053"), // Trục Căng Chỉ Gãy
                    Title = "Thay trục căng chỉ bị gãy",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000054"),
                    ErrorId = Guid.Parse("e1d1a162-0054-0054-0054-000000000054"), // Răng Cưa Không Đồng Bộ
                    Title = "Đồng bộ răng cưa với kim",
                    EstimatedRepairTime = TimeSpan.FromMinutes(90),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000055"),
                    ErrorId = Guid.Parse("e1d1a163-0055-0055-0055-000000000055"), // Bộ Nguồn Hỏng
                    Title = "Thay bộ nguồn cung cấp điện",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000056"),
                    ErrorId = Guid.Parse("e1d1a164-0056-0056-0056-000000000056"), // Dây Curoa Gãy
                    Title = "Thay dây curoa truyền động bị gãy",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000057"),
                    ErrorId = Guid.Parse("e1d1a165-0057-0057-0057-000000000057"), // Cảm Biến Chỉ Lỗi
                    Title = "Sửa cảm biến chỉ để nhận diện đúng",
                    EstimatedRepairTime = TimeSpan.FromMinutes(60),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000058"),
                    ErrorId = Guid.Parse("e1d1a166-0058-0058-0058-000000000058"), // Bộ Khớp Nối Hỏng
                    Title = "Thay bộ khớp nối truyền động",
                    EstimatedRepairTime = TimeSpan.FromMinutes(120),
                    Priority = Priority.Urgent
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000059"),
                    ErrorId = Guid.Parse("e1d1a167-0059-0059-0059-000000000059"), // Lò Xo Căng Chỉ Yếu
                    Title = "Thay lò xo căng chỉ để đủ lực",
                    EstimatedRepairTime = TimeSpan.FromMinutes(45),
                    Priority = Priority.Medium
                },
                new ErrorGuideline
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000060"),
                    ErrorId = Guid.Parse("e1d1a168-0060-0060-0060-000000000060"), // Mô Động Cơ Mòn
                    Title = "Thay mô động cơ bị mòn",
                    EstimatedRepairTime = TimeSpan.FromMinutes(150),
                    Priority = Priority.Urgent
                }
            );
        }
    }
}