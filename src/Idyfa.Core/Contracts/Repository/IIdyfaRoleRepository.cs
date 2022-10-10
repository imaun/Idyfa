using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts.Repository;

public interface IIdyfaRoleRepository : IIdyfaBaseRepository<Role, string>, IRoleStore<Role>
{
    
    Task AddClaimAsync(RoleClaim claim);

    Task<Role> GetByNameAsync(string roleName);

    Task<IEnumerable<RoleClaim>> GetClaimsAsync(string roleId);
}