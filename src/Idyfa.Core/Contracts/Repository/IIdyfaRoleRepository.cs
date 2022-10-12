using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts;

public interface IIdyfaRoleRepository : IIdyfaBaseRepository<Role, string>, IRoleStore<Role>
{
    
    Task AddClaimAsync(RoleClaim claim);

    Task<Role> GetByNameAsync(string roleName);

    Task<IEnumerable<RoleClaim>> GetClaimsAsync(string roleId, CancellationToken cancellationToken = default);
    
    Task AddClaimAsync(
        Role role, Claim claim, CancellationToken cancellationToken = default);
    
    Task<Role> FindByNameAsync(
        string name, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(
        Role role, CancellationToken cancellationToken = default);
    
    Task RemoveClaimAsync(
        Role role, Claim claim, CancellationToken cancellationToken = default);
}