using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class IssueTechnicalSymptomConfiguration : IEntityTypeConfiguration<IssueTechnicalSymptom>
    {
        public void Configure(EntityTypeBuilder<IssueTechnicalSymptom> builder)
        {

            builder.HasKey(its => new { its.IssueId, its.TechnicalSymptomId });

            builder.HasData(
                // MAY_NONG -> MAY_TOA_NHIET_QUA_MUC, MUI_KHET_TU_MAY
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    TechnicalSymptomId = Guid.Parse("a1a1a1a1-1111-1111-1111-111111111111")
                },
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    TechnicalSymptomId = Guid.Parse("c2c2c2c2-2222-2222-2222-222222222224")
                },
                // KIM_GAY -> KIM_GAY_THUONG_XUYEN
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    TechnicalSymptomId = Guid.Parse("a2a2a2a2-2222-2222-2222-222222222222")
                },
                // MAY_KHONG_CHAY -> MAY_KHONG_NHAN_DIEN
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    TechnicalSymptomId = Guid.Parse("a3a3a3a3-3333-3333-3333-333333333333")
                },
                // CHAY_DAU -> RO_RI_DAU_BOI_TRON, MUI_DAU_BOI_TRON
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    TechnicalSymptomId = Guid.Parse("a4a4a4a4-4444-4444-4444-444444444444")
                },
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                    TechnicalSymptomId = Guid.Parse("b8b8b8b8-8888-8888-8888-888888888889")
                }
                ,
                // KEU_TO -> TIENG_ON_CO_HOC, MAY_RUNG_LAC_MANH
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    TechnicalSymptomId = Guid.Parse("a5a5a5a5-5555-5555-5555-555555555555")
                },
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("55555555-5555-5555-5555-555555555555"),
                    TechnicalSymptomId = Guid.Parse("b7b7b7b7-7777-7777-7777-777777777778")
                },
                // RACH_VAI -> HU_HONG_VAI
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("66666666-6666-6666-6666-666666666666"),
                    TechnicalSymptomId = Guid.Parse("a6a6a6a6-6666-6666-6666-666666666666")
                },
                // DUONG_MAY_LOI -> DUONG_MAY_KHONG_ON_DINH
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("77777777-7777-7777-7777-777777777777"),
                    TechnicalSymptomId = Guid.Parse("a7a7a7a7-7777-7777-7777-777777777777")
                },
                // DUT_CHI -> CHI_MAY_DUT_LIEN_TUC
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("88888888-8888-8888-8888-888888888888"),
                    TechnicalSymptomId = Guid.Parse("a8a8a8a8-8888-8888-8888-888888888888")
                },
                // MAY_CHOP_TAT -> MAY_HOAT_DONG_GIAN_DOAN
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("99999999-9999-9999-9999-999999999999"),
                    TechnicalSymptomId = Guid.Parse("a9a9a9a9-9999-9999-9999-999999999999")
                },
                // MAY_CHAY_CHAM -> TOC_DO_MAY_CHAM
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"),
                    TechnicalSymptomId = Guid.Parse("b1b1b1b1-1111-1111-1111-111111111112")
                },
                // CHI_KHONG_DEU -> CANG_CHI_KHONG_DEU
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"),
                    TechnicalSymptomId = Guid.Parse("b2b2b2b2-2222-2222-2222-222222222223")
                },
                // DEN_BAO_LOI -> DEN_BAO_LOI_KICH_HOAT
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("cccccccc-cccc-cccc-cccc-cccccccccccc"),
                    TechnicalSymptomId = Guid.Parse("b3b3b3b3-3333-3333-3333-333333333334")
                },
                // BAN_DAP_KHONG_HOAT_DONG -> BAN_DAP_KHONG_PHAN_HOI
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("dddddddd-dddd-dddd-dddd-dddddddddddd"),
                    TechnicalSymptomId = Guid.Parse("b4b4b4b4-4444-4444-4444-444444444445")
                },
                // DEN_KHONG_SANG -> DEN_CHIEU_SANG_HONG
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"),
                    TechnicalSymptomId = Guid.Parse("b5b5b5b5-5555-5555-5555-555555555556")
                },
                // KIM_KHONG_DI_CHUYEN -> KIM_KHONG_DI_CHUYEN
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("ffffffff-ffff-ffff-ffff-ffffffffffff"),
                    TechnicalSymptomId = Guid.Parse("b6b6b6b6-6666-6666-6666-666666666667")
                },
                // MAY_RUNG_MANH -> MAY_RUNG_LAC_MANH, TIENG_ON_CO_HOC
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"),
                    TechnicalSymptomId = Guid.Parse("b7b7b7b7-7777-7777-7777-777777777778")
                },
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("12121212-1212-1212-1212-121212121212"),
                    TechnicalSymptomId = Guid.Parse("a5a5a5a5-5555-5555-5555-555555555555")
                },
                // MUI_DAU_NANG -> MUI_DAU_BOI_TRON, RO_RI_DAU_BOI_TRON
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("23232323-2323-2323-2323-232323232323"),
                    TechnicalSymptomId = Guid.Parse("b8b8b8b8-8888-8888-8888-888888888889")
                },
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("23232323-2323-2323-2323-232323232323"),
                    TechnicalSymptomId = Guid.Parse("a4a4a4a4-4444-4444-4444-444444444444")
                },
                // VAI_KHONG_DI_CHUYEN -> VAI_KHONG_TIEN
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("abababab-abab-abab-abab-abababababab"),
                    TechnicalSymptomId = Guid.Parse("b9b9b9b9-9999-9999-9999-999999999990")
                },
                // MAY_TU_DUNG -> MAY_TU_NGUNG
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("45454545-4545-4545-4545-454545454545"),
                    TechnicalSymptomId = Guid.Parse("c1c1c1c1-1111-1111-1111-111111111113")
                },
                // MUI_KHET -> MUI_KHET_TU_MAY
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("56565656-5656-5656-5656-565656565656"),
                    TechnicalSymptomId = Guid.Parse("c2c2c2c2-2222-2222-2222-222222222224")
                },

                // MAY_KHONG_LOG -> CHI_KHONG_LEN, THOI_DIEM_MOC_SAI
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                    TechnicalSymptomId = Guid.Parse("d1d1d1d1-1111-1111-1111-111111111114")
                },
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("10101010-1010-1010-1010-101010101010"),
                    TechnicalSymptomId = Guid.Parse("d2d2d2d2-2222-2222-2222-222222222225")
                },
                // TIENG_XI_XI -> TIENG_KHI_XI
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("20202020-2020-2020-2020-202020202020"),
                    TechnicalSymptomId = Guid.Parse("d3d3d3d3-3333-3333-3333-333333333336")
                },
                // VAI_BI_KET -> RANG_CUA_KET
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("30303030-3030-3030-3030-303030303030"),
                    TechnicalSymptomId = Guid.Parse("d4d4d4d4-4444-4444-4444-444444444447")
                },
                // CHI_QUAN_LUNG_TUNG -> CANG_CHI_DUOI_LONG
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("40404040-4040-4040-4040-404040404040"),
                    TechnicalSymptomId = Guid.Parse("d5d5d5d5-5555-5555-5555-555555555558")
                },
                // KIM_DAM_VAO_VAI -> KICH_THUOC_KIM_SAI
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("60606060-6060-6060-6060-606060606060"),
                    TechnicalSymptomId = Guid.Parse("d6d6d6d6-6666-6666-6666-666666666669")
                },
                // MAY_BI_GIAT -> NGUON_DIEN_KHONG_ON_DINH
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("a3a3a3a3-a3a3-a3a3-a3a3-a3a3a3a3a3a3"),
                    TechnicalSymptomId = Guid.Parse("d7d7d7d7-7777-7777-7777-777777777780")
                },
                // MAY_BOC_KHOI -> DONG_CO_QUA_NONG
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("f8f8f8f8-f8f8-f8f8-f8f8-f8f8f8f8f8f8"),
                    TechnicalSymptomId = Guid.Parse("d8d8d8d8-8888-8888-8888-888888888891")
                },
                // TIENG_LACH_CACH -> BO_PHAN_CO_KHI_LONG
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("70707070-7070-7070-7070-707070707070"),
                    TechnicalSymptomId = Guid.Parse("e1e1e1e1-1111-1111-1111-111111111115")
                },
                // MAY_BI_DO -> NGUON_DIEN_HONG
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("80808080-8080-8080-8080-808080808080"),
                    TechnicalSymptomId = Guid.Parse("e2e2e2e2-2222-2222-2222-222222222226")
                },
                // CHI_KHONG_XUYEN_VAI -> KIM_MAI_MON
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("90909090-9090-9090-9090-909090909090"),
                    TechnicalSymptomId = Guid.Parse("e3e3e3e3-3333-3333-3333-333333333337")
                },
                // MAY_CHAY_NHUNG_KHONG_MAY -> THANH_KIM_NGO
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("1a1a1a1a-1a1a-1a1a-1a1a-1a1a1a1a1a1a"),
                    TechnicalSymptomId = Guid.Parse("e4e4e4e4-4444-4444-4444-444444444448")
                },
                // MUI_NONG_TU_MAY -> LINH_KIEN_DIEN_QUA_NONG
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("2b2b2b2b-2b2b-2b2b-2b2b-2b2b2b2b2b2b"),
                    TechnicalSymptomId = Guid.Parse("e5e4e5e4-5554-5555-4444-555555555559")
                },
                // CHAN_VIT_NANG -> CO_CAU_CHAN_VIT_CUNG
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("3c3c3c3c-3c3c-3c3c-3c3c-3c3c3c3c3c3c"),
                    TechnicalSymptomId = Guid.Parse("e6e6e6e6-6666-6666-6666-666666666670")
                },
                // TIENG_RE_RE -> O_CAU_DONG_CO_MAI_MON
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("4d4d4d4d-4d4d-4d4d-4d4d-4d4d4d4d4d4d"),
                    TechnicalSymptomId = Guid.Parse("e7e7e7e7-7777-7777-7777-777777777781")
                },
                // VAI_BI_NHĂN -> AP_LUC_CHAN_VIT_SAI
                new IssueTechnicalSymptom
                {
                    IssueId = Guid.Parse("5e5e5e5e-5e5e-5e5e-5e5e-5e5e5e5e5e5e"),
                    TechnicalSymptomId = Guid.Parse("e8e8e8e8-8888-8888-8888-888888888892")
                }
            );
        }
    }
}
