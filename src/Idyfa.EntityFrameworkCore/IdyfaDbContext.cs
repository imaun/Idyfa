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
    
    public IdyfaDbContext(DbContextOptions options, IdyfaOptions idyfaOptions): base(options)
    {
        _idyfaOptions = idyfaOptions ?? throw new IdyfaOptionsNotFoundException();
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddRoleConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddRoleClaimConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddUserConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddPermissionRecordConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddUserCategoryConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddUserClaimConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddUserLoginConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddUserRoleConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddUserTokenConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddUserLoginRecordConfiguration(_idyfaOptions.TableNamesPrefix);
        builder.AddUserUsedPasswordConfiguration(_idyfaOptions.TableNamesPrefix);
    }
}