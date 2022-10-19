using System.Security.Claims;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Idyfa.Core.Services;

/// <summary>
/// UserManager for <see cref="User"/>
/// </summary>
public class IdyfaUserManager : UserManager<User>, IIdyfaUserManager
{
    private readonly IIdyfaUserRepository _store;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IdentityErrorDescriber _errorDescriber;
    private readonly ILookupNormalizer _keyNormalizer;
    private readonly ILogger<IdyfaUserManager> _logger;
    private readonly IOptions<IdentityOptions> _optionsAccessor;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IEnumerable<IPasswordValidator<User>> _passwordValidators;
    private readonly IServiceProvider _serviceProvider;
    private readonly IEnumerable<IUserValidator<User>> _userValidators;
    private readonly IIdyfaUserUsedPasswordManager _usedPasswordManager;
    private User _currentUser;

    public IdyfaUserManager(
        IHttpContextAccessor httpContextAccessor,
        IIdyfaUserRepository store, 
        IOptions<IdentityOptions> optionsAccessor, 
        IPasswordHasher<User> passwordHasher, 
        IEnumerable<IUserValidator<User>> userValidators, 
        IEnumerable<IPasswordValidator<User>> passwordValidators, 
        ILookupNormalizer keyNormalizer, 
        IdentityErrorDescriber errorDescriber, 
        IServiceProvider services, 
        ILogger<IdyfaUserManager> logger, 
        IIdyfaUserUsedPasswordManager usedPasswordManager
    ) : base(store, optionsAccessor, passwordHasher, userValidators, 
                passwordValidators, keyNormalizer, errorDescriber, services, logger)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _optionsAccessor = optionsAccessor ?? throw new ArgumentNullException(nameof(optionsAccessor));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _userValidators = userValidators ?? throw new ArgumentNullException(nameof(userValidators));
        _errorDescriber = errorDescriber ?? throw new ArgumentNullException(nameof(errorDescriber));
        _keyNormalizer = keyNormalizer ?? throw new ArgumentNullException(nameof(keyNormalizer));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _usedPasswordManager = usedPasswordManager ?? throw new ArgumentNullException(nameof(usedPasswordManager));
    }

    public async Task<User> FindByUserNameAsync(string userName)
    {
        if (userName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userName));
        
        return await _store.FindByUserNameAsync(userName);
    }

    public async Task<IReadOnlyCollection<Claim>> GetClaimsAsync(User user)
    {
        var claims = await _store.GetClaimsAsync(user);
        return claims.Any() 
            ? claims.Select(c => new Claim(c.ClaimType, c.ClaimValue)).ToList() 
            : null;
    }

    public async Task<IReadOnlyCollection<string>> GetRolesAsync(User user)
    {
        var roles = await _store.GetRolesAsync(user);
        return roles;
    }

    public async Task<bool> IsPreviouslyUsedPasswordAsync(User user, string newPassword)
        => await _usedPasswordManager.IsPasswordUsedBeforeAsync(user, newPassword);

    public async Task<bool> IsLastUserPasswordTooOldAsync(string userId)
    {
        var user = await FindByIdAsync(userId);
        user.CheckReferenceIsNull(nameof(user));
        return await _usedPasswordManager.IsUserPasswordTooOldAsync(user);
    }

    public async Task<DateTime?> GetLastUserPasswordChangeDateAsync(string userId)
    {
        if (userId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userId));

        var user = await FindByIdAsync(userId);
        return await _usedPasswordManager.GetLastUserChangePasswordDateAsync(user);
    }

    public Task<bool> HasPasswordAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<PasswordVerificationResult> VerifyPasswordAsync(IUserPasswordStore<User> store, User user, string password)
    {
        throw new NotImplementedException();
    }

    public string CreateTwoFactorRecoveryCode()
    {
        throw new NotImplementedException();
    }

    public Task<IdentityResult> UpdatePasswordHash(User user, string newPassword, bool validatePassword)
    {
        throw new NotImplementedException();
    }
}