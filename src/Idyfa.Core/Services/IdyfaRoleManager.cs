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

        var roleClaim = new RoleClaim
        {
            RoleId = role.Id,
            ClaimType = claim.Type,
            ClaimValue = claim.Value
        };
        await _store.AddClaimAsync(roleClaim);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> CreateAsync(Role role)
    {
        role.CheckArgumentIsNull(nameof(role));
        //TODO : check role unique props

        await _store.AddAsync(role);
        return IdentityResult.Success;
    }

    public async Task<IdentityResult> UpdateAsync(Role role)
    {
        role.CheckArgumentIsNull(nameof(role));
        await _store.UpdateAsync(role);
        
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
        var claims = await _store.GetClaimsAsync(role.Id);
        return claims?.Select(_ => new Claim(_.ClaimType, _.ClaimValue)).ToList()!;
    }

    public async Task<IReadOnlyCollection<Role>> FindUserRolesAsync(string userId)
    {
        throw new NotImplementedException();
    }
}