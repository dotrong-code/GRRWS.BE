using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GRRWS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
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
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
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
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    AssigneeId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
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
                    { new Guid("10000000-0000-0000-0000-000000000001"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3895), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3896) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3900), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3900) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3905), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3906) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3908), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3909) },
                    { new Guid("10000000-0000-0000-0000-000000000005"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3913), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3913) },
                    { new Guid("10000000-0000-0000-0000-000000000006"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3917), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3917) },
                    { new Guid("10000000-0000-0000-0000-000000000007"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3920), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3920) },
                    { new Guid("10000000-0000-0000-0000-000000000008"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3923), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3923) },
                    { new Guid("10000000-0000-0000-0000-000000000009"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3927), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3927) },
                    { new Guid("10000000-0000-0000-0000-000000000010"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3930), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3930) },
                    { new Guid("10000000-0000-0000-0000-000000000011"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3935), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3935) },
                    { new Guid("10000000-0000-0000-0000-000000000012"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3938), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3938) },
                    { new Guid("10000000-0000-0000-0000-000000000013"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3969), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3970) },
                    { new Guid("10000000-0000-0000-0000-000000000014"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3973), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3973) },
                    { new Guid("10000000-0000-0000-0000-000000000015"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3976), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3976) },
                    { new Guid("10000000-0000-0000-0000-000000000016"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3979), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3979) },
                    { new Guid("10000000-0000-0000-0000-000000000017"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3983), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3983) },
                    { new Guid("10000000-0000-0000-0000-000000000018"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3987), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3988) },
                    { new Guid("10000000-0000-0000-0000-000000000019"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3992), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3992) },
                    { new Guid("10000000-0000-0000-0000-000000000020"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3995), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3995) },
                    { new Guid("10000000-0000-0000-0000-000000000021"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3998), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3998) },
                    { new Guid("10000000-0000-0000-0000-000000000022"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4001), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4001) },
                    { new Guid("10000000-0000-0000-0000-000000000023"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4004), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4004) },
                    { new Guid("10000000-0000-0000-0000-000000000024"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4008), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4008) },
                    { new Guid("10000000-0000-0000-0000-000000000025"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4011), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4011) },
                    { new Guid("10000000-0000-0000-0000-000000000026"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4014), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4014) },
                    { new Guid("10000000-0000-0000-0000-000000000027"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4019), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4019) },
                    { new Guid("10000000-0000-0000-0000-000000000028"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4022), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4022) },
                    { new Guid("10000000-0000-0000-0000-000000000029"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4024), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4025) },
                    { new Guid("10000000-0000-0000-0000-000000000030"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4027), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(4028) },
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2261), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2262) },
                    { new Guid("12121212-1212-1212-1212-121212121212"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2316), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2317) },
                    { new Guid("21111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(1211), null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2268), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2268) },
                    { new Guid("23232323-2323-2323-2323-232323232323"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2319), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2319) },
                    { new Guid("32222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(1236), null, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2270), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2271) },
                    { new Guid("34343434-3434-3434-3434-343434343434"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2321), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2322) },
                    { new Guid("43333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(1239), null, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2273), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2273) },
                    { new Guid("45454545-4545-4545-4545-454545454545"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2324), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2325) },
                    { new Guid("54444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(1241), null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2281), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2281) },
                    { new Guid("56565656-5656-5656-5656-565656565656"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2328), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2328) },
                    { new Guid("65555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(1243), null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2287), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2288) },
                    { new Guid("77777777-7777-7777-7777-777777777777"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2291), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2291) },
                    { new Guid("88888888-8888-8888-8888-888888888888"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2293), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2294) },
                    { new Guid("99999999-9999-9999-9999-999999999999"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2296), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2296) },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2298), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2298) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2300), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2301) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2303), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2303) },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2306), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2307) },
                    { new Guid("e1d1a111-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3085), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3085) },
                    { new Guid("e1d1a123-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3138), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3138) },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3141), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3141) },
                    { new Guid("e1d1a125-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3145), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3146) },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3148), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3149) },
                    { new Guid("e1d1a127-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3151), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3152) },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3155), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3155) },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3157), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3158) },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3161), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3161) },
                    { new Guid("e1d1a131-0023-0023-0023-000000000023"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3164), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3165) },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3167), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3168) },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3172), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3172) },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3175), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3175) },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3178), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3178) },
                    { new Guid("e1d1a136-0028-0028-0028-000000000028"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3181), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3181) },
                    { new Guid("e1d1a137-0029-0029-0029-000000000029"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3184), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3184) },
                    { new Guid("e1d1a138-0030-0030-0030-000000000030"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3187), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3187) },
                    { new Guid("e1d1a222-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3090), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3091) },
                    { new Guid("e1d1a333-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3094), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3095) },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3097), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3098) },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3102), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3102) },
                    { new Guid("e1d1a666-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3105), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3106) },
                    { new Guid("e1d1a777-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3109), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3110) },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3113), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3113) },
                    { new Guid("e1d1a999-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3117), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3117) },
                    { new Guid("e1d1abbb-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3120), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3120) },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3135), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3135) },
                    { new Guid("e1d1addd-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3124), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3124) },
                    { new Guid("e1d1aeee-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3129), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3129) },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3132), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(3132) },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2309), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2309) },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2312), null, new DateTime(2025, 5, 20, 12, 15, 18, 136, DateTimeKind.Utc).AddTicks(2312) }
                });

            migrationBuilder.InsertData(
                table: "Errors",
                columns: new[] { "Id", "Description", "DisplayName", "ErrorCode", "EstimatedRepairTime", "IsCommon", "OccurrenceCount", "Severity" },
                values: new object[,]
                {
                    { new Guid("e1d1a111-0001-0001-0001-000000000001"), "Bàn đạp không phản hồi hoặc mất tín hiệu.", "Hỏng Bàn Đạp", "HONG_BAN_DAP", new TimeSpan(0, 1, 0, 0, 0), true, 20, "Medium" },
                    { new Guid("e1d1a123-0015-0015-0015-000000000015"), "Quạt tản nhiệt không hoạt động gây quá nhiệt.", "Lỗi Quạt Gió", "LOI_QUAT_GIO", new TimeSpan(0, 1, 0, 0, 0), true, 7, "Low" },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), "Trục chính không quay đồng tâm gây rung.", "Trục Chính Lệch", "TRUC_CHINH_LAC", new TimeSpan(0, 2, 0, 0, 0), false, 4, "High" },
                    { new Guid("e1d1a125-0017-0017-0017-000000000017"), "Đèn máy không sáng do đuôi đèn bị hỏng.", "Đuôi Đèn Cháy", "DUI_DEN_CHAY", new TimeSpan(0, 0, 20, 0, 0), true, 15, "Low" },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), "Bộ điều khiển không lưu lại các thiết lập máy.", "Mất Bộ Nhớ Lưu Thông Số", "MAT_BO_NHO_LUU_THONG_SO", new TimeSpan(0, 1, 30, 0, 0), false, 2, "High" },
                    { new Guid("e1d1a127-0019-0019-0019-000000000019"), "Áp lực chân vịt không ổn định do cảm biến sai số.", "Cảm Biến Áp Lực Lỗi", "CAM_BIEN_AP_LUC_LOI", new TimeSpan(0, 1, 0, 0, 0), false, 5, "Medium" },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), "Vải bị kéo không đều do lỗi bộ cấp vải.", "Rong Không Đủ Siêu", "ROONG_KHONG_DU_SIEU", new TimeSpan(0, 1, 0, 0, 0), true, 10, "Medium" },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), "Bộ phận giữ chỉ không đủ lực siết, gây bung chỉ khi may.", "Mỏ Trói Chỉ Bị Lỏng", "MO_TROI_CHI", new TimeSpan(0, 0, 45, 0, 0), true, 13, "Medium" },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), "Bánh răng truyền động bị mòn, phát ra tiếng kêu hoặc trượt.", "Bánh Răng Mòn", "BANH_RANG_MON", new TimeSpan(0, 1, 30, 0, 0), true, 17, "Medium" },
                    { new Guid("e1d1a131-0023-0023-0023-000000000023"), "Bộ phận điều khiển bằng tay không ăn khớp với cơ cấu truyền động.", "Cần Tay Không Ăn Khớp", "CAM_TAY_KHONG_AN_KHOP", new TimeSpan(0, 1, 0, 0, 0), false, 3, "Low" },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), "Kim va vào mặt vải hoặc phụ liệu, có thể gây hỏng bề mặt.", "Kim Chạm Vải", "KIM_CHAM_VAI", new TimeSpan(0, 0, 30, 0, 0), true, 22, "Low" },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), "Dây nối từ nút khởi động đến động cơ bị hở hoặc đứt.", "Dây Khởi Động Lỗi", "DAY_KHOI_DONG_LOI", new TimeSpan(0, 1, 0, 0, 0), false, 6, "High" },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), "Một số bu lông cố định các cụm máy bị lỏng gây rung lắc.", "Bu Lông Lỏng", "BULONG_LONG", new TimeSpan(0, 0, 40, 0, 0), true, 19, "Medium" },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), "Hỏng mạch điện đèn chiếu sáng, gây mất tầm nhìn khu vực may.", "Mạch Đèn Lỗi", "MACH_DEN_LOI", new TimeSpan(0, 0, 30, 0, 0), true, 12, "Low" },
                    { new Guid("e1d1a136-0028-0028-0028-000000000028"), "Dầu bôi trơn ra quá nhiều gây loang vải hoặc trơn trượt bộ truyền.", "Dầu Bôi Trơn Nhiều", "DAU_BO_NHIEU", new TimeSpan(0, 1, 0, 0, 0), true, 11, "Medium" },
                    { new Guid("e1d1a137-0029-0029-0029-000000000029"), "Quạt thông gió hoạt động yếu, không đủ làm mát cho mô tơ.", "Quạt Thông Gió Yếu", "QUAT_THONG_GIO_YEU", new TimeSpan(0, 1, 30, 0, 0), false, 4, "Medium" },
                    { new Guid("e1d1a138-0030-0030-0030-000000000030"), "Cửa kim không thẳng hàng với trục kim gây lệch đường may.", "Cửa Kim Lệch", "CUA_KIM_LECH", new TimeSpan(0, 1, 0, 0, 0), true, 20, "Medium" },
                    { new Guid("e1d1a222-0002-0002-0002-000000000002"), "Dây curoa lỏng hoặc mòn, gây mất chuyển động.", "Dây Curoa Trượt", "DAYCUROA_TRUOT", new TimeSpan(0, 1, 30, 0, 0), true, 15, "Medium" },
                    { new Guid("e1d1a333-0003-0003-0003-000000000003"), "Bo điều khiển bị lỗi, không kiểm soát được tốc độ.", "Máy Chạy Luôn Lượt", "MAY_CHAY_LUON_LUOT", new TimeSpan(0, 2, 0, 0, 0), false, 5, "High" },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), "Động cơ chính bị cháy do quá tải hoặc ngắn mạch.", "Cháy Motor", "CHAY_MOTOR", new TimeSpan(0, 3, 0, 0, 0), false, 3, "High" },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), "Cơ chế giữ kim bị lệch hoặc gãy.", "Khóa Kim Hỏng", "KHOA_KIM_HONG", new TimeSpan(0, 0, 40, 0, 0), true, 18, "Medium" },
                    { new Guid("e1d1a666-0006-0006-0006-000000000006"), "Dầu rò ra ngoài do gioăng hoặc phớt bị mòn.", "Gioăng Dầu Bị Rò", "GIOANG_DAU_BI_RO", new TimeSpan(0, 1, 30, 0, 0), true, 10, "Medium" },
                    { new Guid("e1d1a777-0007-0007-0007-000000000007"), "Cảm biến vị trí bị lệch dẫn đến dừng máy không đúng lúc.", "Cảm Biến Lệch", "CAM_BIEN_LECH", new TimeSpan(0, 1, 0, 0, 0), false, 6, "Low" },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), "Bo mạch điều khiển bị chập, không phản hồi.", "Lỗi Mạch Điều Khiển", "LOI_MACH_DIEU_KHIEN", new TimeSpan(0, 2, 0, 0, 0), false, 4, "High" },
                    { new Guid("e1d1a999-0009-0009-0009-000000000009"), "Cơ chế chống trôi vải không ăn khớp.", "Chống Trôi Không Hoạt Động", "CHONG_TROI_KHONG_HOAT_DONG", new TimeSpan(0, 0, 50, 0, 0), true, 12, "Medium" },
                    { new Guid("e1d1abbb-0010-0010-0010-000000000010"), "Chốt vải bị kẹt, gây gián đoạn chu trình may.", "Chốt Vải Kẹt", "CHOT_VAI_KET", new TimeSpan(0, 0, 30, 0, 0), true, 22, "Low" },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), "Kim không đúng trục tâm, đâm lệch lỗ.", "Kim Lỗi Tâm", "KIM_LOI_TAM", new TimeSpan(0, 1, 0, 0, 0), true, 18, "Medium" },
                    { new Guid("e1d1addd-0011-0011-0011-000000000011"), "Vòng bạc trục bị mòn dẫn đến rung lắc hoặc tiếng ồn lớn.", "Vòng Bạc Mòn", "VONG_BAC_MON", new TimeSpan(0, 1, 30, 0, 0), true, 14, "Medium" },
                    { new Guid("e1d1aeee-0012-0012-0012-000000000012"), "Dao cắt không bén, gây xơ vải hoặc rách mép.", "Dao Cắt Không Sắc", "DAO_CAT_KHONG_SAC", new TimeSpan(0, 0, 30, 0, 0), true, 25, "Low" },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), "Cảm biến không phát hiện được vải khi đưa vào.", "Cảm Biến Vải Không Nhận", "CAM_BIEN_VAI_KHONG_NHAN", new TimeSpan(0, 1, 0, 0, 0), false, 6, "Medium" }
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
                table: "Spareparts",
                columns: new[] { "Id", "Description", "IsAvailable", "SparepartCode", "SparepartName", "Specification", "StockQuantity", "Unit", "UnitPrice" },
                values: new object[,]
                {
                    { new Guid("10000000-0000-0000-0000-000000000001"), "Kim thép không gỉ dùng cho máy may công nghiệp", false, "SP001", "Kim May Công Nghiệp", "Loại DBX1, cỡ 90/14", 150, "Cái", 5000m },
                    { new Guid("10000000-0000-0000-0000-000000000002"), "Dây truyền động cho máy may", false, "SP002", "Dây Curoa", "Chiều dài 1m, bản 10mm", 60, "Cái", 12000m },
                    { new Guid("10000000-0000-0000-0000-000000000003"), "Bàn đạp điều khiển tốc độ", false, "SP003", "Bàn Đạp Máy", "Điện áp 220V", 20, "Cái", 45000m },
                    { new Guid("10000000-0000-0000-0000-000000000004"), "Ống chỉ nhựa cho máy may tự động", false, "SP004", "Ống Chỉ", "Đường kính 2.5cm", 500, "Cái", 1500m },
                    { new Guid("10000000-0000-0000-0000-000000000005"), "Điều chỉnh nhiệt độ máy may", false, "SP005", "Bộ Điều Khiển Nhiệt", "Tối đa 200°C", 10, "Bộ", 120000m },
                    { new Guid("10000000-0000-0000-0000-000000000006"), "Công tắc bật/tắt máy may", false, "SP006", "Công Tắc Máy May", "Công suất 250V - 10A", 35, "Cái", 8000m },
                    { new Guid("10000000-0000-0000-0000-000000000007"), "Mô tơ điện cho máy may công nghiệp", false, "SP007", "Mô Tơ Máy May", "Công suất 370W, điện 220V", 8, "Cái", 350000m },
                    { new Guid("10000000-0000-0000-0000-000000000008"), "Đèn chiếu sáng cho khu vực may", false, "SP008", "Đèn LED Gắn Máy", "LED 12V, 5W, dán keo", 120, "Cái", 18000m },
                    { new Guid("10000000-0000-0000-0000-000000000009"), "Trụ gắn kim thay thế cho đầu máy", false, "SP009", "Trụ Gắn Kim", "Thép hợp kim bền cao", 40, "Cái", 22000m },
                    { new Guid("10000000-0000-0000-0000-000000000010"), "Cơ cấu truyền động kim máy may", false, "SP010", "Bộ Truyền Kim", "Cơ khí chính xác cao", 12, "Bộ", 85000m },
                    { new Guid("10000000-0000-0000-0000-000000000011"), "Bộ phận giữ chỉ dưới trong máy may", false, "SP011", "Ổ Chỉ Dưới", "Kim loại bền, chuẩn công nghiệp", 100, "Cái", 10000m },
                    { new Guid("10000000-0000-0000-0000-000000000012"), "Bánh răng dẫn động kim và trụ máy", false, "SP012", "Bánh Răng Truyền Động", "Hợp kim, răng xoắn", 60, "Cái", 30000m },
                    { new Guid("10000000-0000-0000-0000-000000000013"), "Trục truyền động từ mô tơ đến kim", false, "SP013", "Trục Kim Máy May", "Thép tôi cứng, chống mài mòn", 25, "Cái", 40000m },
                    { new Guid("10000000-0000-0000-0000-000000000014"), "Khung giữ ống chỉ phía trên máy", false, "SP014", "Giá Đỡ Ống Chỉ", "Nhựa chịu lực hoặc kim loại", 80, "Cái", 7000m },
                    { new Guid("10000000-0000-0000-0000-000000000015"), "Phụ kiện cảm biến tốc độ quay mô tơ", false, "SP015", "Cảm Biến Tốc Độ", "Điện áp 5V TTL, chuẩn hall sensor", 15, "Cái", 65000m },
                    { new Guid("10000000-0000-0000-0000-000000000016"), "Bộ khung bên ngoài cho máy loại nhỏ", false, "SP016", "Khung Máy Nhỏ", "Nhôm đúc", 12, "Bộ", 90000m },
                    { new Guid("10000000-0000-0000-0000-000000000017"), "Lót chân máy may giảm rung, chống ồn", false, "SP017", "Đế Cao Su Chống Rung", "Cao su tổng hợp, đường kính 5cm", 150, "Cái", 3000m },
                    { new Guid("10000000-0000-0000-0000-000000000018"), "Bánh dẫn puli gắn với động cơ", false, "SP018", "Puli Dây Curoa", "Đường kính 80mm, thép hợp kim", 30, "Cái", 18000m },
                    { new Guid("10000000-0000-0000-0000-000000000019"), "Dây cấp nguồn cho mô tơ máy may", false, "SP019", "Dây Điện Động Cơ", "2 lõi, dài 1.5m, bọc cách điện", 100, "Cuộn", 10000m },
                    { new Guid("10000000-0000-0000-0000-000000000020"), "Chắn dầu chống tràn ra khỏi ổ", false, "SP020", "Bộ Gương Chắn Dầu", "Nhựa chịu nhiệt, lắp trong trục máy", 50, "Bộ", 22000m },
                    { new Guid("10000000-0000-0000-0000-000000000021"), "Kim chuyên dụng cho vải dày, da, nỉ", false, "SP021", "Kim May Dày", "Cỡ 100/16, loại DPX17", 80, "Cái", 6000m },
                    { new Guid("10000000-0000-0000-0000-000000000022"), "Loại dây curoa dự phòng cho máy lập trình", false, "SP022", "Dây Curoa Dự Phòng", "Bản rộng 8mm, răng hình thang", 40, "Cái", 15000m },
                    { new Guid("10000000-0000-0000-0000-000000000023"), "Loại đèn LED gắn bên cạnh trục kim", false, "SP023", "Đèn Chiếu Sáng Máy May", "LED trắng 6W, 220V", 100, "Cái", 20000m },
                    { new Guid("10000000-0000-0000-0000-000000000024"), "Cơ cấu điều khiển chân vịt tự động", false, "SP024", "Bộ Điều Khiển Chân Vịt", "Tích hợp cảm biến áp suất", 18, "Bộ", 95000m },
                    { new Guid("10000000-0000-0000-0000-000000000025"), "Ổ chỉ dưới thay thế cho máy Brother", false, "SP025", "Ổ Chỉ Dưới (Loại A)", "Chuẩn A, có lò xo giữ", 75, "Cái", 11000m },
                    { new Guid("10000000-0000-0000-0000-000000000026"), "Thanh truyền động từ bàn đạp đến mô tơ", false, "SP026", "Trục Quay Bàn Đạp", "Thép đặc, dài 30cm", 25, "Cái", 27000m },
                    { new Guid("10000000-0000-0000-0000-000000000027"), "Đế cao su chống trượt cho máy may", false, "SP027", "Đế Máy May", "4 miếng/bộ, cao su EPDM", 90, "Bộ", 18000m },
                    { new Guid("10000000-0000-0000-0000-000000000028"), "Giá đỡ đèn LED trên thân máy", false, "SP028", "Khung Gắn Đèn", "Inox không gỉ", 40, "Cái", 9000m },
                    { new Guid("10000000-0000-0000-0000-000000000029"), "Bo mạch điều khiển trung tâm cho máy điện tử", false, "SP029", "Bộ Điều Khiển Điện Tử", "Mainboard 8-bit MCU", 5, "Bộ", 350000m },
                    { new Guid("10000000-0000-0000-0000-000000000030"), "Khung gắn chỉ đứng dùng cho máy công nghiệp", false, "SP030", "Giá Đỡ Chỉ Đứng", "2 trục, cao 60cm", 55, "Cái", 13000m }
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
                table: "ErrorSpareparts",
                columns: new[] { "ErrorId", "SparepartId", "QuantityNeeded" },
                values: new object[,]
                {
                    { new Guid("e1d1a111-0001-0001-0001-000000000001"), new Guid("10000000-0000-0000-0000-000000000003"), 1 },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), new Guid("10000000-0000-0000-0000-000000000013"), 1 },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), new Guid("10000000-0000-0000-0000-000000000027"), 1 },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), new Guid("10000000-0000-0000-0000-000000000014"), 1 },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), new Guid("10000000-0000-0000-0000-000000000029"), 1 },
                    { new Guid("e1d1a127-0019-0019-0019-000000000019"), new Guid("10000000-0000-0000-0000-000000000015"), 1 },
                    { new Guid("e1d1a127-0019-0019-0019-000000000019"), new Guid("10000000-0000-0000-0000-000000000028"), 1 },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), new Guid("10000000-0000-0000-0000-000000000024"), 2 },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), new Guid("10000000-0000-0000-0000-000000000006"), 2 },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), new Guid("10000000-0000-0000-0000-000000000012"), 1 },
                    { new Guid("e1d1a131-0023-0023-0023-000000000023"), new Guid("10000000-0000-0000-0000-000000000026"), 1 },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), new Guid("10000000-0000-0000-0000-000000000001"), 1 },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), new Guid("10000000-0000-0000-0000-000000000021"), 1 },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), new Guid("10000000-0000-0000-0000-000000000019"), 1 },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), new Guid("10000000-0000-0000-0000-000000000008"), 2 },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), new Guid("10000000-0000-0000-0000-000000000030"), 1 },
                    { new Guid("e1d1a136-0028-0028-0028-000000000028"), new Guid("10000000-0000-0000-0000-000000000020"), 1 },
                    { new Guid("e1d1a137-0029-0029-0029-000000000029"), new Guid("10000000-0000-0000-0000-000000000007"), 1 },
                    { new Guid("e1d1a222-0002-0002-0002-000000000002"), new Guid("10000000-0000-0000-0000-000000000002"), 1 },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("10000000-0000-0000-0000-000000000007"), 1 },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), new Guid("10000000-0000-0000-0000-000000000001"), 1 },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), new Guid("10000000-0000-0000-0000-000000000009"), 1 },
                    { new Guid("e1d1a999-0009-0009-0009-000000000009"), new Guid("10000000-0000-0000-0000-000000000017"), 3 }
                });

            migrationBuilder.InsertData(
                table: "IssueErrors",
                columns: new[] { "ErrorId", "IssueId" },
                values: new object[,]
                {
                    { new Guid("e1d1a123-0015-0015-0015-000000000015"), new Guid("11111111-1111-1111-1111-111111111111") },
                    { new Guid("e1d1a131-0023-0023-0023-000000000023"), new Guid("12121212-1212-1212-1212-121212121212") },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), new Guid("12121212-1212-1212-1212-121212121212") },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), new Guid("23232323-2323-2323-2323-232323232323") },
                    { new Guid("e1d1a222-0002-0002-0002-000000000002"), new Guid("23232323-2323-2323-2323-232323232323") },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), new Guid("34343434-3434-3434-3434-343434343434") },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), new Guid("34343434-3434-3434-3434-343434343434") },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), new Guid("34343434-3434-3434-3434-343434343434") },
                    { new Guid("e1d1a666-0006-0006-0006-000000000006"), new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("e1d1a137-0029-0029-0029-000000000029"), new Guid("45454545-4545-4545-4545-454545454545") },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), new Guid("45454545-4545-4545-4545-454545454545") },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("e1d1addd-0011-0011-0011-000000000011"), new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("e1d1aeee-0012-0012-0012-000000000012"), new Guid("66666666-6666-6666-6666-666666666666") },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), new Guid("77777777-7777-7777-7777-777777777777") },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), new Guid("88888888-8888-8888-8888-888888888888") },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), new Guid("99999999-9999-9999-9999-999999999999") },
                    { new Guid("e1d1a333-0003-0003-0003-000000000003"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa") },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb") },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc") },
                    { new Guid("e1d1a111-0001-0001-0001-000000000001"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd") },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee") },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee") },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff") },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff") }
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
