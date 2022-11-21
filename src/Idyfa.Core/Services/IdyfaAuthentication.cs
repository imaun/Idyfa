using System.Security.Claims;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;

namespace Idyfa.Core.Services;

public class IdyfaAuthentication : IIdyfaAuthentication
{
    private readonly IIdyfaSignInManager _signInManager;
    private readonly IIdyfaUserManager _userManager;
    private readonly IIdyfaUserRepository _userRepository;
    private readonly IdyfaOptions _options;


    public IdyfaAuthentication(
        IdyfaOptions options, IIdyfaSignInManager signInManager,
        IIdyfaUserManager userManager, IIdyfaUserRepository userRepository)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }
    

    public async Task AuthenticateAsync(
        string userName, string password, bool rememberMe = false,
        CancellationToken cancellationToken = default)
    {
        if (userName.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userName));

        if (password.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(password));
        
        
    }

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

    public Task SignOutAsync(ClaimsPrincipal principal)
    {
        throw new NotImplementedException();
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