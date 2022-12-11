using System.Security.Claims;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Idyfa.Core.Services;

public class IdyfaRoleManager : RoleManager<Role>, IIdyfaRoleManager
{
    private readonly IIdyfaRoleRepository _store;
    
    public IdyfaRoleManager(
        IIdyfaRoleRepository store,
        IEnumerable<IRoleValidator<Role>> roleValidators,
        ILookupNormalizer normalizer,
        IdentityErrorDescriber errorDescriber,
        ILogger<IdyfaRoleManager> logger): base(store, roleValidators, normalizer, errorDescriber, logger)
    {
        _store = store ?? throw new ArgumentNullException(nameof(store));
    }
    
    public async Task<IdentityResult> AddClaimAsync(Role role, Claim claim)
    {
        role.CheckArgumentIsNull(nameof(role));
        claim.CheckArgumentIsNull(nameof(claim));

        var roleClaim = RoleClaim.New(role.Id, claim.Type, claim.Value);
        await _store.AddClaimAsync(roleClaim);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> CreateAsync(Role role)
    {
        role.CheckArgumentIsNull(nameof(role));
        
        if(await _store.ExistByNameAsync(role.Id, role.Name))
            return IdentityResult.Failed(ErrorDescriber.DuplicateRoleName(role.Name));

        role.NormalizedName = role.Name.ToUpperInvariant();
        await _store.AddAndSaveAsync(role);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role)
    {
        role.CheckArgumentIsNull(nameof(role));
        if(await _store.ExistByNameAsync(role.Id, role.Name))
            return IdentityResult.Failed(ErrorDescriber.DuplicateRoleName(role.Name));

        role.NormalizedName = role.Name.ToUpperInvariant();
        await _store.UpdateAndSaveAsync(role);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> DeleteAsync(Role role)
    {
        role.CheckArgumentIsNull(nameof(role));
        await _store.DeleteAsync(role);
        
        return IdentityResult.Success;
    }

    public async Task<Role> FindByIdAsync(string roleId)
    {
        if (roleId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(roleId));

        var role = await _store.FindByIdAsync(roleId);
        role.CheckReferenceIsNull(nameof(role));
        
        return await Task.FromResult(role);
    }

    public async Task<Role> FindByNameAsync(string roleName)
    {
        if (roleName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(roleName));

        var role = await _store.GetByNameAsync(roleName);
        role.CheckReferenceIsNull(nameof(role));
        
        return await Task.FromResult(role);
    }

    public async Task<IReadOnlyCollection<Claim>> GetClaimsAsync(Role role)
    {
        role.CheckArgumentIsNull(nameof(role));
        var claims = await _store.GetClaimsAsync(role.Id).ConfigureAwait(false);
        return claims?.Select(_ => new Claim(_.ClaimType, _.ClaimValue)).ToList()!;
    }

    public async Task<IReadOnlyCollection<Role>> FindUserRolesAsync(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ExistByNameAsync(string roleId, string roleName)
    {
        return await _store.ExistByNameAsync(roleId, roleName).ConfigureAwait(false);
    }
}