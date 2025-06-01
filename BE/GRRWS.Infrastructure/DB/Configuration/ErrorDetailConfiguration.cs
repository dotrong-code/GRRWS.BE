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
                    ReportId = Guid.Parse("e1f2a3b4-0006-0006-0006-300000000006"), // Juki DDL-8700 Unit 3
                    ErrorId = Guid.Parse("e1d1a444-0004-0004-0004-000000000004"), // CHAY_MOTOR
                    TaskId = Guid.Parse("b1c2d3e4-0002-0002-0002-100000000002"), // Fix Motor Issue
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