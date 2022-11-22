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
        // services.AddIdyfaServices();
        services.AddIdentityServices(options);
        
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
        var myoptions = services.AddIdyfaOptions();
        services.AddIdyfaServices();
        services.AddIdentityServices(myoptions);
        
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
        var optionSnapshot = provider.GetRequiredService<IOptionsSnapshot<IdyfaConfigRoot>>();
        var options = optionSnapshot.Value.Idyfa;

        if (options is null)
            throw new IdyfaOptionsNotFoundException();

        services.AddIdyfaOptions(options);
        return options;
    }

    public static IdyfaOptions GetIdyfaOptions(this IServiceCollection services)
    {
        services.CheckArgumentIsNull(nameof(services));
        var provider = services.BuildServiceProvider();
        var optionsSnapshot = provider.GetRequiredService<IOptionsSnapshot<IdyfaConfigRoot>>();
        var options = optionsSnapshot.Value.Idyfa;

        if (options is null)
            throw new IdyfaOptionsNotFoundException();

        return options;
    }
    
    private static IServiceCollection AddIdyfaServices(this IServiceCollection services)
    {
        services.CheckArgumentIsNull(nameof(services));

        services.AddHttpContextAccessor();
        services.AddScoped<IIdyfaUserContext, IdyfaUserContext>();
        services.AddScoped<ILookupNormalizer, IdyfaLookupNormalizer>();
        // services.AddScoped<IIdyfaRoleManager, IdyfaRoleManager>();
        // services.AddScoped<IIdyfaUserManager, IdyfaUserManager>();
        services.AddScoped<IIdyfaUserUsedPasswordManager, IdyfaUserUsedPasswordManager>();
        // services.AddScoped<IIdyfaSignInManager, IdyfaSignInManager>();
        services.AddScoped<IPasswordValidator<User>, IdyfaPasswordValidator>();
        services.AddScoped<ISecurityStampValidator, IdyfaSecurityStampValidator>();
        services.AddScoped<IUserClaimsPrincipalFactory<User>, IdyfaClaimPrincipalFactory>();
        
        return services;
    }

    private static void AddIdentityServices(
        this IServiceCollection services, IdyfaOptions options)
    {
        if (options is null)
            throw new IdyfaOptionsNotFoundException();

        services.AddHttpContextAccessor();
        services.AddScoped<IIdyfaUserContext, IdyfaUserContext>();
        services.AddScoped<ILookupNormalizer, IdyfaLookupNormalizer>();
        services.AddScoped<IIdyfaRoleManager, IdyfaRoleManager>();
        services.AddScoped<IIdyfaUserManager, IdyfaUserManager>();
        services.AddScoped<IIdyfaUserUsedPasswordManager, IdyfaUserUsedPasswordManager>();
        services.AddScoped<IIdyfaSignInManager, IdyfaSignInManager>();
        services.AddScoped<IPasswordValidator<User>, IdyfaPasswordValidator>();
        services.AddScoped<ISecurityStampValidator, IdyfaSecurityStampValidator>();
        services.AddScoped<IIdyfaUserUsedPasswordManager, IdyfaUserUsedPasswordManager>();
        services.AddScoped<IUserClaimsPrincipalFactory<User>, IdyfaClaimPrincipalFactory>();
        services.AddScoped<UserClaimsPrincipalFactory<User, Role>, IdyfaClaimPrincipalFactory>();
        
        services.AddIdentity<User, Role>(__ =>
        {
            setSignInOptions(__.SignIn, options);
            setLockoutOptions(__.Lockout, options);
            setPasswordOptions(__.Password, options);
        })
            .AddRoleManager<IdyfaRoleManager>()
            .AddDefaultTokenProviders()
            .AddSignInManager<IdyfaSignInManager>()
            .AddUserManager<IdyfaUserManager>()
            .AddErrorDescriber<IdyfaErrorDescriber>()
            .AddPasswordValidator<IdyfaPasswordValidator>()
            .AddUserValidator<IdyfaUserValidator>()
            .AddDefaultTokenProviders()
            .AddClaimsPrincipalFactory<IdyfaClaimPrincipalFactory>();

        services.ConfigureApplicationCookie(cookieOptions =>
        {
            setCookieOptions(cookieOptions, options);
        });

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