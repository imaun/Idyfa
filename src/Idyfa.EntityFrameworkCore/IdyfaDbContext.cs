using Idyfa.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore;

public class IdyfaDbContext : 
    IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
{
    
    public IdyfaDbContext(DbContextOptions options): base(options)
    {
    }
    
    
}