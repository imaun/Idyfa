using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Idyfa.EntityFrameworkCore;
using Idyfa.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class IdyfaSqlServerServiceProvider
{

    public static IServiceCollection AddIdyfaSqlServerDatabase(
        this IServiceCollection services, IdyfaDbConfigItem dbConfig) {
        dbConfig.CheckArgumentIsNull("Database config for SQLite not found.");
        services.AddTransient<IIdyfaDbContext>(
            provider => provider.GetRequiredService<IdyfaDbContext>());
        services.AddEntityFrameworkSqlServer();
        services.AddDbContextPool<IdyfaDbContext, IdyfaSqlServerDbContext>(
            (provider, optionsBuilder)
                => optionsBuilder.Configure(dbConfig, provider)
                );

        return services;
    }


    public static void Configure(
        this DbContextOptionsBuilder optionsBuilder,
        IdyfaDbConfigItem dbConfig, IServiceProvider serviceProvider) {
        optionsBuilder.CheckArgumentIsNull(nameof(optionsBuilder));
        dbConfig.CheckArgumentIsNull("The config for Database is null.");

        optionsBuilder.UseSqlServer(dbConfig.ConnectionString,
            __ => {
                __.CommandTimeout((int)TimeSpan.FromMinutes(dbConfig.Timeout).TotalSeconds);
                __.MigrationsAssembly(typeof(IdyfaSqlServerDbContext).Assembly.FullName);
                __.EnableRetryOnFailure();
                __.MigrationsHistoryTable("__Idyfa_Migrations");
            });
        optionsBuilder.UseInternalServiceProvider(serviceProvider);
    }
}