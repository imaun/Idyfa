using System.Security.Claims;
using Idyfa.Core.Contracts;
using Idyfa.Core.Contracts.Repository;
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
        ILogger<IdyfaUserManager> logger
    ) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errorDescriber, services, logger)
    {
        _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        _store = store ?? throw new ArgumentNullException(nameof(store));
        _optionsAccessor = optionsAccessor ?? throw new ArgumentNullException(nameof(optionsAccessor));
        _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        _userValidators = userValidators ?? throw new ArgumentNullException(nameof(userValidators));
        _errorDescriber = errorDescriber ?? throw new ArgumentNullException(nameof(errorDescriber));
        _keyNormalizer = keyNormalizer ?? throw new ArgumentNullException(nameof(keyNormalizer));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }

    public Task<User> FindByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<Claim>> GetClaimsAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyCollection<string>> GetRolesAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsPreviouslyUsedPasswordAsync(User user, string newPassword)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsLastUserPasswordTooOldAsync(int userId)
    {
        throw new NotImplementedException();
    }

    public Task<DateTime?> GetLastUserPasswordChangeDateAsync(int userId)
    {
        throw new NotImplementedException();
    }
}