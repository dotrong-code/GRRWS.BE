using Google.Cloud.Storage.V1;
using GRRWS.Infrastructure.DB;
using GRRWS.Infrastructure.Implement.Repositories;
using GRRWS.Infrastructure.Interfaces;
using GRRWS.Infrastructure.Interfaces.IRepositories;
using Microsoft.Extensions.Logging;

namespace GRRWS.Infrastructure.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly GRRWSContext _context;
        private readonly ILogger<UnitOfWork> _logger; // Thêm logger
        private readonly ILoggerFactory _loggerFactory;
        public IUserRepository UserRepository { get; private set; }
        public IEmailTemplateRepository EmailTemplateRepository { get; private set; }
        public IFirebaseRepository FirebaseRepository { get; private set; }

        public IIssueRepository IssueRepository { get; private set; }
        public IErrorRepository ErrorRepository { get; private set; }
        public ITechnicalSymtomRepository TechnicalSymtomRepository { get; private set; }
        public IIssueTechnicalSymptomRepository IssueTechnicalSymptomRepository { get; private set; }
        public IDeviceRepository DeviceRepository { get; private set; }
        public IDeviceErrorHistoryRepository DeviceErrorHistoryRepository { get; private set; }
        public IDeviceIssueHistoryRepository DeviceIssueHistoryRepository { get; private set; }
        public IDeviceHistoryRepository DeviceHistoryRepository { get; private set; }
        public IPositionRepository PositionRepository { get; private set; }
        public IZoneRepository ZoneRepository { get; private set; }
        public IAreaRepository AreaRepository { get; private set; }
        public IDeviceWarrantyRepository DeviceWarrantyRepository { get; private set; }
        public IReportRepository ReportRepository { get; private set; }
        public IRequestRepository RequestRepository { get; private set; }

        public ITaskRepository TaskRepository { get; private set; }
        public IWarrantyDetailRepository WarrantyDetailRepository { get; private set; }

        public IErrorDetailRepository ErrorDetailRepository { get; }

        public IIssueErrorRepository IssueErrorRepository { get; private set; }
        public ISparepartRepository SparepartRepository { get; private set; }
        public IErrorFixStepRepository ErrorFixStepRepository { get; private set; }
        public IErrorSparepartRepository ErrorSparepartRepository { get; private set; }
        public IErrorGuidelineRepository ErrorGuidelineRepository { get; private set; }
        public IErrorFixProgressRepository ErrorFixProgressRepository { get; private set; }
        public ISparePartUsageRepository SparePartUsageRepository { get; private set; }
        public IRequestTakeSparePartUsageRepository RequestTakeSparePartUsageRepository { get; private set; }
        public IMachineSparepartRepository MachineSparepartRepository { get; private set; }

        public IMachineRepository MachineRepository { get; private set; }
        public ISupplierRepository SupplierRepository { get; private set; }
        public ITaskGroupRepository TaskGroupRepository { get; private set; }
        public IShiftRepository ShiftRepository { get; private set; }
        public IMechanicShiftRepository MechanicShiftRepository { get; private set; }

        public IPushTokenRepository PushTokenRepository { get; private set; }
        public INotificationRepository NotificationRepository { get; private set; }


        public IHOTDashboardRepository HOTDashboardRepository { get; private set; }

        public IMechanicPerformanceHistoryRepository MechanicPerformanceHistoryRepository { get; private set; }

        public IMechanicPerformanceRepository MechanicPerformanceRepository { get; private set; }
        public IWarrantyClaimRepository WarrantyClaimRepository { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public UnitOfWork(GRRWSContext context, StorageClient storageClient, ILogger<UnitOfWork> logger, IErrorDetailRepository errorDetailRepository, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = logger;
            _loggerFactory = loggerFactory;

            UserRepository = new UserRepository(_context);
            FirebaseRepository = new FirebaseRepository(storageClient);
            EmailTemplateRepository = new EmailTemplateRepository(_context);

            PushTokenRepository = new PushTokenRepository(_context, _loggerFactory.CreateLogger<PushTokenRepository>());
            NotificationRepository = new NotificationRepository(_context, _loggerFactory.CreateLogger<NotificationRepository>());

            IssueRepository = new IssueRepository(_context);
            ErrorRepository = new ErrorRepository(_context);
            TechnicalSymtomRepository = new TechnicalSymtomRepository(_context);
            IssueTechnicalSymptomRepository = new IssueTechnicalSymptomRepository(_context);
            DeviceRepository = new DeviceRepository(_context);
            PositionRepository = new PositionRepository(_context);
            ZoneRepository = new ZoneRepository(_context);
            AreaRepository = new AreaRepository(_context);
            DeviceWarrantyRepository = new DeviceWarrantyRepository(_context);

            ReportRepository = new ReportRepository(_context);
            RequestRepository = new RequestRepository(_context);
            TaskRepository = new TaskRepository(_context);
            DeviceErrorHistoryRepository = new DeviceErrorHistoryRepository(_context);
            DeviceIssueHistoryRepository = new DeviceIssueHistoryRepository(_context);
            DeviceHistoryRepository = new DeviceHistoryRepository(_context);
            WarrantyDetailRepository = new WarrantyDetailRepository(_context);
            ErrorDetailRepository = errorDetailRepository;
            IssueErrorRepository = new IssueErrorRepository(_context);
            SparepartRepository = new SparepartRepository(_context);
            ErrorFixStepRepository = new ErrorFixStepRepository(_context);
            ErrorSparepartRepository = new ErrorSparepartRepository(_context);
            ErrorGuidelineRepository = new ErrorGuidelineRepository(_context);
            ErrorFixProgressRepository = new ErrorFixProgressRepository(_context);
            SparePartUsageRepository = new SparePartUsageRepository(_context);
            RequestTakeSparePartUsageRepository = new RequestTakeSparePartUsageRepository(_context);
            MachineSparepartRepository = new MachineSparepartRepository(_context);
            MachineRepository = new MachineRepository(_context);
            SupplierRepository = new SupplierRepository(_context);
            TaskGroupRepository = new TaskGroupRepository(_context);
            ShiftRepository = new ShiftRepository(_context);
            MechanicShiftRepository = new MechanicShiftRepository(_context);
            HOTDashboardRepository = new HOTDashboardRepository(_context);
            MechanicPerformanceHistoryRepository = new MechanicPerformanceHistoryRepository(_context);
            MechanicPerformanceRepository = new MechanicPerformanceRepository(_context);
            WarrantyClaimRepository = new WarrantyClaimRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void ClearChangeTracker()
        {
            _context.ChangeTracker.Clear();
        }
        public void Dispose()
        {
            try
            {
                _context.Dispose();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error disposing context");
            }
        }
    }
}
