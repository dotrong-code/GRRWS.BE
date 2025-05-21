using GRRWS.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace GRRWS.Infrastructure.DB.Configuration
{
    public class ErrorDetailConfiguration : IEntityTypeConfiguration<ErrorDetail>
    {
        public void Configure(EntityTypeBuilder<ErrorDetail> builder)
        {
            builder.HasData(
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0001-0001-0001-300000000001"), // Juki DDL-8700 Unit 4
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // CHAY_MOTOR
                    TaskId = Guid.Parse("b1c2d3e4-0002-0002-0002-100000000002"), // Fix Motor Issue
                },
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0002-0002-0002-300000000002"), // Juki DDL-8700 Unit 7
                    ErrorId = Guid.Parse("e1d1abcf-0014-0014-0014-000000000014"), // KIM_LOI_TAM
                    TaskId = Guid.Parse("b1c2d3e4-0004-0004-0004-100000000004"), // Fix Needle Issue
                },
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0003-0003-0003-300000000003"), // Brother B957 Unit 1
                    ErrorId = Guid.Parse("e1d1afff-0013-0013-0013-000000000013"), // CAM_BIEN_VAI_KHONG_NHAN
                    TaskId = Guid.Parse("b1c2d3e4-0008-0008-0008-100000000008"), // Fix Thread Issue
                },
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0004-0004-0004-300000000004"), // Singer 4452 Unit 1
                    ErrorId = Guid.Parse("e1d1a133-0025-0025-0025-000000000025"), // DAY_KHOI_DONG_LOI
                    TaskId = Guid.Parse("b1c2d3e4-0009-0009-0009-100000000009"), // Repair Singer 4452 Unit 1
                },
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0005-0005-0005-300000000005"), // Juki DDL-9000C Unit 2
                    ErrorId = Guid.Parse("e1d1a129-0021-0021-0021-000000000021"), // MO_TROI_CHI
                    TaskId = null
                },
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0006-0006-0006-300000000006"), // Juki DDL-8700 Unit 3
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // CHAY_MOTOR
                    TaskId = Guid.Parse("b1c2d3e4-0002-0002-0002-100000000002"), // Fix Motor Issue
                },
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0007-0007-0007-300000000007"), // Brother B957 Unit 2
                    ErrorId = Guid.Parse("e1d1addd-0011-0011-0011-000000000011"), // VONG_BAC_MON
                    TaskId = null
                },
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0008-0008-0008-300000000008"), // Singer 4452 Unit 3
                    ErrorId = Guid.Parse("e1d1a128-0020-0020-0020-000000000020"), // ROONG_KHONG_DU_SIEU
                    TaskId = Guid.Parse("b1c2d3e4-0010-0010-0010-100000000010"), // Adjust Fabric Feed
                },
                new ErrorDetail
                {
                    ReportId = Guid.Parse("e1f2a3b4-0009-0009-0009-300000000009"), // Brother B957 Unit 3
                    ErrorId = Guid.Parse("e1d1a136-0028-0028-0028-000000000028"), // DAU_BO_NHIEU
                    TaskId = null
                }
            );
        }
    }
}