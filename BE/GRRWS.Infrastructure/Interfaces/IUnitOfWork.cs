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
        ITechnicalSymtomRepository TechnicalSymtomRepository { get; }
        IIssueTechnicalSymptomRepository IssueTechnicalSymptomRepository { get; }
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
        IWarrantyDetailRepository WarrantyDetailRepository { get; }
        IIssueErrorRepository IssueErrorRepository { get; }
        IErrorFixStepRepository ErrorFixStepRepository { get; }
        IErrorSparepartRepository ErrorSparepartRepository { get; }
        ISparepartRepository SparepartRepository { get; }
        IErrorGuidelineRepository ErrorGuidelineRepository { get; }
        IErrorFixProgressRepository ErrorFixProgressRepository { get; }
        ISparePartUsageRepository SparePartUsageRepository { get; }
        IRequestTakeSparePartUsageRepository RequestTakeSparePartUsageRepository { get; }
        IMachineSparepartRepository MachineSparepartRepository { get; }
        int Complete();
        Task<int> SaveChangesAsync();
    }
}
