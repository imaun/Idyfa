using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts.Repository;

public interface IIdyfaRoleRepository : IIdyfaBaseRepository<Role, string>, IRoleStore<Role>
{
    
}