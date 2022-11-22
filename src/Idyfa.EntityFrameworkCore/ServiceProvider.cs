using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Idyfa.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{
 
    public static void AddIdyfaEntityFrameworkCore(
        this IServiceCollection services)
    {
        services.CheckArgumentIsNull(nameof(services));
        // services.AddDbContext<IdyfaDbContext>(dbContextOptionBuilder);
        
        services.AddScoped<IUserStore<User>, IdyfaUserRepository>();
        services.AddScoped<IIdyfaUserRepository, IdyfaUserRepository>();
        services.AddScoped<IRoleStore<Role>, IdyfaRoleRepository>();
        services.AddScoped<IIdyfaRoleRepository, IdyfaRoleRepository>();
        services.AddScoped<IIdyfaUserUsedPasswordRepository, IdyfaUserUsedPasswordRepository>();
    }
    
}