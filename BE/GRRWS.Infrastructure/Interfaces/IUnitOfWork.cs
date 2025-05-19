using GRRWS.Infrastructure.Interfaces.IRepositories;

namespace GRRWS.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository UserRepository { get; }
        IEmailTemplateRepository EmailTemplateRepository { get; }
        IFirebaseRepository FirebaseRepository { get; }
        IIssueRepository IssueRepository { get; }
        int Complete();
        Task<int> SaveChangesAsync();
    }
}
