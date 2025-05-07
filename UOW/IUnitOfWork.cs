using TestToken.Repositories.Interfaces;

namespace TestToken.UOW
{
    public interface IUnitOfWork : IDisposable
    {
        public IUserRepository Users { get; }
        public IAccountRepository Customers { get; }


        public IEmployeeRepository Employees { get;  }


        public ITokenService TokenService { get; }
       


        Task<int> SaveCompleted();

    }
}
