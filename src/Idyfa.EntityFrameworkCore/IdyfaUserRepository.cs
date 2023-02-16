using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Idyfa.EntityFrameworkCore;

/// <inheritdoc />
public class IdyfaUserRepository : 
    IdyfaBaseRepository<User, string>, IIdyfaUserRepository, IUserPasswordStore<User>
{
    public IdyfaUserRepository(IdyfaDbContext db, ILogger<IdyfaUserRepository> logger)
        : base(db, logger)
    {
    }

    public void Dispose()
    {
    }

    /// <inheritdoc />
    public async Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.Id);
    }

    /// <inheritdoc />
    public async Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken)
    {
        return await Task.FromResult(user.UserName);
    }

    /// <inheritdoc />
    public async Task SetUserNameAsync(
        User user, string userName, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.UserName = userName;
    }

    /// <inheritdoc />
    public async Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return await Task.FromResult(user.NormalizedUserName);
    }

    /// <inheritdoc />
    public async Task SetNormalizedUserNameAsync(
        User user, string normalizedName, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.NormalizedUserName = normalizedName;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        
        _set.Add(user);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        
        _set.Update(user);
        _db.Entry(user).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return IdentityResult.Success;
    }

    /// <inheritdoc />
    public async Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        
        _set.Remove(user);
        _db.Entry(user).State = EntityState.Deleted;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        
        return IdentityResult.Success;
    }

    /// <inheritdoc />
    public async Task<User> FindByNameAsync(
        string normalizedUserName, CancellationToken cancellationToken)
    {
        if (normalizedUserName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(normalizedUserName));
        
        var user = await _set.FirstOrDefaultAsync(
            u => u.NormalizedUserName == normalizedUserName, cancellationToken);
        return user!;
    }

    public async Task<bool> ExistByUserNameAsync(string userId, string userName)
    {
        return await _set.AnyAsync(_ => _.Id != userId && _.UserName.ToUpper() == userName.ToUpper());
    }

    /// <inheritdoc />
    public async Task<User> FindByUserNameAsync(
        string userName, CancellationToken cancellationToken = default)
    {
        if (userName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userName));

        var user = await _set.FirstOrDefaultAsync(
            u => u.UserName == userName, cancellationToken);
        return user!;
    }

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
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

    /// <inheritdoc />
    public async Task RemoveFromRoleAsync(
        User user, string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        user.CheckArgumentIsNull(nameof(user));
        if (normalizedRoleName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(normalizedRoleName));
        
        var role = await _db.Set<Role>().FirstOrDefaultAsync(
            r => r.NormalizedName == normalizedRoleName, cancellationToken).ConfigureAwait(false);
        role.CheckReferenceIsNull(nameof(role));

        var userRole = await _db.Set<UserRole>().FirstOrDefaultAsync(
            r => r.UserId == user.Id && r.RoleId == role.Id, cancellationToken).ConfigureAwait(false);
        if (userRole is not null)
        {
            _db.Set<UserRole>().Remove(userRole);
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
    }

     async Task<IList<string>> IUserRoleStore<User>.GetRolesAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        var roleNames = new List<string>();
        //TODO : read from Roles form Navigation property.

        return (await GetRolesAsync(user, cancellationToken)).ToList();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<string>> GetRolesAsync(
        User user, CancellationToken cancellationToken = default)
    {
        user.CheckArgumentIsNull(nameof(user));
        var userRoles = await _db.Set<UserRole>()
            .Where(r => r.UserId == user.Id)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        var result = new List<string>();
        foreach (var item in userRoles)
        {
            var role = await _db.Set<Role>()
                .FirstOrDefaultAsync(r => r.Id == item.RoleId, cancellationToken)
                .ConfigureAwait(false);
            result.Add(role.NormalizedName);
        }

        return await Task.FromResult(result);
    }

    /// <inheritdoc />
    public async Task<bool> IsInRoleAsync(
        User user, string normalizedRoleName, CancellationToken cancellationToken = default)
    {
        user.CheckArgumentIsNull(nameof(user));
        if (normalizedRoleName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(normalizedRoleName));

        var role = await _db.Set<Role>().FirstOrDefaultAsync(
            r => r.NormalizedName == normalizedRoleName, cancellationToken).ConfigureAwait(false);
        role.CheckReferenceIsNull(nameof(role));

        return await _db.Set<UserRole>()
            .AnyAsync(x => x.RoleId == role.Id, cancellationToken).ConfigureAwait(false);
    }

     async Task<IList<User>> IUserRoleStore<User>.GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        return (await GetUsersInRoleAsync(roleName, cancellationToken)).ToList();
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<UserClaim>> GetClaimsAsync(
        User user, CancellationToken cancellationToken = default)
    {
        user.CheckArgumentIsNull(nameof(user));
        var result = await _db.Set<UserClaim>()
            .Where(c => c.UserId == user.Id)
            .ToListAsync(cancellationToken).ConfigureAwait(false);
        return result;
    }

    /// <inheritdoc />
    public async Task AddClaimsAsync(
        User user, IEnumerable<UserClaim> claims, CancellationToken cancellationToken = default)
    {
        user.CheckArgumentIsNull(nameof(user));
        if(claims is null || !claims.Any())
            throw new ArgumentNullException(nameof(claims));

        foreach (var claim in claims)
        {
            _db.Set<UserClaim>().Add(claim);
        }

        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task ReplaceClaimAsync(
        User user, UserClaim claim, UserClaim newClaim, CancellationToken cancellationToken = default)
    {
        user.CheckArgumentIsNull(nameof(user));
        claim.CheckArgumentIsNull(nameof(claim));
        newClaim.CheckArgumentIsNull(nameof(newClaim));

        var userClaim = await _db.Set<UserClaim>()
            .FirstOrDefaultAsync(
                c => c.UserId == user.Id && c.ClaimType == newClaim.ClaimType, cancellationToken
            ).ConfigureAwait(false);
        userClaim.CheckReferenceIsNull(nameof(userClaim));

        userClaim.ClaimValue = newClaim.ClaimValue;
        _db.Entry(userClaim).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    /// <inheritdoc />
    public async Task RemoveClaimsAsync(
        User user, IEnumerable<UserClaim> claims, CancellationToken cancellationToken = default)
    {
        user.CheckArgumentIsNull(nameof(user));
        if (claims is null || !claims.Any())
            throw new ArgumentNullException(nameof(claims));

        foreach (var claim in claims)
        {
            var userClaim = await _db.Set<UserClaim>()
                .FirstOrDefaultAsync(c => c.UserId == user.Id && c.ClaimType == claim.ClaimType, cancellationToken)
                .ConfigureAwait(false);
            userClaim.CheckReferenceIsNull(nameof(userClaim));
            _db.Set<UserClaim>().Remove(userClaim);
        }

        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public Task SetEmailAsync(User user, string email, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.Email = email;
        return Task.CompletedTask;
    }

    public Task<string> GetEmailAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return Task.FromResult(user.Email);
    }

    public Task<bool> GetEmailConfirmedAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return Task.FromResult(user.EmailConfirmed);
    }

    public Task SetEmailConfirmedAsync(User user, bool confirmed, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.EmailConfirmed = confirmed;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<User> FindByEmailAsync(
        string email, CancellationToken cancellationToken = default)
    {
        if (email.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(email));

        var user = await _set.FirstOrDefaultAsync(u => u.NormalizedEmail == email.ToUpper(), cancellationToken)
            .ConfigureAwait(false);
        return user!;
    }

    public Task<string> GetNormalizedEmailAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return Task.FromResult(user.NormalizedEmail);
    }

    public Task SetNormalizedEmailAsync(User user, string normalizedEmail, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.NormalizedEmail = normalizedEmail;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyCollection<User>> GetUsersInRoleAsync(
        string roleName, CancellationToken cancellationToken = default)
    {
        if (roleName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(roleName));

        var role = await _db.Set<Role>().FirstOrDefaultAsync(
            r => r.NormalizedName == roleName, cancellationToken).ConfigureAwait(false);
        role.CheckReferenceIsNull(nameof(role));

        var userRoles = await _db.Set<UserRole>().Where(r => r.RoleId == role.Id)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        var result = new List<User>();
        foreach (var userRole in userRoles)
        {
            result.Add((await _set.FindAsync(
                userRole.UserId, cancellationToken).ConfigureAwait(false))!);
        }

        return await Task.FromResult(result);
    }

    /// <inheritdoc />
    public async Task AddUserTokenAsync(
        User user, UserToken token, CancellationToken cancellationToken = default)
    {
        token.CheckArgumentIsNull(nameof(token));
        user.CheckArgumentIsNull(nameof(user));
        
        await _db.Set<UserToken>().AddAsync(
            UserToken.New()
                .WithUserId(user.Id)
                .WithName(token.Name)
                .WithLoginProvider(token.LoginProvider)
                .WithValue(token.Value), cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc />
    public async Task RemoveUserTokenAsync(
        UserToken token, CancellationToken cancellationToken = default)
    {
        token.CheckArgumentIsNull(nameof(token));
        _db.Set<UserToken>().Remove(token);
        _db.Entry(token).State = EntityState.Deleted;
        await _db.SaveChangesAsync(cancellationToken);
    }

    public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken)
    {
        user.PasswordHash = passwordHash;
        return Task.FromResult(user);
    }

    public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash);
    }

    public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken)
    {
        return Task.FromResult(user.PasswordHash.IsNotNullOrEmpty());
    }
    
    #region UserRoleStore
    
    public Task<string> GetUserIdAsync(UserRole user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return Task.FromResult(user.UserId);
    }

    public async Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        var user = await _set.FindAsync(userId).ConfigureAwait(false);
        user.CheckArgumentIsNull(nameof(user));

        return user;
    }


    public async Task AddToRoleAsync(UserRole user, string roleName, CancellationToken cancellationToken)
    {
        user.WithRoleId(user.RoleId);
    }

    public Task RemoveFromRoleAsync(UserRole user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<IList<string>> GetRolesAsync(UserRole user, CancellationToken cancellationToken)
    {
        // return (await GetRolesAsync(user, cancellationToken)).ToList();
        //TODO :
        return new List<string>();
    }

    public async Task<bool> IsInRoleAsync(UserRole user, string roleName, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        //TODO : must implement properly with navigation properties.
        return true;
    }
    
    #endregion

    #region UserLockoutStore
    public async Task<DateTimeOffset?> GetLockoutEndDateAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return await Task.FromResult(user.LockoutEnd);
    }

    public async Task SetLockoutEndDateAsync(
        User user, DateTimeOffset? lockoutEnd, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.LockoutEnd = lockoutEnd;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<int> IncrementAccessFailedCountAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.AccessFailedCount++;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return await Task.FromResult(user.AccessFailedCount);
    }

    public async Task ResetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.AccessFailedCount = 0;
        user.LockoutEnd = null;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<int> GetAccessFailedCountAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return await Task.FromResult(user.AccessFailedCount);
    }

    public async Task<bool> GetLockoutEnabledAsync(User user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return await Task.FromResult(user.LockoutEnabled);
    }

    public async Task SetLockoutEnabledAsync(User user, bool enabled, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        user.LockoutEnabled = enabled;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
    #endregion
}