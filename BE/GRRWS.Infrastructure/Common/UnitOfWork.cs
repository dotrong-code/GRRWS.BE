using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public IDeviceRepository DeviceRepository { get; private set; }
        public IPositionRepository PositionRepository { get; private set; }
        public IZoneRepository ZoneRepository { get; private set; }
        public IAreaRepository AreaRepository { get; private set; }
        public IDeviceWarrantyRepository DeviceWarrantyRepository { get; private set; }
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
            DeviceRepository = new DeviceRepository(_context);
            PositionRepository = new PositionRepository(_context);
            ZoneRepository = new ZoneRepository(_context);
            AreaRepository = new AreaRepository(_context);
            DeviceWarrantyRepository = new DeviceWarrantyRepository(_context);

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
