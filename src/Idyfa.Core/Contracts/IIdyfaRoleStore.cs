using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts;

public interface IIdyfaRoleStore
{ 
    
    IQueryable<Role> Roles { get; }
    
    Task<IdentityResult> CreateAsync(
        Role role, CancellationToken cancellationToken = default);
    
    Task<IdentityResult> UpdateAsync(
        Role role, CancellationToken cancellationToken = default);
    
    Task<IdentityResult> DeleteAsync(
        Role role, CancellationToken cancellationToken = default);
    
    Task AddClaimAsync(
        Role role, Claim claim, CancellationToken cancellationToken = default);
    
    Task<Role> FindByIdAsync(
        string id, CancellationToken cancellationToken = default);
    
    Task<Role> FindByNameAsync(
        string name, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(
        Role role, CancellationToken cancellationToken = default);
    
    Task RemoveClaimAsync(
        Role role, Claim claim, CancellationToken cancellationToken = default);
    
}