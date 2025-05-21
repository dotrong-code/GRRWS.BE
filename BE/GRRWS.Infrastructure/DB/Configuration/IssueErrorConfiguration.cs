using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class IssueErrorConfiguration : IEntityTypeConfiguration<IssueError>
    {
        public void Configure(EntityTypeBuilder<IssueError> builder)
        {

            builder.HasKey(ie => new { ie.IssueId, ie.ErrorId });


            builder.HasData(
                new IssueError
                {
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // MAY_NONG
                    ErrorId = Guid.Parse("e1d1a123-0015-0015-0015-000000000015")  // LOI_QUAT_GIO

                },
                new IssueError
                {
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY

                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014")  // KIM_LOI_TAM

                },
                new IssueError
                {
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY

                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004")  // CHAY_MOTOR

                },
                new IssueError
                {
                    IssueId = Guid.Parse("44444444-4444-4444-4444-444444444444"), // CHAY_DAU

                    ErrorId = Guid.Parse("e1d1a666-0006-0006-0006-000000000006")  // GIOANG_DAU_BI_RO

                },
                new IssueError
                {
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO

                    ErrorId = Guid.Parse("e1d1addd-0011-0011-0011-000000000011")  // VONG_BAC_MON
                },
                new IssueError
                {
                    IssueId = Guid.Parse("66666666-6666-6666-6666-666666666666"), // RACH_VAI
                    ErrorId = Guid.Parse("e1d1aeee-0012-0012-0012-000000000012")  // DAO_CAT_KHONG_SAC
                },
                new IssueError
                {
                    IssueId = Guid.Parse("77777777-7777-7777-7777-777777777777"), // LUOI_KIM
                    ErrorId = Guid.Parse("e1d1a132-0024-0024-0024-000000000024")  // KIM_CHAM_VAI

                },
                new IssueError
                {
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI

                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021")  // MO_TROI_CHI

                },
                new IssueError
                {
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // KHONG_CUON_CHI

                    ErrorId = Guid.Parse("e1d1afff-0013-0013-0013-000000000013")  // CAM_BIEN_VAI_KHONG_NHAN

                },
                new IssueError
                {
                    IssueId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // MAY_CHAY_CHAM

                    ErrorId = Guid.Parse("e1d1a333-0003-0003-0003-000000000003")  // MAY_CHAY_LUON_LUOT

                },
                new IssueError
                {
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU

                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026")  // BULONG_LONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008")  // LOI_MACH_DIEU_KHIEN

                },
                new IssueError
                {
                    IssueId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // BAN_DAP_KHONG_HOAT_DONG

                    ErrorId = Guid.Parse("e1d1a111-0001-0001-0001-000000000001")  // HONG_BAN_DAP
                },
                new IssueError
                {
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // VAI_BI_NHAN
                    ErrorId = Guid.Parse("e1d1a128-0020-0020-0020-000000000020")  // ROONG_KHONG_DU_SIEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), // KIM_KHONG_DI_CHUYEN
                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016")  // TRUC_CHINH_LAC
                },
                new IssueError
                {
                    IssueId = Guid.Parse("23232323-2323-2323-2323-232323232323"), // DAY_CUROA_LOI
                    ErrorId = Guid.Parse("e1d1a222-0002-0002-0002-000000000002")  // DAYCUROA_TRUOT
                },
                new IssueError
                {
                    IssueId = Guid.Parse("23232323-2323-2323-2323-232323232323"), // DAY_CUROA_LOI
                    ErrorId = Guid.Parse("e1d1a133-0025-0025-0025-000000000025")  // DAY_KHOI_DONG_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("34343434-3434-3434-3434-343434343434"), // CHI_DUOI_LOI
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021")  // MO_TROI_CHI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("34343434-3434-3434-3434-343434343434"), // CHI_DUOI_LOI
                    ErrorId = Guid.Parse("e1d1afff-0013-0013-0013-000000000013")  // CAM_BIEN_VAI_KHONG_NHAN
                },
                new IssueError
                {
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // VAI_BI_NHAN
                    ErrorId = Guid.Parse("e1d1a126-0018-0018-0018-000000000018")  // MAT_BO_NHO_LUU_THONG_SO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // ONG_CHI_LOI
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026")  // BULONG_LONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // ONG_CHI_LOI
                    ErrorId = Guid.Parse("e1d1a131-0023-0023-0023-000000000023")  // CAM_TAY_KHONG_AN_KHOP
                },
                new IssueError
                {
                    IssueId = Guid.Parse("45454545-4545-4545-4545-454545454545"), // MAY_TU_DUNG
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008")  // LOI_MACH_DIEU_KHIEN
                },
                new IssueError
                {
                    IssueId = Guid.Parse("45454545-4545-4545-4545-454545454545"), // MAY_TU_DUNG
                    ErrorId = Guid.Parse("e1d1a137-0029-0029-0029-000000000029")  // QUAT_THONG_GIO_YEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    ErrorId = Guid.Parse("e1d1a135-0027-0027-0027-000000000027")  // MACH_DEN_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), // KIM_KHONG_DI_CHUYEN
                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005")  // KHOA_KIM_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    ErrorId = Guid.Parse("e1d1a130-0022-0022-0022-000000000022")  // BANH_RANG_MON
                },
                new IssueError
                {
                    IssueId = Guid.Parse("34343434-3434-3434-3434-343434343434"), // CHI_DUOI_LOI
                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016")  // TRUC_CHINH_LAC
                },
                new IssueError
                {
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // KHONG_CUON_CHI
                    ErrorId = Guid.Parse("e1d1a135-0027-0027-0027-000000000027")  // MACH_DEN_LOI
                }
            );
        }
    }
}
