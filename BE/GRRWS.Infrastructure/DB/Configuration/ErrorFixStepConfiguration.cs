using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorFixStepConfiguration : IEntityTypeConfiguration<ErrorFixStep>
    {
        public void Configure(EntityTypeBuilder<ErrorFixStep> builder)
        {
            builder.HasIndex(e => e.StepDescription).IsUnique();

            builder.HasData(

                // ErrorGuideline 1
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000001"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000001"), StepOrder = 1, StepDescription = "Tắt nguồn máy để đảm bảo an toàn." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000002"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000001"), StepOrder = 2, StepDescription = "Tháo rời bàn đạp khỏi máy." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000003"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000001"), StepOrder = 3, StepDescription = "Kiểm tra dây kết nối bàn đạp với motor." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000004"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000001"), StepOrder = 4, StepDescription = "Thay thế bàn đạp nếu phát hiện lỗi phần cứng." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000005"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000001"), StepOrder = 5, StepDescription = "Lắp lại và kiểm tra vận hành." },

                // ErrorGuideline 2
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000006"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000002"), StepOrder = 1, StepDescription = "Tắt nguồn và tháo vỏ bảo vệ dây curoa." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000007"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000002"), StepOrder = 2, StepDescription = "Kiểm tra độ mòn hoặc trượt của dây curoa." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000008"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000002"), StepOrder = 3, StepDescription = "Thay dây curoa mới đúng thông số." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000009"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000002"), StepOrder = 4, StepDescription = "Căn chỉnh lực căng và kiểm tra quay trơn tru." },

                // ErrorGuideline 3
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000010"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000003"), StepOrder = 1, StepDescription = "Ngắt toàn bộ nguồn điện máy." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000011"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000003"), StepOrder = 2, StepDescription = "Kiểm tra bo mạch điều khiển trung tâm." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000012"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000003"), StepOrder = 3, StepDescription = "Thay thế bo mạch nếu phát hiện cháy, hỏng IC." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000013"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000003"), StepOrder = 4, StepDescription = "Cập nhật phần mềm điều khiển nếu cần." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000014"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000003"), StepOrder = 5, StepDescription = "Test vận hành sau khi thay thế." },

                // ErrorGuideline 4
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000015"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000004"), StepOrder = 1, StepDescription = "Ngắt nguồn điện hoàn toàn trước khi tháo motor." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000016"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000004"), StepOrder = 2, StepDescription = "Tháo cụm motor ra khỏi thân máy." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000017"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000004"), StepOrder = 3, StepDescription = "Lắp motor mới theo đúng chủng loại công suất." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000018"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000004"), StepOrder = 4, StepDescription = "Kết nối điện trở lại và kiểm tra tốc độ chạy thử." },

                // ErrorGuideline 5
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000019"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000005"), StepOrder = 1, StepDescription = "Tháo cụm kim ra khỏi trục truyền động." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000020"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000005"), StepOrder = 2, StepDescription = "Kiểm tra phần ngàm giữ kim có bị lệch hay nứt." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000021"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000005"), StepOrder = 3, StepDescription = "Thay cơ cấu giữ kim nếu cần." },
                new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000022"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000005"), StepOrder = 4, StepDescription = "Lắp lại kim đúng tâm trục vận hành." },

                // Tiếp nối file cũ: ErrorFixStepConfiguration

// ErrorGuideline 6
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000023"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000006"), StepOrder = 1, StepDescription = "Tắt nguồn và tháo cụm bơm dầu." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000024"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000006"), StepOrder = 2, StepDescription = "Tháo gioăng/phớt dầu cũ ra khỏi bộ phận truyền động." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000025"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000006"), StepOrder = 3, StepDescription = "Làm sạch hoàn toàn vị trí lắp gioăng mới." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000026"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000006"), StepOrder = 4, StepDescription = "Lắp gioăng/phớt mới theo đúng kích cỡ chuẩn." },

// ErrorGuideline 7
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000027"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000007"), StepOrder = 1, StepDescription = "Tắt nguồn, tháo nắp bảo vệ cảm biến vị trí." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000028"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000007"), StepOrder = 2, StepDescription = "Kiểm tra độ lệch cơ học của mắt cảm biến." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000029"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000007"), StepOrder = 3, StepDescription = "Căn chỉnh lại vị trí đúng chuẩn thông số gốc." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000030"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000007"), StepOrder = 4, StepDescription = "Thử chạy máy, kiểm tra điểm dừng cảm biến." },

// ErrorGuideline 8
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000031"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000008"), StepOrder = 1, StepDescription = "Ngắt điện hoàn toàn trước khi sửa bo mạch." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000032"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000008"), StepOrder = 2, StepDescription = "Tháo bo mạch điều khiển khỏi vỏ máy." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000033"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000008"), StepOrder = 3, StepDescription = "Kiểm tra toàn bộ mạch, xác định vị trí cháy IC hoặc đường mạch đứt." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000034"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000008"), StepOrder = 4, StepDescription = "Thay thế linh kiện IC điều khiển chính." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000035"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000008"), StepOrder = 5, StepDescription = "Lắp bo mạch lại vào máy và kiểm tra." },

// ErrorGuideline 9
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000036"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000009"), StepOrder = 1, StepDescription = "Tháo cụm cơ cấu chống trôi khỏi cụm may." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000037"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000009"), StepOrder = 2, StepDescription = "Kiểm tra bánh răng, lò xo có bị mòn hoặc giãn lỏng." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000038"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000009"), StepOrder = 3, StepDescription = "Căn chỉnh lại trục và thay thế lò xo nếu yếu." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000039"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000009"), StepOrder = 4, StepDescription = "Lắp lại cụm chống trôi và thử vận hành máy." },

// ErrorGuideline 10
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000040"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000010"), StepOrder = 1, StepDescription = "Ngắt máy và tháo bộ chốt vải ra ngoài." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000041"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000010"), StepOrder = 2, StepDescription = "Vệ sinh bụi, cặn trong khe chốt." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000042"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000010"), StepOrder = 3, StepDescription = "Kiểm tra cơ cấu kẹp có bị cong vênh." },
new ErrorFixStep { Id = Guid.Parse("30000000-0000-0000-0000-000000000043"), ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000010"), StepOrder = 4, StepDescription = "Thay chốt mới nếu hư hỏng hoặc chỉnh lại khe hở kẹp vải." }

            );
        }
    }
}
