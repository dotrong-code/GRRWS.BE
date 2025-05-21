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
                    AreaName = table.Column<string>(type: "nvarchar(450)", nullable: false)
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
                    { new Guid("10000000-0000-0000-0000-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3410), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3410) },
                    { new Guid("10000000-0000-0000-0000-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3419), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3419) },
                    { new Guid("10000000-0000-0000-0000-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3423), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3423) },
                    { new Guid("10000000-0000-0000-0000-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3426), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3426) },
                    { new Guid("10000000-0000-0000-0000-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3429), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3429) },
                    { new Guid("10000000-0000-0000-0000-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3436), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3437) },
                    { new Guid("10000000-0000-0000-0000-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3442), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3442) },
                    { new Guid("10000000-0000-0000-0000-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3445), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3445) },
                    { new Guid("10000000-0000-0000-0000-000000000009"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3448), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3448) },
                    { new Guid("10000000-0000-0000-0000-000000000010"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3451), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3451) },
                    { new Guid("10000000-0000-0000-0000-000000000011"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3454), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3454) },
                    { new Guid("10000000-0000-0000-0000-000000000012"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3457), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3457) },
                    { new Guid("10000000-0000-0000-0000-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3459), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3460) },
                    { new Guid("10000000-0000-0000-0000-000000000014"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3464), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3464) },
                    { new Guid("10000000-0000-0000-0000-000000000015"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3468), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3468) },
                    { new Guid("10000000-0000-0000-0000-000000000016"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3471), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3471) },
                    { new Guid("10000000-0000-0000-0000-000000000017"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3474), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3474) },
                    { new Guid("10000000-0000-0000-0000-000000000018"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3480), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3480) },
                    { new Guid("10000000-0000-0000-0000-000000000019"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3483), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3483) },
                    { new Guid("10000000-0000-0000-0000-000000000020"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3485), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3486) },
                    { new Guid("10000000-0000-0000-0000-000000000021"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3489), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3489) },
                    { new Guid("10000000-0000-0000-0000-000000000022"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3492), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3492) },
                    { new Guid("10000000-0000-0000-0000-000000000023"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3497), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3497) },
                    { new Guid("10000000-0000-0000-0000-000000000024"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3500), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3500) },
                    { new Guid("10000000-0000-0000-0000-000000000025"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3503), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3503) },
                    { new Guid("10000000-0000-0000-0000-000000000026"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3506), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3506) },
                    { new Guid("10000000-0000-0000-0000-000000000027"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3508), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3509) },
                    { new Guid("10000000-0000-0000-0000-000000000028"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3511), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3512) },
                    { new Guid("10000000-0000-0000-0000-000000000029"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3514), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3514) },
                    { new Guid("10000000-0000-0000-0000-000000000030"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3518), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(3519) },
                    { new Guid("11111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2136), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2137) },
                    { new Guid("12121212-1212-1212-1212-121212121212"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2183), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2184) },
                    { new Guid("21111111-1111-1111-1111-111111111111"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1015), null, null },
                    { new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2145), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2146) },
                    { new Guid("23232323-2323-2323-2323-232323232323"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2187), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2187) },
                    { new Guid("23333333-3333-3333-3333-333333333343"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1125), null, null },
                    { new Guid("23333333-3333-3333-3333-333333333344"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1126), null, null },
                    { new Guid("32222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1044), null, null },
                    { new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2148), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2148) },
                    { new Guid("33cc4a77-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5891), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5892) },
                    { new Guid("33cc4a77-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5900), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5900) },
                    { new Guid("33cc4a77-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5907), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5907) },
                    { new Guid("33cc4a77-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5912), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5912) },
                    { new Guid("33cc4a77-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5917), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5917) },
                    { new Guid("33cc4a77-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5926), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5927) },
                    { new Guid("33cc4a77-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5932), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5932) },
                    { new Guid("33cc4a77-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5937), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5937) },
                    { new Guid("33cc4a77-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5942), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5943) },
                    { new Guid("33cc4a77-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5947), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5947) },
                    { new Guid("34343434-3434-3434-3434-343434343434"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2189), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2189) },
                    { new Guid("43333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1047), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333334"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1056), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333335"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1060), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333336"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1062), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333337"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1066), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333338"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1067), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333339"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1069), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333340"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1071), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333341"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1121), null, null },
                    { new Guid("43333333-3333-3333-3333-333333333342"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1123), null, null },
                    { new Guid("44444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2150), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2151) },
                    { new Guid("45454545-4545-4545-4545-454545454545"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2191), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2191) },
                    { new Guid("54444444-4444-4444-4444-444444444444"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1052), null, null },
                    { new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2152), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2153) },
                    { new Guid("56565656-5656-5656-5656-565656565656"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2193), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2194) },
                    { new Guid("65555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(1054), null, null },
                    { new Guid("66666666-6666-6666-6666-666666666666"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2155), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2155) },
                    { new Guid("77777777-7777-7777-7777-777777777777"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2161), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2161) },
                    { new Guid("88888888-8888-8888-8888-888888888888"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2163), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2163) },
                    { new Guid("99999999-9999-9999-9999-999999999999"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2167), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2167) },
                    { new Guid("a1b2c3d4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8322), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8322) },
                    { new Guid("a1b2c3d4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8328), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8328) },
                    { new Guid("a1b2c3d4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8332), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8332) },
                    { new Guid("a1b2c3d4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8336), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8336) },
                    { new Guid("a1b2c3d4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8340), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8340) },
                    { new Guid("a1b2c3d4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8346), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8346) },
                    { new Guid("a1b2c3d4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8350), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8351) },
                    { new Guid("a1b2c3d4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8355), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8355) },
                    { new Guid("a1b2c3d4-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8362), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8363) },
                    { new Guid("a1b2c3d4-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8366), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8367) },
                    { new Guid("a1b2c3d4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8372), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8373) },
                    { new Guid("a1b2c3d4-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8381), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8381) },
                    { new Guid("a1b2c3d4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8385), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8385) },
                    { new Guid("a1b2c3d4-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8389), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8389) },
                    { new Guid("a1b2c3d4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8392), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8392) },
                    { new Guid("a1b2c3d4-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8395), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8395) },
                    { new Guid("a1b2c3d4-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8399), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8399) },
                    { new Guid("a1b2c3d4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8402), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8403) },
                    { new Guid("a1b2c3d4-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8407), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8407) },
                    { new Guid("a1b2c3d4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8411), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(8411) },
                    { new Guid("a1e2f3a4-0001-0001-1001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9688), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9688) },
                    { new Guid("a1f2e3d4-0002-0002-1002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9695), null, new DateTime(2025, 5, 21, 16, 45, 22, 429, DateTimeKind.Utc).AddTicks(9695) },
                    { new Guid("a1f2e3d4-0003-0003-1003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9708), null, new DateTime(2025, 5, 21, 15, 45, 22, 429, DateTimeKind.Utc).AddTicks(9708) },
                    { new Guid("a1f2e3d4-0004-0004-1004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9712), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9713) },
                    { new Guid("a1f2e3d4-0005-0005-1005-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9716), null, new DateTime(2025, 5, 21, 16, 45, 22, 429, DateTimeKind.Utc).AddTicks(9717) },
                    { new Guid("a1f2e3d4-0006-0006-1006-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9723), null, new DateTime(2025, 5, 21, 15, 45, 22, 429, DateTimeKind.Utc).AddTicks(9723) },
                    { new Guid("a1f2e3d4-0007-0007-1007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9727), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9728) },
                    { new Guid("a1f2e3d4-0008-0008-1008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9732), null, new DateTime(2025, 5, 21, 17, 45, 22, 429, DateTimeKind.Utc).AddTicks(9732) },
                    { new Guid("a1f2e3d4-0009-0009-1009-000000000009"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9738), null, new DateTime(2025, 5, 21, 15, 45, 22, 429, DateTimeKind.Utc).AddTicks(9738) },
                    { new Guid("a1f2e3d4-0010-0010-1010-000000000010"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9741), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9742) },
                    { new Guid("a1f2e3d4-0011-0011-1011-000000000011"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9746), null, new DateTime(2025, 5, 21, 16, 45, 22, 429, DateTimeKind.Utc).AddTicks(9746) },
                    { new Guid("a1f2e3d4-0012-0012-1012-000000000012"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9750), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9750) },
                    { new Guid("a1f2e3d4-0013-0013-1013-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9754), null, new DateTime(2025, 5, 21, 16, 45, 22, 429, DateTimeKind.Utc).AddTicks(9754) },
                    { new Guid("a1f2e3d4-0014-0014-1014-000000000014"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9757), null, new DateTime(2025, 5, 21, 17, 45, 22, 429, DateTimeKind.Utc).AddTicks(9758) },
                    { new Guid("a1f2e3d4-0015-0015-1015-000000000015"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9761), null, new DateTime(2025, 5, 21, 17, 45, 22, 429, DateTimeKind.Utc).AddTicks(9761) },
                    { new Guid("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2170), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2170) },
                    { new Guid("b1c2d3e4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(548), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(548) },
                    { new Guid("b1c2d3e4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(550), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(551) },
                    { new Guid("b1c2d3e4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(553), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(553) },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2172), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2172) },
                    { new Guid("c1d2e3f4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9904), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9904) },
                    { new Guid("c1d2e3f4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9908), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9909) },
                    { new Guid("c1d2e3f4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9911), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9911) },
                    { new Guid("c1d2e3f4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9914), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9914) },
                    { new Guid("c1d2e3f4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9917), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9917) },
                    { new Guid("c1d2e3f4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9919), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9920) },
                    { new Guid("c1d2e3f4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9922), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9923) },
                    { new Guid("c1d2e3f4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9925), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9925) },
                    { new Guid("c1d2e3f4-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9927), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9928) },
                    { new Guid("c1d2e3f4-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9931), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9932) },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2174), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2175) },
                    { new Guid("d1e2f3a4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9338), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9339) },
                    { new Guid("d1e2f3a4-0001-4001-8001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6041), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6042) },
                    { new Guid("d1e2f3a4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9346), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9346) },
                    { new Guid("d1e2f3a4-0002-4002-8002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6048), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6049) },
                    { new Guid("d1e2f3a4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9352), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9352) },
                    { new Guid("d1e2f3a4-0003-4003-8003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6053), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6053) },
                    { new Guid("d1e2f3a4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9393), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9393) },
                    { new Guid("d1e2f3a4-0004-4004-8004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6058), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6058) },
                    { new Guid("d1e2f3a4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9405), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9405) },
                    { new Guid("d1e2f3a4-0005-4005-8005-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6062), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6062) },
                    { new Guid("d1e2f3a4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9410), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9411) },
                    { new Guid("d1e2f3a4-0006-4006-8006-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6066), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6067) },
                    { new Guid("d1e2f3a4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9417), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9417) },
                    { new Guid("d1e2f3a4-0007-4007-8007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6071), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6071) },
                    { new Guid("d1e2f3a4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9423), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9424) },
                    { new Guid("d1e2f3a4-0008-4008-8008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6075), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(6075) },
                    { new Guid("d1e2f3a4-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9429), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9430) },
                    { new Guid("d1e2f3a4-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9438), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9438) },
                    { new Guid("d1e2f3a4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9446), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9447) },
                    { new Guid("d1e2f3a4-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9457), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9457) },
                    { new Guid("d1e2f3a4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9463), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9463) },
                    { new Guid("d1e2f3a4-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9471), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9471) },
                    { new Guid("d1e2f3a4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9477), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9477) },
                    { new Guid("d1e2f3a4-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9483), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9483) },
                    { new Guid("d1e2f3a4-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9493), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9493) },
                    { new Guid("d1e2f3a4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9498), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9499) },
                    { new Guid("d1e2f3a4-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9504), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9504) },
                    { new Guid("d1e2f3a4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9509), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(9509) },
                    { new Guid("dddddddd-dddd-dddd-dddd-dddddddddddd"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2177), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2177) },
                    { new Guid("e1d1a111-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2639), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2639) },
                    { new Guid("e1d1a123-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2695), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2695) },
                    { new Guid("e1d1a124-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2698), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2698) },
                    { new Guid("e1d1a125-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2701), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2703) },
                    { new Guid("e1d1a126-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2705), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2705) },
                    { new Guid("e1d1a127-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2708), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2708) },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2710), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2711) },
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2714), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2715) },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2717), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2717) },
                    { new Guid("e1d1a131-0023-0023-0023-000000000023"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2720), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2720) },
                    { new Guid("e1d1a132-0024-0024-0024-000000000024"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2723), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2723) },
                    { new Guid("e1d1a133-0025-0025-0025-000000000025"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2726), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2726) },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2766), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2766) },
                    { new Guid("e1d1a135-0027-0027-0027-000000000027"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2769), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2769) },
                    { new Guid("e1d1a136-0028-0028-0028-000000000028"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2772), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2772) },
                    { new Guid("e1d1a137-0029-0029-0029-000000000029"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2776), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2776) },
                    { new Guid("e1d1a138-0030-0030-0030-000000000030"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2779), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2779) },
                    { new Guid("e1d1a222-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2644), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2644) },
                    { new Guid("e1d1a333-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2649), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2649) },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2651), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2652) },
                    { new Guid("e1d1a555-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2657), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2658) },
                    { new Guid("e1d1a666-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2660), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2660) },
                    { new Guid("e1d1a777-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2663), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2663) },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2666), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2666) },
                    { new Guid("e1d1a999-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2669), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2669) },
                    { new Guid("e1d1abbb-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2671), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2672) },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2692), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2692) },
                    { new Guid("e1d1addd-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2675), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2676) },
                    { new Guid("e1d1aeee-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2684), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2684) },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2688), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2689) },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2179), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2179) },
                    { new Guid("f1e2d3c4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(670), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(671) },
                    { new Guid("f1e2d3c4-0002-0002-0002-000000000002"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(674), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(674) },
                    { new Guid("f1e2d3c4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(677), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(678) },
                    { new Guid("f1e2d3c4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(681), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(681) },
                    { new Guid("f1e2d3c4-0005-0005-0005-000000000005"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(686), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(686) },
                    { new Guid("f1e2d3c4-0006-0006-0006-000000000006"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(691), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(691) },
                    { new Guid("f1e2d3c4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(694), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(695) },
                    { new Guid("f1e2d3c4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(698), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(698) },
                    { new Guid("f1e2d3c4-0009-0009-0009-000000000009"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(701), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(701) },
                    { new Guid("f1e2d3c4-0010-0010-0010-000000000010"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(704), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(705) },
                    { new Guid("f1e2d3c4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(708), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(708) },
                    { new Guid("f1e2d3c4-0012-0012-0012-000000000012"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(778), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(779) },
                    { new Guid("f1e2d3c4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(784), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(784) },
                    { new Guid("f1e2d3c4-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(787), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(787) },
                    { new Guid("f1e2d3c4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(790), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(790) },
                    { new Guid("f1e2d3c4-0016-0016-0016-000000000016"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(793), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(793) },
                    { new Guid("f1e2d3c4-0017-0017-0017-000000000017"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(796), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(796) },
                    { new Guid("f1e2d3c4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(799), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(799) },
                    { new Guid("f1e2d3c4-0019-0019-0019-000000000019"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(802), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(802) },
                    { new Guid("f1e2d3c4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(805), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(805) },
                    { new Guid("f1e2d3c4-0021-0021-0021-000000000021"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(811), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(811) },
                    { new Guid("f1e2d3c4-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(814), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(814) },
                    { new Guid("f1e2d3c4-0023-0023-0023-000000000023"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(817), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(817) },
                    { new Guid("f1e2d3c4-0024-0024-0024-000000000024"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(822), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(822) },
                    { new Guid("f1e2d3c4-0025-0025-0025-000000000025"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(825), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(825) },
                    { new Guid("f1e2d3c4-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(827), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(828) },
                    { new Guid("f1e2d3c4-0027-0027-0027-000000000027"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(830), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(830) },
                    { new Guid("f1e2d3c4-0028-0028-0028-000000000028"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(832), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(833) },
                    { new Guid("f1e2d3c4-0029-0029-0029-000000000029"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(836), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(837) },
                    { new Guid("f1e2d3c4-0030-0030-0030-000000000030"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(839), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(839) },
                    { new Guid("ffffffff-ffff-ffff-ffff-ffffffffffff"), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2181), null, new DateTime(2025, 5, 21, 14, 45, 22, 429, DateTimeKind.Utc).AddTicks(2181) }
                });

            migrationBuilder.InsertData(
                table: "Areas",
                columns: new[] { "Id", "AreaName" },
                values: new object[,]
                {
                    { new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "Main Production Floor" },
                    { new Guid("b1c2d3e4-0002-0002-0002-000000000002"), "Finishing Department" },
                    { new Guid("b1c2d3e4-0003-0003-0003-000000000003"), "Quality Control Area" }
                });

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
                    { new Guid("a1b2c3d4-0001-0001-0001-000000000001"), "High-speed single needle lockstitch sewing machine, unit 1, for lightweight fabrics.", "MC001-JUKI-DDL8700-01", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_01.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-001", "Active" },
                    { new Guid("a1b2c3d4-0002-0002-0002-000000000002"), "High-speed single needle lockstitch sewing machine, unit 2, for medium-weight fabrics.", "MC002-JUKI-DDL8700-02", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_02.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-002", "Active" },
                    { new Guid("a1b2c3d4-0003-0003-0003-000000000003"), "High-speed single needle lockstitch sewing machine, unit 3, in maintenance.", "MC003-JUKI-DDL8700-03", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_03.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-003", "InRepair" },
                    { new Guid("a1b2c3d4-0004-0004-0004-000000000004"), "High-speed single needle lockstitch sewing machine, unit 4, for cotton fabrics.", "MC004-JUKI-DDL8700-04", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_04.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-004", "Active" },
                    { new Guid("a1b2c3d4-0005-0005-0005-000000000005"), "High-speed single needle lockstitch sewing machine, unit 5, for synthetic fabrics.", "MC005-JUKI-DDL8700-05", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_05.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-005", "Active" },
                    { new Guid("a1b2c3d4-0006-0006-0006-000000000006"), "High-speed single needle lockstitch sewing machine, unit 6, retired unit.", "MC006-JUKI-DDL8700-06", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_06.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-006", "Retired" },
                    { new Guid("a1b2c3d4-0007-0007-0007-000000000007"), "High-speed single needle lockstitch sewing machine, unit 7, for heavy fabrics.", "MC007-JUKI-DDL8700-07", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_07.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-007", "Active" },
                    { new Guid("a1b2c3d4-0008-0008-0008-000000000008"), "High-speed single needle lockstitch sewing machine, unit 8, for thin fabrics.", "MC008-JUKI-DDL8700-08", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_08.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-008", "Active" },
                    { new Guid("a1b2c3d4-0009-0009-0009-000000000009"), "High-speed single needle lockstitch sewing machine, unit 9, for mixed fabrics.", "MC009-JUKI-DDL8700-09", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_09.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-009", "Active" },
                    { new Guid("a1b2c3d4-0010-0010-0010-000000000010"), "High-speed single needle lockstitch sewing machine, unit 10, for general use.", "MC010-JUKI-DDL8700-10", "Industrial Sewing Machine", "Juki", "DDL-8700", "https://example.com/photos/juki_ddl8700_10.jpg", new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5500 SPM, Max stitch length: 5mm, Serial: J8700-010", "Active" },
                    { new Guid("a1b2c3d4-0011-0011-0011-000000000011"), "Digital lockstitch machine with auto thread trimmer, unit 1.", "MC011-JUKI-DDL9000C-01", "Digital Lockstitch Machine", "Juki", "DDL-9000C", "https://example.com/photos/juki_ddl9000c_01.jpg", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5000 SPM, Auto thread trimmer, Serial: J9000C-001", "Active" },
                    { new Guid("a1b2c3d4-0012-0012-0012-000000000012"), "Digital lockstitch machine with auto thread trimmer, unit 2.", "MC012-JUKI-DDL9000C-02", "Digital Lockstitch Machine", "Juki", "DDL-9000C", "https://example.com/photos/juki_ddl9000c_02.jpg", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5000 SPM, Auto thread trimmer, Serial: J9000C-002", "Active" },
                    { new Guid("a1b2c3d4-0013-0013-0013-000000000013"), "Digital lockstitch machine with auto thread trimmer, unit 3.", "MC013-JUKI-DDL9000C-03", "Digital Lockstitch Machine", "Juki", "DDL-9000C", "https://example.com/photos/juki_ddl9000c_03.jpg", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5000 SPM, Auto thread trimmer, Serial: J9000C-003", "InRepair" },
                    { new Guid("a1b2c3d4-0014-0014-0014-000000000014"), "Digital lockstitch machine with auto thread trimmer, unit 4.", "MC014-JUKI-DDL9000C-04", "Digital Lockstitch Machine", "Juki", "DDL-9000C", "https://example.com/photos/juki_ddl9000c_04.jpg", new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 5000 SPM, Auto thread trimmer, Serial: J9000C-004", "Active" },
                    { new Guid("a1b2c3d4-0015-0015-0015-000000000015"), "Three-thread overlock sewing machine, unit 1, for lightweight fabrics.", "MC015-BROTHER-B957-01", "Overlock Machine", "Brother", "B957", "https://example.com/photos/brother_b957_01.jpg", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0, Serial: B957-001", "Active" },
                    { new Guid("a1b2c3d4-0016-0016-0016-000000000016"), "Three-thread overlock sewing machine, unit 2, for synthetic fabrics.", "MC016-BROTHER-B957-02", "Overlock Machine", "Brother", "B957", "https://example.com/photos/brother_b957_02.jpg", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0, Serial: B957-002", "Active" },
                    { new Guid("a1b2c3d4-0017-0017-0017-000000000017"), "Three-thread overlock sewing machine, unit 3, for thin materials.", "MC017-BROTHER-B957-03", "Overlock Machine", "Brother", "B957", "https://example.com/photos/brother_b957_03.jpg", new DateTime(2019, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0, Serial: B957-003", "Active" },
                    { new Guid("a1b2c3d4-0018-0018-0018-000000000018"), "Heavy-duty machine for thick materials, unit 1, for denim.", "MC018-SINGER-4452-01", "Heavy Duty Sewing Machine", "Singer", "4452", "https://example.com/photos/singer_4452_01.jpg", new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 1100 SPM, Presser foot lift: 6mm, Serial: S4452-001", "Active" },
                    { new Guid("a1b2c3d4-0019-0019-0019-000000000019"), "Heavy-duty machine for thick materials, unit 2, for leather.", "MC019-SINGER-4452-02", "Heavy Duty Sewing Machine", "Singer", "4452", "https://example.com/photos/singer_4452_02.jpg", new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 1100 SPM, Presser foot lift: 6mm, Serial: S4452-002", "Active" },
                    { new Guid("a1b2c3d4-0020-0020-0020-000000000020"), "Heavy-duty machine for thick materials, unit 3, for canvas.", "MC020-SINGER-4452-03", "Heavy Duty Sewing Machine", "Singer", "4452", "https://example.com/photos/singer_4452_03.jpg", new DateTime(2021, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Max speed: 1100 SPM, Presser foot lift: 6mm, Serial: S4452-003", "InRepair" }
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
                    { new Guid("d1e2f3a4-0001-0001-0001-000000000001"), "Single needle lockstitch device for lightweight fabrics.", "DEV001-JUKI-DDL8700-01", "Juki DDL-8700 Unit 1", new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0001-0001-0001-000000000001"), new DateTime(2020, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_01.jpg", new Guid("f1e2d3c4-0001-0001-0001-000000000001"), 15000000m, "J8700-D001", "Max speed: 5500 SPM, Stitch length: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0002-0002-0002-000000000002"), "Single needle lockstitch device for medium-weight fabrics.", "DEV002-JUKI-DDL8700-02", "Juki DDL-8700 Unit 2", new DateTime(2020, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0002-0002-0002-000000000002"), new DateTime(2020, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_02.jpg", new Guid("f1e2d3c4-0002-0002-0002-000000000002"), 15000000m, "J8700-D002", "Max speed: 5500 SPM, Stitch length: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0003-0003-0003-000000000003"), "Single needle lockstitch device, currently in repair.", "DEV003-JUKI-DDL8700-03", "Juki DDL-8700 Unit 3", new DateTime(2020, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0003-0003-0003-000000000003"), new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_03.jpg", null, 15000000m, "J8700-D003", "Max speed: 5500 SPM, Stitch length: 5mm", "InRepair", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0004-0004-0004-000000000004"), "Single needle lockstitch device for cotton fabrics.", "DEV004-JUKI-DDL8700-04", "Juki DDL-8700 Unit 4", new DateTime(2020, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0004-0004-0004-000000000004"), new DateTime(2020, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_04.jpg", new Guid("f1e2d3c4-0003-0003-0003-000000000003"), 15000000m, "J8700-D004", "Max speed: 5500 SPM, Stitch length: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0005-0005-0005-000000000005"), "Single needle lockstitch device for synthetic fabrics.", "DEV005-JUKI-DDL8700-05", "Juki DDL-8700 Unit 5", new DateTime(2020, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0005-0005-0005-000000000005"), new DateTime(2020, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_05.jpg", new Guid("f1e2d3c4-0004-0004-0004-000000000004"), 15000000m, "J8700-D005", "Max speed: 5500 SPM, Stitch length: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0006-0006-0006-000000000006"), "Single needle lockstitch device, retired.", "DEV006-JUKI-DDL8700-06", "Juki DDL-8700 Unit 6", new DateTime(2020, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0006-0006-0006-000000000006"), new DateTime(2020, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_06.jpg", null, 15000000m, "J8700-D006", "Max speed: 5500 SPM, Stitch length: 5mm", "Retired", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0007-0007-0007-000000000007"), "Single needle lockstitch device for heavy fabrics.", "DEV007-JUKI-DDL8700-07", "Juki DDL-8700 Unit 7", new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0007-0007-0007-000000000007"), new DateTime(2020, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_07.jpg", new Guid("f1e2d3c4-0006-0006-0006-000000000006"), 15000000m, "J8700-D007", "Max speed: 5500 SPM, Stitch length: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0008-0008-0008-000000000008"), "Single needle lockstitch device for thin fabrics.", "DEV008-JUKI-DDL8700-08", "Juki DDL-8700 Unit 8", new DateTime(2020, 2, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0008-0008-0008-000000000008"), new DateTime(2020, 1, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_08.jpg", new Guid("f1e2d3c4-0007-0007-0007-000000000007"), 15000000m, "J8700-D008", "Max speed: 5500 SPM, Stitch length: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0009-0009-0009-000000000009"), "Single needle lockstitch device for mixed fabrics.", "DEV009-JUKI-DDL8700-09", "Juki DDL-8700 Unit 9", new DateTime(2020, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0009-0009-0009-000000000009"), new DateTime(2020, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_09.jpg", new Guid("f1e2d3c4-0010-0010-0010-000000000010"), 15000000m, "J8700-D009", "Max speed: 5500 SPM, Stitch length: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0010-0010-0010-000000000010"), "Single needle lockstitch device for general use.", "DEV010-JUKI-DDL8700-10", "Juki DDL-8700 Unit 10", new DateTime(2020, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0010-0010-0010-000000000010"), new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-8700", "https://example.com/photos/device_juki_ddl8700_10.jpg", new Guid("f1e2d3c4-0011-0011-0011-000000000011"), 15000000m, "J8700-D010", "Max speed: 5500 SPM, Stitch length: 5mm", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0011-0011-0011-000000000011"), "Digital lockstitch device with auto thread trimmer, unit 1.", "DEV011-JUKI-DDL9000C-01", "Juki DDL-9000C Unit 1", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0011-0011-0011-000000000011"), new DateTime(2022, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-9000C", "https://example.com/photos/device_juki_ddl9000c_01.jpg", new Guid("f1e2d3c4-0008-0008-0008-000000000008"), 20000000m, "J9000C-D001", "Max speed: 5000 SPM, Auto thread trimmer", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0012-0012-0012-000000000012"), "Digital lockstitch device with auto thread trimmer, unit 2.", "DEV012-JUKI-DDL9000C-02", "Juki DDL-9000C Unit 2", new DateTime(2022, 3, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0012-0012-0012-000000000012"), new DateTime(2022, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-9000C", "https://example.com/photos/device_juki_ddl9000c_02.jpg", new Guid("f1e2d3c4-0012-0012-0012-000000000012"), 20000000m, "J9000C-D002", "Max speed: 5000 SPM, Auto thread trimmer", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0013-0013-0013-000000000013"), "Digital lockstitch device with auto thread trimmer, in repair.", "DEV013-JUKI-DDL9000C-03", "Juki DDL-9000C Unit 3", new DateTime(2022, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0013-0013-0013-000000000013"), new DateTime(2022, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-9000C", "https://example.com/photos/device_juki_ddl9000c_03.jpg", null, 20000000m, "J9000C-D003", "Max speed: 5000 SPM, Auto thread trimmer", "InRepair", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0014-0014-0014-000000000014"), "Digital lockstitch device with auto thread trimmer, unit 4.", "DEV014-JUKI-DDL9000C-04", "Juki DDL-9000C Unit 4", new DateTime(2022, 3, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0014-0014-0014-000000000014"), new DateTime(2022, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Juki", "DDL-9000C", "https://example.com/photos/device_juki_ddl9000c_04.jpg", new Guid("f1e2d3c4-0013-0013-0013-000000000013"), 20000000m, "J9000C-D004", "Max speed: 5000 SPM, Auto thread trimmer", "Active", "Juki Vietnam" },
                    { new Guid("d1e2f3a4-0015-0015-0015-000000000015"), "Three-thread overlock device for lightweight fabrics.", "DEV015-BROTHER-B957-01", "Brother B957 Unit 1", new DateTime(2019, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0015-0015-0015-000000000015"), new DateTime(2019, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "B957", "https://example.com/photos/device_brother_b957_01.jpg", new Guid("f1e2d3c4-0019-0019-0019-000000000019"), 12000000m, "B957-D001", "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0016-0016-0016-000000000016"), "Three-thread overlock device for synthetic fabrics.", "DEV016-BROTHER-B957-02", "Brother B957 Unit 2", new DateTime(2019, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0016-0016-0016-000000000016"), new DateTime(2019, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "B957", "https://example.com/photos/device_brother_b957_02.jpg", new Guid("f1e2d3c4-0020-0020-0020-000000000020"), 12000000m, "B957-D002", "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0017-0017-0017-000000000017"), "Three-thread overlock device for thin materials.", "DEV017-BROTHER-B957-03", "Brother B957 Unit 3", new DateTime(2019, 7, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, new Guid("a1b2c3d4-0017-0017-0017-000000000017"), new DateTime(2019, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Brother", "B957", "https://example.com/photos/device_brother_b957_03.jpg", new Guid("f1e2d3c4-0021-0021-0021-000000000021"), 12000000m, "B957-D003", "Max speed: 7000 SPM, Differential feed ratio: 0.7-2.0", "Active", "Brother Vietnam" },
                    { new Guid("d1e2f3a4-0018-0018-0018-000000000018"), "Heavy-duty device for denim fabrics.", "DEV018-SINGER-4452-01", "Singer 4452 Unit 1", new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0018-0018-0018-000000000018"), new DateTime(2021, 3, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "4452", "https://example.com/photos/device_singer_4452_01.jpg", new Guid("f1e2d3c4-0023-0023-0023-000000000023"), 18000000m, "S4452-D001", "Max speed: 1100 SPM, Presser foot lift: 6mm", "Active", "Singer Vietnam" },
                    { new Guid("d1e2f3a4-0019-0019-0019-000000000019"), "Heavy-duty device for leather fabrics.", "DEV019-SINGER-4452-02", "Singer 4452 Unit 2", new DateTime(2021, 4, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0019-0019-0019-000000000019"), new DateTime(2021, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "4452", "https://example.com/photos/device_singer_4452_02.jpg", new Guid("f1e2d3c4-0024-0024-0024-000000000024"), 18000000m, "S4452-D002", "Max speed: 1100 SPM, Presser foot lift: 6mm", "Active", "Singer Vietnam" },
                    { new Guid("d1e2f3a4-0020-0020-0020-000000000020"), "Heavy-duty device for canvas, in repair.", "DEV020-SINGER-4452-03", "Singer 4452 Unit 3", new DateTime(2021, 4, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, new Guid("a1b2c3d4-0020-0020-0020-000000000020"), new DateTime(2021, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Singer", "4452", "https://example.com/photos/device_singer_4452_03.jpg", null, 18000000m, "S4452-D003", "Max speed: 1100 SPM, Presser foot lift: 6mm", "InRepair", "Singer Vietnam" }
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

            migrationBuilder.InsertData(
                table: "MachineErrorHistories",
                columns: new[] { "ErrorId", "MachineId", "CreatedBy", "CreatedDate", "Id", "IsDeleted", "LastOccurredDate", "ModifiedBy", "ModifiedDate", "Notes", "OccurrenceCount" },
                values: new object[,]
                {
                    { new Guid("e1d1a129-0021-0021-0021-000000000021"), new Guid("a1b2c3d4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8174), new Guid("82abe7b6-3378-48d8-b0cc-6aa73f753d9c"), false, new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), null, null, "Mỏ trói chỉ bị lỏng, đã điều chỉnh lực siết.", 3 },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("a1b2c3d4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8181), new Guid("c8be4d9b-4259-41b2-a4f9-94ba8e677aab"), false, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), null, null, "Động cơ cháy do quá tải, cần thay mô tơ mới.", 1 },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("a1b2c3d4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8185), new Guid("783edf25-3ce2-43e9-9a52-b4d0cd4ab034"), false, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Mô tơ bị chập điện, đang chờ phụ tùng thay thế.", 1 },
                    { new Guid("e1d1abcf-0014-0014-0014-000000000014"), new Guid("a1b2c3d4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8187), new Guid("715a72e6-b40d-41e7-905a-04081d806e9c"), false, new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), null, null, "Kim lệch tâm, đã căn chỉnh lại trục kim.", 2 },
                    { new Guid("e1d1a130-0022-0022-0022-000000000022"), new Guid("a1b2c3d4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8193), new Guid("c5147ee3-3dc7-4163-88d7-0d2519464332"), false, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), null, null, "Bánh răng mòn gây tiếng ồn, đã lên kế hoạch thay mới.", 2 },
                    { new Guid("e1d1a888-0008-0008-0008-000000000008"), new Guid("a1b2c3d4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8197), new Guid("a7426a8a-6fb3-4e87-bcc1-61c144d61a93"), false, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), null, null, "Bo mạch điều khiển lỗi, đang kiểm tra để sửa chữa.", 1 },
                    { new Guid("e1d1a134-0026-0026-0026-000000000026"), new Guid("a1b2c3d4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8199), new Guid("f0fc8279-bf30-4d95-a221-8b18ab8e7df3"), false, new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), null, null, "Bulong lỏng ở bộ truyền, đã siết chặt lại.", 3 },
                    { new Guid("e1d1afff-0013-0013-0013-000000000013"), new Guid("a1b2c3d4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8202), new Guid("8c88c229-11ec-45b5-88fc-260e4dffe425"), false, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), null, null, "Cảm biến vải không nhận, đã thay cảm biến mới.", 2 },
                    { new Guid("e1d1a444-0004-0004-0004-000000000004"), new Guid("a1b2c3d4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8206), new Guid("b3511592-efe0-4b80-9258-126ab1406017"), false, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), null, null, "Mô tơ bị cháy, đang chờ thay thế phụ tùng.", 1 },
                    { new Guid("e1d1a128-0020-0020-0020-000000000020"), new Guid("a1b2c3d4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(8209), new Guid("383d4791-f312-4fd5-a4e6-d12c86d7e63e"), false, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), null, null, "Bộ cấp vải hoạt động không đều, đã điều chỉnh lại.", 2 }
                });

            migrationBuilder.InsertData(
                table: "MachineIssueHistories",
                columns: new[] { "IssueId", "MachineId", "CreatedBy", "CreatedDate", "Id", "IsDeleted", "LastOccurredDate", "ModifiedBy", "ModifiedDate", "Notes", "OccurrenceCount" },
                values: new object[,]
                {
                    { new Guid("88888888-8888-8888-8888-888888888888"), new Guid("a1b2c3d4-0001-0001-0001-000000000001"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(407), new Guid("84631d4a-fc02-436e-a9f1-60b365cbf89b"), false, new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), null, null, "Chỉ bị đứt do kẹt ở ống chỉ, đã thay ống chỉ mới.", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a1b2c3d4-0003-0003-0003-000000000003"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(414), new Guid("ac9a90cc-5cef-4aa0-adca-6c15d22058c1"), false, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy không chạy do lỗi động cơ, đang chờ sửa chữa.", 2 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a1b2c3d4-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(418), new Guid("5bc7816b-141e-4fd5-9fa1-a3ceaa190848"), false, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy ngừng hoạt động, kiểm tra phát hiện lỗi dây điện.", 1 },
                    { new Guid("22222222-2222-2222-2222-222222222222"), new Guid("a1b2c3d4-0007-0007-0007-000000000007"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(420), new Guid("a01c9ebe-be98-4b6d-af46-18e7d25c7d52"), false, new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), null, null, "Kim gãy do sử dụng sai loại kim, đã thay kim phù hợp.", 4 },
                    { new Guid("55555555-5555-5555-5555-555555555555"), new Guid("a1b2c3d4-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(422), new Guid("ff27eb2d-c630-4352-b11f-ebde43619b8f"), false, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), null, null, "Tiếng ồn lớn do bánh răng mòn, cần thay thế.", 2 },
                    { new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), new Guid("a1b2c3d4-0011-0011-0011-000000000011"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(424), new Guid("289affdb-9c85-4b6e-87a5-d85432a14b1e"), false, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), null, null, "Đèn báo lỗi sáng, kiểm tra mạch điện tử đang được tiến hành.", 1 },
                    { new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), new Guid("a1b2c3d4-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(431), new Guid("f959d734-c5f9-4756-8d61-f63d89eafc89"), false, new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), null, null, "Chỉ không đều do bulong lỏng, đã siết lại.", 2 },
                    { new Guid("34343434-3434-3434-3434-343434343434"), new Guid("a1b2c3d4-0015-0015-0015-000000000015"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(433), new Guid("01d3d6df-efc8-4ba2-928c-e433aa9dca67"), false, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), null, null, "Chỉ dưới không kéo lên, kiểm tra cảm biến và thay mới.", 3 },
                    { new Guid("33333333-3333-3333-3333-333333333333"), new Guid("a1b2c3d4-0018-0018-0018-000000000018"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(436), new Guid("62f3ccf6-c58d-4fda-9d4c-f6ff2ad98186"), false, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy không chạy, kiểm tra phát hiện lỗi mô tơ.", 1 },
                    { new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), new Guid("a1b2c3d4-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 21, 14, 45, 22, 431, DateTimeKind.Utc).AddTicks(439), new Guid("864ce8c3-10a2-4d4b-b933-abcde6f8d501"), false, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), null, null, "Vải bị nhăn do điều chỉnh áp suất không đúng, đã điều chỉnh lại.", 2 }
                });

            migrationBuilder.InsertData(
                table: "Zones",
                columns: new[] { "Id", "AreaId", "ZoneName" },
                values: new object[,]
                {
                    { new Guid("c1d2e3f4-0001-0001-0001-000000000001"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "Sewing Line A" },
                    { new Guid("c1d2e3f4-0002-0002-0002-000000000002"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "Sewing Line B" },
                    { new Guid("c1d2e3f4-0003-0003-0003-000000000003"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "Sewing Line C" },
                    { new Guid("c1d2e3f4-0004-0004-0004-000000000004"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "Cutting Section" },
                    { new Guid("c1d2e3f4-0005-0005-0005-000000000005"), new Guid("b1c2d3e4-0001-0001-0001-000000000001"), "Fabric Preparation Zone" },
                    { new Guid("c1d2e3f4-0006-0006-0006-000000000006"), new Guid("b1c2d3e4-0002-0002-0002-000000000002"), "Overlock Section" },
                    { new Guid("c1d2e3f4-0007-0007-0007-000000000007"), new Guid("b1c2d3e4-0002-0002-0002-000000000002"), "Heavy Duty Stitching Zone" },
                    { new Guid("c1d2e3f4-0008-0008-0008-000000000008"), new Guid("b1c2d3e4-0002-0002-0002-000000000002"), "Trimming and Packing Zone" },
                    { new Guid("c1d2e3f4-0009-0009-0009-000000000009"), new Guid("b1c2d3e4-0003-0003-0003-000000000003"), "Inspection Zone 1" },
                    { new Guid("c1d2e3f4-0010-0010-0010-000000000010"), new Guid("b1c2d3e4-0003-0003-0003-000000000003"), "Inspection Zone 2" }
                });

            migrationBuilder.InsertData(
                table: "DeviceErrorHistories",
                columns: new[] { "DeviceId", "ErrorId", "CreatedBy", "CreatedDate", "Id", "IsDeleted", "LastOccurredDate", "ModifiedBy", "ModifiedDate", "Notes", "OccurrenceCount" },
                values: new object[,]
                {
                    { new Guid("d1e2f3a4-0001-0001-0001-000000000001"), new Guid("e1d1a129-0021-0021-0021-000000000021"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3491), new Guid("aad730b2-fbb6-4fb8-b9bc-cb2f8657c07d"), false, new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), null, null, "Mỏ trói chỉ bị lỏng, đã điều chỉnh lực siết.", 3 },
                    { new Guid("d1e2f3a4-0003-0003-0003-000000000003"), new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3499), new Guid("cd73a3fb-dc96-4a26-9b64-dd03150de8ae"), false, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), null, null, "Động cơ cháy do quá tải, cần thay mô tơ mới.", 1 },
                    { new Guid("d1e2f3a4-0004-0004-0004-000000000004"), new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3505), new Guid("0211d2db-318d-4b27-8445-4d041a21a5a1"), false, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Mô tơ bị chập điện, đang chờ phụ tùng thay thế.", 1 },
                    { new Guid("d1e2f3a4-0007-0007-0007-000000000007"), new Guid("e1d1abcf-0014-0014-0014-000000000014"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3508), new Guid("e5d4ac7f-daa9-40c1-b591-ae3169eb34a7"), false, new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), null, null, "Kim lệch tâm, đã căn chỉnh lại trục kim.", 2 },
                    { new Guid("d1e2f3a4-0008-0008-0008-000000000008"), new Guid("e1d1a130-0022-0022-0022-000000000022"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3511), new Guid("0576a0a2-33f3-4742-b5ce-6ee79ba6ad0c"), false, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), null, null, "Bánh răng mòn gây tiếng ồn, đã lên kế hoạch thay mới.", 2 },
                    { new Guid("d1e2f3a4-0011-0011-0011-000000000011"), new Guid("e1d1a888-0008-0008-0008-000000000008"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3514), new Guid("d7cff4e0-427b-4d35-8710-b8eeb06cce72"), false, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), null, null, "Bo mạch điều khiển lỗi, đang kiểm tra để sửa chữa.", 1 },
                    { new Guid("d1e2f3a4-0013-0013-0013-000000000013"), new Guid("e1d1a134-0026-0026-0026-000000000026"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3519), new Guid("e4a23562-23e2-42a7-a316-ba3e8d89006f"), false, new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), null, null, "Bulong lỏng ở bộ truyền, đã siết chặt lại.", 3 },
                    { new Guid("d1e2f3a4-0015-0015-0015-000000000015"), new Guid("e1d1afff-0013-0013-0013-000000000013"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3522), new Guid("8012b548-e38a-4f41-9ff0-bfc912f8df15"), false, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), null, null, "Cảm biến vải không nhận, đã thay cảm biến mới.", 2 },
                    { new Guid("d1e2f3a4-0018-0018-0018-000000000018"), new Guid("e1d1a444-0004-0004-0004-000000000004"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3525), new Guid("199845d0-2773-4dd9-b9ae-59924aeb3886"), false, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), null, null, "Mô tơ bị cháy, đang chờ thay thế phụ tùng.", 1 },
                    { new Guid("d1e2f3a4-0020-0020-0020-000000000020"), new Guid("e1d1a128-0020-0020-0020-000000000020"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(3527), new Guid("31510e37-7786-49fa-8619-8258b6cdfaaf"), false, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), null, null, "Bộ cấp vải hoạt động không đều, đã điều chỉnh lại.", 2 }
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
                    { new Guid("d1e2f3a4-0001-0001-0001-000000000001"), new Guid("88888888-8888-8888-8888-888888888888"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5652), new Guid("86553d68-6660-4a8c-8be1-9c4aa1897074"), false, new DateTime(2025, 4, 15, 10, 30, 0, 0, DateTimeKind.Utc), null, null, "Chỉ bị đứt do kẹt ở ống chỉ, đã thay ống chỉ mới.", 3 },
                    { new Guid("d1e2f3a4-0003-0003-0003-000000000003"), new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5659), new Guid("f60f8d52-03a0-475d-9602-b5bc2447dbbf"), false, new DateTime(2025, 5, 1, 14, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy không chạy do lỗi động cơ, đang chờ sửa chữa.", 2 },
                    { new Guid("d1e2f3a4-0004-0004-0004-000000000004"), new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5661), new Guid("0cd0f1a8-6108-4d46-98f8-b038d6db8925"), false, new DateTime(2025, 5, 10, 9, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy ngừng hoạt động, kiểm tra phát hiện lỗi dây điện.", 1 },
                    { new Guid("d1e2f3a4-0007-0007-0007-000000000007"), new Guid("22222222-2222-2222-2222-222222222222"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5686), new Guid("74c24c69-81c6-4bd1-87bf-2fbb27941c28"), false, new DateTime(2025, 3, 20, 11, 15, 0, 0, DateTimeKind.Utc), null, null, "Kim gãy do sử dụng sai loại kim, đã thay kim phù hợp.", 4 },
                    { new Guid("d1e2f3a4-0008-0008-0008-000000000008"), new Guid("55555555-5555-5555-5555-555555555555"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5691), new Guid("711d287c-1c61-42b9-aeed-1f7bc53b035c"), false, new DateTime(2025, 4, 25, 13, 45, 0, 0, DateTimeKind.Utc), null, null, "Tiếng ồn lớn do bánh răng mòn, cần thay thế.", 2 },
                    { new Guid("d1e2f3a4-0011-0011-0011-000000000011"), new Guid("cccccccc-cccc-cccc-cccc-cccccccccccc"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5695), new Guid("8b1e3ea7-b3cf-49e8-b372-0ffa588ecd9c"), false, new DateTime(2025, 5, 5, 8, 20, 0, 0, DateTimeKind.Utc), null, null, "Đèn báo lỗi sáng, kiểm tra mạch điện tử đang được tiến hành.", 1 },
                    { new Guid("d1e2f3a4-0013-0013-0013-000000000013"), new Guid("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5698), new Guid("89caa753-acf6-4c18-9e49-f77bf14bef06"), false, new DateTime(2025, 4, 10, 16, 0, 0, 0, DateTimeKind.Utc), null, null, "Chỉ không đều do bulong lỏng, đã siết lại.", 2 },
                    { new Guid("d1e2f3a4-0015-0015-0015-000000000015"), new Guid("34343434-3434-3434-3434-343434343434"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5700), new Guid("7cd5dd20-cedb-4bc2-af91-45bb9b1d1282"), false, new DateTime(2025, 5, 15, 12, 10, 0, 0, DateTimeKind.Utc), null, null, "Chỉ dưới không kéo lên, kiểm tra cảm biến và thay mới.", 3 },
                    { new Guid("d1e2f3a4-0018-0018-0018-000000000018"), new Guid("33333333-3333-3333-3333-333333333333"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5703), new Guid("b919c148-c1d7-42c2-854d-2d95b501b93c"), false, new DateTime(2025, 5, 18, 10, 0, 0, 0, DateTimeKind.Utc), null, null, "Máy không chạy, kiểm tra phát hiện lỗi mô tơ.", 1 },
                    { new Guid("d1e2f3a4-0020-0020-0020-000000000020"), new Guid("eeeeeeee-eeee-eeee-eeee-eeeeeeeeeeee"), null, new DateTime(2025, 5, 21, 14, 45, 22, 430, DateTimeKind.Utc).AddTicks(5705), new Guid("c76d0016-fccf-434d-8867-c7adef5c6551"), false, new DateTime(2025, 5, 20, 15, 30, 0, 0, DateTimeKind.Utc), null, null, "Vải bị nhăn do điều chỉnh áp suất không đúng, đã điều chỉnh lại.", 2 }
                });

            migrationBuilder.InsertData(
                table: "DeviceWarranties",
                columns: new[] { "Id", "Cost", "DeviceId", "DocumentUrl", "Notes", "Provider", "SparePartCode", "SparePartName", "Status", "WarrantyCode", "WarrantyEndDate", "WarrantyReason", "WarrantyStartDate", "WarrantyType" },
                values: new object[,]
                {
                    { new Guid("d1e2f3a4-0001-4001-8001-000000000001"), 0m, new Guid("d1e2f3a4-0001-0001-0001-000000000001"), "https://example.com/docs/warranty_juki_ddl8700_01.pdf", "Bảo hành định kỳ cho máy mới, bao gồm kiểm tra cơ chế căng chỉ", "Juki Vietnam", null, null, "Completed", "WAR-JUKI-001", new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Máy mới", new DateTime(2020, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manufacturer" },
                    { new Guid("d1e2f3a4-0002-4002-8002-000000000002"), 500000m, new Guid("d1e2f3a4-0003-0003-0003-000000000003"), "https://example.com/docs/warranty_juki_ddl8700_03.pdf", "Gia hạn bảo hành sau sửa chữa động cơ bị cháy", "Juki Vietnam", "SP007", "Mô Tơ Máy May", "Pending", "WAR-JUKI-002", new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Sau sửa chữa", new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Extended" },
                    { new Guid("d1e2f3a4-0003-4003-8003-000000000003"), 0m, new Guid("d1e2f3a4-0004-0004-0004-000000000004"), "https://example.com/docs/warranty_juki_ddl8700_04.pdf", "Bảo hành sau khi thay thế động cơ do hỏng hóc", "Juki Vietnam", "SP007", "Mô Tơ Máy May", "Completed", "WAR-JUKI-003", new DateTime(2025, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Sau thay thế", new DateTime(2024, 11, 20, 0, 0, 0, 0, DateTimeKind.Utc), "Manufacturer" },
                    { new Guid("d1e2f3a4-0004-4004-8004-000000000004"), 0m, new Guid("d1e2f3a4-0007-0007-0007-000000000007"), "https://example.com/docs/warranty_juki_ddl8700_07.pdf", "Bảo hành máy mới, kiểm tra và sửa lỗi kẹt kim", "Juki Vietnam", "SP009", "Trụ Gắn Kim", "Completed", "WAR-JUKI-004", new DateTime(2022, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Máy mới", new DateTime(2020, 2, 15, 0, 0, 0, 0, DateTimeKind.Utc), "Manufacturer" },
                    { new Guid("d1e2f3a4-0005-4005-8005-000000000005"), 0m, new Guid("d1e2f3a4-0011-0011-0011-000000000011"), "https://example.com/docs/warranty_juki_ddl9000c_01.pdf", "Bảo hành máy mới, hiệu chỉnh hệ thống cắt chỉ tự động", "Juki Vietnam", "SP029", "Bộ Điều Khiển Điện Tử", "Completed", "WAR-JUKI-005", new DateTime(2024, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Máy mới", new DateTime(2022, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manufacturer" },
                    { new Guid("d1e2f3a4-0006-4006-8006-000000000006"), 500000m, new Guid("d1e2f3a4-0013-0013-0013-000000000013"), "https://example.com/docs/warranty_juki_ddl9000c_03.pdf", "Gia hạn bảo hành sau sửa chữa bộ phận cắt chỉ", "Juki Vietnam", null, null, "Pending", "WAR-JUKI-006", new DateTime(2026, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Sau sửa chữa", new DateTime(2025, 5, 12, 0, 0, 0, 0, DateTimeKind.Utc), "Extended" },
                    { new Guid("d1e2f3a4-0007-4007-8007-000000000007"), 1000000m, new Guid("d1e2f3a4-0015-0015-0015-000000000015"), "https://example.com/docs/warranty_brother_b957_01.pdf", "Bảo hành bên thứ ba sau thay thế bộ phận cấp liệu khác biệt", "Brother Vietnam", null, null, "Completed", "WAR-BROTHER-001", new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Sau thay thế", new DateTime(2024, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), "ThirdParty" },
                    { new Guid("d1e2f3a4-0008-4008-8008-000000000008"), 0m, new Guid("d1e2f3a4-0018-0018-0018-000000000018"), "https://example.com/docs/warranty_singer_4452_01.pdf", "Bảo hành máy mới, sửa chữa bo mạch nguồn bị lỗi", "Singer Vietnam", "SP029", "Bộ Điều Khiển Điện Tử", "Completed", "WAR-SINGER-001", new DateTime(2023, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Máy mới", new DateTime(2021, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), "Manufacturer" }
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
                    { new Guid("f1e2d3c4-0005-0005-0005-000000000005"), null, 5, new Guid("c1d2e3f4-0001-0001-0001-000000000001") },
                    { new Guid("f1e2d3c4-0006-0006-0006-000000000006"), new Guid("d1e2f3a4-0007-0007-0007-000000000007"), 1, new Guid("c1d2e3f4-0002-0002-0002-000000000002") },
                    { new Guid("f1e2d3c4-0007-0007-0007-000000000007"), new Guid("d1e2f3a4-0008-0008-0008-000000000008"), 2, new Guid("c1d2e3f4-0002-0002-0002-000000000002") },
                    { new Guid("f1e2d3c4-0008-0008-0008-000000000008"), new Guid("d1e2f3a4-0011-0011-0011-000000000011"), 3, new Guid("c1d2e3f4-0002-0002-0002-000000000002") },
                    { new Guid("f1e2d3c4-0009-0009-0009-000000000009"), null, 4, new Guid("c1d2e3f4-0002-0002-0002-000000000002") },
                    { new Guid("f1e2d3c4-0010-0010-0010-000000000010"), new Guid("d1e2f3a4-0009-0009-0009-000000000009"), 1, new Guid("c1d2e3f4-0003-0003-0003-000000000003") },
                    { new Guid("f1e2d3c4-0011-0011-0011-000000000011"), new Guid("d1e2f3a4-0010-0010-0010-000000000010"), 2, new Guid("c1d2e3f4-0003-0003-0003-000000000003") },
                    { new Guid("f1e2d3c4-0012-0012-0012-000000000012"), new Guid("d1e2f3a4-0012-0012-0012-000000000012"), 3, new Guid("c1d2e3f4-0003-0003-0003-000000000003") },
                    { new Guid("f1e2d3c4-0013-0013-0013-000000000013"), new Guid("d1e2f3a4-0014-0014-0014-000000000014"), 4, new Guid("c1d2e3f4-0003-0003-0003-000000000003") },
                    { new Guid("f1e2d3c4-0014-0014-0014-000000000014"), null, 1, new Guid("c1d2e3f4-0004-0004-0004-000000000004") },
                    { new Guid("f1e2d3c4-0015-0015-0015-000000000015"), null, 2, new Guid("c1d2e3f4-0004-0004-0004-000000000004") },
                    { new Guid("f1e2d3c4-0016-0016-0016-000000000016"), null, 1, new Guid("c1d2e3f4-0005-0005-0005-000000000005") },
                    { new Guid("f1e2d3c4-0017-0017-0017-000000000017"), null, 2, new Guid("c1d2e3f4-0005-0005-0005-000000000005") },
                    { new Guid("f1e2d3c4-0018-0018-0018-000000000018"), null, 3, new Guid("c1d2e3f4-0005-0005-0005-000000000005") },
                    { new Guid("f1e2d3c4-0019-0019-0019-000000000019"), new Guid("d1e2f3a4-0015-0015-0015-000000000015"), 1, new Guid("c1d2e3f4-0006-0006-0006-000000000006") },
                    { new Guid("f1e2d3c4-0020-0020-0020-000000000020"), new Guid("d1e2f3a4-0016-0016-0016-000000000016"), 2, new Guid("c1d2e3f4-0006-0006-0006-000000000006") },
                    { new Guid("f1e2d3c4-0021-0021-0021-000000000021"), new Guid("d1e2f3a4-0017-0017-0017-000000000017"), 3, new Guid("c1d2e3f4-0006-0006-0006-000000000006") },
                    { new Guid("f1e2d3c4-0022-0022-0022-000000000022"), null, 4, new Guid("c1d2e3f4-0006-0006-0006-000000000006") },
                    { new Guid("f1e2d3c4-0023-0023-0023-000000000023"), new Guid("d1e2f3a4-0018-0018-0018-000000000018"), 1, new Guid("c1d2e3f4-0007-0007-0007-000000000007") },
                    { new Guid("f1e2d3c4-0024-0024-0024-000000000024"), new Guid("d1e2f3a4-0019-0019-0019-000000000019"), 2, new Guid("c1d2e3f4-0007-0007-0007-000000000007") },
                    { new Guid("f1e2d3c4-0025-0025-0025-000000000025"), null, 3, new Guid("c1d2e3f4-0007-0007-0007-000000000007") },
                    { new Guid("f1e2d3c4-0026-0026-0026-000000000026"), null, 1, new Guid("c1d2e3f4-0008-0008-0008-000000000008") },
                    { new Guid("f1e2d3c4-0027-0027-0027-000000000027"), null, 2, new Guid("c1d2e3f4-0008-0008-0008-000000000008") },
                    { new Guid("f1e2d3c4-0028-0028-0028-000000000028"), null, 1, new Guid("c1d2e3f4-0009-0009-0009-000000000009") },
                    { new Guid("f1e2d3c4-0029-0029-0029-000000000029"), null, 2, new Guid("c1d2e3f4-0009-0009-0009-000000000009") },
                    { new Guid("f1e2d3c4-0030-0030-0030-000000000030"), null, 1, new Guid("c1d2e3f4-0010-0010-0010-000000000010") }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "Description", "DeviceId", "DueDate", "Priority", "ReportId", "RequestTitle", "RequestedById", "SerderId", "Status" },
                values: new object[,]
                {
                    { new Guid("a1e2f3a4-0001-0001-1001-000000000001"), "Máy ngừng hoạt động do đứt chỉ tại Dây chuyền May A, Vị trí 1, làm gián đoạn sản xuất vải mỏng.", new Guid("d1e2f3a4-0001-0001-0001-000000000001"), new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), "High", null, "Máy bị đứt chỉ Juki DDL-8700 Đơn vị 1", new Guid("32222222-2222-2222-2222-222222222222"), null, "Pending" },
                    { new Guid("a1f2e3d4-0002-0002-1002-000000000002"), "Động cơ ngừng hoạt động tại Dây chuyền May A, Vị trí 3. Quan trọng cho sản xuất vải cotton.", new Guid("d1e2f3a4-0004-0004-0004-000000000004"), new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "High", null, "Hỏng động cơ Juki DDL-8700 Đơn vị 4", new Guid("32222222-2222-2222-2222-222222222222"), null, "Approved" },
                    { new Guid("a1f2e3d4-0003-0003-1003-000000000003"), "Kim bị kẹt tại Dây chuyền May B, Vị trí 1. Ảnh hưởng đến sản xuất vải dày.", new Guid("d1e2f3a4-0007-0007-0007-000000000007"), new DateTime(2025, 5, 24, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", null, "Kẹt kim Juki DDL-8700 Đơn vị 7", new Guid("32222222-2222-2222-2222-222222222222"), null, "InProgress" },
                    { new Guid("a1f2e3d4-0004-0004-1004-000000000004"), "Máy cắt chỉ tự động bị lệch tại Dây chuyền May B, Vị trí 3. Gây ra mũi may không đều.", new Guid("d1e2f3a4-0011-0011-0011-000000000011"), new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", null, "Sự cố hiệu chỉnh Juki DDL-9000C Đơn vị 1", new Guid("32222222-2222-2222-2222-222222222222"), null, "Pending" },
                    { new Guid("a1f2e3d4-0005-0005-1005-000000000005"), "Bộ phận cấp liệu khác biệt bị trục trặc tại Khu vực Vắt Sổ, Vị trí 1. Ảnh hưởng đến hoàn thiện vải mỏng.", new Guid("d1e2f3a4-0015-0015-0015-000000000015"), new DateTime(2025, 5, 22, 0, 0, 0, 0, DateTimeKind.Utc), "High", null, "Sự cố máy vắt sổ Brother B957 Đơn vị 1", new Guid("32222222-2222-2222-2222-222222222222"), null, "Approved" },
                    { new Guid("a1f2e3d4-0006-0006-1006-000000000006"), "Nguồn điện bị gián đoạn tại Khu vực May Nặng, Vị trí 1. Ảnh hưởng đến sản xuất vải denim.", new Guid("d1e2f3a4-0018-0018-0018-000000000018"), new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), "High", null, "Sự cố nguồn điện Singer 4452 Đơn vị 1", new Guid("23333333-3333-3333-3333-333333333343"), null, "InProgress" },
                    { new Guid("a1f2e3d4-0007-0007-1007-000000000007"), "Cần bảo trì định kỳ cho Juki DDL-8700 tại Dây chuyền May C, Vị trí 1 để ngăn ngừa sự cố.", new Guid("d1e2f3a4-0009-0009-0009-000000000009"), new DateTime(2025, 5, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Low", null, "Kiểm tra bảo trì Juki DDL-8700 Đơn vị 9", new Guid("23333333-3333-3333-3333-333333333343"), null, "Pending" },
                    { new Guid("a1f2e3d4-0008-0008-1008-000000000008"), "Căng chỉ không đúng tại Dây chuyền May C, Vị trí 3. Ảnh hưởng đến chất lượng mũi may.", new Guid("d1e2f3a4-0012-0012-0012-000000000012"), new DateTime(2025, 5, 18, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", null, "Sự cố căng chỉ Juki DDL-9000C Đơn vị 2", new Guid("23333333-3333-3333-3333-333333333343"), null, "Completed" },
                    { new Guid("a1f2e3d4-0009-0009-1009-000000000009"), "Máy đang sửa chữa cần thay động cơ. Hiện không được gán vị trí.", new Guid("d1e2f3a4-0003-0003-0003-000000000003"), new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", null, "Yêu cầu sửa chữa Juki DDL-8700 Đơn vị 3", new Guid("23333333-3333-3333-3333-333333333343"), null, "InProgress" },
                    { new Guid("a1f2e3d4-0010-0010-1010-000000000010"), "Dây đai truyền động bị trượt tại Khu vực May Nặng, Vị trí 2. Ảnh hưởng đến sản xuất da.", new Guid("d1e2f3a4-0019-0019-0019-000000000019"), new DateTime(2025, 5, 23, 0, 0, 0, 0, DateTimeKind.Utc), "High", null, "Sự cố dây đai Singer 4452 Đơn vị 2", new Guid("23333333-3333-3333-3333-333333333343"), null, "Pending" },
                    { new Guid("a1f2e3d4-0011-0011-1011-000000000011"), "Máy vắt sổ tại Khu vực Vắt Sổ, Vị trí 2 cần bôi trơn để ngăn mòn.", new Guid("d1e2f3a4-0016-0016-0016-000000000016"), new DateTime(2025, 5, 27, 0, 0, 0, 0, DateTimeKind.Utc), "Low", null, "Cần bôi trơn Brother B957 Đơn vị 2", new Guid("23333333-3333-3333-3333-333333333344"), null, "Approved" },
                    { new Guid("a1f2e3d4-0012-0012-1012-000000000012"), "Hệ thống điều khiển số cần cập nhật phần mềm tại Dây chuyền May C, Vị trí 4 để tối ưu hiệu suất.", new Guid("d1e2f3a4-0014-0014-0014-000000000014"), new DateTime(2025, 5, 30, 0, 0, 0, 0, DateTimeKind.Utc), "Low", null, "Cập nhật phần mềm Juki DDL-9000C Đơn vị 4", new Guid("23333333-3333-3333-3333-333333333344"), null, "Pending" },
                    { new Guid("a1f2e3d4-0013-0013-1013-000000000013"), "Máy đang sửa chữa cần thay cơ chế chân vịt. Hiện không được gán vị trí.", new Guid("d1e2f3a4-0020-0020-0020-000000000020"), new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Utc), "Medium", null, "Yêu cầu sửa chữa Singer 4452 Đơn vị 3", new Guid("23333333-3333-3333-3333-333333333344"), null, "Approved" },
                    { new Guid("a1f2e3d4-0014-0014-1014-000000000014"), "Tiếng ồn lạ từ máy tại Dây chuyền May B, Vị trí 2. Có thể do vấn đề ổ bi.", new Guid("d1e2f3a4-0008-0008-0008-000000000008"), null, "Medium", null, "Tiếng ồn bất thường Juki DDL-8700 Đơn vị 8", new Guid("23333333-3333-3333-3333-333333333344"), null, "Denied" },
                    { new Guid("a1f2e3d4-0015-0015-1015-000000000015"), "Máy vắt sổ tại Khu vực Vắt Sổ, Vị trí 3 cần vệ sinh để loại bỏ bụi vải tích tụ.", new Guid("d1e2f3a4-0017-0017-0017-000000000017"), new DateTime(2025, 5, 17, 0, 0, 0, 0, DateTimeKind.Utc), "Low", null, "Yêu cầu vệ sinh Brother B957 Đơn vị 3", new Guid("23333333-3333-3333-3333-333333333344"), null, "Completed" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Areas_AreaName",
                table: "Areas",
                column: "AreaName",
                unique: true,
                filter: "[AreaName] IS NOT NULL");

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
