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
                // Corrected and retained existing valid mappings
                new IssueError
                {
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // MAY_NONG
                    ErrorId = Guid.Parse("e1d1a123-0015-0015-0015-000000000015")  // LOI_QUAT_GIO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"), // MAY_NONG
                    ErrorId = Guid.Parse("e1d1a160-0052-0052-0052-000000000052")  // BO_DIEU_KHIEN_QUA_NHIET
                },
                new IssueError
                {
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014")  // KIM_LOI_TAM
                },
                new IssueError
                {
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"), // KIM_GAY
                    ErrorId = Guid.Parse("e1d1a144-0036-0036-0036-000000000036")  // TRUC_KIM_GAY
                },
                new IssueError
                {
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004")  // CHAY_MOTOR
                },
                new IssueError
                {
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"), // MAY_KHONG_CHAY
                    ErrorId = Guid.Parse("e1d1a142-0034-0034-0034-000000000034")  // DAY_DIEN_NGUON_HO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("44444444-4444-4444-4444-444444444444"), // CHAY_DAU
                    ErrorId = Guid.Parse("e1d1a666-0006-0006-0006-000000000006")  // GIOANG_DAU_BI_RO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("44444444-4444-4444-4444-444444444444"), // CHAY_DAU
                    ErrorId = Guid.Parse("e1d1a136-0028-0028-0028-000000000028")  // DAU_BO_NHIEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    ErrorId = Guid.Parse("e1d1addd-0011-0011-0011-000000000011")  // VONG_BAC_MON
                },
                new IssueError
                {
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"), // KEU_TO
                    ErrorId = Guid.Parse("e1d1a130-0022-0022-0022-000000000022")  // BANH_RANG_MON
                },
                new IssueError
                {
                    IssueId = Guid.Parse("66666666-6666-6666-6666-666666666666"), // RACH_VAI
                    ErrorId = Guid.Parse("e1d1aeee-0012-0012-0012-000000000012")  // DAO_CAT_KHONG_SAC
                },
                new IssueError
                {
                    IssueId = Guid.Parse("66666666-6666-6666-6666-666666666666"), // RACH_VAI
                    ErrorId = Guid.Parse("e1d1a141-0033-0033-0033-000000000033")  // CHAN_VIT_KHONG_DONG_BO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("77777777-7777-7777-7777-777777777777"), // DUONG_MAY_LOI
                    ErrorId = Guid.Parse("e1d1a132-0024-0024-0024-000000000024")  // KIM_CHAM_VAI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("77777777-7777-7777-7777-777777777777"), // DUONG_MAY_LOI
                    ErrorId = Guid.Parse("e1d1a138-0030-0030-0030-000000000030")  // CUA_KIM_LECH
                },
                new IssueError
                {
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021")  // MO_TROI_CHI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"), // DUT_CHI
                    ErrorId = Guid.Parse("e1d1a139-0031-0031-0031-000000000031")  // ONG_DAN_CHI_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // MAY_CHOP_TAT
                    ErrorId = Guid.Parse("e1d1afff-0013-0013-0013-000000000013")  // CAM_BIEN_VAI_KHONG_NHAN
                },
                new IssueError
                {
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"), // MAY_CHOP_TAT
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008")  // LOI_MACH_DIEU_KHIEN
                },
                new IssueError
                {
                    IssueId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // MAY_CHAY_CHAM
                    ErrorId = Guid.Parse("e1d1a333-0003-0003-0003-000000000003")  // MAY_CHAY_LUON_LUOT
                },
                new IssueError
                {
                    IssueId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), // MAY_CHAY_CHAM
                    ErrorId = Guid.Parse("e1d1a156-0048-0048-0048-000000000048")  // BO_TRUYEN_DONG_YEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041")  // MO_CANG_CHI_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), // CHI_KHONG_DEU
                    ErrorId = Guid.Parse("e1d1a167-0059-0059-0059-000000000059")  // LO_XO_CANG_CHI_YEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    ErrorId = Guid.Parse("e1d1a888-0008-0008-0008-000000000008")  // LOI_MACH_DIEU_KHIEN
                },
                new IssueError
                {
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"), // DEN_BAO_LOI
                    ErrorId = Guid.Parse("e1d1a135-0027-0027-0027-000000000027")  // MACH_DEN_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"), // BAN_DAP_KHONG_HOAT_DONG
                    ErrorId = Guid.Parse("e1d1a111-0001-0001-0001-000000000001")  // HONG_BAN_DAP
                },
                new IssueError
                {
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // DEN_KHONG_SANG
                    ErrorId = Guid.Parse("e1d1a125-0017-0017-0017-000000000017")  // DUI_DEN_CHAY
                },
                new IssueError
                {
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), // DEN_KHONG_SANG
                    ErrorId = Guid.Parse("e1d1a135-0027-0027-0027-000000000027")  // MACH_DEN_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), // KIM_KHONG_DI_CHUYEN
                    ErrorId = Guid.Parse("e1d1a555-0005-0005-0005-000000000005")  // KHOA_KIM_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"), // KIM_KHONG_DI_CHUYEN
                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016")  // TRUC_CHINH_LAC
                },
                new IssueError
                {
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // MAY_RUNG_LAC
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026")  // BULONG_LONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"), // MAY_RUNG_LAC
                    ErrorId = Guid.Parse("e1d1a168-0060-0060-0060-000000000060")  // MO_DONG_CO_MON
                },
                new IssueError
                {
                    IssueId = Guid.Parse("23232323-2323-2323-2323-232323232323"), // MUI_DAU_NANG
                    ErrorId = Guid.Parse("e1d1a666-0006-0006-0006-000000000006")  // GIOANG_DAU_BI_RO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("23232323-2323-2323-2323-232323232323"), // MUI_DAU_NANG
                    ErrorId = Guid.Parse("e1d1a136-0028-0028-0028-000000000028")  // DAU_BO_NHIEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("abababab-abab-abab-abab-abababababab"), // VAI_KHONG_DI_CHUYEN
                    ErrorId = Guid.Parse("e1d1a128-0020-0020-0020-000000000020")  // ROONG_KHONG_DU_SIEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("abababab-abab-abab-abab-abababababab"), // VAI_KHONG_DI_CHUYEN
                    ErrorId = Guid.Parse("e1d1a147-0039-0039-0039-000000000039")  // RANH_CUA_MON
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
                    IssueId = Guid.Parse("56565656-5656-5656-5656-565656565656"), // MUI_KHET
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004")  // CHAY_MOTOR
                },
                new IssueError
                {
                    IssueId = Guid.Parse("56565656-5656-5656-5656-565656565656"), // MUI_KHET
                    ErrorId = Guid.Parse("e1d1a160-0052-0052-0052-000000000052")  // BO_DIEU_KHIEN_QUA_NHIET
                },
                // Corrected mappings for CHI_DUOI_LOI (replaced invalid IssueId)
                new IssueError
                {
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021")  // MO_TROI_CHI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    ErrorId = Guid.Parse("e1d1a153-0045-0045-0045-000000000045")  // PHAO_CHI_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("e1e1e1e1-e1e1-e1e1-e1e1-e1e1e1e1e1e1"), // CHI_DUOI_LOI
                    ErrorId = Guid.Parse("e1d1a154-0046-0046-0046-000000000046")  // BO_CAU_CHI_LOI
                },
                // New mappings for additional issues
                new IssueError
                {
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"), // MAY_KHONG_LEN_CHI
                    ErrorId = Guid.Parse("e1d1a153-0045-0045-0045-000000000045")  // PHAO_CHI_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"), // MAY_KHONG_LEN_CHI
                    ErrorId = Guid.Parse("e1d1a154-0046-0046-0046-000000000046")  // BO_CAU_CHI_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("20202020-2020-2020-2020-202020202020"), // TIENG_XI_XI
                    ErrorId = Guid.Parse("e1d1a155-0047-0047-0047-000000000047")  // ONG_DAU_BI_TAT
                },
                new IssueError
                {
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    ErrorId = Guid.Parse("e1d1abbb-0010-0010-0010-000000000010")  // CHOT_VAI_KET
                },
                new IssueError
                {
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"), // VAI_BI_KET
                    ErrorId = Guid.Parse("e1d1a141-0033-0033-0033-000000000033")  // CHAN_VIT_KHONG_DONG_BO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    ErrorId = Guid.Parse("e1d1a139-0031-0031-0031-000000000031")  // ONG_DAN_CHI_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"), // CHI_QUAN_LUNG_TUNG
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041")  // MO_CANG_CHI_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("60606060-6060-6060-6060-606060606060"), // KIM_DAM_VAO_VAI
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014")  // KIM_LOI_TAM
                },
                new IssueError
                {
                    IssueId = Guid.Parse("70707070-7070-7070-7070-707070707070"), // TIENG_LACH_CACH
                    ErrorId = Guid.Parse("e1d1a130-0022-0022-0022-000000000022")  // BANH_RANG_MON
                },
                new IssueError
                {
                    IssueId = Guid.Parse("70707070-7070-7070-7070-707070707070"), // TIENG_LACH_CACH
                    ErrorId = Guid.Parse("e1d1a162-0054-0054-0054-000000000054")  // RANH_CUA_KHONG_DONG_BO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("80808080-8080-8080-8080-808080808080"), // MAY_BI_DO
                    ErrorId = Guid.Parse("e1d1a142-0034-0034-0034-000000000034")  // DAY_DIEN_NGUON_HO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("80808080-8080-8080-8080-808080808080"), // MAY_BI_DO
                    ErrorId = Guid.Parse("e1d1a163-0055-0055-0055-000000000055")  // BO_NGUON_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("90909090-9090-9090-9090-909090909090"), // CHI_KHONG_XUYEN_VAI
                    ErrorId = Guid.Parse("e1d1a140-0032-0032-0032-000000000032")  // SUOT_CHI_KHONG_QUAY
                },
                new IssueError
                {
                    IssueId = Guid.Parse("1a1a1a1a-1a1a-1a1a-1a1a-1a1a1a1a1a1a"), // MAY_CHAY_NHUNG_KHONG_MAY
                    ErrorId = Guid.Parse("e1d1a158-0050-0050-0050-000000000050")  // CAM_BIEN_KIM_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("2b2b2b2b-2b2b-2b2b-2b2b-2b2b2b2b2b2b"), // MUI_NONG_TU_MAY
                    ErrorId = Guid.Parse("e1d1a145-0037-0037-0037-000000000037")  // CAM_BIEN_NHIET_DO_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("3c3c3c3c-3c3c-3c3c-3c3c-3c3c3c3c3c3c"), // CHAN_VIT_NANG
                    ErrorId = Guid.Parse("e1d1a157-0049-0049-0049-000000000049")  // CHAN_VIT_KHONG_NANG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("4d4d4d4d-4d4d-4d4d-4d4d-4d4d4d4d4d4d"), // TIENG_RE_RE
                    ErrorId = Guid.Parse("e1d1a168-0060-0060-0060-000000000060")  // MO_DONG_CO_MON
                },
                new IssueError
                {
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"), // VAI_BI_NHAN
                    ErrorId = Guid.Parse("e1d1a159-0051-0051-0051-000000000051")  // MO_DAN_VAI_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("6f6f6f6f-6f6f-6f6f-6f6f-6f6f6f6f6f6f"), // MAY_GIAT_DIEN
                    ErrorId = Guid.Parse("e1d1a142-0034-0034-0034-000000000034")  // DAY_DIEN_NGUON_HO
                },
                new IssueError
                {
                    IssueId = Guid.Parse("7a7a7a7a-7a7a-7a7a-7a7a-7a7a7a7a7a7a"), // KIM_DI_SAI
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014")  // KIM_LOI_TAM
                },
                new IssueError
                {
                    IssueId = Guid.Parse("8b8b8b8b-8b8b-8b8b-8b8b-8b8b8b8b8b8b"), // MAY_PHAT_TIENG_LA
                    ErrorId = Guid.Parse("e1d1a146-0038-0038-0038-000000000038")  // MO_HOP_SO_KHONG_KHOP
                },
                new IssueError
                {
                    IssueId = Guid.Parse("9c9c9c9c-9c9c-9c9c-9c9c-9c9c9c9c9c9c"), // CHI_LUNG_TREN_VAI
                    ErrorId = Guid.Parse("e1d1a167-0059-0059-0059-000000000059")  // LO_XO_CANG_CHI_YEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("adadadad-adad-adad-adad-adadadadadad"), // MAY_HOI_RUN
                    ErrorId = Guid.Parse("e1d1a134-0026-0026-0026-000000000026")  // BULONG_LONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("bebebebe-bebe-bebe-bebe-bebebebebebe"), // DAU_BAM_TREN_VAI
                    ErrorId = Guid.Parse("e1d1a136-0028-0028-0028-000000000028")  // DAU_BO_NHIEU
                },
                new IssueError
                {
                    IssueId = Guid.Parse("cfcfcfcf-cfcf-cfcf-cfcf-cfcfcfcfcfcf"), // TIENG_KEN_KET
                    ErrorId = Guid.Parse("e1d1a151-0043-0043-0043-000000000043")  // TRUC_CHUYEN_DONG_KET
                },
                new IssueError
                {
                    IssueId = Guid.Parse("d0d0d0d0-d0d0-d0d0-d0d0-d0d0d0d0d0d0"), // MAY_KHONG_ON_DINH
                    ErrorId = Guid.Parse("e1d1a152-0044-0044-0044-000000000044")  // CAM_BIEN_VAN_TOC_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("f2f2f2f2-f2f2-f2f2-f2f2-f2f2f2f2f2f2"), // KIM_LAM_MUNG
                    ErrorId = Guid.Parse("e1d1a132-0024-0024-0024-000000000024")  // KIM_CHAM_VAI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"), // MAY_BI_GIAT
                    ErrorId = Guid.Parse("e1d1a124-0016-0016-0016-000000000016")  // TRUC_CHINH_LAC
                },
                new IssueError
                {
                    IssueId = Guid.Parse("b4b4b4b4-b4b4-b4b4-b4b4-b4b4b4b4b4b4"), // VAI_DI_SAI
                    ErrorId = Guid.Parse("e1d1a159-0051-0051-0051-000000000051")  // MO_DAN_VAI_HONG
                },
                new IssueError
                {
                    IssueId = Guid.Parse("c5c5c5c5-c5c5-c5c5-c5c5-c5c5c5c5c5c5"), // CHI_TREN_LOI
                    ErrorId = Guid.Parse("e1d1a149-0041-0041-0041-000000000041")  // MO_CANG_CHI_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("d6d6d6d6-d6d6-d6d6-d6d6-d6d6d6d6d6d6"), // MAY_KHONG_MAY_DUOC
                    ErrorId = Guid.Parse("e1d1a158-0050-0050-0050-000000000050")  // CAM_BIEN_KIM_LOI
                },
                new IssueError
                {
                    IssueId = Guid.Parse("e7e7e7e7-e7e7-e7e7-e7e7-e7e7e7e7e7e7"), // TIENG_KHUC_KHAC
                    ErrorId = Guid.Parse("e1d1a151-0043-0043-0043-000000000043")  // TRUC_CHUYEN_DONG_KET
                },
                new IssueError
                {
                    IssueId = Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"), // MAY_BOC_KHOI
                    ErrorId = Guid.Parse("e1d1a160-0052-0052-0052-000000000052")  // BO_DIEU_KHIEN_QUA_NHIET
                },
                new IssueError
                {
                    IssueId = Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"), // MAY_BOC_KHOI
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004")  // CHAY_MOTOR
                }
            );
        }
    }
}