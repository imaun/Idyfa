using Idyfa.Core;
using Idyfa.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Idyfa.EntityFrameworkCore;

public class IdyfaUserPasswordRepository : IdyfaBaseRepository<UserUsedPassword, long>, IIdyfaUserPasswordRepository
{
    
    public IdyfaUserPasswordRepository(IdyfaDbContext db, ILogger<IdyfaUserPasswordRepository> logger) 
        : base(db, logger)
    {
    }
    
    public void Dispose()
    {
    }

    public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.Id);
    }

    public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.UserName);
    }

    public async Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken)
    {
        user.UserName = userName;
    }

    public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.NormalizedUserName);
    }

    public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    {
        user.NormalizedUserName = normalizedName;
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}