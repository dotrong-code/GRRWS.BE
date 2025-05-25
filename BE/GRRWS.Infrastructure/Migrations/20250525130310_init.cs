using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace GRRWS.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
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
                    AreaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AreaCode = table.Column<string>(type: "nvarchar(450)", nullable: true)
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
                    ZoneCode = table.Column<string>(type: "nvarchar(450)", nullable: true),
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
                    MachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastOccurredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineErrorHistories", x => new { x.MachineId, x.ErrorId });
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
                    MachineId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastOccurredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MachineIssueHistories", x => new { x.MachineId, x.IssueId });
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
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ErrorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastOccurredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceErrorHistories", x => new { x.DeviceId, x.ErrorId });
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
                    DeviceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IssueId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastOccurredDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OccurrenceCount = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceIssueHistories", x => new { x.DeviceId, x.IssueId });
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
                    { new Guid("10000000-0000-0000-0000-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4981), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4981) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4985), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4985) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4988), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4988) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4992), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4992) },
                    { new Guid("10000000-0000-0000-0000-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4996), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4996) },
                    { new Guid("10000000-0000-0000-0000-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5002), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5002) },
                    { new Guid("10000000-0000-0000-0000-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5006), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5006) },
                    { new Guid("10000000-0000-0000-0000-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5009), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5010) },
                    { new Guid("10000000-0000-0000-0000-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5012), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5012) },
                    { new Guid("10000000-0000-0000-0000-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5058), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5058) },
                    { new Guid("10000000-0000-0000-0000-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5061), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5061) },
                    { new Guid("10000000-0000-0000-0000-000000000012"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5064), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5064) },
                    { new Guid("10000000-0000-0000-0000-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5068), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5068) },
                    { new Guid("10000000-0000-0000-0000-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5073), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5073) },
                    { new Guid("10000000-0000-0000-0000-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5076), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5076) },
                    { new Guid("10000000-0000-0000-0000-000000000016"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5079), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5079) },
                    { new Guid("10000000-0000-0000-0000-000000000017"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5081), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5082) },
                    { new Guid("10000000-0000-0000-0000-000000000018"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5084), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5085) },
                    { new Guid("10000000-0000-0000-0000-000000000019"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5088), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5088) },
                    { new Guid("10000000-0000-0000-0000-000000000020"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5091), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5091) },
                    { new Guid("10000000-0000-0000-0000-000000000021"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5094), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5094) },
                    { new Guid("10000000-0000-0000-0000-000000000022"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5102), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5102) },
                    { new Guid("10000000-0000-0000-0000-000000000023"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5104), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5105) },
                    { new Guid("10000000-0000-0000-0000-000000000024"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5107), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5108) },
                    { new Guid("10000000-0000-0000-0000-000000000025"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5110), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5110) },
                    { new Guid("10000000-0000-0000-0000-000000000026"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5113), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5113) },
                    { new Guid("10000000-0000-0000-0000-000000000027"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5115), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5116) },
                    { new Guid("10000000-0000-0000-0000-000000000028"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5119), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5119) },
                    { new Guid("10000000-0000-0000-0000-000000000029"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5122), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5123) },
                    { new Guid("10000000-0000-0000-0000-000000000030"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5128), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(5128) },
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2934), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2935) },
                    { new Guid("12121212-1212-1212-1212-121212121212"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2982), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2982) },
                    { new Guid("21111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2100), null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2942), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2943) },
                    { new Guid("23232323-2323-2323-2323-232323232323"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2985), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2986) },
                    { new Guid("23333333-3333-3333-3333-333333333343"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2186), null, null },
                    { new Guid("23333333-3333-3333-3333-333333333344"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2188), null, null },
                    { new Guid("32222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2119), null, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2946), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2946) },
                    { new Guid("33cc4a77-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9366), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9367) },
                    { new Guid("33cc4a77-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9375), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9375) },
                    { new Guid("33cc4a77-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9379), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9380) },
                    { new Guid("33cc4a77-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9384), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9386) },
                    { new Guid("33cc4a77-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9390), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9391) },
                    { new Guid("33cc4a77-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9395), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9395) },
                    { new Guid("33cc4a77-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9402), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9403) },
                    { new Guid("33cc4a77-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9407), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9408) },
                    { new Guid("33cc4a77-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9412), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9412) },
                    { new Guid("33cc4a77-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9417), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9417) },
                    { new Guid("34343434-3434-3434-3434-343434343434"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2988), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2989) },
                    { new Guid("43333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2120), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333334"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2126), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333335"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2136), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333336"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2138), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333337"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2140), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333338"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2142), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333339"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2143), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333340"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2145), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333341"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2147), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333342"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2182), null, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2949), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2950) },
                    { new Guid("45454545-4545-4545-4545-454545454545"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2991), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2991) },
                    { new Guid("54444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2122), null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2952), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2953) },
                    { new Guid("56565656-5656-5656-5656-565656565656"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2994), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2995) },
                    { new Guid("65555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2124), null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2955), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2955) },
                    { new Guid("66666666-6666-6666-6666-666666666667"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3769), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3770) },
                    { new Guid("77777777-7777-7777-7777-777777777777"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2959), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2960) },
                    { new Guid("88888888-8888-8888-8888-888888888888"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2962), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2962) },
                    { new Guid("99999999-9999-9999-9999-999999999999"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2964), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2965) },
                    { new Guid("a1b2c3d4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2636), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2636) },
                    { new Guid("a1b2c3d4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2641), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2641) },
                    { new Guid("a1b2c3d4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2648), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2648) },
                    { new Guid("a1b2c3d4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2653), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2654) },
                    { new Guid("a1b2c3d4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2658), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2658) },
                    { new Guid("a1b2c3d4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2661), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2661) },
                    { new Guid("a1b2c3d4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2665), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2665) },
                    { new Guid("a1b2c3d4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2668), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2669) },
                    { new Guid("a1b2c3d4-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2671), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2672) },
                    { new Guid("a1b2c3d4-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2675), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2676) },
                    { new Guid("a1b2c3d4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2681), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2681) },
                    { new Guid("a1b2c3d4-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2684), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2684) },
                    { new Guid("a1b2c3d4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2687), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2688) },
                    { new Guid("a1b2c3d4-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2691), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2691) },
                    { new Guid("a1b2c3d4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2694), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2694) },
                    { new Guid("a1b2c3d4-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2698), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2698) },
                    { new Guid("a1b2c3d4-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2701), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2701) },
                    { new Guid("a1b2c3d4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2705), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2705) },
                    { new Guid("a1b2c3d4-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2710), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2710) },
                    { new Guid("a1b2c3d4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2717), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2718) },
                    { new Guid("a1b2c3d4-0021-0021-0021-000000000021"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2720), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2720) },
                    { new Guid("a1b2c3d4-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2723), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2723) },
                    { new Guid("a1b2c3d4-0023-0023-0023-000000000023"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2726), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2727) },
                    { new Guid("a1b2c3d4-0024-0024-0024-000000000024"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2759), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2760) },
                    { new Guid("a1b2c3d4-0025-0025-0025-000000000025"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2765), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2765) },
                    { new Guid("a1b2c3d4-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2769), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2769) },
                    { new Guid("a1b2c3d4-0027-0027-0027-000000000027"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2773), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2773) },
                    { new Guid("a1b2c3d4-0028-0028-0028-000000000028"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2776), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2776) },
                    { new Guid("a1b2c3d4-0029-0029-0029-000000000029"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2779), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2780) },
                    { new Guid("a1b2c3d4-0030-0030-0030-000000000030"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2783), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2783) },
                    { new Guid("a1e2f3a4-0001-0001-1001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2919), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2919) },
                    { new Guid("a1f2e3d4-0002-0002-1002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2930), null, new DateTime(2025, 5, 25, 15, 3, 10, 117, DateTimeKind.Utc).AddTicks(2930) },
                    { new Guid("a1f2e3d4-0003-0003-1003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2941), null, new DateTime(2025, 5, 25, 14, 3, 10, 117, DateTimeKind.Utc).AddTicks(2942) },
                    { new Guid("a1f2e3d4-0004-0004-1004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2945), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2946) },
                    { new Guid("a1f2e3d4-0005-0005-1005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2954), null, new DateTime(2025, 5, 25, 15, 3, 10, 117, DateTimeKind.Utc).AddTicks(2955) },
                    { new Guid("a1f2e3d4-0006-0006-1006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2959), null, new DateTime(2025, 5, 25, 14, 3, 10, 117, DateTimeKind.Utc).AddTicks(2960) },
                    { new Guid("a1f2e3d4-0007-0007-1007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2964), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2964) },
                    { new Guid("a1f2e3d4-0008-0008-1008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2968), null, new DateTime(2025, 5, 25, 16, 3, 10, 117, DateTimeKind.Utc).AddTicks(2968) },
                    { new Guid("a1f2e3d4-0009-0009-1009-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2973), null, new DateTime(2025, 5, 25, 14, 3, 10, 117, DateTimeKind.Utc).AddTicks(2973) },
                    { new Guid("a1f2e3d4-0010-0010-1010-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2978), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2978) },
                    { new Guid("a1f2e3d4-0011-0011-1011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2983), null, new DateTime(2025, 5, 25, 15, 3, 10, 117, DateTimeKind.Utc).AddTicks(2983) },
                    { new Guid("a1f2e3d4-0012-0012-1012-000000000012"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2986), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2987) },
                    { new Guid("a1f2e3d4-0013-0013-1013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2993), null, new DateTime(2025, 5, 25, 15, 3, 10, 117, DateTimeKind.Utc).AddTicks(2993) },
                    { new Guid("a1f2e3d4-0014-0014-1014-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(2997), null, new DateTime(2025, 5, 25, 16, 3, 10, 117, DateTimeKind.Utc).AddTicks(2997) },
                    { new Guid("a1f2e3d4-0015-0015-1015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3001), null, new DateTime(2025, 5, 25, 16, 3, 10, 117, DateTimeKind.Utc).AddTicks(3001) },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2967), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2967) },
                    { new Guid("b1c2d3e4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3930), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3930) },
                    { new Guid("b1c2d3e4-0001-0001-0001-100000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4629), null, null },
                    { new Guid("b1c2d3e4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3933), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3933) },
                    { new Guid("b1c2d3e4-0002-0002-0002-100000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4638), null, null },
                    { new Guid("b1c2d3e4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3935), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3936) },
                    { new Guid("b1c2d3e4-0003-0003-0003-100000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4666), null, null },
                    { new Guid("b1c2d3e4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3938), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3938) },
                    { new Guid("b1c2d3e4-0004-0004-0004-100000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4670), null, null },
                    { new Guid("b1c2d3e4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3940), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3940) },
                    { new Guid("b1c2d3e4-0005-0005-0005-100000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4675), null, null },
                    { new Guid("b1c2d3e4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3942), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3943) },
                    { new Guid("b1c2d3e4-0006-0006-0006-100000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4687), null, null },
                    { new Guid("b1c2d3e4-0007-0007-0007-100000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4691), null, null },
                    { new Guid("b1c2d3e4-0008-0008-0008-100000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4696), null, null },
                    { new Guid("b1c2d3e4-0009-0009-0009-100000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4700), null, null },
                    { new Guid("b1c2d3e4-0010-0010-0010-100000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4704), null, null },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2969), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2969) },
                    { new Guid("c1d2e3f4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3398), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3398) },
                    { new Guid("c1d2e3f4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3402), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3403) },
                    { new Guid("c1d2e3f4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3406), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3407) },
                    { new Guid("c1d2e3f4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3409), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3410) },
                    { new Guid("c1d2e3f4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3413), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3414) },
                    { new Guid("c1d2e3f4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3421), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3422) },
                    { new Guid("c1d2e3f4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3424), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3425) },
                    { new Guid("c1d2e3f4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3428), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3428) },
                    { new Guid("c1d2e3f4-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3431), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3431) },
                    { new Guid("c1d2e3f4-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3434), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3434) },
                    { new Guid("c1d2e3f4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3418), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3419) },
                    { new Guid("c1d2e3f4-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3437), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3437) },
                    { new Guid("c1d2e3f4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3440), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3440) },
                    { new Guid("c1d2e3f4-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3444), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3445) },
                    { new Guid("c1d2e3f4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3447), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(3448) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2971), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2971) },
                    { new Guid("d1e2f3a4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3731), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3731) },
                    { new Guid("d1e2f3a4-0001-4001-8001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9544), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9545) },
                    { new Guid("d1e2f3a4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3737), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3738) },
                    { new Guid("d1e2f3a4-0002-4002-8002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9569), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9569) },
                    { new Guid("d1e2f3a4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3745), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3746) },
                    { new Guid("d1e2f3a4-0003-4003-8003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9579), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9580) },
                    { new Guid("d1e2f3a4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3751), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3752) },
                    { new Guid("d1e2f3a4-0004-4004-8004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9594), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9595) },
                    { new Guid("d1e2f3a4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3757), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3757) },
                    { new Guid("d1e2f3a4-0005-4005-8005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9617), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9617) },
                    { new Guid("d1e2f3a4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3763), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3763) },
                    { new Guid("d1e2f3a4-0006-4006-8006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9626), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9626) },
                    { new Guid("d1e2f3a4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3775), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3776) },
                    { new Guid("d1e2f3a4-0007-4007-8007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9672), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9672) },
                    { new Guid("d1e2f3a4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3781), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3781) },
                    { new Guid("d1e2f3a4-0008-4008-8008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9676), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9676) },
                    { new Guid("d1e2f3a4-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3786), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3787) },
                    { new Guid("d1e2f3a4-0009-4009-8009-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9551), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9552) },
                    { new Guid("d1e2f3a4-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3793), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3794) },
                    { new Guid("d1e2f3a4-0010-4010-8010-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9557), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9557) },
                    { new Guid("d1e2f3a4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3799), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3799) },
                    { new Guid("d1e2f3a4-0011-4011-8011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9562), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9562) },
                    { new Guid("d1e2f3a4-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3806), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3806) },
                    { new Guid("d1e2f3a4-0012-4012-8012-000000000012"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9575), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9575) },
                    { new Guid("d1e2f3a4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3812), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3812) },
                    { new Guid("d1e2f3a4-0013-4013-8013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9584), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9584) },
                    { new Guid("d1e2f3a4-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3817), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3818) },
                    { new Guid("d1e2f3a4-0014-4014-8014-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9589), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9590) },
                    { new Guid("d1e2f3a4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3824), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3825) },
                    { new Guid("d1e2f3a4-0015-4015-8015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9599), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9600) },
                    { new Guid("d1e2f3a4-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3830), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3830) },
                    { new Guid("d1e2f3a4-0016-4016-8016-000000000016"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9605), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9606) },
                    { new Guid("d1e2f3a4-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3835), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3835) },
                    { new Guid("d1e2f3a4-0017-4017-8017-000000000017"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9612), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9612) },
                    { new Guid("d1e2f3a4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3873), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3874) },
                    { new Guid("d1e2f3a4-0018-4018-8018-000000000018"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9622), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9622) },
                    { new Guid("d1e2f3a4-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3885), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3885) },
                    { new Guid("d1e2f3a4-0019-4019-8019-000000000019"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9667), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9667) },
                    { new Guid("d1e2f3a4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3890), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3891) },
                    { new Guid("d1e2f3a4-0021-0021-0021-000000000021"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3896), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3897) },
                    { new Guid("d1e2f3a4-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3902), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3902) },
                    { new Guid("d1e2f3a4-0023-0023-0023-000000000023"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3909), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3909) },
                    { new Guid("d1e2f3a4-0024-0024-0024-000000000024"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3914), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3915) },
                    { new Guid("d1e2f3a4-0025-0025-0025-000000000025"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3920), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3920) },
                    { new Guid("d1e2f3a4-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3931), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3931) },
                    { new Guid("d1e2f3a4-0027-0027-0027-000000000027"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3938), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3938) },
                    { new Guid("d1e2f3a4-0028-0028-0028-000000000028"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3949), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3950) },
                    { new Guid("d1e2f3a4-0029-0029-0029-000000000029"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3956), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3956) },
                    { new Guid("d1e2f3a4-0030-0030-0030-000000000030"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3964), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(3964) },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2973), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2974) },
                    { new Guid("e1d1a111-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4349), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4349) },
                    { new Guid("e1d1a123-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4400), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4401) },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4404), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4404) },
                    { new Guid("e1d1a125-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4406), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4407) },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4409), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4410) },
                    { new Guid("e1d1a127-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4412), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4412) },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4416), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4417) },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4419), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4419) },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4422), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4422) },
                    { new Guid("e1d1a131-0023-0023-0023-000000000023"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4425), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4425) },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4431), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4431) },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4434), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4434) },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4437), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4437) },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4440), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4440) },
                    { new Guid("e1d1a136-0028-0028-0028-000000000028"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4444), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4444) },
                    { new Guid("e1d1a137-0029-0029-0029-000000000029"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4447), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4447) },
                    { new Guid("e1d1a138-0030-0030-0030-000000000030"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4449), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4450) },
                    { new Guid("e1d1a222-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4353), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4354) },
                    { new Guid("e1d1a333-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4356), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4357) },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4362), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4363) },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4366), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4367) },
                    { new Guid("e1d1a666-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4369), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4370) },
                    { new Guid("e1d1a777-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4372), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4373) },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4376), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4376) },
                    { new Guid("e1d1a999-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4379), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4379) },
                    { new Guid("e1d1abbb-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4382), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4382) },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4398), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4398) },
                    { new Guid("e1d1addd-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4386), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4386) },
                    { new Guid("e1d1aeee-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4391), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4391) },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4394), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(4394) },
                    { new Guid("e1f2a3b4-0001-0001-0001-300000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4822), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4822) },
                    { new Guid("e1f2a3b4-0002-0002-0002-300000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4826), null, new DateTime(2025, 5, 25, 14, 3, 10, 118, DateTimeKind.Utc).AddTicks(4826) },
                    { new Guid("e1f2a3b4-0003-0003-0003-300000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4832), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4833) },
                    { new Guid("e1f2a3b4-0004-0004-0004-300000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4837), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4838) },
                    { new Guid("e1f2a3b4-0005-0005-0005-300000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4841), null, new DateTime(2025, 5, 25, 15, 3, 10, 118, DateTimeKind.Utc).AddTicks(4842) },
                    { new Guid("e1f2a3b4-0006-0006-0006-300000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4846), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4846) },
                    { new Guid("e1f2a3b4-0007-0007-0007-300000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4850), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4851) },
                    { new Guid("e1f2a3b4-0008-0008-0008-300000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4857), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4857) },
                    { new Guid("e1f2a3b4-0009-0009-0009-300000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4860), null, new DateTime(2025, 5, 25, 15, 3, 10, 118, DateTimeKind.Utc).AddTicks(4860) },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2976), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2976) },
                    { new Guid("f1e2d3c4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4070), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4071) },
                    { new Guid("f1e2d3c4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4074), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4074) },
                    { new Guid("f1e2d3c4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4078), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4078) },
                    { new Guid("f1e2d3c4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4081), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4081) },
                    { new Guid("f1e2d3c4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4084), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4085) },
                    { new Guid("f1e2d3c4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4088), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4088) },
                    { new Guid("f1e2d3c4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4092), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4093) },
                    { new Guid("f1e2d3c4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4096), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4096) },
                    { new Guid("f1e2d3c4-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4100), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4101) },
                    { new Guid("f1e2d3c4-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4104), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4104) },
                    { new Guid("f1e2d3c4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4107), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4107) },
                    { new Guid("f1e2d3c4-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4110), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4110) },
                    { new Guid("f1e2d3c4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4113), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4113) },
                    { new Guid("f1e2d3c4-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4116), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4117) },
                    { new Guid("f1e2d3c4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4119), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4120) },
                    { new Guid("f1e2d3c4-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4126), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4126) },
                    { new Guid("f1e2d3c4-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4131), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4131) },
                    { new Guid("f1e2d3c4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4134), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4134) },
                    { new Guid("f1e2d3c4-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4179), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4179) },
                    { new Guid("f1e2d3c4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4183), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4183) },
                    { new Guid("f1e2d3c4-0021-0011-0021-000000000021"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4187), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4187) },
                    { new Guid("f1e2d3c4-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4190), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4191) },
                    { new Guid("f1e2d3c4-0023-0023-0023-000000000023"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4193), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4194) },
                    { new Guid("f1e2d3c4-0024-0024-0024-000000000024"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4197), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4197) },
                    { new Guid("f1e2d3c4-0025-0025-0025-000000000025"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4201), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4201) },
                    { new Guid("f1e2d3c4-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4205), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4205) },
                    { new Guid("f1e2d3c4-0027-0027-0027-000000000027"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4208), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4208) },
                    { new Guid("f1e2d3c4-0028-0028-0028-000000000028"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4211), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4212) },
                    { new Guid("f1e2d3c4-0029-0029-0029-000000000029"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4214), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4215) },
                    { new Guid("f1e2d3c4-0030-0030-0030-000000000030"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4219), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(4220) },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2979), null, new DateTime(2025, 5, 25, 13, 3, 10, 116, DateTimeKind.Utc).AddTicks(2980) }
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "AreaCode", "AreaName" },
                values: new object[,]
                {
                    { new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "SXA", "Khu Sản Xuất Chính A" },
                    { new Guid("b1c2d3e4-0002-0002-0002-000000000002"), "SXB", "Khu Sản Xuất Chính B" },
                    { new Guid("b1c2d3e4-0003-0003-0003-000000000003"), "KKT", "Khu Kiểm Tra Chất Lượng" },
                    { new Guid("b1c2d3e4-0004-0004-0004-000000000004"), "KCV", "Khu Cắt Vải" },
                    { new Guid("b1c2d3e4-0005-0005-0005-000000000005"), "KTV", "Khu Thêu" },
                    { new Guid("b1c2d3e4-0006-0006-0006-000000000006"), "KLK", "Khu Lưu Kho Thiết Bị" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "Description", "DeviceCode", "DeviceName", "InstallationDate", "IsUnderWarranty", "MachineId", "ManufactureDate", "Manufacturer", "Model", "PhotoUrl", "PositionId", "PurchasePrice", "SerialNumber", "Specifications", "Status", "Supplier" },
                values: new object[] { new Guid("66666666-6666-6666-6666-666666666667"), "Compact sewing machine", "SM004", "Sewing Machine 4", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, null, new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "Model F", "http://example.com/photos/sm004.jpg", null, 1000.00m, "SN006", "{\"speed\": \"800 SPM\", \"type\": \"Compact\"}", "Active", "Brother Industries" });

            migrationBuilder.InsertData(
                table: "Errors",
                columns: new[] { "Id", "Description", "ErrorCode", "EstimatedRepairTime", "IsCommon", "Name", "OccurrenceCount", "Severity" },
                values: new object[,]
                {
                    { new Guid("e1d1a111-0001-0001-0001-000000000001"), "Bàn đạp không phản hồi hoặc mất tín hiệu.", "HONG_BAN_DAP", new TimeSpan(0, 1, 0, 0, 0), true, "Hỏng Bàn Đạp", 20, "Medium" },
                    { new Guid("e1d1a123-0015-0015-0015-000000000015"), "Quạt tản nhiệt không hoạt động gây quá nhiệt.", "LOI_QUAT_GIO", new TimeSpan(0, 1, 0, 0, 0), true, "Lỗi Quạt Gió", 7, "Low" },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), "Trục chính không quay đồng tâm gây rung.", "TRUC_CHINH_LAC", new TimeSpan(0, 2, 0, 0, 0), false, "Trục Chính Lệch", 4, "High" },
                    { new Guid("e1d1a125-0017-0017-0017-000000000017"), "Đèn máy không sáng do đuôi đèn bị hỏng.", "DUI_DEN_CHAY", new TimeSpan(0, 0, 20, 0, 0), true, "Đuôi Đèn Cháy", 15, "Low" },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), "Bộ điều khiển không lưu lại các thiết lập máy.", "MAT_BO_NHO_LUU_THONG_SO", new TimeSpan(0, 1, 30, 0, 0), false, "Mất Bộ Nhớ Lưu Thông Số", 2, "High" },
                    { new Guid("e1d1a127-0019-0019-0019-000000000019"), "Áp lực chân vịt không ổn định do cảm biến sai số.", "CAM_BIEN_AP_LUC_LOI", new TimeSpan(0, 1, 0, 0, 0), false, "Cảm Biến Áp Lực Lỗi", 5, "Medium" },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), "Vải bị kéo không đều do lỗi bộ cấp vải.", "ROONG_KHONG_DU_SIEU", new TimeSpan(0, 1, 0, 0, 0), true, "Rong Không Đủ Siêu", 10, "Medium" },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), "Bộ phận giữ chỉ không đủ lực siết, gây bung chỉ khi may.", "MO_TROI_CHI", new TimeSpan(0, 0, 45, 0, 0), true, "Mỏ Trói Chỉ Bị Lỏng", 13, "Medium" },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), "Bánh răng truyền động bị mòn, phát ra tiếng kêu hoặc trượt.", "BANH_RANG_MON", new TimeSpan(0, 1, 30, 0, 0), true, "Bánh Răng Mòn", 17, "Medium" },
                    { new Guid("e1d1a131-0023-0023-0023-000000000023"), "Bộ phận điều khiển bằng tay không ăn khớp với cơ cấu truyền động.", "CAM_TAY_KHONG_AN_KHOP", new TimeSpan(0, 1, 0, 0, 0), false, "Cần Tay Không Ăn Khớp", 3, "Low" },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), "Kim va vào mặt vải hoặc phụ liệu, có thể gây hỏng bề mặt.", "KIM_CHAM_VAI", new TimeSpan(0, 0, 30, 0, 0), true, "Kim Chạm Vải", 22, "Low" },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), "Dây nối từ nút khởi động đến động cơ bị hở hoặc đứt.", "DAY_KHOI_DONG_LOI", new TimeSpan(0, 1, 0, 0, 0), false, "Dây Khởi Động Lỗi", 6, "High" },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), "Một số bu lông cố định các cụm máy bị lỏng gây rung lắc.", "BULONG_LONG", new TimeSpan(0, 0, 40, 0, 0), true, "Bu Lông Lỏng", 19, "Medium" },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), "Hỏng mạch điện đèn chiếu sáng, gây mất tầm nhìn khu vực may.", "MACH_DEN_LOI", new TimeSpan(0, 0, 30, 0, 0), true, "Mạch Đèn Lỗi", 12, "Low" },
                    { new Guid("e1d1a136-0028-0028-0028-000000000028"), "Dầu bôi trơn ra quá nhiều gây loang vải hoặc trơn trượt bộ truyền.", "DAU_BO_NHIEU", new TimeSpan(0, 1, 0, 0, 0), true, "Dầu Bôi Trơn Nhiều", 11, "Medium" },
                    { new Guid("e1d1a137-0029-0029-0029-000000000029"), "Quạt thông gió hoạt động yếu, không đủ làm mát cho mô tơ.", "QUAT_THONG_GIO_YEU", new TimeSpan(0, 1, 30, 0, 0), false, "Quạt Thông Gió Yếu", 4, "Medium" },
                    { new Guid("e1d1a138-0030-0030-0030-000000000030"), "Cửa kim không thẳng hàng với trục kim gây lệch đường may.", "CUA_KIM_LECH", new TimeSpan(0, 1, 0, 0, 0), true, "Cửa Kim Lệch", 20, "Medium" },
                    { new Guid("e1d1a222-0002-0002-0002-000000000002"), "Dây curoa lỏng hoặc mòn, gây mất chuyển động.", "DAYCUROA_TRUOT", new TimeSpan(0, 1, 30, 0, 0), true, "Dây Curoa Trượt", 15, "Medium" },
                    { new Guid("e1d1a333-0003-0003-0003-000000000003"), "Bo điều khiển bị lỗi, không kiểm soát được tốc độ.", "MAY_CHAY_LUON_LUOT", new TimeSpan(0, 2, 0, 0, 0), false, "Máy Chạy Luôn Lượt", 5, "High" },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), "Động cơ chính bị cháy do quá tải hoặc ngắn mạch.", "CHAY_MOTOR", new TimeSpan(0, 3, 0, 0, 0), false, "Cháy Motor", 3, "High" },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), "Cơ chế giữ kim bị lệch hoặc gãy.", "KHOA_KIM_HONG", new TimeSpan(0, 0, 40, 0, 0), true, "Khóa Kim Hỏng", 18, "Medium" },
                    { new Guid("e1d1a666-0006-0006-0006-000000000006"), "Dầu rò ra ngoài do gioăng hoặc phớt bị mòn.", "GIOANG_DAU_BI_RO", new TimeSpan(0, 1, 30, 0, 0), true, "Gioăng Dầu Bị Rò", 10, "Medium" },
                    { new Guid("e1d1a777-0007-0007-0007-000000000007"), "Cảm biến vị trí bị lệch dẫn đến dừng máy không đúng lúc.", "CAM_BIEN_LECH", new TimeSpan(0, 1, 0, 0, 0), false, "Cảm Biến Lệch", 6, "Low" },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), "Bo mạch điều khiển bị chập, không phản hồi.", "LOI_MACH_DIEU_KHIEN", new TimeSpan(0, 2, 0, 0, 0), false, "Lỗi Mạch Điều Khiển", 4, "High" },
                    { new Guid("e1d1a999-0009-0009-0009-000000000009"), "Cơ chế chống trôi vải không ăn khớp.", "CHONG_TROI_KHONG_HOAT_DONG", new TimeSpan(0, 0, 50, 0, 0), true, "Chống Trôi Không Hoạt Động", 12, "Medium" },
                    { new Guid("e1d1abbb-0010-0010-0010-000000000010"), "Chốt vải bị kẹt, gây gián đoạn chu trình may.", "CHOT_VAI_KET", new TimeSpan(0, 0, 30, 0, 0), true, "Chốt Vải Kẹt", 22, "Low" },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), "Kim không đúng trục tâm, đâm lệch lỗ.", "KIM_LOI_TAM", new TimeSpan(0, 1, 0, 0, 0), true, "Kim Lỗi Tâm", 18, "Medium" },
                    { new Guid("e1d1addd-0011-0011-0011-000000000011"), "Vòng bạc trục bị mòn dẫn đến rung lắc hoặc tiếng ồn lớn.", "VONG_BAC_MON", new TimeSpan(0, 1, 30, 0, 0), true, "Vòng Bạc Mòn", 14, "Medium" },
                    { new Guid("e1d1aeee-0012-0012-0012-000000000012"), "Dao cắt không bén, gây xơ vải hoặc rách mép.", "DAO_CAT_KHONG_SAC", new TimeSpan(0, 0, 30, 0, 0), true, "Dao Cắt Không Sắc", 25, "Low" },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), "Cảm biến không phát hiện được vải khi đưa vào.", "CAM_BIEN_VAI_KHONG_NHAN", new TimeSpan(0, 1, 0, 0, 0), false, "Cảm Biến Vải Không Nhận", 6, "Medium" }
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
                table: "Machines",
                columns: new[] { "Id", "Description", "MachineCode", "MachineName", "Manufacturer", "Model", "PhotoUrl", "ReleaseDate", "Specifications", "Status" },
                values: new object[,]
                {
                    { new Guid("a1b2c3d4-0001-0001-0001-000000000001"), "Máy may kim đơn tốc độ cao, đơn vị 1, dùng cho vải nhẹ.", "MC001-JUKI-DDL8700-01", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_01.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-001", "Active" },
                    { new Guid("a1b2c3d4-0002-0002-0002-000000000002"), "Máy may kim đơn tốc độ cao, đơn vị 2, dùng cho vải trung bình.", "MC002-JUKI-DDL8700-02", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_02.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-002", "Active" },
                    { new Guid("a1b2c3d4-0003-0003-0003-000000000003"), "Máy may kim đơn tốc độ cao, đơn vị 3, đang bảo trì.", "MC003-JUKI-DDL8700-03", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_03.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-003", "InRepair" },
                    { new Guid("a1b2c3d4-0004-0004-0004-000000000004"), "Máy may kim đơn tốc độ cao, đơn vị 4, dùng cho vải cotton.", "MC004-JUKI-DDL8700-04", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_04.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-004", "Active" },
                    { new Guid("a1b2c3d4-0005-0005-0005-000000000005"), "Máy may kim đơn tốc độ cao, đơn vị 5, dùng cho vải tổng hợp.", "MC005-JUKI-DDL8700-05", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_05.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-005", "Active" },
                    { new Guid("a1b2c3d4-0006-0006-0006-000000000006"), "Máy may kim đơn tốc độ cao, đơn vị 6, đã ngừng sử dụng.", "MC006-JUKI-DDL8700-06", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_06.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-006", "Retired" },
                    { new Guid("a1b2c3d4-0007-0007-0007-000000000007"), "Máy may kim đơn tốc độ cao, đơn vị 7, dùng cho vải dày.", "MC007-JUKI-DDL8700-07", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_07.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-007", "Active" },
                    { new Guid("a1b2c3d4-0008-0008-0008-000000000008"), "Máy may kim đơn tốc độ cao, đơn vị 8, dùng cho vải mỏng.", "MC008-JUKI-DDL8700-08", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_08.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J87008", "Active" },
                    { new Guid("a1b2c3d4-0009-0009-0009-000000000009"), "Máy may kim đơn tốc độ cao, đơn vị 9, dùng cho vải hỗn hợp.", "MC009-JUKI-DDL8700-09", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_09.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-009", "Active" },
                    { new Guid("a1b2c3d4-0010-0010-0010-000000000010"), "Máy may kim đơn tốc độ cao, đơn vị 10, dùng cho mục đích chung.", "MC010-JUKI-DDL8700-10", "Máy May Công Nghiệp", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_10.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5500 SPM, Độ dài mũi may tối đa: 5mm, Serial: J8700-010", "Active" },
                    { new Guid("a1b2c3d4-0011-0011-0011-000000000011"), "Máy may kim đơn kỹ thuật số với chức năng cắt chỉ tự động, đơn vị 1.", "MC011-JUKI-DDL9000C-01", "Máy May Kim Đơn Kỹ Thuật Số", "Juki", "DDL-9000C", "https://example.com/photos/juki_ddl9000c_01.jpg", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: J9000C-001", "Active" },
                    { new Guid("a1b2c3d4-0012-0012-0012-000000000012"), "Máy may kim đơn kỹ thuật số với chức năng cắt chỉ tự động, đơn vị 2.", "MC012-JUKI-DDL9000C-02", "Máy May Kim Đơn Kỹ Thuật Số", "Juki", "DDL-9000C", "https://example.com/photos/juki_ddl9000c_02.jpg", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: J9000C-002", "Active" },
                    { new Guid("a1b2c3d4-0013-0013-0013-000000000013"), "Máy may kim đơn kỹ thuật số với chức năng cắtρού chỉ tự động, đơn vị 3, đang bảo trì.", "MC013-JUKI-DDL9000C-03", "Máy May Kim Đơn Kỹ Thuật Số", "Juki", "DDL-9000C", "https://example.com/photos/juki_ddl9000c_03.jpg", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: J9000C-003", "InRepair" },
                    { new Guid("a1b2c3d4-0014-0014-0014-000000000014"), "Máy may kim đơn kỹ thuật số với chức năng cắt chỉ tự động, đơn vị 4.", "MC014-JUKI-DDL9000C-04", "Máy May Kim Đơn Kỹ Thuật Số", "Juki", "DDL-9000C", "https://example.com/photos/juki_ddl9000c_04.jpg", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: J9000C-004", "Active" },
                    { new Guid("a1b2c3d4-0015-0015-0015-000000000015"), "Máy vắt sổ 3 chỉ, đơn vị 1, dùng cho vải nhẹ.", "MC015-BROTHER-B957-01", "Máy Vắt Sổ", "Brother", "B957", "https://example.com/photos/brother_b957_01.jpg", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B957-001", "Active" },
                    { new Guid("a1b2c3d4-0016-0016-0016-000000000016"), "Máy vắt sổ 3 chỉ, đơn vị 2, dùng cho vải tổng hợp.", "MC016-BROTHER-B957-02", "Máy Vắt Sổ", "Brother", "B957", "https://example.com/photos/brother_b957_02.jpg", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B957-002", "Active" },
                    { new Guid("a1b2c3d4-0017-0017-0017-000000000017"), "Máy vắt sổ 3 chỉ, đơn vị 3, dùng cho vải mỏng.", "MC017-BROTHER-B957-03", "Máy Vắt Sổ", "Brother", "B957", "https://example.com/photos/brother_b957_03.jpg", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B957-003", "Active" },
                    { new Guid("a1b2c3d4-0018-0018-0018-000000000018"), "Máy may nặng cho vật liệu dày, đơn vị 1, dùng cho vải denim.", "MC018-SINGER-4452-01", "Máy May Nặng", "Singer", "4452", "https://example.com/photos/singer_4452_01.jpg", new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4452-001", "Active" },
                    { new Guid("a1b2c3d4-0019-0019-0019-000000000019"), "Máy may nặng cho vật liệu dày, đơn vị 2, dùng cho da.", "MC019-SINGER-4452-02", "Máy May Nặng", "Singer", "4452", "https://example.com/photos/singer_4452_02.jpg", new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4452-002", "Active" },
                    { new Guid("a1b2c3d4-0020-0020-0020-000000000020"), "Máy may nặng cho vật liệu dày, đơn vị 3, dùng cho vải canvas, đang bảo trì.", "MC020-SINGER-4452-03", "Máy May Nặng", "Singer", "4452", "https://example.com/photos/singer_4452_03.jpg", new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4452-003", "InRepair" },
                    { new Guid("a1b2c3d4-0021-0021-0021-000000000021"), "Máy vắt sổ 4 chỉ tốc độ cao, đơn vị 1, dùng cho vải cotton và vải tổng hợp.", "MC021-JUKI-MO6714S-01", "Máy Vắt Sổ Công Nghiệp", "Juki", "MO-6714S", "https://example.com/photos/juki_mo6714s_01.jpg", new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: MO6714S-001", "Active" },
                    { new Guid("a1b2c3d4-0022-0022-0022-000000000022"), "Máy vắt sổ 4 chỉ tốc độ cao, đơn vị 2, dùng cho vải nhẹ, đang bảo trì.", "MC022-JUKI-MO6714S-02", "Máy Vắt Sổ Công Nghiệp", "Juki", "MO-6714S", "https://example.com/photos/juki_mo6714s_02.jpg", new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: MO6714S-002", "InRepair" },
                    { new Guid("a1b2c3d4-0023-0023-0023-000000000023"), "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đơn vị 1, dùng cho vải trung bình.", "MC023-BROTHER-S7200C-01", "Máy May Kim Đơn Kỹ Thuật Số", "Brother", "S-7200C", "https://example.com/photos/brother_s7200c_01.jpg", new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: S7200C-001", "Active" },
                    { new Guid("a1b2c3d4-0024-0024-0024-000000000024"), "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đơn vị 2, dùng cho vải dày.", "MC024-BROTHER-S7200C-02", "Máy May Kim Đơn Kỹ Thuật Số", "Brother", "S-7200C", "https://example.com/photos/brother_s7200c_02.jpg", new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động, Serial: S7200C-002", "Active" },
                    { new Guid("a1b2c3d4-0025-0025-0025-000000000025"), "Máy may nặng cho vật liệu dày, đơn vị 1, dùng cho vải denim và canvas.", "MC025-SINGER-4423-01", "Máy May Nặng", "Singer", "4423", "https://example.com/photos/singer_4423_01.jpg", new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4423-001", "Active" },
                    { new Guid("a1b2c3d4-0026-0026-0026-000000000026"), "Máy may nặng cho vật liệu dày, đơn vị 2, dùng cho da, đã ngừng sử dụng.", "MC026-SINGER-4423-02", "Máy May Nặng", "Singer", "4423", "https://example.com/photos/singer_4423_02.jpg", new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm, Serial: S4423-002", "Retired" },
                    { new Guid("a1b2c3d4-0027-0027-0027-000000000027"), "Máy may hai kim công nghiệp, đơn vị 1, dùng cho vải jeans.", "MC027-JUKI-LH3568S-01", "Máy May Hai Kim", "Juki", "LH-3568S", "https://example.com/photos/juki_lh3568s_01.jpg", new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 3000 SPM, Độ dài mũi may tối đa: 5mm, Serial: LH3568S-001", "Active" },
                    { new Guid("a1b2c3d4-0028-0028-0028-000000000028"), "Máy may hai kim công nghiệp, đơn vị 2, dùng cho vải dày, đang bảo trì.", "MC028-JUKI-LH3568S-02", "Máy May Hai Kim", "Juki", "LH-3568S", "https://example.com/photos/juki_lh3568s_02.jpg", new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 3000 SPM, Độ dài mũi may tối đa: 5mm, Serial: LH3568S-002", "InRepair" },
                    { new Guid("a1b2c3d4-0029-0029-0029-000000000029"), "Máy vắt sổ 3 chỉ, đơn vị 1, dùng cho vải mỏng và vải tổng hợp.", "MC029-BROTHER-B735-01", "Máy Vắt Sổ", "Brother", "B735", "https://example.com/photos/brother_b735_01.jpg", new DateTime(2020, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 6500 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B735-001", "Active" },
                    { new Guid("a1b2c3d4-0030-0030-0030-000000000030"), "Máy vắt sổ 3 chỉ, đơn vị 2, dùng cho vải cotton, đã ngừng sử dụng.", "MC030-BROTHER-B735-02", "Máy Vắt Sổ", "Brother", "B735", "https://example.com/photos/brother_b735_02.jpg", new DateTime(2020, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tốc độ tối đa: 6500 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0, Serial: B735-002", "Retired" }
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
                    { new Guid("23333333-3333-3333-3333-333333333343"), null, "tech2@gmail.com", null, "Head of Tech 2", null, null, "String123!", "09785628660", null, null, 2, null, "Head of Tech 3" },
                    { new Guid("23333333-3333-3333-3333-333333333344"), null, "tech3@gmail.com", null, "Head of Tech 3", null, null, "String123!", "09785628660", null, null, 2, null, "Head of Tech 3" },
                    { new Guid("32222222-2222-2222-2222-222222222222"), null, "hot@gmail.com", null, "Head of Team", null, null, "String123!", "09785628660", null, null, 2, null, "Head of Team" },
                    { new Guid("43333333-3333-3333-3333-333333333333"), null, "staff@gmail.com", null, "Staff Member", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member" },
                    { new Guid("43333333-3333-3333-3333-333333333334"), null, "staff2@gmail.com", null, "Staff Member 2", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 2" },
                    { new Guid("43333333-3333-3333-3333-333333333335"), null, "staff3@gmail.com", null, "Staff Member 3", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 3" },
                    { new Guid("43333333-3333-3333-3333-333333333336"), null, "staff4@gmail.com", null, "Staff Member 4", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 4" },
                    { new Guid("43333333-3333-3333-3333-333333333337"), null, "staff5@gmail.com", null, "Staff Member 5", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 5" },
                    { new Guid("43333333-3333-3333-3333-333333333338"), null, "staff6@gmail.com", null, "Staff Member 6", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 6" },
                    { new Guid("43333333-3333-3333-3333-333333333339"), null, "staff7@gmail.com", null, "Staff Member 7", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 7" },
                    { new Guid("43333333-3333-3333-3333-333333333340"), null, "staff8@gmail.com", null, "Staff Member 8", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 8" },
                    { new Guid("43333333-3333-3333-3333-333333333341"), null, "staff9@gmail.com", null, "Staff Member 9", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 9" },
                    { new Guid("43333333-3333-3333-3333-333333333342"), null, "staff10@gmail.com", null, "Staff Member 10", null, null, "String123!", "09785628660", null, null, 3, null, "Staff Member 10" },
                    { new Guid("54444444-4444-4444-4444-444444444444"), null, "sk@gmail.com", null, "Support Staff", null, null, "String123!", "09785628660", null, null, 4, null, "Support Staff" },
                    { new Guid("65555555-5555-5555-5555-555555555555"), null, "admin@gmail.com", null, "Administrator", null, null, "String123!", "09785628660", null, null, 5, null, "Administrator" }
                });

            migrationBuilder.InsertData(
                table: "Devices",
                columns: new[] { "Id", "Description", "DeviceCode", "DeviceName", "InstallationDate", "IsUnderWarranty", "MachineId", "ManufactureDate", "Manufacturer", "Model", "PhotoUrl", "PositionId", "PurchasePrice", "SerialNumber", "Specifications", "Status", "Supplier" },
                values: new object[,]
                {
                    { new Guid("d1e2f3a4-0001-0001-0001-000000000001"), "Máy may kim đơn tốc độ cao cho vải nhẹ.", "DEV001-JUKI-DDL8700-01", "Juki DDL-8700 Unit 1", new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0001-0001-0001-000000000001"), new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_01.jpg", new Guid("f1e2d3c4-0001-0001-0001-000000000001"), 15000000m, "J8700-D001", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0002-0002-0002-000000000002"), "Máy may kim đơn tốc độ cao cho vải trung bình.", "DEV002-JUKI-DDL8700-02", "Juki DDL-8700 Unit 2", new DateTime(2020, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0002-0002-0002-000000000002"), new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_02.jpg", new Guid("f1e2d3c4-0002-0002-0002-000000000002"), 15000000m, "J8700-D002", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0003-0003-0003-000000000003"), "Máy may kim đơn tốc độ cao, đang sửa chữa.", "DEV003-JUKI-DDL8700-03", "Juki DDL-8700 Unit 3", new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0003-0003-0003-000000000003"), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_03.jpg", new Guid("f1e2d3c4-0014-0014-0014-000000000014"), 15000000m, "J8700-D003", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "InRepair", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0004-0004-0004-000000000004"), "Máy may kim đơn tốc độ cao cho vải cotton.", "DEV004-JUKI-DDL8700-04", "Juki DDL-8700 Unit 4", new DateTime(2020, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0004-0004-0004-000000000004"), new DateTime(2020, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_04.jpg", new Guid("f1e2d3c4-0003-0003-0003-000000000003"), 15000000m, "J8700-D004", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0005-0005-0005-000000000005"), "Máy may kim đơn tốc độ cao cho vải tổng hợp.", "DEV005-JUKI-DDL8700-05", "Juki DDL-8700 Unit 5", new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0005-0005-0005-000000000005"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_05.jpg", new Guid("f1e2d3c4-0004-0004-0004-000000000004"), 15000000m, "J8700-D005", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0006-0006-0006-000000000006"), "Máy may kim đơn tốc độ cao, đã ngừng sử dụng.", "DEV006-JUKI-DDL8700-06", "Juki DDL-8700 Unit 6", new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0006-0006-0006-000000000006"), new DateTime(2020, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_06.jpg", new Guid("f1e2d3c4-0015-0015-0015-000000000015"), 15000000m, "J8700-D006", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Retired", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0007-0007-0007-000000000007"), "Máy may kim đơn tốc độ cao cho vải dày.", "DEV007-JUKI-DDL8700-07", "Juki DDL-8700 Unit 7", new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0007-0007-0007-000000000007"), new DateTime(2020, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_07.jpg", new Guid("f1e2d3c4-0006-0006-0006-000000000006"), 15000000m, "J8700-D007", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0008-0008-0008-000000000008"), "Máy may kim đơn tốc độ cao cho vải mỏng.", "DEV008-JUKI-DDL8700-08", "Juki DDL-8700 Unit 8", new DateTime(2020, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0008-0008-0008-000000000008"), new DateTime(2020, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_08.jpg", new Guid("f1e2d3c4-0007-0007-0007-000000000007"), 15000000m, "J8700-D008", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0009-0009-0009-000000000009"), "Máy may kim đơn tốc độ cao cho vải hỗn hợp.", "DEV009-JUKI-DDL8700-09", "Juki DDL-8700 Unit 9", new DateTime(2020, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0009-0009-0009-000000000009"), new DateTime(2020, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_09.jpg", new Guid("f1e2d3c4-0010-0010-0010-000000000010"), 15000000m, "J8700-D009", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0010-0010-0010-000000000010"), "Máy may kim đơn tốc độ cao cho mục đích chung.", "DEV010-JUKI-DDL8700-10", "Juki DDL-8700 Unit 10", new DateTime(2020, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0010-0010-0010-000000000010"), new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_10.jpg", new Guid("f1e2d3c4-0011-0011-0011-000000000011"), 15000000m, "J8700-D010", "Tốc độ tối đa: 5500 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0011-0011-0011-000000000011"), "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đơn vị 1.", "DEV011-JUKI-DDL9000C-01", "Juki DDL-9000C Unit 1", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0011-0011-0011-000000000011"), new DateTime(2022, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-9000C", "https://example.com/photos/device_juki_ddl9000c_01.jpg", new Guid("f1e2d3c4-0008-0008-0008-000000000008"), 20000000m, "J9000C-D001", "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0012-0012-0012-000000000012"), "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đơn vị 2.", "DEV012-JUKI-DDL9000C-02", "Juki DDL-9000C Unit 2", new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0012-0012-0012-000000000012"), new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-9000C", "https://example.com/photos/device_juki_ddl9000c_02.jpg", new Guid("f1e2d3c4-0012-0012-0012-000000000012"), 20000000m, "J9000C-D002", "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0013-0013-0013-000000000013"), "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đang sửa chữa.", "DEV013-JUKI-DDL9000C-03", "Juki DDL-9000C Unit 3", new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0013-0013-0013-000000000013"), new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-9000C", "https://example.com/photos/device_juki_ddl9000c_03.jpg", new Guid("f1e2d3c4-0028-0028-0028-000000000028"), 20000000m, "J9000C-D003", "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động", "InRepair", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0014-0014-0014-000000000014"), "Máy may kim đơn kỹ thuật số với cắt chỉ tự động, đơn vị 4.", "DEV014-JUKI-DDL9000C-04", "Juki DDL-9000C Unit 4", new DateTime(2022, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0014-0014-0014-000000000014"), new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-9000C", "https://example.com/photos/device_juki_ddl9000c_04.jpg", new Guid("f1e2d3c4-0013-0013-0013-000000000013"), 20000000m, "J9000C-D004", "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0015-0015-0015-000000000015"), "Máy vắt sổ 3 chỉ cho vải nhẹ.", "DEV015-BROTHER-B957-01", "Brother B957 Unit 1", new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0015-0015-0015-000000000015"), new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "B957", "https://example.com/photos/device_brother_b957_01.jpg", new Guid("f1e2d3c4-0019-0019-0019-000000000019"), 12000000m, "B957-D001", "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0016-0016-0016-000000000016"), "Máy vắt sổ 3 chỉ cho vải tổng hợp.", "DEV016-BROTHER-B957-02", "Brother B957 Unit 2", new DateTime(2019, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0016-0016-0016-000000000016"), new DateTime(2019, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "B957", "https://example.com/photos/device_brother_b957_02.jpg", new Guid("f1e2d3c4-0020-0020-0020-000000000020"), 12000000m, "B957-D002", "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0017-0017-0017-000000000017"), "Máy vắt sổ 3 chỉ cho vải mỏng.", "DEV017-BROTHER-B957-03", "Brother B957 Unit 3", new DateTime(2019, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0017-0017-0017-000000000017"), new DateTime(2019, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "B957", "https://example.com/photos/device_brother_b957_03.jpg", new Guid("f1e2d3c4-0021-0011-0021-000000000021"), 12000000m, "B957-D003", "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0018-0018-0018-000000000018"), "Máy may nặng cho vải denim.", "DEV018-SINGER-4452-01", "Singer 4452 Unit 1", new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0018-0018-0018-000000000018"), new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "4452", "https://example.com/photos/device_singer_4452_01.jpg", new Guid("f1e2d3c4-0023-0023-0023-000000000023"), 18000000m, "S4452-D001", "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm", "Active", "Singer Vietnam" },
                    { new Guid("d1e2f3a4-0019-0019-0019-000000000019"), "Máy may nặng cho vải da.", "DEV019-SINGER-4452-02", "Singer 4452 Unit 2", new DateTime(2021, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0019-0019-0019-000000000019"), new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "4452", "https://example.com/photos/device_singer_4452_02.jpg", new Guid("f1e2d3c4-0024-0024-0024-000000000024"), 18000000m, "S4452-D002", "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm", "Active", "Singer Vietnam" },
                    { new Guid("d1e2f3a4-0020-0020-0020-000000000020"), "Máy may nặng cho vải canvas, đang sửa chữa.", "DEV020-SINGER-4452-03", "Singer 4452 Unit 3", new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0020-0020-0020-000000000020"), new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "4452", "https://example.com/photos/device_singer_4452_03.jpg", new Guid("f1e2d3c4-0029-0029-0029-000000000029"), 18000000m, "S4452-D003", "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm", "InRepair", "Singer Vietnam" },
                    { new Guid("d1e2f3a4-0021-0021-0021-000000000021"), "Máy vắt sổ 4 chỉ tốc độ cao cho vải cotton và tổng hợp.", "DEV021-JUKI-MO6714S-01", "Juki MO-6714S Unit 1", new DateTime(2021, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0021-0021-0021-000000000021"), new DateTime(2021, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "MO-6714S", "https://example.com/photos/device_juki_mo6714s_01.jpg", new Guid("f1e2d3c4-0009-0009-0009-000000000009"), 14000000m, "MO6714S-D001", "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0022-0022-0022-000000000022"), "Máy vắt sổ 4 chỉ tốc độ cao cho vải nhẹ, đang sửa chữa.", "DEV022-JUKI-MO6714S-02", "Juki MO-6714S Unit 2", new DateTime(2021, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0022-0022-0022-000000000022"), new DateTime(2021, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "MO-6714S", "https://example.com/photos/device_juki_mo6714s_02.jpg", new Guid("f1e2d3c4-0026-0026-0026-000000000026"), 14000000m, "MO6714S-D002", "Tốc độ tối đa: 7000 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0", "InRepair", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0023-0023-0023-000000000023"), "Máy may kim đơn kỹ thuật số với cắt chỉ tự động cho vải trung bình.", "DEV023-BROTHER-S7200C-01", "Brother S-7200C Unit 1", new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0023-0023-0023-000000000023"), new DateTime(2022, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "S-7200C", "https://example.com/photos/device_brother_s7200c_01.jpg", new Guid("f1e2d3c4-0005-0005-0005-000000000005"), 22000000m, "S7200C-D001", "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0024-0024-0024-000000000024"), "Máy may kim đơn kỹ thuật số với cắt chỉ tự động cho vải dày.", "DEV024-BROTHER-S7200C-02", "Brother S-7200C Unit 2", new DateTime(2022, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0024-0024-0024-000000000024"), new DateTime(2022, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "S-7200C", "https://example.com/photos/device_brother_s7200c_02.jpg", new Guid("f1e2d3c4-0022-0022-0022-000000000022"), 22000000m, "S7200C-D002", "Tốc độ tối đa: 5000 SPM, Cắt chỉ tự động", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0025-0025-0025-000000000025"), "Máy may nặng cho vải denim và canvas.", "DEV025-SINGER-4423-01", "Singer 4423 Unit 1", new DateTime(2020, 11, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0025-0025-0025-000000000025"), new DateTime(2020, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "4423", "https://example.com/photos/device_singer_4423_01.jpg", new Guid("f1e2d3c4-0025-0025-0025-000000000025"), 17000000m, "S4423-D001", "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm", "Active", "Singer Vietnam" },
                    { new Guid("d1e2f3a4-0026-0026-0026-000000000026"), "Máy may nặng cho vải da, đã ngừng sử dụng.", "DEV026-SINGER-4423-02", "Singer 4423 Unit 2", new DateTime(2020, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0026-0026-0026-000000000026"), new DateTime(2020, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "4423", "https://example.com/photos/device_singer_4423_02.jpg", new Guid("f1e2d3c4-0030-0030-0030-000000000030"), 17000000m, "S4423-D002", "Tốc độ tối đa: 1100 SPM, Độ nâng chân vịt: 6mm", "Retired", "Singer Vietnam" },
                    { new Guid("d1e2f3a4-0027-0027-0027-000000000027"), "Máy may hai kim công nghiệp cho vải jeans.", "DEV027-JUKI-LH3568S-01", "Juki LH-3568S Unit 1", new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0027-0027-0027-000000000027"), new DateTime(2021, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "LH-3568S", "https://example.com/photos/device_juki_lh3568s_01.jpg", new Guid("f1e2d3c4-0016-0016-0016-000000000016"), 25000000m, "LH3568S-D001", "Tốc độ tối đa: 3000 SPM, Độ dài mũi may: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0028-0028-0028-000000000028"), "Máy may hai kim công nghiệp cho vải dày, đang sửa chữa.", "DEV028-JUKI-LH3568S-02", "Juki LH-3568S Unit 2", new DateTime(2022, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0028-0028-0028-000000000028"), new DateTime(2021, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "LH-3568S", "https://example.com/photos/device_juki_lh3568s_02.jpg", new Guid("f1e2d3c4-0018-0018-0018-000000000018"), 25000000m, "LH3568S-D002", "Tốc độ tối đa: 3000 SPM, Độ dài mũi may: 5mm", "InRepair", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0029-0029-0029-000000000029"), "Máy vắt sổ 3 chỉ cho vải mỏng và tổng hợp.", "DEV029-BROTHER-B735-01", "Brother B735 Unit 1", new DateTime(2020, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0029-0029-0029-000000000029"), new DateTime(2020, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "B735", "https://example.com/photos/device_brother_b735_01.jpg", new Guid("f1e2d3c4-0017-0017-0017-000000000017"), 13000000m, "B735-D001", "Tốc độ tối đa: 6500 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0030-0030-0030-000000000030"), "Máy vắt sổ 3 chỉ cho vải cotton, đã ngừng sử dụng.", "DEV030-BROTHER-B735-02", "Brother B735 Unit 2", new DateTime(2020, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0030-0030-0030-000000000030"), new DateTime(2020, 4, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "B735", "https://example.com/photos/device_brother_b735_02.jpg", new Guid("f1e2d3c4-0027-0027-0027-000000000027"), 13000000m, "B735-D002", "Tốc độ tối đa: 6500 SPM, Tỷ lệ cấp liệu vi sai: 0.7-2.0", "Retired", "Brother Vietnam" }
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
                columns: new[] { "ErrorId", "IssueId", "Id" },
                values: new object[,]
                {
                    { new Guid("e1d1a123-0015-0015-0015-000000000015"), new Guid("11111111-1111-1111-1111-111111111111"), new Guid("aaac1814-a74e-4d87-85b4-bd491c894985") },
                    { new Guid("e1d1a131-0023-0023-0023-000000000023"), new Guid("12121212-1212-1212-1212-121212121212"), new Guid("783b1ab1-a7ba-4211-a969-f2ce6ca044a2") },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), new Guid("12121212-1212-1212-1212-121212121212"), new Guid("2e520c0b-6f37-4485-bdec-eedfb41da569") },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("960b8979-b4fd-4c60-bde0-f46ca0881260") },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), new Guid("23232323-2323-2323-2323-232323232323"), new Guid("02cc68ba-a5ef-401e-81e4-cb0e40c568dd") },
                    { new Guid("e1d1a222-0002-0002-0002-000000000002"), new Guid("23232323-2323-2323-2323-232323232323"), new Guid("bcc5fb79-6f7d-459e-b2e1-813552277f5f") },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("f5e91542-b7bd-4f21-a6c4-cd8d1aab5554") },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), new Guid("34343434-3434-3434-3434-343434343434"), new Guid("c769d254-d011-4338-9862-673e79c272f3") },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), new Guid("34343434-3434-3434-3434-343434343434"), new Guid("6e129b97-ef16-47c1-b5d5-59368b1bd1ea") },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), new Guid("34343434-3434-3434-3434-343434343434"), new Guid("0121cef8-7d01-45bd-8601-f7b43756c377") },
                    { new Guid("e1d1a666-0006-0006-0006-000000000006"), new Guid("44444444-4444-4444-4444-444444444444"), new Guid("5ac0aaa7-5f65-4497-bd05-d6e274e5f37f") },
                    { new Guid("e1d1a137-0029-0029-0029-000000000029"), new Guid("45454545-4545-4545-4545-454545454545"), new Guid("8bf17ac5-d961-45bb-8932-dff4924c8706") },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), new Guid("45454545-4545-4545-4545-454545454545"), new Guid("1a814f02-f3e6-4a87-a311-a3088f76c354") },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("e3951290-cf4a-4b00-b070-723ed4252b5e") },
                    { new Guid("e1d1addd-0011-0011-0011-000000000011"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("6fb97681-25c0-4372-8358-91a5cb557252") },
                    { new Guid("e1d1aeee-0012-0012-0012-000000000012"), new Guid("66666666-6666-6666-6666-666666666666"), new Guid("dc6a99c3-d847-4a59-88ed-e702eea1160a") },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), new Guid("77777777-7777-7777-7777-777777777777"), new Guid("3a18b908-b08e-4d76-b885-a8b3ba86ffb7") },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), new Guid("88888888-8888-8888-8888-888888888888"), new Guid("09712b14-d10e-4509-90cb-b12815aab911") },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("3d23d839-a558-459a-817c-1a61cc532f27") },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), new Guid("99999999-9999-9999-9999-999999999999"), new Guid("ac4f4881-a6ff-4a0c-b318-1e4c9881ebf1") },
                    { new Guid("e1d1a333-0003-0003-0003-000000000003"), new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), new Guid("111b1c09-f589-442a-9074-cc0e2067d564") },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("8ca08dcc-f299-4408-a85b-3910562e5825") },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("fe0fd05e-59f9-4580-9b7a-3861f23f2e85") },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("36205a62-fbfb-42ac-b92f-b6708e3a86b6") },
                    { new Guid("e1d1a111-0001-0001-0001-000000000001"), new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), new Guid("83d446a7-ac15-4333-8d2a-0b16ce1b56e6") },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("aca1e4c3-1541-4701-ab71-88d268c45663") },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("835ce18a-79d8-44fa-b4d9-0cc61c1b207e") },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("23cfdd31-1172-452e-8b83-3515d4c27525") },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), new Guid("81a50f72-1421-4257-ba37-6fca9d3b4d61") }
                });

            migrationBuilder.InsertData(
                table: "MachineErrorHistories",
                columns: new[] { "ErrorId", "MachineId", "CreatedBy", "CreatedDate", "Id", "IsDeleted", "LastOccurredDate", "ModifiedBy", "ModifiedDate", "Notes", "OccurrenceCount" },
                values: new object[,]
                {
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), new Guid("a1b2c3d4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1848), new Guid("ddf424a2-7d1f-4bbd-b667-be5a200e029d"), false, new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), null, null, "Mỏ trói chỉ bị lỏng, đã điều chỉnh lực siết.", 3 },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("a1b2c3d4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1856), new Guid("78cc7b6e-511d-42c2-b94d-90f2380101f1"), false, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), null, null, "Động cơ cháy do quá tải, cần thay mô tơ mới.", 1 },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("a1b2c3d4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1858), new Guid("821a5841-68ce-4a7d-b5bb-e9edc3aebca4"), false, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Mô tơ bị chập điện, đang chờ phụ tùng thay thế.", 1 },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), new Guid("a1b2c3d4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1860), new Guid("737c5a00-55a0-49ba-aea2-f17fdcc1aa72"), false, new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), null, null, "Kim lệch tâm, đã căn chỉnh lại trục kim.", 2 },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), new Guid("a1b2c3d4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1863), new Guid("429fb2ca-8a49-44fc-92cd-822c4113de91"), false, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), null, null, "Bánh răng mòn gây tiếng ồn, đã lên kế hoạch thay mới.", 2 },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), new Guid("a1b2c3d4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1865), new Guid("537e83d6-78ca-4c35-af1e-1d6d066c2613"), false, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), null, null, "Bo mạch điều khiển lỗi, đang kiểm tra để sửa chữa.", 1 },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), new Guid("a1b2c3d4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1868), new Guid("865eb029-b15b-4c77-a493-177314dbfdec"), false, new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), null, null, "Bulong lỏng ở bộ truyền, đã siết chặt lại.", 3 },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), new Guid("a1b2c3d4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1870), new Guid("c18a8250-9fb0-4ef7-aec6-9c14a07d4452"), false, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), null, null, "Cảm biến vải không nhận, đã thay cảm biến mới.", 2 },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("a1b2c3d4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1872), new Guid("3f8b3f82-196c-4fba-b1f4-9d551c73c974"), false, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), null, null, "Mô tơ bị cháy, đang chờ thay thế phụ tùng.", 1 },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), new Guid("a1b2c3d4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(1877), new Guid("0bc8b224-eb83-47e0-93a4-8ed24a4ba0f2"), false, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), null, null, "Bộ cấp vải hoạt động không đều, đã điều chỉnh lại.", 2 }
                });

            migrationBuilder.InsertData(
                table: "MachineIssueHistories",
                columns: new[] { "IssueId", "MachineId", "CreatedBy", "CreatedDate", "Id", "IsDeleted", "LastOccurredDate", "ModifiedBy", "ModifiedDate", "Notes", "OccurrenceCount" },
                values: new object[,]
                {
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("a1b2c3d4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4349), new Guid("03f1a1cf-1201-430d-bb94-c08659def9f0"), false, new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), null, null, "Chỉ bị đứt do kẹt ở ống chỉ, đã thay ống chỉ mới.", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a1b2c3d4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4354), new Guid("eaa24662-9d86-48d7-b12f-c92016dd9e7d"), false, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy không chạy do lỗi động cơ, đang chờ sửa chữa.", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a1b2c3d4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4357), new Guid("65f6125f-f6ee-447a-9147-7829b9b3c03c"), false, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy ngừng hoạt động, kiểm tra phát hiện lỗi dây điện.", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a1b2c3d4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4359), new Guid("48effd16-60a2-47a6-a4b8-eed6fd635b13"), false, new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), null, null, "Kim gãy do sử dụng sai loại kim, đã thay kim phù hợp.", 4 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("a1b2c3d4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4361), new Guid("8118690d-a354-4ef0-8132-cc7692692df6"), false, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), null, null, "Tiếng ồn lớn do bánh răng mòn, cần thay thế.", 2 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("a1b2c3d4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4363), new Guid("55285a08-44bd-45e9-b949-9639347a9b82"), false, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), null, null, "Đèn báo lỗi sáng, kiểm tra mạch điện tử đang được tiến hành.", 1 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("a1b2c3d4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4365), new Guid("aab0d30f-4aa4-4a64-93a3-1a0f82747204"), false, new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), null, null, "Chỉ không đều do bulong lỏng, đã siết lại.", 2 },
                    { new Guid("34343434-3434-3434-3434-343434343434"), new Guid("a1b2c3d4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4370), new Guid("ce69a83b-4304-41ee-96ad-8dfefcd5408f"), false, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), null, null, "Chỉ dưới không kéo lên, kiểm tra cảm biến và thay mới.", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a1b2c3d4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4372), new Guid("96e9858a-79e6-4408-9918-d390ed3c03d5"), false, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy không chạy, kiểm tra phát hiện lỗi mô tơ.", 1 },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("a1b2c3d4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 25, 13, 3, 10, 118, DateTimeKind.Utc).AddTicks(4375), new Guid("f2a12c9a-553d-4a43-a354-7f03c0b565fc"), false, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), null, null, "Vải bị nhăn do điều chỉnh áp suất không đúng, đã điều chỉnh lại.", 2 }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "AssigneeId", "DeviceCondition", "DeviceReturnTime", "EndTime", "ExpectedTime", "Priority", "ReportNotes", "StartTime", "Status", "TaskDescription", "TaskName", "TaskType" },
                values: new object[,]
                {
                    { new Guid("b1c2d3e4-0001-0001-0001-100000000001"), new Guid("43333333-3333-3333-3333-333333333333"), "Máy hoạt động bình thường sau khi thay mỏ trói chỉ.", new DateTime(2025, 4, 15, 16, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 15, 16, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 16, 10, 30, 0, 0, DateTimeKind.Utc), 2, "Đã kiểm tra và thay mỏ trói chỉ mới, vận hành thử ổn định.", new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), "Completed", "Sửa lỗi đứt chỉ do mỏ trói chỉ lỏng trên máy Juki DDL-8700 Unit 1 (MC001-JUKI-DDL8700-01).", "Repair Juki DDL-8700 Unit 1", "Maintenance" },
                    { new Guid("b1c2d3e4-0002-0002-0002-100000000002"), new Guid("43333333-3333-3333-3333-333333333334"), null, null, null, new DateTime(2025, 5, 4, 14, 0, 0, 0, DateTimeKind.Utc), 3, null, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), "InProgress", "Sửa lỗi máy không chạy do động cơ cháy trên máy Juki DDL-8700 Unit 3 (MC003-JUKI-DDL8700-03).", "Fix Motor Issue on Juki DDL-8700 Unit 3", "Repair" },
                    { new Guid("b1c2d3e4-0003-0003-0003-100000000003"), new Guid("43333333-3333-3333-3333-333333333335"), null, null, null, new DateTime(2025, 5, 13, 9, 0, 0, 0, DateTimeKind.Utc), 3, null, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), "Pending", "Sửa lỗi máy ngừng hoạt động do chập điện trên máy Juki DDL-8700 Unit 4 (MC004-JUKI-DDL8700-04).", "Repair Juki DDL-8700 Unit 4", "Repair" },
                    { new Guid("b1c2d3e4-0004-0004-0004-100000000004"), new Guid("43333333-3333-3333-3333-333333333336"), "Máy hoạt động tốt sau khi căn chỉnh trục kim.", new DateTime(2025, 3, 20, 15, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 20, 15, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 3, 21, 11, 15, 0, 0, DateTimeKind.Utc), 2, "Đã thay kim mới và căn chỉnh trục kim, kiểm tra vận hành ổn.", new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), "Completed", "Sửa lỗi kim gãy do lệch tâm trên máy Juki DDL-8700 Unit 7 (MC007-JUKI-DDL8700-07).", "Fix Needle Issue on Juki DDL-8700 Unit 7", "Maintenance" },
                    { new Guid("b1c2d3e4-0005-0005-0005-100000000005"), new Guid("43333333-3333-3333-3333-333333333337"), null, null, null, new DateTime(2025, 4, 27, 13, 45, 0, 0, DateTimeKind.Utc), 1, null, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), "InProgress", "Bảo trì tiếng ồn lớn do bánh răng mòn trên máy Juki DDL-8700 Unit 8 (MC008-JUKI-DDL8700-08).", "Reduce Noise on Juki DDL-8700 Unit 8", "Maintenance" },
                    { new Guid("b1c2d3e4-0006-0006-0006-100000000006"), new Guid("43333333-3333-3333-3333-333333333338"), null, null, null, new DateTime(2025, 5, 8, 8, 20, 0, 0, DateTimeKind.Utc), 3, null, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), "Pending", "Sửa lỗi đèn báo lỗi do mạch điều khiển trên máy Juki DDL-9000C Unit 1 (MC011-JUKI-DDL9000C-01).", "Fix Error Light on Juki DDL-9000C Unit 1", "Repair" },
                    { new Guid("b1c2d3e4-0007-0007-0007-100000000007"), new Guid("43333333-3333-3333-3333-333333333339"), "Máy may đều sau khi siết lại bulong.", new DateTime(2025, 4, 10, 18, 30, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 10, 18, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 4, 11, 16, 0, 0, 0, DateTimeKind.Utc), 2, "Đã siết chặt bulong bộ truyền, kiểm tra đường chỉ ổn định.", new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), "Completed", "Sửa lỗi chỉ không đều do bulong lỏng trên máy Juki DDL-9000C Unit 3 (MC013-JUKI-DDL9000C-03).", "Adjust Stitch on Juki DDL-9000C Unit 3", "Maintenance" },
                    { new Guid("b1c2d3e4-0008-0008-0008-100000000008"), new Guid("43333333-3333-3333-3333-333333333340"), null, null, null, new DateTime(2025, 5, 17, 12, 10, 0, 0, DateTimeKind.Utc), 2, null, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), "InProgress", "Sửa lỗi chỉ dưới không kéo lên trên máy Brother B957 Unit 1 (MC015-BROTHER-B957-01).", "Fix Thread Issue on Brother B957 Unit 1", "Repair" },
                    { new Guid("b1c2d3e4-0009-0009-0009-100000000009"), new Guid("43333333-3333-3333-3333-333333333341"), null, null, null, new DateTime(2025, 5, 21, 10, 0, 0, 0, DateTimeKind.Utc), 3, null, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), "Pending", "Sửa lỗi máy không chạy do mô tơ cháy trên máy Singer 4452 Unit 1 (MC018-SINGER-4452-01).", "Repair Singer 4452 Unit 1", "Repair" },
                    { new Guid("b1c2d3e4-0010-0010-0010-100000000010"), new Guid("43333333-3333-3333-3333-333333333342"), null, null, null, new DateTime(2025, 5, 22, 15, 30, 0, 0, DateTimeKind.Utc), 2, null, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), "InProgress", "Sửa lỗi vải bị nhăn do bộ cấp vải không đều trên máy Singer 4452 Unit 3 (MC020-SINGER-4452-03).", "Adjust Fabric Feed on Singer 4452 Unit 3", "Maintenance" }
                });

            migrationBuilder.InsertData(
                table: "Zones",
                columns: new[] { "Id", "AreaId", "ZoneCode", "ZoneName" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-0001-0001-0001-000000000001"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "A01", "Dây Chuyền May A" },
                    { new Guid("c1d2e3f4-0002-0002-0002-000000000002"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "A02", "Dây Chuyền May B" },
                    { new Guid("c1d2e3f4-0003-0003-0003-000000000003"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "A03", "Dây Chuyền May C" },
                    { new Guid("c1d2e3f4-0004-0004-0004-000000000004"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "A04", "Khu Cắt May" },
                    { new Guid("c1d2e3f4-0005-0005-0005-000000000005"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "A05", "Khu Chuẩn Bị Vải" },
                    { new Guid("c1d2e3f4-0006-0006-0006-000000000006"), new Guid("b1c2d3e4-0002-0002-0002-000000000002"), "B01", "Khu May Nặng A" },
                    { new Guid("c1d2e3f4-0007-0007-0007-000000000007"), new Guid("b1c2d3e4-0002-0002-0002-000000000002"), "B02", "Khu May Nặng B" },
                    { new Guid("c1d2e3f4-0008-0008-0008-000000000008"), new Guid("b1c2d3e4-0002-0002-0002-000000000002"), "B03", "Khu Cắt Gọt và Đóng Gói" },
                    { new Guid("c1d2e3f4-0009-0009-0009-000000000009"), new Guid("b1c2d3e4-0003-0003-0003-000000000003"), "KT1", "Khu Kiểm Tra 1" },
                    { new Guid("c1d2e3f4-0010-0010-0010-000000000010"), new Guid("b1c2d3e4-0003-0003-0003-000000000003"), "KT2", "Khu Kiểm Tra 2" },
                    { new Guid("c1d2e3f4-0011-0011-0011-000000000011"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "A06", "Dây Chuyền May D" },
                    { new Guid("c1d2e3f4-0012-0012-0012-000000000012"), new Guid("b1c2d3e4-0004-0004-0004-000000000004"), "CV1", "Khu Cắt Vải Tự Động" },
                    { new Guid("c1d2e3f4-0013-0013-0013-000000000013"), new Guid("b1c2d3e4-0004-0004-0004-000000000004"), "CV2", "Khu May Nặng C" },
                    { new Guid("c1d2e3f4-0014-0014-0014-000000000014"), new Guid("b1c2d3e4-0005-0005-0005-000000000005"), "TV1", "Khu May Nặng D" },
                    { new Guid("c1d2e3f4-0015-0015-0015-000000000015"), new Guid("b1c2d3e4-0006-0006-0006-000000000006"), "LK1", "Khu Lưu Trữ Máy May" }
                });

            migrationBuilder.InsertData(
                table: "DeviceErrorHistories",
                columns: new[] { "DeviceId", "ErrorId", "CreatedBy", "CreatedDate", "Id", "IsDeleted", "LastOccurredDate", "ModifiedBy", "ModifiedDate", "Notes", "OccurrenceCount" },
                values: new object[,]
                {
                    { new Guid("d1e2f3a4-0001-0001-0001-000000000001"), new Guid("e1d1a129-0021-0021-0021-000000000021"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6629), new Guid("004708e2-ec46-41a2-a2a8-9ea12d1b10bb"), false, new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), null, null, "Mỏ trói chỉ bị lỏng, đã điều chỉnh lực siết.", 3 },
                    { new Guid("d1e2f3a4-0003-0003-0003-000000000003"), new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6637), new Guid("6a9f51c1-8005-4e12-9dd5-3de91c9bafb2"), false, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), null, null, "Động cơ cháy do quá tải, cần thay mô tơ mới.", 1 },
                    { new Guid("d1e2f3a4-0004-0004-0004-000000000004"), new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6644), new Guid("24488424-edde-44d0-b82f-f436d32d25b2"), false, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Mô tơ bị chập điện, đang chờ phụ tùng thay thế.", 1 },
                    { new Guid("d1e2f3a4-0007-0007-0007-000000000007"), new Guid("e1d1abcf-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6648), new Guid("a06e92c6-a137-4218-bf83-b390d4eca04e"), false, new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), null, null, "Kim lệch tâm, đã căn chỉnh lại trục kim.", 2 },
                    { new Guid("d1e2f3a4-0008-0008-0008-000000000008"), new Guid("e1d1a130-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6650), new Guid("4f109e6e-763a-4dc2-82d1-87efb339a190"), false, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), null, null, "Bánh răng mòn gây tiếng ồn, đã lên kế hoạch thay mới.", 2 },
                    { new Guid("d1e2f3a4-0011-0011-0011-000000000011"), new Guid("e1d1a888-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6654), new Guid("6627fd9e-e94e-47f1-9c23-913da9cff8ac"), false, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), null, null, "Bo mạch điều khiển lỗi, đang kiểm tra để sửa chữa.", 1 },
                    { new Guid("d1e2f3a4-0013-0013-0013-000000000013"), new Guid("e1d1a134-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6658), new Guid("dd1863dd-13a8-4315-ba8d-73d3f2f730a3"), false, new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), null, null, "Bulong lỏng ở bộ truyền, đã siết chặt lại.", 3 },
                    { new Guid("d1e2f3a4-0015-0015-0015-000000000015"), new Guid("e1d1afff-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6660), new Guid("c77ed434-82d8-41f7-a8ce-5a856b4eb7dd"), false, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), null, null, "Cảm biến vải không nhận, đã thay cảm biến mới.", 2 },
                    { new Guid("d1e2f3a4-0018-0018-0018-000000000018"), new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6664), new Guid("5a85b72f-08ae-4c12-be22-7e8c22e6d68d"), false, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), null, null, "Mô tơ bị cháy, đang chờ thay thế phụ tùng.", 1 },
                    { new Guid("d1e2f3a4-0020-0020-0020-000000000020"), new Guid("e1d1a128-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(6666), new Guid("549bf79a-222d-4029-9b38-e0bda5ef661f"), false, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), null, null, "Bộ cấp vải hoạt động không đều, đã điều chỉnh lại.", 2 }
                });

            migrationBuilder.InsertData(
                table: "DeviceHistories",
                columns: new[] { "Id", "ActionType", "ComponentCode", "ComponentName", "Cost", "Description", "DeviceId", "DocumentUrl", "EventDate", "Provider", "Reason", "RelatedTaskId", "Status" },
                values: new object[,]
                {
                    { new Guid("33cc4a77-0001-0001-0001-000000000001"), "Warranty", "TNS-001", "Thread Tension Unit", 0m, "Bảo hành máy do lỗi đứt chỉ liên tục", new Guid("d1e2f3a4-0001-0001-0001-000000000001"), "https://example.com/docs/warranty_juki_ddl8700_01.pdf", new DateTime(2023, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Juki Vietnam", "Lỗi kỹ thuật từ nhà sản xuất trong cơ chế căng chỉ", null, "Completed" },
                    { new Guid("33cc4a77-0002-0002-0002-000000000002"), "Repair", "MTR-001", "Motor", 1500000m, "Sửa chữa máy do hỏng động cơ", new Guid("d1e2f3a4-0003-0003-0003-000000000003"), "https://example.com/docs/repair_juki_ddl8700_03.pdf", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Juki Vietnam", "Động cơ bị cháy do quá tải trong sản xuất", null, "Pending" },
                    { new Guid("33cc4a77-0003-0003-0003-000000000003"), "Replacement", "MTR-002", "Motor", 2000000m, "Thay thế động cơ cho máy Juki DDL-8700", new Guid("d1e2f3a4-0004-0004-0004-000000000004"), "https://example.com/docs/replacement_juki_ddl8700_04.pdf", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Juki Vietnam", "Động cơ cũ bị hỏng do sử dụng lâu dài", null, "Completed" },
                    { new Guid("33cc4a77-0004-0004-0004-000000000004"), "Warranty", "NDL-001", "Needle Bar", 0m, "Bảo hành máy do kẹt kim", new Guid("d1e2f3a4-0007-0007-0007-000000000007"), "https://example.com/docs/warranty_juki_ddl8700_07.pdf", new DateTime(2022, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Juki Vietnam", "Lỗi cơ chế kim từ nhà sản xuất", null, "Completed" },
                    { new Guid("33cc4a77-0005-0005-0005-000000000005"), "Repair", "BRG-001", "Bearing", 800000m, "Sửa chữa tiếng ồn bất thường từ máy", new Guid("d1e2f3a4-0008-0008-0008-000000000008"), "https://example.com/docs/repair_juki_ddl8700_08.pdf", new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Juki Vietnam", "Ổ bi bị mòn do thiếu bôi trơn", null, "Failed" },
                    { new Guid("33cc4a77-0006-0006-0006-000000000006"), "Warranty", "CTR-001", "Control Unit", 0m, "Bảo hành máy do lỗi hiệu chỉnh cắt chỉ tự động", new Guid("d1e2f3a4-0011-0011-0011-000000000011"), "https://example.com/docs/warranty_juki_ddl9000c_01.pdf", new DateTime(2023, 12, 5, 0, 0, 0, 0, DateTimeKind.Utc), "Juki Vietnam", "Hệ thống điều khiển số bị lỗi từ nhà sản xuất", null, "Completed" },
                    { new Guid("33cc4a77-0007-0007-0007-000000000007"), "Repair", "CTR-002", "Thread Trimmer", 1200000m, "Sửa chữa máy do lỗi hệ thống cắt chỉ", new Guid("d1e2f3a4-0013-0013-0013-000000000013"), "https://example.com/docs/repair_juki_ddl9000c_03.pdf", new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Juki Vietnam", "Bộ phận cắt chỉ bị kẹt do hao mòn", null, "Pending" },
                    { new Guid("33cc4a77-0008-0008-0008-000000000008"), "Replacement", "DFD-001", "Differential Feed Dog", 1000000m, "Thay thế bộ phận cấp liệu khác biệt", new Guid("d1e2f3a4-0015-0015-0015-000000000015"), "https://example.com/docs/replacement_brother_b957_01.pdf", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Brother Vietnam", "Bộ phận cấp liệu bị hỏng do sử dụng sai", null, "Completed" },
                    { new Guid("33cc4a77-0009-0009-0009-000000000009"), "Warranty", "PWR-001", "Power Board", 0m, "Bảo hành máy do sự cố nguồn điện", new Guid("d1e2f3a4-0018-0018-0018-000000000018"), "https://example.com/docs/warranty_singer_4452_01.pdf", new DateTime(2022, 10, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Singer Vietnam", "Bo mạch nguồn bị lỗi từ nhà sản xuất", null, "Completed" },
                    { new Guid("33cc4a77-0010-0010-0010-000000000010"), "Repair", "PFT-001", "Presser Foot Mechanism", 900000m, "Sửa chữa máy do hỏng cơ chế chân vịt", new Guid("d1e2f3a4-0020-0020-0020-000000000020"), "https://example.com/docs/repair_singer_4452_03.pdf", new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Singer Vietnam", "Cơ chế chân vịt bị mòn do sử dụng lâu dài", null, "Pending" }
                });

            migrationBuilder.InsertData(
                table: "DeviceIssueHistories",
                columns: new[] { "DeviceId", "IssueId", "CreatedBy", "CreatedDate", "Id", "IsDeleted", "LastOccurredDate", "ModifiedBy", "ModifiedDate", "Notes", "OccurrenceCount" },
                values: new object[,]
                {
                    { new Guid("d1e2f3a4-0001-0001-0001-000000000001"), new Guid("88888888-8888-8888-8888-888888888888"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9038), new Guid("3c94c907-7c9d-4441-b2bb-d741e519f2b7"), false, new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), null, null, "Chỉ bị đứt do kẹt ở ống chỉ, đã thay ống chỉ mới.", 3 },
                    { new Guid("d1e2f3a4-0003-0003-0003-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9044), new Guid("660d4615-aa3e-4584-95aa-2f2d5648de3e"), false, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy không chạy do lỗi động cơ, đang chờ sửa chữa.", 2 },
                    { new Guid("d1e2f3a4-0004-0004-0004-000000000004"), new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9054), new Guid("96fe3871-2079-4756-ba13-feae22a2d928"), false, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy ngừng hoạt động, kiểm tra phát hiện lỗi dây điện.", 1 },
                    { new Guid("d1e2f3a4-0007-0007-0007-000000000007"), new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9057), new Guid("336bb25f-6629-42d6-9d2f-34e2f6f3040d"), false, new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), null, null, "Kim gãy do sử dụng sai loại kim, đã thay kim phù hợp.", 4 },
                    { new Guid("d1e2f3a4-0008-0008-0008-000000000008"), new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9059), new Guid("d8835be8-4746-4cd7-ac11-34b571f00f70"), false, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), null, null, "Tiếng ồn lớn do bánh răng mòn, cần thay thế.", 2 },
                    { new Guid("d1e2f3a4-0011-0011-0011-000000000011"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9062), new Guid("f008f630-78a3-4ab5-81c1-bcd47bd26b9d"), false, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), null, null, "Đèn báo lỗi sáng, kiểm tra mạch điện tử đang được tiến hành.", 1 },
                    { new Guid("d1e2f3a4-0013-0013-0013-000000000013"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9065), new Guid("02bf0387-1f1c-493f-95fb-872e444afc25"), false, new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), null, null, "Chỉ không đều do bulong lỏng, đã siết lại.", 2 },
                    { new Guid("d1e2f3a4-0015-0015-0015-000000000015"), new Guid("34343434-3434-3434-3434-343434343434"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9067), new Guid("62e0030d-1fcb-4115-863e-d8818d99168d"), false, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), null, null, "Chỉ dưới không kéo lên, kiểm tra cảm biến và thay mới.", 3 },
                    { new Guid("d1e2f3a4-0018-0018-0018-000000000018"), new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9070), new Guid("f39c48b0-e2d8-402c-b799-26953dda08e2"), false, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy không chạy, kiểm tra phát hiện lỗi mô tơ.", 1 },
                    { new Guid("d1e2f3a4-0020-0020-0020-000000000020"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 5, 25, 13, 3, 10, 117, DateTimeKind.Utc).AddTicks(9073), new Guid("c55ca1d7-0b25-4d48-8ae8-c6619769824d"), false, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), null, null, "Vải bị nhăn do điều chỉnh áp suất không đúng, đã điều chỉnh lại.", 2 }
                });

            migrationBuilder.InsertData(
                table: "DeviceWarranties",
                columns: new[] { "Id", "Cost", "DeviceId", "DocumentUrl", "Notes", "Provider", "SparePartCode", "SparePartName", "Status", "WarrantyCode", "WarrantyEndDate", "WarrantyReason", "WarrantyStartDate", "WarrantyType" },
                values: new object[,]
                {
                    { new Guid("d1e2f3a4-0001-4001-8001-000000000001"), 0m, new Guid("d1e2f3a4-0001-0001-0001-000000000001"), "https://example.com/docs/warranty_juki_ddl8700_01.pdf", "Bảo hành định kỳ cho máy mới, bao gồm kiểm tra cơ chế căng chỉ", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-001", new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0002-4002-8002-000000000002"), 500000m, new Guid("d1e2f3a4-0003-0003-0003-000000000003"), "https://example.com/docs/warranty_juki_ddl8700_03.pdf", "Gia hạn bảo hành sau sửa chữa động cơ bị cháy", "Juki Vietnam", "SP007", "Mô Tơ Máy May", "Pending", "WAR-JUKI-002", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "AfterRepair", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Extended" },
                    { new Guid("d1e2f3a4-0003-4003-8003-000000000003"), 0m, new Guid("d1e2f3a4-0004-0004-0004-000000000004"), "https://example.com/docs/warranty_juki_ddl8700_04.pdf", "Bảo hành sau khi thay thế động cơ do hỏng hóc", "Juki Vietnam", "SP007", "Mô Tơ Máy May", "Completed", "WAR-JUKI-003", new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "AfterReplacement", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0004-4004-8004-000000000004"), 0m, new Guid("d1e2f3a4-0007-0007-0007-000000000007"), "https://example.com/docs/warranty_juki_ddl8700_07.pdf", "Bảo hành máy mới, kiểm tra và sửa lỗi kẹt kim", "Juki Vietnam", "SP009", "Trụ Gắn Kim", "Completed", "WAR-JUKI-004", new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0005-4005-8005-000000000005"), 0m, new Guid("d1e2f3a4-0011-0011-0011-000000000011"), "https://example.com/docs/warranty_juki_ddl9000c_01.pdf", "Bảo hành máy mới, hiệu chỉnh hệ thống cắt chỉ tự động", "Juki Vietnam", "SP029", "Bộ Điều Khiển Điện Tử", "Completed", "WAR-JUKI-005", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0006-4006-8006-000000000006"), 500000m, new Guid("d1e2f3a4-0013-0013-0013-000000000013"), "https://example.com/docs/warranty_juki_ddl9000c_03.pdf", "Gia hạn bảo hành sau sửa chữa bộ phận cắt chỉ", "Juki Vietnam", null, null, "Pending", "WAR-JUKI-006", new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "AfterRepair", new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Extended" },
                    { new Guid("d1e2f3a4-0007-4007-8007-000000000007"), 1000000m, new Guid("d1e2f3a4-0015-0015-0015-000000000015"), "https://example.com/docs/warranty_brother_b957_01.pdf", "Bảo hành bên thứ ba sau thay thế bộ phận cấp liệu khác biệt", "Brother Vietnam", null, null, "Completed", "WAR-BROTHER-001", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "AfterReplacement", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "ThirdParty" },
                    { new Guid("d1e2f3a4-0008-4008-8008-000000000008"), 0m, new Guid("d1e2f3a4-0018-0018-0018-000000000018"), "https://example.com/docs/warranty_singer_4452_01.pdf", "Bảo hành máy mới, sửa chữa bo mạch nguồn bị lỗi", "Singer Vietnam", "SP029", "Bộ Điều Khiển Điện Tử", "Completed", "WAR-SINGER-001", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0009-4009-8009-000000000009"), 500000m, new Guid("d1e2f3a4-0001-0001-0001-000000000001"), "https://example.com/docs/warranty_juki_ddl8700_01_ext.pdf", "Gia hạn bảo hành sau khi hết bảo hành nhà sản xuất", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-009", new DateTime(2023, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "AfterWarranty", new DateTime(2022, 2, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "Extended" },
                    { new Guid("d1e2f3a4-0010-4010-8010-000000000010"), 800000m, new Guid("d1e2f3a4-0001-0001-0001-000000000001"), "https://example.com/docs/warranty_juki_ddl8700_01_repair.pdf", "Bảo hành sau sửa chữa bộ phận cấp liệu", "Juki Vietnam", "SP008", "Bộ Cấp Liệu", "InProgress", "WAR-JUKI-010", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "AfterRepair", new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ThirdParty" },
                    { new Guid("d1e2f3a4-0011-4011-8011-000000000011"), 0m, new Guid("d1e2f3a4-0002-0002-0002-000000000002"), "https://example.com/docs/warranty_juki_ddl8700_02.pdf", "Bảo hành máy mới, kiểm tra hệ thống kim và chỉ", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-011", new DateTime(2022, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0012-4012-8012-000000000012"), 0m, new Guid("d1e2f3a4-0003-0003-0003-000000000003"), "https://example.com/docs/warranty_juki_ddl8700_03_new.pdf", "Bảo hành máy mới, kiểm tra động cơ và bộ căng chỉ", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-012", new DateTime(2022, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0013-4013-8013-000000000013"), 0m, new Guid("d1e2f3a4-0005-0005-0005-000000000005"), "https://example.com/docs/warranty_juki_ddl8700_05.pdf", "Bảo hành máy mới, hiệu chỉnh hệ thống cấp liệu", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-013", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0014-4014-8014-000000000014"), 0m, new Guid("d1e2f3a4-0006-0006-0006-000000000006"), "https://example.com/docs/warranty_juki_ddl8700_06.pdf", "Bảo hành máy mới, đã hết hạn trước khi máy ngừng sử dụng", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-014", new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0015-4015-8015-000000000015"), 0m, new Guid("d1e2f3a4-0008-0008-0008-000000000008"), "https://example.com/docs/warranty_juki_ddl8700_08.pdf", "Bảo hành máy mới, kiểm tra hệ thống căng chỉ", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-015", new DateTime(2022, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0016-4016-8016-000000000016"), 0m, new Guid("d1e2f3a4-0009-0009-0009-000000000009"), "https://example.com/docs/warranty_juki_ddl8700_09.pdf", "Bảo hành máy mới, hiệu chỉnh động cơ", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-016", new DateTime(2022, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0017-4017-8017-000000000017"), 0m, new Guid("d1e2f3a4-0010-0010-0010-000000000010"), "https://example.com/docs/warranty_juki_ddl8700_10.pdf", "Bảo hành máy mới, kiểm tra hệ thống cấp liệu", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-017", new DateTime(2022, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2020, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0018-4018-8018-000000000018"), 0m, new Guid("d1e2f3a4-0012-0012-0012-000000000012"), "https://example.com/docs/warranty_juki_ddl9000c_02.pdf", "Bảo hành máy mới, kiểm tra hệ thống cắt chỉ", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-018", new DateTime(2024, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" },
                    { new Guid("d1e2f3a4-0019-4019-8019-000000000019"), 0m, new Guid("d1e2f3a4-0013-0013-0013-000000000013"), "https://example.com/docs/warranty_juki_ddl9000c_03_new.pdf", "Bảo hành máy mới, hiệu chỉnh bộ cắt chỉ tự động", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-019", new DateTime(2024, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "NewDevice", new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Manufacturer" }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "DeviceId", "Index", "ZoneId" },
                values: new object[,]
                {
                    { new Guid("f1e2d3c4-0001-0001-0001-000000000001"), new Guid("d1e2f3a4-0001-0001-0001-000000000001"), 1, new Guid("c1d2e3f4-0001-0001-0001-000000000001") },
                    { new Guid("f1e2d3c4-0002-0002-0002-000000000002"), new Guid("d1e2f3a4-0002-0002-0002-000000000002"), 2, new Guid("c1d2e3f4-0001-0001-0001-000000000001") },
                    { new Guid("f1e2d3c4-0003-0003-0003-000000000003"), new Guid("d1e2f3a4-0004-0004-0004-000000000004"), 3, new Guid("c1d2e3f4-0001-0001-0001-000000000001") },
                    { new Guid("f1e2d3c4-0004-0004-0004-000000000004"), new Guid("d1e2f3a4-0005-0005-0005-000000000005"), 4, new Guid("c1d2e3f4-0001-0001-0001-000000000001") },
                    { new Guid("f1e2d3c4-0005-0005-0005-000000000005"), new Guid("d1e2f3a4-0023-0023-0023-000000000023"), 5, new Guid("c1d2e3f4-0001-0001-0001-000000000001") },
                    { new Guid("f1e2d3c4-0006-0006-0006-000000000006"), new Guid("d1e2f3a4-0007-0007-0007-000000000007"), 1, new Guid("c1d2e3f4-0002-0002-0002-000000000002") },
                    { new Guid("f1e2d3c4-0007-0007-0007-000000000007"), new Guid("d1e2f3a4-0008-0008-0008-000000000008"), 2, new Guid("c1d2e3f4-0002-0002-0002-000000000002") },
                    { new Guid("f1e2d3c4-0008-0008-0008-000000000008"), new Guid("d1e2f3a4-0011-0011-0011-000000000011"), 3, new Guid("c1d2e3f4-0002-0002-0002-000000000002") },
                    { new Guid("f1e2d3c4-0009-0009-0009-000000000009"), new Guid("d1e2f3a4-0021-0021-0021-000000000021"), 4, new Guid("c1d2e3f4-0002-0002-0002-000000000002") },
                    { new Guid("f1e2d3c4-0010-0010-0010-000000000010"), new Guid("d1e2f3a4-0009-0009-0009-000000000009"), 1, new Guid("c1d2e3f4-0003-0003-0003-000000000003") },
                    { new Guid("f1e2d3c4-0011-0011-0011-000000000011"), new Guid("d1e2f3a4-0010-0010-0010-000000000010"), 2, new Guid("c1d2e3f4-0003-0003-0003-000000000003") },
                    { new Guid("f1e2d3c4-0012-0012-0012-000000000012"), new Guid("d1e2f3a4-0012-0012-0012-000000000012"), 3, new Guid("c1d2e3f4-0003-0003-0003-000000000003") },
                    { new Guid("f1e2d3c4-0013-0013-0013-000000000013"), new Guid("d1e2f3a4-0014-0014-0014-000000000014"), 4, new Guid("c1d2e3f4-0003-0003-0003-000000000003") },
                    { new Guid("f1e2d3c4-0014-0014-0014-000000000014"), new Guid("d1e2f3a4-0003-0003-0003-000000000003"), 1, new Guid("c1d2e3f4-0011-0011-0011-000000000011") },
                    { new Guid("f1e2d3c4-0015-0015-0015-000000000015"), new Guid("d1e2f3a4-0006-0006-0006-000000000006"), 2, new Guid("c1d2e3f4-0011-0011-0011-000000000011") },
                    { new Guid("f1e2d3c4-0016-0016-0016-000000000016"), new Guid("d1e2f3a4-0027-0027-0027-000000000027"), 1, new Guid("c1d2e3f4-0005-0005-0005-000000000005") },
                    { new Guid("f1e2d3c4-0017-0017-0017-000000000017"), new Guid("d1e2f3a4-0029-0029-0029-000000000029"), 2, new Guid("c1d2e3f4-0005-0005-0005-000000000005") },
                    { new Guid("f1e2d3c4-0018-0018-0018-000000000018"), new Guid("d1e2f3a4-0028-0028-0028-000000000028"), 3, new Guid("c1d2e3f4-0005-0005-0005-000000000005") },
                    { new Guid("f1e2d3c4-0019-0019-0019-000000000019"), new Guid("d1e2f3a4-0015-0015-0015-000000000015"), 1, new Guid("c1d2e3f4-0006-0006-0006-000000000006") },
                    { new Guid("f1e2d3c4-0020-0020-0020-000000000020"), new Guid("d1e2f3a4-0016-0016-0016-000000000016"), 2, new Guid("c1d2e3f4-0006-0006-0006-000000000006") },
                    { new Guid("f1e2d3c4-0021-0011-0021-000000000021"), new Guid("d1e2f3a4-0017-0017-0017-000000000017"), 3, new Guid("c1d2e3f4-0006-0006-0006-000000000006") },
                    { new Guid("f1e2d3c4-0022-0022-0022-000000000022"), new Guid("d1e2f3a4-0024-0024-0024-000000000024"), 4, new Guid("c1d2e3f4-0006-0006-0006-000000000006") },
                    { new Guid("f1e2d3c4-0023-0023-0023-000000000023"), new Guid("d1e2f3a4-0018-0018-0018-000000000018"), 1, new Guid("c1d2e3f4-0007-0007-0007-000000000007") },
                    { new Guid("f1e2d3c4-0024-0024-0024-000000000024"), new Guid("d1e2f3a4-0019-0019-0019-000000000019"), 2, new Guid("c1d2e3f4-0007-0007-0007-000000000007") },
                    { new Guid("f1e2d3c4-0025-0025-0025-000000000025"), new Guid("d1e2f3a4-0025-0025-0025-000000000025"), 3, new Guid("c1d2e3f4-0007-0007-0007-000000000007") },
                    { new Guid("f1e2d3c4-0026-0026-0026-000000000026"), new Guid("d1e2f3a4-0022-0022-0022-000000000022"), 1, new Guid("c1d2e3f4-0013-0013-0013-000000000013") },
                    { new Guid("f1e2d3c4-0027-0027-0027-000000000027"), new Guid("d1e2f3a4-0030-0030-0030-000000000030"), 2, new Guid("c1d2e3f4-0013-0013-0013-000000000013") },
                    { new Guid("f1e2d3c4-0028-0028-0028-000000000028"), new Guid("d1e2f3a4-0013-0013-0013-000000000013"), 1, new Guid("c1d2e3f4-0014-0014-0014-000000000014") },
                    { new Guid("f1e2d3c4-0029-0029-0029-000000000029"), new Guid("d1e2f3a4-0020-0020-0020-000000000020"), 2, new Guid("c1d2e3f4-0014-0014-0014-000000000014") },
                    { new Guid("f1e2d3c4-0030-0030-0030-000000000030"), new Guid("d1e2f3a4-0026-0026-0026-000000000026"), 1, new Guid("c1d2e3f4-0015-0015-0015-000000000015") }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Description", "DeviceId", "DueDate", "Priority", "ReportId", "RequestTitle", "RequestedById", "SerderId", "Status" },
                values: new object[,]
                {
                    { new Guid("a1e2f3a4-0001-0001-1001-000000000001"), "Máy ngừng hoạt động do đứt chỉ tại Dây chuyền May A, Vị trí 1, làm gián đoạn sản xuất vải mỏng.", new Guid("d1e2f3a4-0001-0001-0001-000000000001"), new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), "High", null, "SXA-A01-1-4A7B2", new Guid("32222222-2222-2222-2222-222222222222"), null, "Pending" },
                    { new Guid("a1f2e3d4-0002-0002-1002-000000000002"), "Động cơ ngừng hoạt động tại Dây chuyền May A, Vị trí 3. Quan trọng cho sản xuất vải cotton.", new Guid("d1e2f3a4-0004-0004-0004-000000000004"), new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "High", new Guid("e1f2a3b4-0001-0001-0001-300000000001"), "SXA-A01-3-8C9D4", new Guid("32222222-2222-2222-2222-222222222222"), null, "Approved" },
                    { new Guid("a1f2e3d4-0003-0003-1003-000000000003"), "Kim bị kẹt tại Dây chuyền May B, Vị trí 1. Ảnh hưởng đến sản xuất vải dày.", new Guid("d1e2f3a4-0007-0007-0007-000000000007"), new DateTime(2025, 5, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", new Guid("e1f2a3b4-0002-0002-0002-300000000002"), "SXA-A02-1-2E5F6", new Guid("32222222-2222-2222-2222-222222222222"), null, "InProgress" },
                    { new Guid("a1f2e3d4-0004-0004-1004-000000000004"), "Máy cắt chỉ tự động bị lệch tại Dây chuyền May B, Vị trí 3. Gây ra mũi may không đều.", new Guid("d1e2f3a4-0011-0011-0011-000000000011"), new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", null, "SXA-A02-3-7G8H9", new Guid("32222222-2222-2222-2222-222222222222"), null, "Pending" },
                    { new Guid("a1f2e3d4-0005-0005-1005-000000000005"), "Bộ phận cấp liệu khác biệt bị trục trặc tại Khu vực Vắt Sổ, Vị trí 1. Ảnh hưởng đến hoàn thiện vải mỏng.", new Guid("d1e2f3a4-0015-0015-0015-000000000015"), new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "High", new Guid("e1f2a3b4-0003-0003-0003-300000000003"), "SXB-B01-1-1J2K3", new Guid("32222222-2222-2222-2222-222222222222"), null, "Approved" },
                    { new Guid("a1f2e3d4-0006-0006-1006-000000000006"), "Nguồn điện bị gián đoạn tại Khu vực May Nặng, Vị trí 1. Ảnh hưởng đến sản xuất vải denim.", new Guid("d1e2f3a4-0018-0018-0018-000000000018"), new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), "High", new Guid("e1f2a3b4-0004-0004-0004-300000000004"), "SXB-B02-1-4L5M6", new Guid("23333333-3333-3333-3333-333333333343"), null, "InProgress" },
                    { new Guid("a1f2e3d4-0007-0007-1007-000000000007"), "Cần bảo trì định kỳ cho Juki DDL-8700 tại Dây chuyền May C, Vị trí 1 để ngăn ngừa sự cố.", new Guid("d1e2f3a4-0009-0009-0009-000000000009"), new DateTime(2025, 5, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Low", null, "SXA-A03-1-8N9P0", new Guid("23333333-3333-3333-3333-333333333343"), null, "Pending" },
                    { new Guid("a1f2e3d4-0008-0008-1008-000000000008"), "Căng chỉ không đúng tại Dây chuyền May C, Vị trí 3. Ảnh hưởng đến chất lượng mũi may.", new Guid("d1e2f3a4-0012-0012-0012-000000000012"), new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", new Guid("e1f2a3b4-0005-0005-0005-300000000005"), "SXA-A03-3-2Q3R4", new Guid("23333333-3333-3333-3333-333333333343"), null, "Completed" },
                    { new Guid("a1f2e3d4-0009-0009-1009-000000000009"), "Máy đang sửa chữa cần thay động cơ. Hiện không được gán vị trí.", new Guid("d1e2f3a4-0003-0003-0003-000000000003"), new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", new Guid("e1f2a3b4-0006-0006-0006-300000000006"), "SXA-A04-1-5S6T7", new Guid("23333333-3333-3333-3333-333333333343"), null, "InProgress" },
                    { new Guid("a1f2e3d4-0010-0010-1010-000000000010"), "Dây đai truyền động bị trượt tại Khu vực May Nặng, Vị trí 2. Ảnh hưởng đến sản xuất da.", new Guid("d1e2f3a4-0019-0019-0019-000000000019"), new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), "High", null, "SXB-B02-2-8U9V0", new Guid("23333333-3333-3333-3333-333333333343"), null, "Pending" },
                    { new Guid("a1f2e3d4-0011-0011-1011-000000000011"), "Máy vắt sổ tại Khu vực Vắt Sổ, Vị trí 2 cần bôi trơn để ngăn mòn.", new Guid("d1e2f3a4-0016-0016-0016-000000000016"), new DateTime(2025, 5, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Low", new Guid("e1f2a3b4-0007-0007-0007-300000000007"), "SXB-B01-2-1W2X3", new Guid("23333333-3333-3333-3333-333333333344"), null, "Approved" },
                    { new Guid("a1f2e3d4-0012-0012-1012-000000000012"), "Hệ thống điều khiển số cần cập nhật phần mềm tại Dây chuyền May C, Vị trí 4 để tối ưu hiệu suất.", new Guid("d1e2f3a4-0014-0014-0014-000000000014"), new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Low", null, "SXA-A03-4-4Y5Z6", new Guid("23333333-3333-3333-3333-333333333344"), null, "Pending" },
                    { new Guid("a1f2e3d4-0013-0013-1013-000000000013"), "Máy đang sửa chữa cần thay cơ chế chân vịt. Hiện không được gán vị trí.", new Guid("d1e2f3a4-0020-0020-0020-000000000020"), new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", new Guid("e1f2a3b4-0008-0008-0008-300000000008"), "KTV-TV1-2-7A8B9", new Guid("23333333-3333-3333-3333-333333333344"), null, "Approved" },
                    { new Guid("a1f2e3d4-0014-0014-1014-000000000014"), "Tiếng ồn lạ từ máy tại Dây chuyền May B, Vị trí 2. Có thể do vấn đề ổ bi.", new Guid("d1e2f3a4-0008-0008-0008-000000000008"), null, "Medium", null, "SXA-A02-2-0C1D2", new Guid("23333333-3333-3333-3333-333333333344"), null, "Denied" },
                    { new Guid("a1f2e3d4-0015-0015-1015-000000000015"), "Máy vắt sổ tại Khu vực Vắt Sổ, Vị trí 3 cần vệ sinh để loại bỏ bụi vải tích tụ.", new Guid("d1e2f3a4-0017-0017-0017-000000000017"), new DateTime(2025, 5, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Low", new Guid("e1f2a3b4-0009-0009-0009-300000000009"), "SXB-B01-3-3E4F5", new Guid("23333333-3333-3333-3333-333333333344"), null, "Completed" }
                });

            migrationBuilder.InsertData(
                table: "Reports",
                columns: new[] { "Id", "Location", "Priority", "RequestId", "Status" },
                values: new object[,]
                {
                    { new Guid("e1f2a3b4-0001-0001-0001-300000000001"), "Khu Vực: Khu Sản Xuất Chính A, Khu: Dây Chuyền May A, Vị trí: 3", 3, new Guid("a1f2e3d4-0002-0002-1002-000000000002"), "InProgress" },
                    { new Guid("e1f2a3b4-0002-0002-0002-300000000002"), "Khu Vực: Khu Sản Xuất Chính A, Khu: Dây Chuyền May B, Vị trí: 1", 2, new Guid("a1f2e3d4-0003-0003-1003-000000000003"), "Completed" },
                    { new Guid("e1f2a3b4-0003-0003-0003-300000000003"), "Khu Vực: Khu Sản Xuất Chính B, Khu: Khu May Nặng A, Vị trí: 1", 3, new Guid("a1f2e3d4-0005-0005-1005-000000000005"), "InProgress" },
                    { new Guid("e1f2a3b4-0004-0004-0004-300000000004"), "Khu Vực: Khu Sản Xuất Chính B, Khu: Khu May Nặng B, Vị trí: 1", 3, new Guid("a1f2e3d4-0006-0006-1006-000000000006"), "InProgress" },
                    { new Guid("e1f2a3b4-0005-0005-0005-300000000005"), "Khu Vực: Khu Sản Xuất Chính A, Khu: Dây Chuyền May C, Vị trí: 3", 2, new Guid("a1f2e3d4-0008-0008-1008-000000000008"), "Completed" },
                    { new Guid("e1f2a3b4-0006-0006-0006-300000000006"), "Khu Vực: Khu Sản Xuất Chính A, Khu: Khu Cắt May, Vị trí: 1", 2, new Guid("a1f2e3d4-0009-0009-1009-000000000009"), "InProgress" },
                    { new Guid("e1f2a3b4-0007-0007-0007-300000000007"), "Khu Vực: Khu Sản Xuất Chính B, Khu: Khu May Nặng A, Vị trí: 2", 1, new Guid("a1f2e3d4-0011-0011-1011-000000000011"), "InProgress" },
                    { new Guid("e1f2a3b4-0008-0008-0008-300000000008"), "Khu Vực: Khu Thêu, Khu: Khu May Nặng D, Vị trí: 2", 2, new Guid("a1f2e3d4-0013-0013-1013-000000000013"), "InProgress" },
                    { new Guid("e1f2a3b4-0009-0009-0009-300000000009"), "Khu Vực: Khu Sản Xuất Chính B, Khu: Khu May Nặng A, Vị trí: 3", 1, new Guid("a1f2e3d4-0015-0015-1015-000000000015"), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "RequestIssues",
                columns: new[] { "Id", "IssueId", "RequestId", "Status" },
                values: new object[,]
                {
                    { new Guid("f1a2b3c4-0001-0001-0001-800000000001"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a1f2e3d4-0002-0002-1002-000000000002"), "InProgress" },
                    { new Guid("f1a2b3c4-0002-0002-0002-800000000002"), new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a1f2e3d4-0003-0003-1003-000000000003"), "Completed" },
                    { new Guid("f1a2b3c4-0003-0003-0003-800000000003"), new Guid("34343434-3434-3434-3434-343434343434"), new Guid("a1f2e3d4-0005-0005-1005-000000000005"), "InProgress" },
                    { new Guid("f1a2b3c4-0004-0004-0004-800000000004"), new Guid("45454545-4545-4545-4545-454545454545"), new Guid("a1f2e3d4-0006-0006-1006-000000000006"), "InProgress" },
                    { new Guid("f1a2b3c4-0005-0005-0005-800000000005"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("a1f2e3d4-0008-0008-1008-000000000008"), "Completed" },
                    { new Guid("f1a2b3c4-0006-0006-0006-800000000006"), new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a1f2e3d4-0009-0009-1009-000000000009"), "InProgress" },
                    { new Guid("f1a2b3c4-0007-0007-0007-800000000007"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("a1f2e3d4-0011-0011-1011-000000000011"), "InProgress" },
                    { new Guid("f1a2b3c4-0008-0008-0008-800000000008"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("a1f2e3d4-0013-0013-1013-000000000013"), "InProgress" },
                    { new Guid("f1a2b3c4-0009-0009-0009-800000000009"), new Guid("55555555-5555-5555-5555-555555555555"), new Guid("a1f2e3d4-0015-0015-1015-000000000015"), "Completed" }
                });

            migrationBuilder.InsertData(
                table: "ErrorDetails",
                columns: new[] { "ErrorId", "ReportId", "TaskId" },
                values: new object[,]
                {
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("e1f2a3b4-0001-0001-0001-300000000001"), new Guid("b1c2d3e4-0002-0002-0002-100000000002") },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), new Guid("e1f2a3b4-0002-0002-0002-300000000002"), new Guid("b1c2d3e4-0004-0004-0004-100000000004") },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), new Guid("e1f2a3b4-0003-0003-0003-300000000003"), new Guid("b1c2d3e4-0008-0008-0008-100000000008") },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), new Guid("e1f2a3b4-0004-0004-0004-300000000004"), new Guid("b1c2d3e4-0009-0009-0009-100000000009") },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), new Guid("e1f2a3b4-0005-0005-0005-300000000005"), null },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("e1f2a3b4-0006-0006-0006-300000000006"), new Guid("b1c2d3e4-0002-0002-0002-100000000002") },
                    { new Guid("e1d1addd-0011-0011-0011-000000000011"), new Guid("e1f2a3b4-0007-0007-0007-300000000007"), null },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), new Guid("e1f2a3b4-0008-0008-0008-300000000008"), new Guid("b1c2d3e4-0010-0010-0010-100000000010") },
                    { new Guid("e1d1a136-0028-0028-0028-000000000028"), new Guid("e1f2a3b4-0009-0009-0009-300000000009"), null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_AreaCode",
                table: "Areas",
                column: "AreaCode",
                unique: true,
                filter: "[AreaCode] IS NOT NULL");

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
                name: "IX_Areas_ZoneCode",
                table: "Zones",
                column: "ZoneCode");

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
