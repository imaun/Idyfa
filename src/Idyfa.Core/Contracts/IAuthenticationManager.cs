using System.Security.Claims;

namespace Idyfa.Core.Contracts;

/// <summary>
/// Authenticate (Sign-in, TwoFactor, ...) the <see cref="User"/> with
/// ResetPassword and ChangePassword capabilities.
/// </summary>
public interface IAuthenticationManager
{

    /// <summary>
    /// Try to Sign-in the <see cref="User"/> with the given UserName ans Password.
    /// </summary>
    /// <param name="userName">UserName entered by the user, could be a name, email address or phone number based on <see cref="UserNameType"/> specified in IdyfaOptions.</param>
    /// <param name="password">Password entered by the user.</param>
    /// <param name="rememberMe">If true, a cookie will be generated to keep the user logged-in for the caller client.</param>
    /// <param name="cancellationToken">Token to cancel the process.</param>
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