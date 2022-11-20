using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Idyfa.EntityFrameworkCore;
using Idyfa.EntityFrameworkCore.SQLite;
using Microsoft.EntityFrameworkCore;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceProvider
{

    public static IServiceCollection AddIdyfaSQLiteDatabase(
        this IServiceCollection services, IdyfaDbConfigItem dbConfig)
    {
        dbConfig.CheckArgumentIsNull("Database config for SQLite not found.");
        services.AddTransient<IIdyfaDbContext>(
            provider => provider.GetRequiredService<IdyfaDbContext>());
        services.AddEntityFrameworkSqlite();
        services.AddDbContextPool<IdyfaDbContext, IdyfaSQLiteDbContext>(
            (provider, optionsBuilder)
                => optionsBuilder.Configure(dbConfig, provider)
                );

        return services;
    }
    
    public static void Configure(
        this DbContextOptionsBuilder optionsBuilder,
        IdyfaDbConfigItem dbConfig, IServiceProvider serviceProvider)
    {
        optionsBuilder.CheckArgumentIsNull(nameof(optionsBuilder));
        dbConfig.CheckArgumentIsNull("The config for Database is null.");

        optionsBuilder.UseSqlite(dbConfig.ConnectionString,
            __ =>
            {
                __.CommandTimeout((int)TimeSpan.FromMinutes(dbConfig.Timeout).TotalSeconds);
                __.MigrationsAssembly(typeof(IdyfaSQLiteDbContext).Assembly.FullName);
                __.MigrationsHistoryTable(IdyfaConsts.HistoryTableName);
            });
        optionsBuilder.UseInternalServiceProvider(serviceProvider);
    }
}