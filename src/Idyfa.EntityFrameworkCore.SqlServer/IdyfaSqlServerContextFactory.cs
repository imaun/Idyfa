using Idyfa.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace Idyfa.EntityFrameworkCore.SqlServer
{
    public class IdyfaSqlServerContextFactory : IDesignTimeDbContextFactory<IdyfaSqlServerDbContext>
    {
        public IdyfaSqlServerDbContext CreateDbContext(string[] args) {
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
            if (options.IdyfaDbConfig is null)
                Console.WriteLine("Error : DbConfig NOT FOUND!!!");
            var dbConfig = options.IdyfaDbConfig;
            if (dbConfig is not null)
                Console.WriteLine("DbConfig Found...");
            Console.WriteLine(dbConfig.DbTypeName);
            if (!dbConfig.Databases.Any())
                Console.WriteLine("Databases not found...");
            var sqlserverCfg = dbConfig.Databases.FirstOrDefault(_ => string.Equals(
                _.Name, "sqlserver",
                StringComparison.CurrentCultureIgnoreCase));

            if (sqlserverCfg is null)
                throw new NullReferenceException("SQLite configurations not found.");

            Console.WriteLine($"Using Database {sqlserverCfg.Name} with ConnectionString: {sqlserverCfg.ConnectionString}");
            services.AddEntityFrameworkSqlServer();
            var optionsBuilder = new DbContextOptionsBuilder<IdyfaDbContext>();
            optionsBuilder.Configure(sqlserverCfg, services.BuildServiceProvider());

            return new IdyfaSqlServerDbContext(optionsBuilder.Options);
        }
    }
}