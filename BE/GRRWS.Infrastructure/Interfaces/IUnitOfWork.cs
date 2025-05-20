using GRRWS.Infrastructure.Interfaces.IRepositories;

namespace GRRWS.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }
        IFirebaseRepository FirebaseRepository { get; }

        IIssueRepository IssueRepository { get; }
        IErrorRepository ErrorRepository { get; }

        IDeviceRepository DeviceRepository { get; }
        IDeviceIssueHistoryRepository DeviceIssueHistoryRepository { get; }
        IDeviceErrorHistoryRepository DeviceErrorHistoryRepository { get; }
        IDeviceHistoryRepository DeviceHistoryRepository { get; }
        IPositionRepository PositionRepository { get; }
        IZoneRepository ZoneRepository { get; }
        IAreaRepository AreaRepository { get; }
        IDeviceWarrantyRepository DeviceWarrantyRepository { get; }
        IReportRepository ReportRepository { get; }
        IRequestRepository RequestRepository { get; }
        ITaskRepository TaskRepository { get; }


        int Complete();
        Task<int> SaveChangesAsync();
    }
}
