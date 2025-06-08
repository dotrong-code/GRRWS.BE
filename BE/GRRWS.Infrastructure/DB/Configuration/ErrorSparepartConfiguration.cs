using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorSparepartConfiguration : IEntityTypeConfiguration<ErrorSparepart>
    {
        public void Configure(EntityTypeBuilder<ErrorSparepart> builder)
        {
            builder.HasKey(e => new { e.ErrorGuidelineId, e.SparepartId });

            builder.HasData(

                // Guideline 1: Bàn đạp - cần thay bàn đạp + trục quay bàn đạp
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000001"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000003"), QuantityNeeded = 1 }, // Bàn Đạp
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000001"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000026"), QuantityNeeded = 1 }, // Trục Quay Bàn Đạp

                // Guideline 2: Dây curoa trượt - cần thay 2 loại dây
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000002"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000002"), QuantityNeeded = 1 }, // Dây Curoa
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000002"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000022"), QuantityNeeded = 1 }, // Dây Curoa Dự Phòng

                // Guideline 3: Board điều khiển lỗi - cần thay bo điều khiển
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000003"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000029"), QuantityNeeded = 1 },

                // Guideline 4: Cháy motor - thay motor + dây điện nguồn
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000004"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000007"), QuantityNeeded = 1 },
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000004"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000019"), QuantityNeeded = 1 },

                // Guideline 5: Khóa kim hỏng - thay kim + trụ gắn kim
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000005"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000001"), QuantityNeeded = 5 },
                new ErrorSparepart { ErrorGuidelineId = Guid.Parse("20000000-0000-0000-0000-000000000005"), SparepartId = Guid.Parse("10000000-0000-0000-0000-000000000009"), QuantityNeeded = 1 }

                // Các guideline còn lại (6-10): Không cần sparepart, vì chủ yếu chỉ vệ sinh, căn chỉnh, thay gioăng có sẵn trong xưởng.

            // => Không thêm dữ liệu vào cho guideline 6-10
            // Có thể mở rộng thêm nếu bạn muốn ở các lần sau.
            // Để đảm bảo chuẩn như bạn yêu cầu là có vài cái có Sparepart, vài cái không có.

            );
        }
    }
}
