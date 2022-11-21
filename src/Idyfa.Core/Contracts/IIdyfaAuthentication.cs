using System.Security.Claims;

namespace Idyfa.Core.Contracts;

public interface IIdyfaAuthentication
{

    Task AuthenticateAsync(
        string userName, string password, 
        bool rememberMe = false, CancellationToken cancellationToken = default);


    Task OtpSingInAsync(string otpKey);


    Task<bool> VerifyOtpSignInAsync(string userId, string token);


    Task TwoFactorSignInAsync(string token, bool rememberMe = false);


    Task SendTwoFactorCodeAsync(string userName);

    Task ResendTwoFactorCodeAsync(string userName);

    Task SignOutAsync(ClaimsPrincipal principal);


    Task RegisterUserAsync(
        string userName, string password, string verifyPassword,
        string displayName, string referralCode = null);

    
    Task<string> GenerateEmailConfirmationTokenAsync(string userName);


    Task ConfirmEmailAsync(string userName, string token);


    Task<string> GenerateResetPasswordTokenAsync(string userName);


    Task<bool> VerifyResetPasswordTokenAsync(string userName, string token);


    Task<string> GeneratePhoneNumberVerificationTokenAsync(string phoneNumber);


    Task<bool> VerifyPhoneNumberTokenAsync(string userName, string token);
}