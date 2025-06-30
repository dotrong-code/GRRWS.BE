using FirebaseAdmin;
using FluentValidation;
using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;
using GRRWS.Application.Common;
using GRRWS.Application.Common.Validator.AreaVali;
using GRRWS.Application.Common.Validator.DeviceVali;
using GRRWS.Application.Common.Validator.DeviceWarranty;
using GRRWS.Application.Common.Validator.Position;
using GRRWS.Application.Common.Validator.Task;
using GRRWS.Application.Common.Validator.UserVali;
using GRRWS.Application.Common.Validator.Zone;
using GRRWS.Application.Implement.Service;
using GRRWS.Application.Interface.IService;
using GRRWS.Application.Interfaces;
using GRRWS.Domain.Entities;
using GRRWS.Infrastructure.Common;
using GRRWS.Infrastructure.DTOs.Area;
using GRRWS.Infrastructure.DTOs.Device;
using GRRWS.Infrastructure.DTOs.DeviceWarranty;
using GRRWS.Infrastructure.DTOs.Position;
using GRRWS.Infrastructure.DTOs.Task;
using GRRWS.Infrastructure.DTOs.Task.Get;
using GRRWS.Infrastructure.DTOs.User.Create;
using GRRWS.Infrastructure.DTOs.User.Login;
using GRRWS.Infrastructure.DTOs.User.Register;
using GRRWS.Infrastructure.DTOs.User.Update;
using GRRWS.Infrastructure.DTOs.Zone;
using GRRWS.Infrastructure.Implement.Repositories;
using GRRWS.Infrastructure.Implement.Repositories.Generic;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using GRRWS.Infrastructure.Interfaces.IRepositories.IGeneric;

namespace GRRWS.Host.Starup
{
    public static class DependencyInjectionSetup
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            var credentialPath = Path.Combine(Directory.GetCurrentDirectory(), "Keys",
                "koiveterinaryservicecent-925db-firebase-adminsdk-vus2r-2b14e43049.json");

            try
            {
                // Initialize Firebase with the credentials from the JSON file
                FirebaseApp.Create(new AppOptions()
                {
                    Credential = GoogleCredential.FromFile(credentialPath)
                });
            }
            catch (Exception ex)
            {
                // Log or handle the exception as necessary
                throw new Exception("Failed to initialize Firebase.", ex);
            }
            services.AddSingleton(StorageClient.Create(GoogleCredential.FromFile(credentialPath)));

            #region Validator
            services.AddTransient<IValidator<LoginRequest>, LoginValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterValidator>();
            services.AddTransient<IValidator<RegisterRequest>, RegisterValidator>();
            services.AddTransient<IValidator<CreateUserRequest>, CreateUserValidator>();
            services.AddTransient<IValidator<UpdateUserRequest>, UpdateUserValidator>();
            services.AddTransient<IValidator<CreateDeviceRequest>, CreateDeviceValidator>();
            services.AddTransient<IValidator<UpdateDeviceRequest>, UpdateDeviceValidator>();
            services.AddTransient<IValidator<CreateAreaRequest>, CreateAreaValidator>();
            services.AddTransient<IValidator<UpdateAreaRequest>, UpdateAreaValidator>();
            services.AddTransient<IValidator<CreatePositionRequest>, CreatePositionRequestValidator>();
            services.AddTransient<IValidator<UpdatePositionRequest>, UpdatePositionRequestValidator>();
            services.AddTransient<IValidator<CreateZoneRequest>, CreateZoneRequestValidator>();
            services.AddTransient<IValidator<UpdateZoneRequest>, UpdateZoneRequestValidator>();
            services.AddTransient<IValidator<CreateDeviceWarrantyRequest>, CreateDeviceWarrantyRequestValidator>();
            services.AddTransient<IValidator<UpdateDeviceWarrantyRequest>, UpdateDeviceWarrantyRequestValidator>();

            // In Program.cs or Startup.cs
            services.AddTransient<IValidator<GetAllSingleTasksRequest>, GetAllSingleTasksValidator>();
            services.AddTransient<IValidator<GetTasksByRequestIdRequest>, GetTasksByRequestIdValidator>();
            services.AddTransient<IValidator<StartTaskRequest>, StartTaskRequestValidator>();
            services.AddTransient<IValidator<CreateTaskReportRequest>, CreateTaskReportRequestValidator>();


            #endregion

            #region Common
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<CheckIsExist>();
            #endregion

            #region Service
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IEmailTemplateService, EmailTemplateService>();
            services.AddTransient<IFirebaseService, FirebaseService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IReportService, ReportService>();
            services.AddTransient<IRequestService, RequestService>();
            services.AddTransient<IIssueService, IssueService>();
            services.AddTransient<IErrorService, ErrorService>();
            services.AddTransient<ITechnicalSymtomService, TechnicalSymtomService>();
            services.AddTransient<IDeviceService, DeviceService>();
            services.AddTransient<IDeviceErrorHistoryService, DeviceErrorHistoryService>();
            services.AddTransient<IDeviceIssueHistoryService, DeviceIssueHistoryService>();
            services.AddTransient<IDeviceHistoryService, DeviceHistoryService>();
            services.AddTransient<IDeviceWarrantyService, DeviceWarrantyService>();
            services.AddTransient<IZoneService, ZoneService>();
            services.AddTransient<IAreaService, AreaService>();
            services.AddTransient<IPositionService, PositionService>();
            services.AddTransient<ITaskService, TaskService>();
            services.AddTransient<IWarrantyDetailService, WarrantyDetailService>();
            services.AddTransient<IErrorDetailService, ErrorDetailService>();
            services.AddTransient<IErrorFixProgressService, ErrorFixProgressService>();
            services.AddTransient<IErrorGuidelineService, ErrorGuidelineService>();
            services.AddTransient<ISparePartUsageService, SparePartUsageService>();
            services.AddTransient<ISparepartService, SparepartService>();
            services.AddTransient<ITaskGroupService, TaskGroupService>();

            services.AddTransient<INotificationService, NotificationService>();
            services.AddTransient<IExpoPushService, ExpoPushService>();
            services.AddTransient<IImportService, ImportService>();

            services.AddTransient<IImportService, ImportService>();
            services.AddTransient<IMachineService, MachineService>();
            services.AddTransient<IHOTDashboardService, HOTDashboardService>();
            services.AddTransient<IMechanicShiftService, MechanicShiftService>();
            services.AddTransient<IWarrantyClaimService, WarrantyClaimService>();
            services.AddTransient<IRequestMachineReplacementService, RequestMachineReplacementService>();



            #endregion

            #region Repositories
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IFirebaseRepository, FirebaseRepository>();
            services.AddTransient<IEmailTemplateRepository, EmailTemplateRepository>();
            services.AddTransient<IReportRepository, ReportRepository>();
            services.AddTransient<IRequestRepository, RequestRepository>();

            services.AddTransient<IIssueRepository, IssueRepository>();
            services.AddTransient<IErrorRepository, ErrorRepository>();
            services.AddTransient<ITechnicalSymtomRepository, TechnicalSymtomRepository>();
            services.AddTransient<IIssueTechnicalSymptomRepository, IssueTechnicalSymptomRepository>();
            services.AddTransient<IDeviceRepository, DeviceRepository>();
            services.AddTransient<IDeviceErrorHistoryRepository, DeviceErrorHistoryRepository>();
            services.AddTransient<IDeviceIssueHistoryRepository, DeviceIssueHistoryRepository>();
            services.AddTransient<IDeviceHistoryRepository, DeviceHistoryRepository>();
            services.AddTransient<IDeviceWarrantyRepository, DeviceWarrantyRepository>();
            services.AddTransient<IZoneRepository, ZoneRepository>();
            services.AddTransient<IAreaRepository, AreaRepository>();
            services.AddTransient<IPositionRepository, PositionRepository>();

            services.AddTransient<ITaskRepository, TaskRepository>();
            services.AddTransient<IWarrantyDetailRepository, WarrantyDetailRepository>();
            services.AddTransient<IIssueErrorRepository, IssueErrorRepository>();
            services.AddTransient<IErrorDetailRepository, ErrorDetailRepository>();
            services.AddTransient<IErrorSparepartRepository, ErrorSparepartRepository>();
            services.AddTransient<IErrorFixStepRepository, ErrorFixStepRepository>();
            services.AddTransient<IErrorFixProgressRepository, ErrorFixProgressRepository>();
            services.AddTransient<IErrorGuidelineRepository, ErrorGuidelineRepository>();
            services.AddTransient<ISparePartUsageRepository, SparePartUsageRepository>();
            services.AddTransient<ISparepartRepository, SparepartRepository>();
            services.AddTransient<IMachineSparepartRepository, MachineSparepartRepository>();
            services.AddTransient<IMachineRepository, MachineRepository>();
            services.AddTransient<ISupplierRepository, SupplierRepository>();
            services.AddTransient<IRequestTakeSparePartUsageRepository, RequestTakeSparePartUsageRepository>();
            services.AddTransient<ITaskGroupRepository, TaskGroupRepository>();
            services.AddTransient<IShiftRepository, ShiftRepository>();
            services.AddTransient<IMechanicShiftRepository, MechanicShiftRepository>();

            services.AddTransient<IPushTokenRepository, PushTokenRepository>();
            services.AddTransient<INotificationRepository, NotificationRepository>();


            services.AddTransient<IHOTDashboardRepository, HOTDashboardRepository>();
            services.AddTransient<IRequestMachineReplacementRepository, RequestMachineReplacementRepository>();
            #endregion

            #region GenericRepositories

            services.AddTransient<IGenericRepository<User>, GenericRepository<User>>();
            services.AddTransient<IGenericRepository<Device>, GenericRepository<Device>>();
            services.AddTransient<IGenericRepository<DeviceErrorHistory>, GenericRepository<DeviceErrorHistory>>();
            services.AddTransient<IGenericRepository<DeviceIssueHistory>, GenericRepository<DeviceIssueHistory>>();
            services.AddTransient<IGenericRepository<DeviceHistory>, GenericRepository<DeviceHistory>>();
            services.AddTransient<IGenericRepository<DeviceWarranty>, GenericRepository<DeviceWarranty>>();
            services.AddTransient<IGenericRepository<Area>, GenericRepository<Area>>();
            services.AddTransient<IGenericRepository<Zone>, GenericRepository<Zone>>();
            services.AddTransient<IGenericRepository<Position>, GenericRepository<Position>>();
            services.AddTransient<IGenericRepository<Request>, GenericRepository<Request>>();
            services.AddTransient<IGenericRepository<Report>, GenericRepository<Report>>();
            services.AddTransient<IGenericRepository<WarrantyDetail>, GenericRepository<WarrantyDetail>>();
            services.AddTransient<IGenericRepository<IssueError>, GenericRepository<IssueError>>();
            services.AddTransient<IGenericRepository<PushToken>, GenericRepository<PushToken>>();
            services.AddTransient<IGenericRepository<Notification>, GenericRepository<Notification>>();
            #endregion



            return services; // Ensure the IServiceCollection is returned
        }
    }
}
