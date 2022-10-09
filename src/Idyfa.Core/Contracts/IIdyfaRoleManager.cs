using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts;

/// <summary>
/// Manage <see cref="Role"/>s 
/// </summary>
public interface IIdyfaRoleManager
{
    Task<IdentityResult> AddClaimAsync(Role role, Claim claim);
    
    Task<IdentityResult> CreateAsync(Role role);
    
    Task<IdentityResult> UpdateAsync(Role role);
    
    Task<IdentityResult> DeleteAsync(Role role);

    Task<Role> FindByIdAsync(string roleId);
    
    Task<Role> FindByNameAsync(string roleName);
    
    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(Role role);

    Task<IReadOnlyCollection<Role>> FindUserRolesAsync(string userId);
    
    
}