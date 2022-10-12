using System.Security.Claims;
using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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

    public Task<IdentityResult> DeleteAsync(Role role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRoleIdAsync(Role role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetRoleNameAsync(Role role, string roleName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<string> GetNormalizedRoleNameAsync(Role role, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task SetNormalizedRoleNameAsync(Role role, string normalizedName, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task AddClaimAsync(RoleClaim claim)
    {
        throw new NotImplementedException();
    }

    public Task<Role> GetByNameAsync(string roleName)
    {
        throw new NotImplementedException();
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