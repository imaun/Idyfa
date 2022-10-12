using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore;

/// <inheritdoc />
public class IdyfaUserRepository : IdyfaBaseRepository<User, string>, IIdyfaUserRepository
{
    public IdyfaUserRepository(IdyfaDbContext db) : base(db)
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
        await _set.AddAsync(user, cancellationToken).ConfigureAwait(false);
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
    public async Task AddUserTokenAsync(UserToken token)
    {
        token.CheckArgumentIsNull(nameof(token));
        var user = await _set.FindAsync(token.UserId).ConfigureAwait(false);
        user.CheckReferenceIsNull(nameof(user));

        await _db.Set<UserToken>().AddAsync(token);
        await _db.SaveChangesAsync();
    }

    public Task RemoveUserTokenAsync(UserToken token)
    {
        throw new NotImplementedException();
    }
}