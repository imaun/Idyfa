using System.Collections;
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
        Console.WriteLine($"Using '{basePath}' as the root.");
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
        Console.WriteLine(options.ToString());
        Console.WriteLine("Options found...");
        if(options.IdyfaDbConfig is null)
            Console.WriteLine("DbConfig NOT FOUND>>>>");
        var dbConfig = options.IdyfaDbConfig;
        if(dbConfig is not null)
            Console.WriteLine("DbConfig Found...");
        Console.WriteLine(dbConfig.DbTypeName);
        if(!dbConfig.Databases.Any())
            Console.WriteLine("Databases not found...");
        var sqliteCfg = dbConfig.Databases.FirstOrDefault(_ => string.Equals(
            _.Name, "sqlite", 
            StringComparison.CurrentCultureIgnoreCase));
        
        if (sqliteCfg is null)
            throw new NullReferenceException("SQLite configurations not found.");
        
        Console.WriteLine($"Using Database {sqliteCfg.Name} with ConnectionString: {sqliteCfg.ConnectionString}");
        services.AddEntityFrameworkSqlite();
        var optionsBuilder = new DbContextOptionsBuilder<IdyfaDbContext>();
        optionsBuilder.Configure(sqliteCfg, services.BuildServiceProvider());
        
        return new IdyfaSQLiteDbContext(optionsBuilder.Options);
    }
}