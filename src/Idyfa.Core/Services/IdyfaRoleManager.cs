using System.Security.Claims;
using Idyfa.Core.Contracts;
using Idyfa.Core.Contracts.Repository;
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
    
    public Task<IdentityResult> AddClaimAsync(Role role, Claim claim)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> CreateAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdateAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> DeleteAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<Role> FindByIdAsync(string roleId)
    {
        throw new NotImplementedException();
    }

    public Task<Role> FindByNameAsync(string roleName)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Claim>> GetClaimsAsync(Role role)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Role>> FindUserRolesAsync(string userId)
    {
        throw new NotImplementedException();
    }
}