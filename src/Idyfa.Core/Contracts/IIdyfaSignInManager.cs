using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Idyfa.Core.Contracts;

/// <summary>
/// Services used to manage <see cref="User"/>'s SignIn and SignOut routines.
/// </summary>
public interface IIdyfaSignInManager
{
    Task<ClaimsPrincipal> CreateUserPrincipalAsync(User user);
    
    bool IsSignedIn(ClaimsPrincipal principal);

    Task<bool> CanSignInAsync(User user);
    
    /// <summary>
    /// Regenerates cookie used to authenticate the <see cref="User"/> and
    /// preserve remember me and other Authentication properties.
    /// </summary>
    /// <param name="user"></param>
    /// <returns></returns>
    Task RefreshSignInAsync(User user);
    
    /// <summary>
    /// Sign-In the <see cref="User"/>, and Remember the login info if <param name="isPersistent"></param>
    /// is set to true.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="isPersistent"></param>
    /// <param name="authenticationMethod"></param>
    /// <returns></returns>
    Task SignInAsync(
        User user, bool isPersistent, string authenticationMethod = null);
    
    /// <summary>
    /// Sign-in the <see cref="User"/> with the given <paramref name="authenticationProperties" />
    /// </summary>
    /// <param name="user"></param>
    /// <param name="authenticationProperties"></param>
    /// <param name="authenticationMethod"></param>
    /// <returns></returns>
    Task SignInAsync(
        User user, AuthenticationProperties authenticationProperties, 
        string authenticationMethod = null);
    
    /// <summary>
    /// Sign-out the current logged-in <see cref="User"/>
    /// </summary>
    /// <returns></returns>
    Task SignOutAsync();
    
    /// <summary>
    /// Sign-in the <see cref="User"/> using a <paramref name="password"/>.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <param name="isPersistent"></param>
    /// <param name="lockoutOnFailure"></param>
    /// <returns></returns>
    Task<SignInResult> PasswordSignInAsync(
        User user, string password,
        bool isPersistent, bool lockoutOnFailure);
    
    /// <summary>
    /// Attempts to Sign-in the <see cref="User"/> with the given <paramref name="password"/> and
    /// lockout the user if it's failed.
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="isPersistent"></param>
    /// <param name="lockoutOnFailure"></param>
    /// <returns></returns>
    Task<SignInResult> PasswordSignInAsync(string userName, string password,
        bool isPersistent, bool lockoutOnFailure);
    
    
    Task<User> GetTwoFactorAuthenticationUserAsync();
    
    /// <summary>
    /// Sign-in the <see cref="User"/> using third-party login provider that has registered for teh user.
    /// </summary>
    /// <param name="loginProvider"></param>
    /// <param name="providerKey"></param>
    /// <param name="isPersistent"></param>
    /// <returns></returns>
    Task<SignInResult> ExternalLoginSignInAsync(
        string loginProvider, string providerKey, bool isPersistent);
    
    
    Task<SignInResult> ExternalLoginSignInAsync(
        string loginProvider, string providerKey, 
        bool isPersistent, bool bypassTwoFactor);
    
    
    Task<IReadOnlyCollection<AuthenticationScheme>> GetExternalAuthenticationSchemesAsync();
    
    AuthenticationProperties ConfigureExternalAuthenticationProperties(
        string provider, string redirectUrl, string userId = null);
    
    Task<SignInResult> SignInOrTwoFactorAsync(
        User user, bool isPersistent, 
        string loginProvider = null, bool bypassTwoFactor = false);
    
    Task<bool> IsLockedOut(User user);
    
    
    /// <summary>
    ///     Gets the <see cref="ILogger" /> used to log messages from the manager.
    /// </summary>
    /// <value>
    ///     The <see cref="ILogger" /> used to log messages from the manager.
    /// </value>
    ILogger Logger { get; set; }

    /// <summary>
    ///     The <see cref="UserManager{TUser}" /> used.
    /// </summary>
    UserManager<User> UserManager { get; set; }

    /// <summary>
    ///     The <see cref="IUserClaimsPrincipalFactory{User}" /> used.
    /// </summary>
    IUserClaimsPrincipalFactory<User> ClaimsFactory { get; set; }

    /// <summary>
    ///     The <see cref="IdentityOptions" /> used.
    /// </summary>
    IdentityOptions Options { get; set; }

    /// <summary>
    ///     Validates the security stamp for the specified <paramref name="principal" /> against
    ///     the persisted stamp for the current user, as an asynchronous operation.
    /// </summary>
    /// <param name="principal">The principal whose stamp should be validated.</param>
    /// <returns>
    ///     The task object representing the asynchronous operation. The task will contain the User
    ///     if the stamp matches the persisted value, otherwise it will return false.
    /// </returns>
    Task<User> ValidateSecurityStampAsync(ClaimsPrincipal principal);

    /// <summary>
    ///     Attempts a password sign in for a user.
    /// </summary>
    /// <param name="user">The user to sign in.</param>
    /// <param name="password">The password to attempt to sign in with.</param>
    /// <param name="lockoutOnFailure">Flag indicating if the user account should be locked if the sign in fails.</param>
    /// <returns>
    ///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
    ///     for the sign-in attempt.
    /// </returns>
    /// <returns></returns>
    Task<SignInResult> CheckPasswordSignInAsync(User user, string password, bool lockoutOnFailure);

    /// <summary>
    ///     Returns a flag indicating if the current client browser has been remembered by two factor authentication
    ///     for the user attempting to login, as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user attempting to login.</param>
    /// <returns>
    ///     The task object representing the asynchronous operation containing true if the browser has been remembered
    ///     for the current user.
    /// </returns>
    Task<bool> IsTwoFactorClientRememberedAsync(User user);

    /// <summary>
    ///     Sets a flag on the browser to indicate the user has selected "Remember this browser" for two factor authentication
    ///     purposes,
    ///     as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user who choose "remember this browser".</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task RememberTwoFactorClientAsync(User user);

    /// <summary>
    ///     Clears the "Remember this browser flag" from the current browser, as an asynchronous operation.
    /// </summary>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task ForgetTwoFactorClientAsync();

    /// <summary>
    ///     Signs in the user without two factor authentication using a two factor recovery code.
    /// </summary>
    /// <param name="recoveryCode">The two factor recovery code.</param>
    /// <returns></returns>
    Task<SignInResult> TwoFactorRecoveryCodeSignInAsync(string recoveryCode);

    /// <summary>
    ///     Validates the sign in code from an authenticator app and creates and signs in the user, as an asynchronous
    ///     operation.
    /// </summary>
    /// <param name="code">The two factor authentication code to validate.</param>
    /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
    /// <param name="rememberClient">
    ///     Flag indicating whether the current browser should be remember, suppressing all further
    ///     two factor authentication prompts.
    /// </param>
    /// <returns>
    ///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
    ///     for the sign-in attempt.
    /// </returns>
    Task<SignInResult> TwoFactorAuthenticatorSignInAsync(string code, bool isPersistent, bool rememberClient);

    /// <summary>
    ///     Validates the two faction sign in code and creates and signs in the user, as an asynchronous operation.
    /// </summary>
    /// <param name="provider">The two factor authentication provider to validate the code against.</param>
    /// <param name="code">The two factor authentication code to validate.</param>
    /// <param name="isPersistent">Flag indicating whether the sign-in cookie should persist after the browser is closed.</param>
    /// <param name="rememberClient">
    ///     Flag indicating whether the current browser should be remember, suppressing all further
    ///     two factor authentication prompts.
    /// </param>
    /// <returns>
    ///     The task object representing the asynchronous operation containing the <see name="SignInResult" />
    ///     for the sign-in attempt.
    /// </returns>
    Task<SignInResult> TwoFactorSignInAsync(string provider, string code, bool isPersistent, bool rememberClient);

    /// <summary>
    ///     Gets the external login information for the current login, as an asynchronous operation.
    /// </summary>
    /// <param name="expectedXsrf">
    ///     Flag indication whether a Cross Site Request Forgery token was expected in the current
    ///     request.
    /// </param>
    /// <returns>
    ///     The task object representing the asynchronous operation containing the <see name="ExternalLoginInfo" />
    ///     for the sign-in attempt.
    /// </returns>
    Task<ExternalLoginInfo> GetExternalLoginInfoAsync(string expectedXsrf = null);

    /// <summary>
    ///     Stores any authentication tokens found in the external authentication cookie into the associated user.
    /// </summary>
    /// <param name="externalLogin">The information from the external login provider.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the
    ///     <see cref="IdentityResult" /> of the operation.
    /// </returns>
    Task<IdentityResult> UpdateExternalAuthenticationTokensAsync(ExternalLoginInfo externalLogin);

    /// <summary>
    ///     Returns a locked out SignInResult.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>A locked out SignInResult</returns>
    Task<SignInResult> LockedOut(User user);

    /// <summary>
    ///     Used to ensure that a user is allowed to sign in.
    /// </summary>
    /// <param name="user">The user</param>
    /// <returns>Null if the user should be allowed to sign in, otherwise the SignInResult why they should be denied.</returns>
    Task<SignInResult> PreSignInCheck(User user);

    /// <summary>
    ///     Used to reset a user's lockout count.
    /// </summary>
    /// <param name="user">The user</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the
    ///     <see cref="IdentityResult" /> of the operation.
    /// </returns>
    Task ResetLockout(User user);
}