using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GRRWS.Infrastructure.Interfaces.IRepositories;

namespace GRRWS.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }
        IFirebaseRepository FirebaseRepository { get; }
        IDeviceRepository DeviceRepository { get; }
        IPositionRepository PositionRepository { get; }
        IZoneRepository ZoneRepository { get; }
        IAreaRepository AreaRepository { get; } 
        IDeviceWarrantyRepository DeviceWarrantyRepository { get; }
        IReportRepository ReportRepository { get; }
        IRequestRepository RequestRepository { get; }
        int Complete();
        Task<int> SaveChangesAsync();
    }
}
