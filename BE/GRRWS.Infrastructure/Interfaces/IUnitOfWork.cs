using GRRWS.Infrastructure.Interfaces.IRepositories;

namespace GRRWS.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }
        IFirebaseRepository FirebaseRepository { get; }

        IIssueRepository IssueRepository { get; }

        IDeviceRepository DeviceRepository { get; }
        IPositionRepository PositionRepository { get; }
        IZoneRepository ZoneRepository { get; }
        IAreaRepository AreaRepository { get; }
        IDeviceWarrantyRepository DeviceWarrantyRepository { get; }

        int Complete();
        Task<int> SaveChangesAsync();
    }
}
