using System.Security.Claims;
using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace Idyfa.EntityFrameworkCore;

/// <inheritdoc />
public class IdyfaRoleRepository : IdyfaBaseRepository<Role, string>, IIdyfaRoleRepository
{
    
    public IdyfaRoleRepository(IdyfaDbContext db) : base(db)
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

    public Task<IEnumerable<RoleClaim>> GetClaimsAsync(string roleId, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<RoleClaim>> GetClaimsAsync(string roleId)
    {
        throw new NotImplementedException();
    }

    public Task AddClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    Task<Role> IIdyfaRoleRepository.FindByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Claim>> GetClaimsAsync(Role role, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task RemoveClaimAsync(Role role, Claim claim, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    Task<Role> IRoleStore<Role>.FindByNameAsync(string normalizedRoleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}