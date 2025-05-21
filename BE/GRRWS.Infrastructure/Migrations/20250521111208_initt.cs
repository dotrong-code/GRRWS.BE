using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GRRWS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Areas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Areas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Areas_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmailTemplates",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    EmailTemplateId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsDelete = table.Column<bool>(type: "bit", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdateBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImageMappingsJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailTemplates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmailTemplates_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Errors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ErrorCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimatedRepairTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    IsCommon = table.Column<bool>(type: "bit", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false),
                    Severity = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Errors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Errors_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Issues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    IssueKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsCommon = table.Column<bool>(type: "bit", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Issues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Issues_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Machines",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MachineName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Machines_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Receiver = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    Enabled = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Notifications_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Spareparts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    SparepartCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SparepartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Specification = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StockQuantity = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Spareparts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Spareparts_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StaffID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProfilePictureUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsEmailConfirmed = table.Column<bool>(type: "bit", nullable: true),
                    IsRegister = table.Column<bool>(type: "bit", nullable: true),
                    Role = table.Column<int>(type: "int", nullable: false),
                    ResetPasswordToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FeedbackId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ZoneName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zones_Areas_AreaId",
                        column: x => x.AreaId,
                        principalTable: "Areas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Zones_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueErrors",
                columns: table => new
                {
                    IssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueErrors", x => new { x.IssueId, x.ErrorId });
                    table.ForeignKey(
                        name: "FK_IssueErrors_Errors_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "Errors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueErrors_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DeviceName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DeviceCode = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SerialNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manufacturer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ManufactureDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    InstallationDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsUnderWarranty = table.Column<bool>(type: "bit", nullable: false),
                    Specifications = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PurchasePrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PositionId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Devices_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Devices_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MachineErrorHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastOccurredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineErrorHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineErrorHistories_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineErrorHistories_Errors_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "Errors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachineErrorHistories_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MachineIssueHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    MachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastOccurredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineIssueHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MachineIssueHistories_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MachineIssueHistories_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MachineIssueHistories_Machines_MachineId",
                        column: x => x.MachineId,
                        principalTable: "Machines",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ErrorSpareparts",
                columns: table => new
                {
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SparepartId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuantityNeeded = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorSpareparts", x => new { x.ErrorId, x.SparepartId });
                    table.ForeignKey(
                        name: "FK_ErrorSpareparts_Errors_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "Errors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorSpareparts_Spareparts_SparepartId",
                        column: x => x.SparepartId,
                        principalTable: "Spareparts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Feedbacks_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Feedbacks_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskDescription = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExpectedTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    AssigneeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceReturnTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeviceCondition = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_AssigneeId",
                        column: x => x.AssigneeId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceErrorHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastOccurredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceErrorHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceErrorHistories_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceErrorHistories_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceErrorHistories_Errors_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "Errors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ActionType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ComponentCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ComponentName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RelatedTaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceHistories_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceHistories_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceIssueHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastOccurredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceIssueHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceIssueHistories_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceIssueHistories_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DeviceIssueHistories_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceWarranties",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyReason = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WarrantyEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WarrantyCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DocumentUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SparePartCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SparePartName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceWarranties", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceWarranties_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceWarranties_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DeviceWarrantyHistories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    DeviceDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    SendDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ReceiveDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Provider = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceWarrantyHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeviceWarrantyHistories_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceWarrantyHistories_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Index = table.Column<int>(type: "int", nullable: false),
                    ZoneId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Positions_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Positions_Zones_ZoneId",
                        column: x => x.ZoneId,
                        principalTable: "Zones",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RequestTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RequestedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SerderId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requests_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requests_Devices_DeviceId",
                        column: x => x.DeviceId,
                        principalTable: "Devices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Requests_Users_SerderId",
                        column: x => x.SerderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RepairSpareparts",
                columns: table => new
                {
                    SpareId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairSpareparts", x => new { x.SpareId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_RepairSpareparts_Spareparts_SpareId",
                        column: x => x.SpareId,
                        principalTable: "Spareparts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RepairSpareparts_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reports_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestIssues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestIssues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestIssues_Issues_IssueId",
                        column: x => x.IssueId,
                        principalTable: "Issues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RequestIssues_Requests_RequestId",
                        column: x => x.RequestId,
                        principalTable: "Requests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ErrorDetails",
                columns: table => new
                {
                    ReportId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TaskId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorDetails", x => new { x.ReportId, x.ErrorId });
                    table.ForeignKey(
                        name: "FK_ErrorDetails_Errors_ErrorId",
                        column: x => x.ErrorId,
                        principalTable: "Errors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorDetails_Reports_ReportId",
                        column: x => x.ReportId,
                        principalTable: "Reports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ErrorDetails_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RequestIssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_BaseEntity_Id",
                        column: x => x.Id,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Images_RequestIssues_RequestIssueId",
                        column: x => x.RequestIssueId,
                        principalTable: "RequestIssues",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "BaseEntity",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "ModifiedBy", "ModifiedDate" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2854), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2854) },
                    { new Guid("11111111-1111-1111-1111-111111111112"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3790), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3790) },
                    { new Guid("11111111-1111-1111-1111-111111111121"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4408), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4408) },
                    { new Guid("11111111-1111-1111-1111-111111111122"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6816), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6817) },
                    { new Guid("12121212-1212-1212-1212-121212121212"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2900), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2901) },
                    { new Guid("21111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(1954), null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2861), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2861) },
                    { new Guid("22222222-2222-2222-2222-222222222223"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3796), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3796) },
                    { new Guid("22222222-2222-2222-2222-222222222232"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4411), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4412) },
                    { new Guid("22222222-2222-2222-2222-222222222233"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6822), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6822) },
                    { new Guid("23232323-2323-2323-2323-232323232323"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2903), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2904) },
                    { new Guid("32222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(1977), null, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2863), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2864) },
                    { new Guid("33333333-3333-3333-3333-333333333334"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3803), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3803) },
                    { new Guid("33333333-3333-3333-3333-333333333343"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4415), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4416) },
                    { new Guid("33333333-3333-3333-3333-333333333344"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6827), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6827) },
                    { new Guid("34343434-3434-3434-3434-343434343434"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2906), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2907) },
                    { new Guid("43333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(1980), null, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2866), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2866) },
                    { new Guid("44444444-4444-4444-4444-444444444445"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3807), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3808) },
                    { new Guid("44444444-4444-4444-4444-444444444454"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4418), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4418) },
                    { new Guid("44444444-4444-4444-4444-444444444455"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6832), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6832) },
                    { new Guid("45454545-4545-4545-4545-454545454545"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2910), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2910) },
                    { new Guid("54444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2044), null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2868), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2869) },
                    { new Guid("55555555-5555-5555-5555-555555555556"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3812), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3813) },
                    { new Guid("55555555-5555-5555-5555-555555555565"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4421), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4421) },
                    { new Guid("55555555-5555-5555-5555-555555555566"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6840), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6840) },
                    { new Guid("56565656-5656-5656-5656-565656565656"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2913), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2913) },
                    { new Guid("65555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2046), null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2871), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2871) },
                    { new Guid("66666666-6666-6666-6666-666666666667"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3818), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3818) },
                    { new Guid("66666666-6666-6666-6666-666666666676"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4424), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4424) },
                    { new Guid("66666666-6666-6666-6666-666666666677"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6846), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6846) },
                    { new Guid("77777777-7777-7777-7777-777777777777"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2876), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2876) },
                    { new Guid("77777777-7777-7777-7777-777777777778"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3823), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3823) },
                    { new Guid("77777777-7777-7777-7777-777777777787"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4427), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4427) },
                    { new Guid("77777777-7777-7777-7777-777777777788"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6850), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6851) },
                    { new Guid("88888888-8888-8888-8888-888888888888"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2878), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2879) },
                    { new Guid("88888888-8888-8888-8888-888888888889"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3827), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3828) },
                    { new Guid("88888888-8888-8888-8888-888888888898"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4430), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4430) },
                    { new Guid("88888888-8888-8888-8888-888888888899"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6880), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6881) },
                    { new Guid("99999999-9999-9999-9999-999999991010"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6888), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6888) },
                    { new Guid("99999999-9999-9999-9999-999999999109"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4434), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4434) },
                    { new Guid("99999999-9999-9999-9999-999999999910"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3836), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3836) },
                    { new Guid("99999999-9999-9999-9999-999999999999"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2881), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2881) },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2884), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2884) },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3842), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(3842) },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaba"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4437), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(4437) },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaabb"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6893), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6894) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2886), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2886) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2888), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2888) },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2890), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2890) },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2892), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2893) },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2897), null, new DateTime(2025, 5, 21, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(2897) }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "Description", "DeviceCode", "DeviceName", "InstallationDate", "IsUnderWarranty", "MachineId", "ManufactureDate", "Manufacturer", "Model", "PhotoUrl", "PositionId", "PurchasePrice", "SerialNumber", "Specifications", "Status", "Supplier" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111112"), "Industrial sewing machine for heavy fabrics", "SM001", "Sewing Machine 1", new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "Model A", "http://example.com/photos/sm001.jpg", null, 1500.00m, "SN001", "{\"speed\": \"1200 SPM\", \"type\": \"Industrial\"}", "Active", "Brother Industries" },
                    { new Guid("22222222-2222-2222-2222-222222222223"), "High-speed sewing machine", "SM002", "Sewing Machine 2", new DateTime(2019, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2019, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "Model B", "http://example.com/photos/sm002.jpg", null, 2000.00m, "SN002", "{\"speed\": \"1500 SPM\", \"type\": \"High-Speed\"}", "InRepair", "Juki Corporation" },
                    { new Guid("33333333-3333-3333-3333-333333333334"), "Fabric cutting machine", "CM001", "Cutting Machine 1", new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, new DateTime(2021, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pegasus", "Model C", "http://example.com/photos/cm001.jpg", null, 1200.00m, "SN003", "{\"blade\": \"10 inch\", \"type\": \"Rotary\"}", "Active", "Pegasus Ltd" },
                    { new Guid("44444444-4444-4444-4444-444444444445"), "Computerized embroidery machine", "EM001", "Embroidery Machine 1", new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, new DateTime(2020, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bernina", "Model D", "http://example.com/photos/em001.jpg", null, 2500.00m, "SN004", "{\"stitches\": \"1000\", \"type\": \"Computerized\"}", "Active", "Bernina International" },
                    { new Guid("55555555-5555-5555-5555-555555555556"), "Heavy-duty sewing machine", "SM003", "Sewing Machine 3", new DateTime(2019, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2018, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "Model E", "http://example.com/photos/sm003.jpg", null, 1800.00m, "SN005", "{\"speed\": \"1100 SPM\", \"type\": \"Heavy-Duty\"}", "Retired", "Singer Corporation" },
                    { new Guid("66666666-6666-6666-6666-666666666667"), "Compact sewing machine", "SM004", "Sewing Machine 4", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "Model F", "http://example.com/photos/sm004.jpg", null, 1000.00m, "SN006", "{\"speed\": \"800 SPM\", \"type\": \"Compact\"}", "Active", "Brother Industries" },
                    { new Guid("77777777-7777-7777-7777-777777777778"), "Automatic fabric cutting machine", "CM002", "Cutting Machine 2", new DateTime(2021, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2020, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "Model G", "http://example.com/photos/cm002.jpg", null, 2200.00m, "SN007", "{\"blade\": \"12 inch\", \"type\": \"Automatic\"}", "InRepair", "Juki Corporation" },
                    { new Guid("88888888-8888-8888-8888-888888888889"), "Multi-needle embroidery machine", "EM002", "Embroidery Machine 2", new DateTime(2019, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, new DateTime(2019, 9, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bernina", "Model H", "http://example.com/photos/em002.jpg", null, 3000.00m, "SN008", "{\"stitches\": \"1200\", \"type\": \"Multi-Needle\"}", "Active", "Bernina International" },
                    { new Guid("99999999-9999-9999-9999-999999999910"), "Portable sewing machine", "SM005", "Sewing Machine 5", new DateTime(2021, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, new DateTime(2021, 7, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "Model I", "http://example.com/photos/sm005.jpg", null, 900.00m, "SN009", "{\"speed\": \"900 SPM\", \"type\": \"Portable\"}", "Active", "Singer Corporation" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"), "Overlock sewing machine", "SM006", "Sewing Machine 6", new DateTime(2020, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, new DateTime(2020, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Pegasus", "Model J", "http://example.com/photos/sm006.jpg", null, 1700.00m, "SN010", "{\"speed\": \"1300 SPM\", \"type\": \"Overlock\"}", "Active", "Pegasus Ltd" }
                });

            migrationBuilder.InsertData(
                table: "Errors",
                columns: new[] { "Id", "Description", "ErrorCode", "EstimatedRepairTime", "IsCommon", "Name", "OccurrenceCount", "Severity" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111121"), "Motor stops working or runs irregularly.", "ERR001", new TimeSpan(0, 2, 0, 0, 0), true, "Motor Failure", 5, "High" },
                    { new Guid("22222222-2222-2222-2222-222222222232"), "Needle breaks during operation.", "ERR002", new TimeSpan(0, 1, 0, 0, 0), true, "Needle Breakage", 8, "Medium" },
                    { new Guid("33333333-3333-3333-3333-333333333343"), "Incorrect thread tension causing uneven stitches.", "ERR003", new TimeSpan(0, 1, 30, 0, 0), true, "Thread Tension Issue", 10, "Medium" },
                    { new Guid("44444444-4444-4444-4444-444444444454"), "Oil leakage from machine components.", "ERR004", new TimeSpan(0, 3, 0, 0, 0), false, "Oil Leak", 2, "High" },
                    { new Guid("55555555-5555-5555-5555-555555555565"), "Machine overheats after prolonged use.", "ERR005", new TimeSpan(0, 2, 30, 0, 0), true, "Overheating", 6, "High" },
                    { new Guid("66666666-6666-6666-6666-666666666676"), "Drive belt slips or breaks.", "ERR006", new TimeSpan(0, 1, 30, 0, 0), false, "Belt Slippage", 3, "Medium" },
                    { new Guid("77777777-7777-7777-7777-777777777787"), "Electrical issues causing machine failure.", "ERR007", new TimeSpan(0, 3, 0, 0, 0), false, "Electrical Fault", 1, "High" },
                    { new Guid("88888888-8888-8888-8888-888888888898"), "Bobbin fails to wind properly.", "ERR008", new TimeSpan(0, 1, 0, 0, 0), true, "Bobbin Winding Issue", 7, "Low" },
                    { new Guid("99999999-9999-9999-9999-999999999109"), "Machine produces irregular stitches.", "ERR009", new TimeSpan(0, 1, 30, 0, 0), true, "Stitch Irregularity", 9, "Medium" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaba"), "Foot pedal does not respond.", "ERR010", new TimeSpan(0, 2, 0, 0, 0), false, "Foot Pedal Failure", 4, "High" }
                });

            migrationBuilder.InsertData(
                table: "Issues",
                columns: new[] { "Id", "Description", "DisplayName", "IsCommon", "IssueKey", "OccurrenceCount" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "Máy may bị nóng sau thời gian sử dụng ngắn.", "Máy Nóng", false, "MAY_NONG", null },
                    { new Guid("12121212-1212-1212-1212-121212121212"), "Ống chỉ bị kẹt hoặc không quay đúng cách.", "Ống Chỉ Lỗi", false, "ONG_CHI_LOI", null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "Kim bị gãy trong quá trình may.", "Kim Gãy", false, "KIM_GAY", null },
                    { new Guid("23232323-2323-2323-2323-232323232323"), "Dây curoa bị lỏng hoặc đứt, gây ngừng máy.", "Dây Curoa Lỗi", false, "DAY_CUROA_LOI", null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Máy không khởi động hoặc không hoạt động khi bật công tắc.", "Máy Không Chạy", false, "MAY_KHONG_CHAY", null },
                    { new Guid("34343434-3434-3434-3434-343434343434"), "Chỉ dưới không được kéo lên đúng cách.", "Chỉ Dưới Lỗi", false, "CHI_DUOI_LOI", null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Máy bị chảy dầu ra ngoài, ảnh hưởng đến hoạt động.", "Chảy Dầu", false, "CHAY_DAU", null },
                    { new Guid("45454545-4545-4545-4545-454545454545"), "Máy tự động dừng trong khi đang hoạt động.", "Máy Tự Dừng", false, "MAY_TU_DUNG", null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Máy phát ra tiếng ồn lớn bất thường khi hoạt động.", "Kêu To", false, "KEU_TO", null },
                    { new Guid("56565656-5656-5656-5656-565656565656"), "Nút điều chỉnh độ căng chỉ không hoạt động.", "Nút Điều Chỉnh Lỗi", false, "NUT_DIEU_CHINH_LOI", null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Máy làm rách vải trong quá trình may.", "Rách Vải", false, "RACH_VAI", null },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Kim không xuyên đúng vị trí gây lỗi đường may.", "Lưỡi Kim", false, "LUOI_KIM", null },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Chỉ bị đứt liên tục trong quá trình sử dụng.", "Đứt Chỉ", false, "DUT_CHI", null },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Máy không cuốn chỉ hoặc chỉ bị rối.", "Không Cuốn Chỉ", false, "KHONG_CUON_CHI", null },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), "Máy chạy chậm hoặc không đều tốc độ.", "Máy Chạy Chậm", false, "MAY_CHAY_CHAM", null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), "Đường chỉ may không đều, lúc chặt lúc lỏng.", "Chỉ Không Đều", false, "CHI_KHONG_DEU", null },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), "Đèn báo lỗi trên máy may sáng liên tục.", "Đèn Báo Lỗi", false, "DEN_BAO_LOI", null },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), "Bàn đạp không phản hồi khi sử dụng.", "Bàn Đạp Không Hoạt Động", false, "BAN_DAP_KHONG_HOAT_DONG", null },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), "Vải bị nhăn hoặc co kéo trong quá trình may.", "Vải Bị Nhăn", false, "VAI_BI_NHAN", null },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), "Kim may không di chuyển khi máy hoạt động.", "Kim Không Di Chuyển", false, "KIM_KHONG_DI_CHUYEN", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "Email", "FeedbackId", "FullName", "IsEmailConfirmed", "IsRegister", "PasswordHash", "PhoneNumber", "ProfilePictureUrl", "ResetPasswordToken", "Role", "StaffID", "UserName" },
                values: new object[,]
                {
                    { new Guid("21111111-1111-1111-1111-111111111111"), null, "hod@gmail.com", null, "Head of Department", null, null, "String123!", "09785628660", null, null, 1, null, "Head of Department" },
                    { new Guid("32222222-2222-2222-2222-222222222222"), null, "hot@gmail.com", null, "Head of Team", null, null, "String123!", "09785628660", null, null, 2, null, "Head of Team" },
                    { new Guid("43333333-3333-3333-3333-333333333333"), null, "staff@gmail.com", null, "Staff Member", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member" },
                    { new Guid("54444444-4444-4444-4444-444444444444"), null, "sk@gmail.com", null, "Support Staff", null, null, "String123!", "09785628660", null, null, 4, null, "Support Staff" },
                    { new Guid("65555555-5555-5555-5555-555555555555"), null, "admin@gmail.com", null, "Administrator", null, null, "String123!", "09785628660", null, null, 5, null, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "IssueErrors",
                columns: new[] { "ErrorId", "IssueId", "Id" },
                values: new object[,]
                {
                    { new Guid("55555555-5555-5555-5555-555555555565"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("49bf7e53-d872-4465-ad0b-c8120fd17425") },
                    { new Guid("22222222-2222-2222-2222-222222222232"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("d513e9b2-d78a-4ffb-a866-20e61f18402d") },
                    { new Guid("77777777-7777-7777-7777-777777777787"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("157af2c5-3262-499a-8a7a-383297e12762") },
                    { new Guid("44444444-4444-4444-4444-444444444454"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("60506f0c-eb36-4108-9d14-a7e95c4b7f36") },
                    { new Guid("66666666-6666-6666-6666-666666666676"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("219944ee-e5e8-45ea-a25b-637fd368f034") },
                    { new Guid("33333333-3333-3333-3333-333333333343"), new Guid("88888888-8888-8888-8888-888888888888"), new Guid("166f4255-527b-4d72-9083-ec36194d64ea") },
                    { new Guid("88888888-8888-8888-8888-888888888898"), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("fa6e7c81-d56d-4e05-8f4f-f5bad8c0da7d") },
                    { new Guid("11111111-1111-1111-1111-111111111121"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("d275e680-cf3b-4e29-b282-7d6e8b2dd96c") },
                    { new Guid("99999999-9999-9999-9999-999999999109"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("f02227e7-101b-4fdd-86bb-0db53cec5a4c") },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaba"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("024d8fe9-e5de-4ccd-a3e7-af5a533d714f") }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Description", "DeviceId", "DueDate", "Priority", "ReportId", "RequestTitle", "RequestedById", "SerderId", "Status" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111122"), "Sewing machine overheating issue.", new Guid("11111111-1111-1111-1111-111111111112"), new DateTime(2025, 5, 24, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6797), "High", null, "Repair Sewing Machine 1", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Pending" },
                    { new Guid("22222222-2222-2222-2222-222222222233"), "Needle broke during operation.", new Guid("22222222-2222-2222-2222-222222222223"), new DateTime(2025, 5, 23, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6819), "Medium", null, "Fix Needle Breakage", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Approved" },
                    { new Guid("33333333-3333-3333-3333-333333333344"), "Machine fails to start.", new Guid("33333333-3333-3333-3333-333333333334"), new DateTime(2025, 5, 25, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6824), "High", null, "Machine Not Starting", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Pending" },
                    { new Guid("44444444-4444-4444-4444-444444444455"), "Oil leaking from machine.", new Guid("44444444-4444-4444-4444-444444444445"), new DateTime(2025, 5, 26, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6830), "High", null, "Oil Leak Repair", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Denied" },
                    { new Guid("55555555-5555-5555-5555-555555555566"), "Machine making loud noise.", new Guid("55555555-5555-5555-5555-555555555556"), new DateTime(2025, 5, 24, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6837), "Medium", null, "Noise Issue", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Pending" },
                    { new Guid("66666666-6666-6666-6666-666666666677"), "Thread tension causing uneven stitches.", new Guid("66666666-6666-6666-6666-666666666667"), new DateTime(2025, 5, 23, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6843), "Medium", null, "Thread Tension Adjustment", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Approved" },
                    { new Guid("77777777-7777-7777-7777-777777777788"), "Bobbin not winding properly.", new Guid("77777777-7777-7777-7777-777777777778"), new DateTime(2025, 5, 24, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6848), "Low", null, "Bobbin Issue", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Pending" },
                    { new Guid("88888888-8888-8888-8888-888888888899"), "Machine running slower than usual.", new Guid("88888888-8888-8888-8888-888888888889"), new DateTime(2025, 5, 25, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6877), "Medium", null, "Slow Machine Operation", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Approved" },
                    { new Guid("99999999-9999-9999-9999-999999991010"), "Irregular stitches detected.", new Guid("99999999-9999-9999-9999-999999999910"), new DateTime(2025, 5, 24, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6883), "Medium", null, "Stitch Irregularity", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Pending" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaabb"), "Foot pedal not responding.", new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaab"), new DateTime(2025, 5, 23, 11, 12, 8, 29, DateTimeKind.Utc).AddTicks(6890), "High", null, "Foot Pedal Repair", new Guid("43333333-3333-3333-3333-333333333333"), new Guid("43333333-3333-3333-3333-333333333333"), "Approved" }
                });

            migrationBuilder.InsertData(
                table: "RequestIssues",
                columns: new[] { "Id", "IssueId", "RequestId", "Status" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111222"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("11111111-1111-1111-1111-111111111122"), "Open" },
                    { new Guid("22222222-2222-2222-2222-222222222333"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("22222222-2222-2222-2222-222222222233"), "InProgress" },
                    { new Guid("33333333-3333-3333-3333-333333333444"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("33333333-3333-3333-3333-333333333344"), "Open" },
                    { new Guid("44444444-4444-4444-4444-444444444555"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("44444444-4444-4444-4444-444444444455"), "Closed" },
                    { new Guid("55555555-5555-5555-5555-555555555666"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("55555555-5555-5555-5555-555555555566"), "Open" },
                    { new Guid("66666666-6666-6666-6666-666666666777"), new Guid("88888888-8888-8888-8888-888888888888"), new Guid("66666666-6666-6666-6666-666666666677"), "InProgress" },
                    { new Guid("77777777-7777-7777-7777-777777777888"), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("77777777-7777-7777-7777-777777777788"), "Open" },
                    { new Guid("88888888-8888-8888-8888-888888888999"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("88888888-8888-8888-8888-888888888899"), "InProgress" },
                    { new Guid("99999999-9999-9999-9999-999999101010"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("99999999-9999-9999-9999-999999991010"), "Open" },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaabbb"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaabb"), "Closed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceErrorHistories_DeviceId_ErrorId",
                table: "DeviceErrorHistories",
                columns: new[] { "DeviceId", "ErrorId" });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceErrorHistories_ErrorId",
                table: "DeviceErrorHistories",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceHistories_DeviceId",
                table: "DeviceHistories",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIssueHistories_DeviceId_IssueId",
                table: "DeviceIssueHistories",
                columns: new[] { "DeviceId", "IssueId" });

            migrationBuilder.CreateIndex(
                name: "IX_DeviceIssueHistories_IssueId",
                table: "DeviceIssueHistories",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_DeviceCode",
                table: "Devices",
                column: "DeviceCode",
                unique: true,
                filter: "[DeviceCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_MachineId",
                table: "Devices",
                column: "MachineId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceWarranties_DeviceId",
                table: "DeviceWarranties",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceWarrantyHistories_DeviceId",
                table: "DeviceWarrantyHistories",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorDetails_ErrorId",
                table: "ErrorDetails",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorDetails_TaskId",
                table: "ErrorDetails",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Errors_ErrorCode",
                table: "Errors",
                column: "ErrorCode",
                unique: true,
                filter: "[ErrorCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorSpareparts_SparepartId",
                table: "ErrorSpareparts",
                column: "SparepartId");

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_UserId",
                table: "Feedbacks",
                column: "UserId",
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Images_RequestIssueId",
                table: "Images",
                column: "RequestIssueId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueErrors_ErrorId",
                table: "IssueErrors",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_Issues_IssueKey",
                table: "Issues",
                column: "IssueKey",
                unique: true,
                filter: "[IssueKey] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_MachineErrorHistories_ErrorId",
                table: "MachineErrorHistories",
                column: "ErrorId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineErrorHistories_MachineId_ErrorId",
                table: "MachineErrorHistories",
                columns: new[] { "MachineId", "ErrorId" });

            migrationBuilder.CreateIndex(
                name: "IX_MachineIssueHistories_IssueId",
                table: "MachineIssueHistories",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_MachineIssueHistories_MachineId_IssueId",
                table: "MachineIssueHistories",
                columns: new[] { "MachineId", "IssueId" });

            migrationBuilder.CreateIndex(
                name: "IX_Machines_MachineCode",
                table: "Machines",
                column: "MachineCode",
                unique: true,
                filter: "[MachineCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DeviceId",
                table: "Positions",
                column: "DeviceId",
                unique: true,
                filter: "[DeviceId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_ZoneId",
                table: "Positions",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_RepairSpareparts_TaskId",
                table: "RepairSpareparts",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_RequestId",
                table: "Reports",
                column: "RequestId",
                unique: true,
                filter: "[RequestId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RequestIssues_IssueId",
                table: "RequestIssues",
                column: "IssueId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestIssues_RequestId_IssueId",
                table: "RequestIssues",
                columns: new[] { "RequestId", "IssueId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Requests_DeviceId",
                table: "Requests",
                column: "DeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_Requests_SerderId",
                table: "Requests",
                column: "SerderId");

            migrationBuilder.CreateIndex(
                name: "IX_Spareparts_SparepartCode",
                table: "Spareparts",
                column: "SparepartCode",
                unique: true,
                filter: "[SparepartCode] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_AssigneeId",
                table: "Tasks",
                column: "AssigneeId");

            migrationBuilder.CreateIndex(
                name: "IX_Zones_AreaId",
                table: "Zones",
                column: "AreaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DeviceErrorHistories");

            migrationBuilder.DropTable(
                name: "DeviceHistories");

            migrationBuilder.DropTable(
                name: "DeviceIssueHistories");

            migrationBuilder.DropTable(
                name: "DeviceWarranties");

            migrationBuilder.DropTable(
                name: "DeviceWarrantyHistories");

            migrationBuilder.DropTable(
                name: "EmailTemplates");

            migrationBuilder.DropTable(
                name: "ErrorDetails");

            migrationBuilder.DropTable(
                name: "ErrorSpareparts");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "IssueErrors");

            migrationBuilder.DropTable(
                name: "MachineErrorHistories");

            migrationBuilder.DropTable(
                name: "MachineIssueHistories");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "RepairSpareparts");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "RequestIssues");

            migrationBuilder.DropTable(
                name: "Errors");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropTable(
                name: "Spareparts");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Issues");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "Areas");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Machines");

            migrationBuilder.DropTable(
                name: "BaseEntity");
        }
    }
}
