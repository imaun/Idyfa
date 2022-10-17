using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Idyfa.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{
 
    public static void AddIdyfaEntityFrameworkCore(
        this IServiceCollection services, 
        Action<DbContextOptionsBuilder> dbContextOptionBuilder)
    {
        services.CheckArgumentIsNull(nameof(services));
        services.AddDbContext<IdyfaDbContext>(dbContextOptionBuilder);
        services.AddScoped<IIdyfaUserRepository, IdyfaUserRepository>();
        services.AddScoped<IIdyfaRoleRepository, IdyfaRoleRepository>();
    }
    
}