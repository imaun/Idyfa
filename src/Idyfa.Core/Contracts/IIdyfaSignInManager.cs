using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;

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
    
}