using Idyfa.Core.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Idyfa.Core.Services;

public class IdyfaSignInManager : SignInManager<User>, IIdyfaSignInManager
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IIdyfaUserManager _userManager;
    private readonly IUserConfirmation<User> _confirmation;
    private readonly ILogger<IdyfaSignInManager> _logger;
    private readonly IOptions<IdentityOptions> _optionAccessor;
    private readonly IAuthenticationSchemeProvider _schemes;
    private readonly IUserClaimsPrincipalFactory<User> _claimsFactory;

    public IdyfaSignInManager(
        IIdyfaUserManager userManager, 
        IHttpContextAccessor contextAccessor, 
        IUserClaimsPrincipalFactory<User> claimsFactory, 
        IOptions<IdentityOptions> optionsAccessor, 
        ILogger<IdyfaSignInManager> logger, 
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<User> confirmation
        ) : base((UserManager<User>)userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes)
    {
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _httpContextAccessor = contextAccessor ?? throw new ArgumentNullException(nameof(contextAccessor));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _optionAccessor = optionsAccessor ?? throw new ArgumentNullException(nameof(optionsAccessor));
        _schemes = schemes ?? throw new ArgumentNullException(nameof(schemes));
        _claimsFactory = claimsFactory ?? throw new ArgumentNullException(nameof(claimsFactory));
        _confirmation = confirmation ?? throw new ArgumentNullException(nameof(confirmation));
    }

    public async Task<IReadOnlyCollection<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync()
    {
        throw new NotImplementedException();
    }

    public Task<SignInResult> SignInOrTwoFactorAsync(
        User user, bool isPersistent, 
        string loginProvider = null, 
        bool bypassTwoFactor = false)
    {
        return base.SignInOrTwoFactorAsync(user, isPersistent, loginProvider, bypassTwoFactor);
    }

    public Task<bool> IsLockedOut(User user)
    {
        return base.IsLockedOut(user);
    }
}