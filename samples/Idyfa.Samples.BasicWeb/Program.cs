using Idyfa.Core;

var builder = WebApplication.CreateBuilder(args);
IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var options = config.Get<IdyfaOptions>();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddRouting(options => options.LowercaseUrls = true);
var sqliteCfg = options.DbConfig.Databases.FirstOrDefault(_ =>
    _.Name.Equals("SQLite", StringComparison.InvariantCultureIgnoreCase));
builder.Services.AddIdyfaSQLiteDatabase(sqliteCfg);
builder.Services.AddIdyfaEntityFrameworkCore();
builder.Services.AddIdyfaCore(options);

var app = builder.Build();
app.MapGet("/", () => "Hello World!");

app.Run();