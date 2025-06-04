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
        public IUserRepository UserRepository { get; private set; }
        public IEmailTemplateRepository EmailTemplateRepository { get; private set; }
        public IFirebaseRepository FirebaseRepository { get; private set; }

        public IIssueRepository IssueRepository { get; private set; }
        public IErrorRepository ErrorRepository { get; private set; }
        public ITechnicalSymtomRepository TechnicalSymtomRepository { get; private set; }
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
        public IIssueErrorRepository IssueErrorRepository { get; private set; }
        public ISparepartRepository SparepartRepository { get; private set; }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
        public UnitOfWork(GRRWSContext context, StorageClient storageClient, ILogger<UnitOfWork> logger)
        {
            _context = context;
            _logger = logger;
            UserRepository = new UserRepository(_context);
            FirebaseRepository = new FirebaseRepository(storageClient);
            EmailTemplateRepository = new EmailTemplateRepository(_context);

            IssueRepository = new IssueRepository(_context);
            ErrorRepository = new ErrorRepository(_context);
            TechnicalSymtomRepository = new TechnicalSymtomRepository(_context);
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
            IssueErrorRepository = new IssueErrorRepository(_context);
            SparepartRepository = new SparepartRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
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
