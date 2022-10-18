using Idyfa.Core;
using Idyfa.Core.Services;
using Idyfa.Core.Contracts;
using Idyfa.Core.Exceptions;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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
        services.AddIdentityServices(options);

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

    private static void AddIdentityServices(
        this IServiceCollection services, IdyfaOptions options)
    {
        if (options is null)
            throw new IdyfaOptionsNotFoundException();

        services.AddIdentityCore<User>(__ =>
        {
            setSignInOptions(__.SignIn, options);
            setLockoutOptions(__.Lockout, options);
            setPasswordOptions(__.Password, options);
        })
            .AddRoles<Role>()
            .AddDefaultTokenProviders()
            .AddSignInManager<IdyfaSignInManager>()
            .AddUserManager<IdyfaUserManager>()
            .AddClaimsPrincipalFactory<IdyfaClaimPrincipalFactory>();
        
        // services.AddIdentity<User, Role>(__ =>
        // {
        //     setSignInOptions(__.SignIn, options);
        //     setLockoutOptions(__.Lockout, options);
        //     setPasswordOptions(__.Password, options);
        // })
        //     .AddRoles<Role>()
        //     // .AddUserStore<IIdyfaUserRepository>()
        //     .AddUserManager<IdyfaUserManager>()
        //     // .AddRoleStore<IIdyfaRoleRepository>()
        //     .AddSignInManager<IdyfaSignInManager>()
        //     .AddErrorDescriber<IdyfaErrorDescriber>()
        //     .AddDefaultTokenProviders()
        //     // .AddTokenProvider<TwoFactorBySmsTokenProvider<User>>("sms")
        //     
        //     ;

        services.AddScoped<IIdyfaRoleManager, IdyfaRoleManager>();
        services.AddScoped<IIdyfaUserManager, IdyfaUserManager>();
        services.AddScoped<IIdyfaUserUsedPasswordManager, IdyfaUserUsedPasswordManager>();
    }

    private static void setPasswordOptions(
        PasswordOptions passwordOptions, IdyfaOptions options)
    {
        passwordOptions.RequireDigit = options.PasswordOptions.RequireDigit;
        passwordOptions.RequireLowercase = options.PasswordOptions.RequireLowercase;
        passwordOptions.RequiredLength = options.PasswordOptions.RequiredLength;
        passwordOptions.RequireUppercase = options.PasswordOptions.RequireUppercase;
        passwordOptions.RequiredUniqueChars = options.PasswordOptions.RequiredUniqueChars;
        passwordOptions.RequireNonAlphanumeric = options.PasswordOptions.RequireNonAlphanumeric;
    }

    private static void setCookieOptions(
        CookieAuthenticationOptions cookieOptions, IdyfaOptions options)
    {
        cookieOptions.Cookie.Name = options.Authentication.CookieName;
        cookieOptions.Cookie.HttpOnly = true;
        cookieOptions.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest;
        cookieOptions.Cookie.IsEssential = true; //TODO : this will store cookie without user's contest!
        cookieOptions.Cookie.SameSite = SameSiteMode.Lax;
        cookieOptions.LoginPath = options.Authentication.LoginPath;
        cookieOptions.LogoutPath = options.Authentication.LogoutPath;
    }
    
    private static void setSignInOptions(
        SignInOptions signInOptions, IdyfaOptions options)
    {
        signInOptions.RequireConfirmedAccount = options.SignIn.RequireConfirmedAccount;
        signInOptions.RequireConfirmedEmail = options.SignIn.RequireConfirmedEmail;
        signInOptions.RequireConfirmedPhoneNumber = options.SignIn.RequireConfirmedPhoneNumber;
    }

    private static void setLockoutOptions(
        LockoutOptions lockoutOptions, IdyfaOptions options)
    {
        lockoutOptions.AllowedForNewUsers = options.Lockout.AllowedForNewUsers;
        lockoutOptions.DefaultLockoutTimeSpan = options.Lockout.DefaultLockoutTimeSpan;
        lockoutOptions.MaxFailedAccessAttempts = options.Lockout.MaxFailedAccessAttempts;
    }
    
    #endregion
}