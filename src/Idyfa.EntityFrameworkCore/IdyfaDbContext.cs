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
    
    public IdyfaDbContext(DbContextOptions options, string tablePrefix = "Idyfa."): base(options)
    {
        // _idyfaOptions = idyfaOptions ?? throw new IdyfaOptionsNotFoundException();
        TablePrefix = tablePrefix;
    }

    
    public string TablePrefix { get; protected set; }
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.AddRoleConfiguration(TablePrefix);
        builder.AddRoleClaimConfiguration(TablePrefix);
        builder.AddUserConfiguration(TablePrefix);
        builder.AddPermissionConfiguration(TablePrefix);
        builder.AddUserCategoryConfiguration(TablePrefix);
        builder.AddUserClaimConfiguration(TablePrefix);
        builder.AddUserLoginConfiguration(TablePrefix);
        builder.AddUserRoleConfiguration(TablePrefix);
        builder.AddUserTokenConfiguration(TablePrefix);
        builder.AddUserLoginRecordConfiguration(TablePrefix);
        builder.AddUserUsedPasswordConfiguration(TablePrefix);
    }

    public bool EnsureCreated()
    {
        return Database.EnsureCreated();
    }

    public async Task<bool> EnsureCreatedAsync(CancellationToken cancellationToken = default)
    {
        return await Database.EnsureCreatedAsync(cancellationToken).ConfigureAwait(false);
    }

    public void MigrateDb()
    {
        Database.Migrate();
    }

    public async Task MigrateDbAsync(CancellationToken cancellationToken = default)
    {
        await Database.MigrateAsync(cancellationToken).ConfigureAwait(false);
    }

    public bool CanConnect()
    {
        return Database.CanConnect();
    }


    public async Task<bool> CanConnectAsync(CancellationToken cancellationToken = default)
    {
        return await Database.CanConnectAsync(cancellationToken).ConfigureAwait(false);
    }
}