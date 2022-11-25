using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Idyfa.EntityFrameworkCore;


/// <inheritdoc />
public class IdyfaRoleRepository : IdyfaBaseRepository<Role, string>, IIdyfaRoleRepository
{
    private readonly ILogger<IdyfaRoleRepository> _logger;
    
    public IdyfaRoleRepository(IdyfaDbContext db, ILogger<IdyfaRoleRepository> logger) : base(db, logger)
    {
    }

    public void Dispose()
    {
    }

    public async Task<IdentityResult> CreateAsync(Role role, CancellationToken cancellationToken)
    {
        role.CheckArgumentIsNull(nameof(role));
        await _set.AddAsync(role, cancellationToken).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role, CancellationToken cancellationToken)
    {
        role.CheckArgumentIsNull(nameof(role));
        _db.Entry(role).State = EntityState.Modified;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        role.CheckArgumentIsNull(nameof(role));
        _set.Remove(role);
        _db.Entry(role).State = EntityState.Deleted;
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        role.CheckArgumentIsNull(nameof(role));
        return await Task.FromResult(role.Id);
    }

    public async Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        role.CheckArgumentIsNull(nameof(role));
        return await Task.FromResult(role.Name);
    }

    public async Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
    {
        role.CheckArgumentIsNull(nameof(role));
        role.Name = roleName;
        role.NormalizedName = roleName.ToUpper();
        _set.Update(role);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        role.CheckArgumentIsNull(nameof(role));
        return await Task.FromResult(role.NormalizedName);
    }

    public async Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
    {
        role.CheckArgumentIsNull(nameof(role));
        role.NormalizedName = normalizedName;
        _set.Update(role);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task AddClaimAsync(RoleClaim claim)
    {
        claim.CheckArgumentIsNull(nameof(claim));
        await _db.Set<RoleClaim>().AddAsync(claim).ConfigureAwait(false);
        await _db.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task<Role> GetByNameAsync(string roleName)
    {
        if (roleName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(roleName));

        var role = await _set.FirstOrDefaultAsync(r => r.Name == roleName).ConfigureAwait(false);
        return role!;
    }

    public async Task<IEnumerable<RoleClaim>> GetClaimsAsync(
        string roleId, CancellationToken cancellationToken = default)
    {
        if (roleId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(roleId));

        var role = await _set.FindAsync(roleId, cancellationToken).ConfigureAwait(false);
        role.CheckReferenceIsNull(nameof(role));

        var result = await _db.Set<RoleClaim>().Where(c => c.RoleId == roleId)
            .ToListAsync(cancellationToken)
            .ConfigureAwait(false);
        return await Task.FromResult(result);
    }

    public async Task<IEnumerable<RoleClaim>> GetClaimsAsync(string roleId)
    {
        return await GetClaimsAsync(roleId, CancellationToken.None);
    }

    public async Task AddClaimAsync(
        Role role, RoleClaim claim, CancellationToken cancellationToken = default)
    {
        role.CheckArgumentIsNull(nameof(role));
        claim.CheckArgumentIsNull(nameof(claim));

        await _db.Set<RoleClaim>().AddAsync(claim, cancellationToken).ConfigureAwait(false);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<IReadOnlyCollection<RoleClaim>> GetClaimsAsync(
        Role role, CancellationToken cancellationToken = default)
    {
        return (await GetClaimsAsync(role.Id, cancellationToken)).ToList();
    }

    public async Task RemoveClaimAsync(
        Role role, RoleClaim claim, CancellationToken cancellationToken = default)
    {
        role.CheckArgumentIsNull(nameof(role));
        claim.CheckArgumentIsNull(nameof(claim));

        _db.Set<RoleClaim>().Remove(claim);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }
    
    async Task<Role> IRoleStore<Role>.FindByNameAsync(
        string normalizedRoleName, CancellationToken cancellationToken)
    {
        var role = await _set.FirstOrDefaultAsync(r => r.NormalizedName == normalizedRoleName, cancellationToken)
            .ConfigureAwait(false);
        return await Task.FromResult(role!);
    }

    #region UserRoleStore
    
    public Task<string> GetUserIdAsync(UserRole user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(user));
        return Task.FromResult(user.UserId);
    }

    public async Task<string> GetUserNameAsync(UserRole user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(UserRole));
        var u = await _db.Users.FindAsync(user.UserId, cancellationToken).ConfigureAwait(false);
        u.CheckReferenceIsNull(nameof(User));

        return await Task.FromResult(u.UserName);
    }

    public async Task SetUserNameAsync(UserRole user, string userName, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(UserRole));
        if (userName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userName));
        
        var u = await _db.Users.FindAsync(user.UserId, cancellationToken).ConfigureAwait(false);
        u.CheckReferenceIsNull(nameof(User));

        u.UserName = userName;
        _db.Users.Update(u);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<string> GetNormalizedUserNameAsync(UserRole user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(UserRole));
        var u = await _db.Users.FindAsync(user.UserId, cancellationToken).ConfigureAwait(false);
        u.CheckReferenceIsNull(nameof(User));

        return await Task.FromResult(u.NormalizedUserName);
    }

    public async Task SetNormalizedUserNameAsync(UserRole user, string normalizedName, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(UserRole));
        if (normalizedName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(normalizedName));
        
        var u = await _db.Users.FindAsync(user.UserId, cancellationToken).ConfigureAwait(false);
        u.CheckReferenceIsNull(nameof(User));

        u.UserName = normalizedName;
        _db.Users.Update(u);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
    }

    public async Task<IdentityResult> CreateAsync(UserRole user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(UserRole));
        _db.UserRoles.Add(user);

        try
        {
            await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex.GetBaseException().Message);
            return IdentityResult.Failed();
        }
        
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(UserRole user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(UserRole));

        _db.UserRoles.Update(user);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(UserRole user, CancellationToken cancellationToken)
    {
        user.CheckArgumentIsNull(nameof(UserRole));

        _db.UserRoles.Remove(user);
        await _db.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
        return IdentityResult.Success;
    }

    public async Task<UserRole> FindByIdAsync(string userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<UserRole> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    
    public Task AddToRoleAsync(UserRole user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task RemoveFromRoleAsync(UserRole user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<string>> GetRolesAsync(UserRole user, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsInRoleAsync(UserRole user, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IList<UserRole>> GetUsersInRoleAsync(string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
    
    #endregion
}