using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts;

public interface IIdyfaUserPasswordRepository : IUserPasswordStore<User>
{
    
}