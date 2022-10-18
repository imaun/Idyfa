using Idyfa.Core;
using Idyfa.Core.Services;
using Idyfa.Core.Contracts;
using Idyfa.Core.Exceptions;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provide extension methods over ServiceCollection to Adds IdyfaCore Services to
/// the default .NET CORE DI container (Microsoft.Extensions.DependencyInjection)
/// </summary>
public static class ServiceProvider  {

    /// <summary>
    /// Adds Idyfa core services to the default .NET CORE DI container.
    /// </summary>
    /// <param name="services"></param>
    public static IServiceCollection AddIdyfaCore(this IServiceCollection services)
    {
        var options = services.AddIdyfaOptions();
        services.AddIdyfaServices();

        return services;
    }
    
    /// <summary>
    /// Adds Idyfa core services to the default .NET CORE DI container with specified <see cref="IdyfaOptions"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options">Idyfa Core options.</param>
    public static IServiceCollection AddIdyfaCore(
        this IServiceCollection services, IdyfaOptions options)
    {
        services.AddIdyfaOptions(options);
        services.AddIdyfaServices();
        
        return services;
    }

    /// <summary>
    /// Adds Idyfa core services and options to the DI container and configures the <see cref="IdyfaOptions"/>.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="options">Idyfa core options.</param>
    public static IServiceCollection AddIdyfaCore(
        this IServiceCollection services, Action<IdyfaOptions> options)
    {
        services.Configure(options);
        services.AddIdyfaOptions();
        services.AddIdyfaServices();
        return services;
    }
    
    #region private helper methods

    private static void AddIdyfaOptions(
        this IServiceCollection services, IdyfaOptions options)
    {
        services.CheckArgumentIsNull(nameof(services));
        services.AddSingleton(options);
    }
    
    private static IdyfaOptions AddIdyfaOptions(this IServiceCollection services)
    {
        services.CheckArgumentIsNull(nameof(services));
        var provider = services.BuildServiceProvider();
        var optionSnapshot = provider.GetRequiredService<IOptionsSnapshot<IdyfaOptions>>();
        var options = optionSnapshot.Value;

        if (options is null)
            throw new IdyfaOptionsNotFoundException();

        services.AddIdyfaOptions(options);
        return options;
    }

    public static IdyfaOptions GetIdyfaOptions(this IServiceCollection services)
    {
        services.CheckArgumentIsNull(nameof(services));
        var provider = services.BuildServiceProvider();
        var optionsSnapshot = provider.GetRequiredService<IOptionsSnapshot<IdyfaOptions>>();
        var options = optionsSnapshot.Value;

        if (options is null)
            throw new IdyfaOptionsNotFoundException();

        return options;
    }
    
    private static IServiceCollection AddIdyfaServices(this IServiceCollection services)
    {
        services.CheckArgumentIsNull(nameof(services));

        services.AddHttpContextAccessor();
        // services.AddScoped<>()
        services.AddScoped<ILookupNormalizer, IdyfaLookupNormalizer>();
        services.AddScoped<IIdyfaRoleManager, IdyfaRoleManager>();
        services.AddScoped<IIdyfaUserManager, IdyfaUserManager>();
        services.AddScoped<IIdyfaUserUsedPasswordManager, IdyfaUserUsedPasswordManager>();
        services.AddScoped<IIdyfaSignInManager, IdyfaSignInManager>();
        
        return services;
    }
    
    #endregion
}