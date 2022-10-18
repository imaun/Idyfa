using Idyfa.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Idyfa.EntityFrameworkCore.SQLite;

public class IdyfaSQLiteContextFactory : IDesignTimeDbContextFactory<IdyfaSQLiteDbContext>
{
    
    public IdyfaSQLiteDbContext CreateDbContext(string[] args)
    {
        var services = new ServiceCollection();
        services.AddOptions();
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddSingleton<ILoggerFactory, LoggerFactory>();
        var basePath = Directory.GetCurrentDirectory();
        Console.WriteLine($"Using '{basePath}' as the ContentRootPath");
        var configuration = new ConfigurationBuilder()
            .SetBasePath(basePath)
            .AddJsonFile("appsettings.json", false, reloadOnChange: true)
            .Build();
        services.AddSingleton<IConfigurationRoot>(provider => configuration);
        services.Configure<IdyfaOptions>(options => configuration.Bind(options));
        Console.WriteLine("Configuration Binded........");
        var options = services.BuildServiceProvider()
            .GetRequiredService<IOptionsSnapshot<IdyfaOptions>>()
            .Value;
        var dbConfig = options.DbConifg;
        var sqliteCfg = dbConfig.Databases.FirstOrDefault(_ => _.Name.ToUpper() == "sqlite".ToUpper());
        if (sqliteCfg is null)
            throw new NullReferenceException("SQLite configurations not found.");
        
        Console.WriteLine($"Using Database {sqliteCfg.Name} with ConnectionString: {sqliteCfg.ConnectionString}");
        services.AddEntityFrameworkSqlite();
        var optionsBuilder = new DbContextOptionsBuilder<IdyfaDbContext>();
        optionsBuilder.Configure(sqliteCfg, services.BuildServiceProvider());
        
        return new IdyfaSQLiteDbContext(optionsBuilder.Options, options);
    }
}