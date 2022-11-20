using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Exceptions;
using Idyfa.EntityFrameworkCore.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore;

public class IdyfaDbContext 
    : IdentityDbContext<User, Role, string, UserClaim, UserRole, UserLogin, RoleClaim, UserToken>, 
        IIdyfaDbContext
{
    private readonly IdyfaOptions _idyfaOptions;
    
    public IdyfaDbContext(DbContextOptions options): base(options)
    {
        // _idyfaOptions = idyfaOptions ?? throw new IdyfaOptionsNotFoundException();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddRoleConfiguration();
        builder.AddRoleClaimConfiguration();
        builder.AddUserConfiguration();
        builder.AddPermissionConfiguration();
        builder.AddUserCategoryConfiguration();
        builder.AddUserClaimConfiguration();
        builder.AddUserLoginConfiguration();
        builder.AddUserRoleConfiguration();
        builder.AddUserTokenConfiguration();
        builder.AddUserLoginRecordConfiguration();
        builder.AddUserUsedPasswordConfiguration();
    }
}