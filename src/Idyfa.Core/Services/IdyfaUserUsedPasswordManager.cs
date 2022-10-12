using Idyfa.Core.Contracts;
using Idyfa.Core.Contracts.Repository;

namespace Idyfa.Core.Services;

public class IdyfaUserUsedPasswordManager : IIdyfaUserUsedPasswordManager
{
    private readonly IIdyfaUserUsedPasswordRepository _repository;
    
    public IdyfaUserUsedPasswordManager(
        IIdyfaUserUsedPasswordRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }
    
    public Task<bool> IsPasswordUsedBeforeAsync(User user, string password)
    {
        throw new NotImplementedException();
    }

    public Task AddToUsedPasswordsAsync(User user, UserUsedPassword password)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsUserPasswordTooOldAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<DateTime?> GetLastUserChangePasswordDateAsync(User user)
    {
        throw new NotImplementedException();
    }
}