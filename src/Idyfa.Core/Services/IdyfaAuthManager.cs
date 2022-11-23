using System.Security.Claims;
using Idyfa.Core.Events;
using Idyfa.Core.Contracts;
using Idyfa.Core.Exceptions;
using Idyfa.Core.Extensions;

namespace Idyfa.Core.Services;

/// <inheritdoc /> 
public class IdyfaAuthManager : IIdyfaAuthManager
{
    private readonly IIdyfaSignInManager _signInManager;
    private readonly IIdyfaUserManager _userManager;
    private readonly IIdyfaUserRepository _userRepository;
    private readonly IdyfaOptions _options;
    private readonly IIdyfaUserValidator _userValidator;
    
    
    public IdyfaAuthManager(
        IdyfaOptions options, IIdyfaSignInManager signInManager,
        IIdyfaUserManager userManager, IIdyfaUserRepository userRepository, 
        IIdyfaUserValidator userValidator)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _userValidator = userValidator ?? throw new ArgumentNullException(nameof(userValidator));
    }


    public event Action<AfterSignInEventArgs> AfterSignInEvent;
    

    private bool _isDevelopment = Environment
        .GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

    private UserNameType GetUserNameType()
    {
        if (_options is null)
            throw new IdyfaOptionsNotFoundException();

        return _options.UserOptions.UserNameType;
    }
    
    /// <inheritdoc /> 
    public async Task AuthenticateAsync(
        string userName, string password, bool rememberMe = false,
        CancellationToken cancellationToken = default)
    {
        if (userName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userName));

        if (password.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(password));
        
        if (GetUserNameType() == UserNameType.PhoneNumber)
        {
            userName = userName.NormalizePhoneNumber();
        }
        
        var validateUsername =  _userValidator.ValidateUserName(userName);
        if (validateUsername.Any())
            throw new InvalidUserNameException(validateUsername);

        var user = await _userManager.FindByNameAsync(userName);
        if (user is null) throw new IdyfaUserNotFoundException();

        // if (_isDevelopment)
        // {
        //     await _signInManager.SignInAsync(user, true);
        //     return;
        // }

        if (user.Status == UserStatus.Blocked ||
            user.Status == UserStatus.Deleted ||
            user.Status == UserStatus.Locked)
        {
            throw new InvalidUserStatusForSignInException(user.Status);
        }

        var signInResult = await _signInManager.PasswordSignInAsync(
            user, password, rememberMe, _options.Authentication.LockoutOnFailure);

        if (!signInResult.Succeeded && signInResult.RequiresTwoFactor)
        {
            //TODO : Generate TwoFactorToken based on User.TwoFactorType -> Sms, Email or Authenticator
            var twoFactorToken = await _userManager.GenerateTwoFactorTokenAsync(user, "sms");
            user.SetTwoFactorCode(twoFactorToken);

            await _userRepository.UpdateAndSaveAsync(user, cancellationToken).ConfigureAwait(false);
            
            //SEND Token VIA SMS or Email

            throw new IdyfaSignInRequireTwoFactorAuthenticationException();
        }
        
        //TODO : raise an event that the user has logged-in successfully.
        AfterSignInEvent?.Invoke(new AfterSignInEventArgs(
            user.Id, user.UserName, user.Status, 
            user.PhoneNumber, user.Email, user.DisplayName));
    }

    /// <inheritdoc /> 
    public Task OtpSingInAsync(string otpKey)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerifyOtpSignInAsync(string userId, string token)
    {
        throw new NotImplementedException();
    }

    public Task TwoFactorSignInAsync(string token, bool rememberMe = false)
    {
        throw new NotImplementedException();
    }

    public Task SendTwoFactorCodeAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task ResendTwoFactorCodeAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public async Task SignOutAsync(ClaimsPrincipal principal)
    {
        var user = await _userManager.FindByNameAsync(principal?.Identity?.Name!).ConfigureAwait(false);
        if(user is null) return;
        
        await _userManager.UpdateSecurityStampAsync(user).ConfigureAwait(false);
        await _signInManager.SignOutAsync().ConfigureAwait(false);
        //TODO : Log signout
    }

    public Task RegisterUserAsync(string userName, string password, string verifyPassword, string displayName,
        string referralCode = null)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task ConfirmEmailAsync(string userName, string token)
    {
        throw new NotImplementedException();
    }

    public Task<string> GenerateResetPasswordTokenAsync(string userName)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerifyResetPasswordTokenAsync(string userName, string token)
    {
        throw new NotImplementedException();
    }

    public Task<string> GeneratePhoneNumberVerificationTokenAsync(string phoneNumber)
    {
        throw new NotImplementedException();
    }

    public Task<bool> VerifyPhoneNumberTokenAsync(string userName, string token)
    {
        throw new NotImplementedException();
    }
}