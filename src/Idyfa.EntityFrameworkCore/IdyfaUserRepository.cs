using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore;

public class IdyfaUserRepository : IdyfaBaseRepository<User, string>, IIdyfaUserRepository
{
    public IdyfaUserRepository(IdyfaDbContext db) : base(db)
    {
    }

    public void Dispose()
    {
        throw new NotImplementedException();
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
        user.CheckArgumentIsNull(nameof(user));
        user.UserName = userName;
    }

    public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return await Task.FromResult(user.NormalizedUserName);
    }

    public async Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.UserName = normalizedName;
    }

    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        await _set.AddAsync(user, cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        _set.Update(user);
        _db.Entry(user).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        _set.Remove(user);
        _db.Entry(user).State = EntityState.Deleted;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<User> FindByNameAsync(
        string normalizedUserName, CancellationToken cancellationToken)
    {
        if (normalizedUserName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(normalizedUserName));
        
        var user = await _set.FirstOrDefaultAsync(
            u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        return user!;
    }

    public async Task<User> FindByUserNameAsync(
        string userName, CancellationToken cancellationToken = default)
    {
        if (userName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userName));

        var user = await _set.FirstOrDefaultAsync(
            u => u.UserName == userName, cancellationToken);
        return user!;
    }

    public async Task<UserRole> FindUserRoleAsync(
        string userId, string roleId, CancellationToken cancellationToken)
    {
        if (userId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userId));

        if (roleId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(roleId));

        var userRole = await _db.Set<UserRole>()
            .FirstOrDefaultAsync(u => u.UserId == userId && u.RoleId == roleId, cancellationToken)
            .ConfigureAwait(false);
        return userRole!;
    }

    public async Task<UserLogin> FindUserLoginAsync(
        string userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
    {
        if (userId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userId));

        var userLogin = await _db.Set<UserLogin>()
            .FirstOrDefaultAsync(l => l.UserId == userId && l.LoginProvider == loginProvider &&
                                      l.ProviderKey == providerKey, cancellationToken).ConfigureAwait(false);
        return userLogin!;
    }

    public async Task AddToRoleAsync(
        User user, string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        user.CheckArgumentIsNull(nameof(user));
        if (normalizedRoleName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(normalizedRoleName));

        var role = await _db.Set<Role>().FirstOrDefaultAsync(
            r => r.NormalizedName == normalizedRoleName, cancellationToken).ConfigureAwait(false);
        role.CheckReferenceIsNull(nameof(role));
        
        var userRole = UserRole.New(user.Id, role.Id);
        await _db.Set<UserRole>().AddAsync(userRole, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task RemoveFromRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsInRoleAsync(User user, string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<UserClaim>> GetClaimsAsync(User user, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddClaimsAsync(User user, IEnumerable<UserClaim> claims, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task ReplaceClaimAsync(User user, UserClaim claim, UserClaim newClaim, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveClaimsAsync(User user, IEnumerable<UserClaim> claims, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<User> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<User>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task AddUserTokenAsync(UserToken token)
    {
        throw new NotImplementedException();
    }

    public Task RemoveUserTokenAsync(UserToken token)
    {
        throw new NotImplementedException();
    }
}