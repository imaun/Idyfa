using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Idyfa.Core.Contracts;

/// <summary>
/// Main service to manager <see cref="User"/>s
/// </summary>
public interface IIdyfaUserManager : IDisposable
{
    
    Task<IdentityResult> CreateAsync(User user);
    
    Task<IdentityResult> UpdateAsync(User user);

    Task<IdentityResult> DeleteAsync(User user);

    Task<User> FindByIdAsync(string userId);
    
    Task<User> FindByUserNameAsync(string userName);

    Task<bool> CheckPasswordAsync(User user, string password);
    
    Task<IdentityResult> AddPasswordAsync(User user, string password);
    
    Task<IdentityResult> ChangePasswordAsync(
        User user, string currentPassword, string newPassword);
    
    Task<IdentityResult> RemovePasswordAsync(User user);
    
    Task<string> GeneratePasswordResetTokenAsync(User user);
    
    Task<IdentityResult> ResetPasswordAsync(
        User user, string token, string newPassword);
    
    Task<IdentityResult> AddClaimAsync(User user, Claim claim);
    
    Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims);

    Task<IdentityResult> ReplaceClaimAsync(User user, Claim claim, Claim newClaim);

    Task<IdentityResult> RemoveClaimAsync(User user, Claim claim);

    Task<IdentityResult> RemoveClaimsAsync(User user, IEnumerable<Claim> claims);

    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(User user);
    
    Task<IdentityResult> AddToRoleAsync(User user, string roleName);
    
    Task<IdentityResult> AddToRolesAsync(User user, IEnumerable<string> roleNames);

    Task<IdentityResult> RemoveFromRoleAsync(User user, string role);
    
    Task<IReadOnlyCollection<string>> GetRolesAsync(User user);
    
    Task<bool> IsInRoleAsync(User user, string roleName);
    
    Task<User> FindByEmailAsync(string email);
    
    Task<string> GenerateEmailConfirmationTokenAsync(User user);
    
    Task<IdentityResult> ConfirmEmailAsync(User user, string token);
    
    Task<bool> IsEmailConfirmedAsync(User user);
    
    Task<string> GenerateChangeEmailTokenAsync(User user, string newEmail);
    
    Task<IdentityResult> ChangeEmailAsync(
        User user, string newEmail, string token);
    
    Task<IdentityResult> SetPhoneNumberAsync(User user, string phoneNumber);
    
    Task<IdentityResult> ChangePhoneNumberAsync(
        User user, string phoneNumber, string token);
    
    Task<bool> IsPhoneNumberConfirmedAsync(User user);
    
    Task<string> GenerateChangePhoneNumberTokenAsync(
        User user, string phoneNumber);
    
    Task<bool> VerifyChangePhoneNumberTokenAsync(
        User user, string token, string phoneNumber);
    
    Task<bool> VerifyUserTokenAsync(
        User user, string tokenProvider, string purpose, string token);
    
    Task<string> GenerateUserTokenAsync(
        User user, string tokenProvider, string purpose);
    
    Task<bool> VerifyTwoFactorTokenAsync(
        User user, string tokenProvider, string token);
    
    Task<string> GenerateTwoFactorTokenAsync(User user, string tokenProvider);
    
    Task<bool> GetTwoFactorEnabledAsync(User user);
    
    Task<IdentityResult> SetTwoFactorEnabledAsync(User user, bool enabled);
    
    Task<bool> IsLockedOutAsync(User user);
    
    Task<IdentityResult> SetLockoutEnabledAsync(User user, bool enabled);
    
    Task<bool> IsPreviouslyUsedPasswordAsync(User user, string newPassword);
    
    Task<bool> IsLastUserPasswordTooOldAsync(string userId);
 
    Task<DateTime?> GetLastUserPasswordChangeDateAsync(string userId);

    /// <summary>
    ///     The <see cref="ILogger" /> used to log messages from the manager.
    /// </summary>
    /// <value>
    ///     The <see cref="ILogger" /> used to log messages from the manager.
    /// </value>
    ILogger Logger { get; set; }

    /// <summary>
    ///     The <see cref="IPasswordHasher{TUser}" /> used to hash passwords.
    /// </summary>
    IPasswordHasher<User> PasswordHasher { get; set; }

    /// <summary>
    ///     The <see cref="IUserValidator{TUser}" /> used to validate users.
    /// </summary>
    IList<IUserValidator<User>> UserValidators { get; }

    /// <summary>
    ///     The <see cref="IPasswordValidator{TUser}" /> used to validate passwords.
    /// </summary>
    IList<IPasswordValidator<User>> PasswordValidators { get; }

    /// <summary>
    ///     The <see cref="ILookupNormalizer" /> used to normalize things like user and role names.
    /// </summary>
    ILookupNormalizer KeyNormalizer { get; set; }

    /// <summary>
    ///     The <see cref="IdentityErrorDescriber" /> used to generate error messages.
    /// </summary>
    IdentityErrorDescriber ErrorDescriber { get; set; }

    /// <summary>
    ///     The <see cref="IdentityOptions" /> used to configure Identity.
    /// </summary>
    IdentityOptions Options { get; set; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports authentication tokens.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports authentication tokens, otherwise false.
    /// </value>
    bool SupportsUserAuthenticationTokens { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports a user authenticator.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports a user authenticatior, otherwise false.
    /// </value>
    bool SupportsUserAuthenticatorKey { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports recovery codes.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports a user authenticatior, otherwise false.
    /// </value>
    bool SupportsUserTwoFactorRecoveryCodes { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports two factor authentication.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports user two factor authentication, otherwise false.
    /// </value>
    bool SupportsUserTwoFactor { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports user passwords.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports user passwords, otherwise false.
    /// </value>
    bool SupportsUserPassword { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports security stamps.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports user security stamps, otherwise false.
    /// </value>
    bool SupportsUserSecurityStamp { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports user roles.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports user roles, otherwise false.
    /// </value>
    bool SupportsUserRole { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports external logins.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports external logins, otherwise false.
    /// </value>
    bool SupportsUserLogin { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports user emails.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports user emails, otherwise false.
    /// </value>
    bool SupportsUserEmail { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports user telephone numbers.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports user telephone numbers, otherwise false.
    /// </value>
    bool SupportsUserPhoneNumber { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports user claims.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports user claims, otherwise false.
    /// </value>
    bool SupportsUserClaim { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports user lock-outs.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports user lock-outs, otherwise false.
    /// </value>
    bool SupportsUserLockout { get; }

    /// <summary>
    ///     Gets a flag indicating whether the backing user store supports returning
    ///     <see cref="IQueryable" /> collections of information.
    /// </summary>
    /// <value>
    ///     true if the backing user store supports returning <see cref="IQueryable" /> collections of
    ///     information, otherwise false.
    /// </value>
    bool SupportsQueryableUsers { get; }

    /// <summary>
    ///     Returns an IQueryable of users if the store is an IQueryableUserStore
    /// </summary>
    IQueryable<User> Users { get; }

    /// <summary>
    ///     Returns the Name claim value if present otherwise returns null.
    /// </summary>
    /// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
    /// <returns>The Name claim value, or null if the claim is not present.</returns>
    /// <remarks>The Name claim is identified by <see cref="ClaimsIdentity.DefaultNameClaimType" />.</remarks>
    string GetUserName(ClaimsPrincipal principal);

    /// <summary>
    ///     Returns the User ID claim value if present otherwise returns null.
    /// </summary>
    /// <param name="principal">The <see cref="ClaimsPrincipal" /> instance.</param>
    /// <returns>The User ID claim value, or null if the claim is not present.</returns>
    /// <remarks>The User ID claim is identified by <see cref="ClaimTypes.NameIdentifier" />.</remarks>
    string GetUserId(ClaimsPrincipal principal);

    /// <summary>
    ///     Returns the user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in
    ///     the principal or null.
    /// </summary>
    /// <param name="principal">The principal which contains the user id claim.</param>
    /// <returns>
    ///     The user corresponding to the IdentityOptions.ClaimsIdentity.UserIdClaimType claim in
    ///     the principal or null
    /// </returns>
    Task<User> GetUserAsync(ClaimsPrincipal principal);

    /// <summary>
    ///     Generates a value suitable for use in concurrency tracking.
    /// </summary>
    /// <param name="user">The user to generate the stamp for.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the security
    ///     stamp for the specified <paramref name="user" />.
    /// </returns>
    Task<string> GenerateConcurrencyStampAsync(User user);
    
    /// <summary>
    ///     Creates the specified <paramref name="user" /> in the backing store with given password,
    ///     as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user to create.</param>
    /// <param name="password">The password for the user to hash and store.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
    ///     of the operation.
    /// </returns>
    Task<IdentityResult> CreateAsync(User user, string password);

    /// <summary>
    ///     Finds and returns a user, if any, who has the specified user name.
    /// </summary>
    /// <param name="userName">The user name to search for.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the user matching the specified
    ///     <paramref name="userName" /> if it exists.
    /// </returns>
    Task<User> FindByNameAsync(string userName);

    /// <summary>
    ///     Normalize a key (user name, email) for consistent comparisons.
    /// </summary>
    /// <param name="email">The key to normalize.</param>
    /// <returns>A normalized value representing the specified <paramref name="email" />.</returns>
    string NormalizeEmail(string email);

    /// <summary>
    ///     Normalize a key (user name, email) for consistent comparisons.
    /// </summary>
    /// <param name="name">The key to normalize.</param>
    /// <returns>A normalized value representing the specified <paramref name="name" />.</returns>
    string NormalizeName(string name);

    /// <summary>
    ///     Updates the normalized user name for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose user name should be normalized and updated.</param>
    /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
    Task UpdateNormalizedUserNameAsync(User user);

    /// <summary>
    ///     Gets the user name for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose name should be retrieved.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the name for the specified
    ///     <paramref name="user" />.
    /// </returns>
    Task<string> GetUserNameAsync(User user);

    /// <summary>
    ///     Sets the given <paramref name="userName" /> for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose name should be set.</param>
    /// <param name="userName">The user name to set.</param>
    /// <returns>The <see cref="Task" /> that represents the asynchronous operation.</returns>
    Task<IdentityResult> SetUserNameAsync(User user, string userName);

    /// <summary>
    ///     Gets the user identifier for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose identifier should be retrieved.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the identifier for the
    ///     specified <paramref name="user" />.
    /// </returns>
    Task<string> GetUserIdAsync(User user);

    /// <summary>
    ///     Gets a flag indicating whether the specified <paramref name="user" /> has a password.
    /// </summary>
    /// <param name="user">The user to return a flag for, indicating whether they have a password or not.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, returning true if the specified
    ///     <paramref name="user" /> has a password
    ///     otherwise false.
    /// </returns>
    Task<bool> HasPasswordAsync(User user);

    Task<bool> HasPasswordAsync(int userId);

    /// <summary>
    ///     Returns a <see cref="PasswordVerificationResult" /> indicating the result of a password hash comparison.
    /// </summary>
    /// <param name="store">The store containing a user's password.</param>
    /// <param name="user">The user whose password should be verified.</param>
    /// <param name="password">The password to verify.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the
    ///     <see cref="PasswordVerificationResult" />
    ///     of the operation.
    /// </returns>
    Task<PasswordVerificationResult> VerifyPasswordAsync(IUserPasswordStore<User> store, User user, string password);

    /// <summary>
    ///     Get the security stamp for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose security stamp should be set.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the security stamp for the
    ///     specified <paramref name="user" />.
    /// </returns>
    Task<string> GetSecurityStampAsync(User user);

    /// <summary>
    ///     Regenerates the security stamp for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose security stamp should be regenerated.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
    ///     of the operation.
    /// </returns>
    /// <remarks>
    ///     Regenerating a security stamp will sign out any saved login for the user.
    /// </remarks>
    Task<IdentityResult> UpdateSecurityStampAsync(User user);
    

    /// <summary>
    ///     Retrieves the user associated with the specified external login provider and login provider key.
    /// </summary>
    /// <param name="loginProvider">The login provider who provided the <paramref name="providerKey" />.</param>
    /// <param name="providerKey">The key provided by the <paramref name="loginProvider" /> to identify a user.</param>
    /// <returns>
    ///     The <see cref="Task" /> for the asynchronous operation, containing the user, if any which matched the specified
    ///     login provider and key.
    /// </returns>
    Task<User> FindByLoginAsync(string loginProvider, string providerKey);

    /// <summary>
    ///     Attempts to remove the provided external login information from the specified <paramref name="user" />.
    ///     and returns a flag indicating whether the removal succeed or not.
    /// </summary>
    /// <param name="user">The user to remove the login information from.</param>
    /// <param name="loginProvider">The login provide whose information should be removed.</param>
    /// <param name="providerKey">The key given by the external login provider for the specified user.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
    ///     of the operation.
    /// </returns>
    Task<IdentityResult> RemoveLoginAsync(User user, string loginProvider, string providerKey);

    /// <summary>
    ///     Adds an external <see cref="UserLoginInfo" /> to the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user to add the login to.</param>
    /// <param name="login">The external <see cref="UserLoginInfo" /> to add to the specified <paramref name="user" />.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
    ///     of the operation.
    /// </returns>
    Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo login);

    /// <summary>
    ///     Retrieves the associated logins for the specified
    ///     <param ref="user" />
    ///     .
    /// </summary>
    /// <param name="user">The user whose associated logins to retrieve.</param>
    /// <returns>
    ///     The <see cref="Task" /> for the asynchronous operation, containing a list of <see cref="UserLoginInfo" /> for the
    ///     specified <paramref name="user" />, if any.
    /// </returns>
    Task<IList<UserLoginInfo>> GetLoginsAsync(User user);
    

    /// <summary>
    ///     Removes the specified <paramref name="user" /> from the named roles.
    /// </summary>
    /// <param name="user">The user to remove from the named roles.</param>
    /// <param name="roles">The name of the roles to remove the user from.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
    ///     of the operation.
    /// </returns>
    Task<IdentityResult> RemoveFromRolesAsync(User user, IEnumerable<string> roles);
    

    /// <summary>
    ///     Gets the email address for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose email should be returned.</param>
    /// <returns>
    ///     The task object containing the results of the asynchronous operation, the email address for the specified
    ///     <paramref name="user" />.
    /// </returns>
    Task<string> GetEmailAsync(User user);

    /// <summary>
    ///     Sets the <paramref name="email" /> address for a <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose email should be set.</param>
    /// <param name="email">The email to set.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the <see cref="IdentityResult" />
    ///     of the operation.
    /// </returns>
    Task<IdentityResult> SetEmailAsync(User user, string email);

    /// <summary>
    ///     Updates the normalized email for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose email address should be normalized and updated.</param>
    /// <returns>The task object representing the asynchronous operation.</returns>
    Task UpdateNormalizedEmailAsync(User user);
    

    /// <summary>
    ///     Gets the telephone number, if any, for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose telephone number should be retrieved.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the user's telephone number, if
    ///     any.
    /// </returns>
    Task<string> GetPhoneNumberAsync(User user);

    /// <summary>
    ///     Registers a token provider.
    /// </summary>
    /// <param name="providerName">The name of the provider to register.</param>
    /// <param name="provider">The provider to register.</param>
    void RegisterTokenProvider(string providerName, IUserTwoFactorTokenProvider<User> provider);

    /// <summary>
    ///     Gets a list of valid two factor token providers for the specified <paramref name="user" />,
    ///     as an asynchronous operation.
    /// </summary>
    /// <param name="user">The user the whose two factor authentication providers will be returned.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents result of the asynchronous operation, a list of two
    ///     factor authentication providers for the specified user.
    /// </returns>
    Task<IList<string>> GetValidTwoFactorProvidersAsync(User user);

    /// <summary>
    ///     Retrieves a flag indicating whether user lockout can enabled for the specified user.
    /// </summary>
    /// <param name="user">The user whose ability to be locked out should be returned.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, true if a user can be locked out, otherwise
    ///     false.
    /// </returns>
    Task<bool> GetLockoutEnabledAsync(User user);

    /// <summary>
    ///     Gets the last <see cref="DateTimeOffset" /> a user's last lockout expired, if any.
    ///     Any time in the past should be indicates a user is not locked out.
    /// </summary>
    /// <param name="user">The user whose lockout date should be retrieved.</param>
    /// <returns>
    ///     A <see cref="Task{TResult}" /> that represents the lookup, a <see cref="DateTimeOffset" /> containing the last time
    ///     a user's lockout expired, if any.
    /// </returns>
    Task<DateTimeOffset?> GetLockoutEndDateAsync(User user);

    /// <summary>
    ///     Locks out a user until the specified end date has passed. Setting a end date in the past immediately unlocks a
    ///     user.
    /// </summary>
    /// <param name="user">The user whose lockout date should be set.</param>
    /// <param name="lockoutEnd">
    ///     The <see cref="DateTimeOffset" /> after which the <paramref name="user" />'s lockout should
    ///     end.
    /// </param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the
    ///     <see cref="IdentityResult" /> of the operation.
    /// </returns>
    Task<IdentityResult> SetLockoutEndDateAsync(User user, DateTimeOffset? lockoutEnd);

    /// <summary>
    ///     Increments the access failed count for the user as an asynchronous operation.
    ///     If the failed access account is greater than or equal to the configured maximum number of attempts,
    ///     the user will be locked out for the configured lockout time span.
    /// </summary>
    /// <param name="user">The user whose failed access count to increment.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the
    ///     <see cref="IdentityResult" /> of the operation.
    /// </returns>
    Task<IdentityResult> AccessFailedAsync(User user);

    /// <summary>
    ///     Resets the access failed count for the specified <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose failed access count should be reset.</param>
    /// <returns>
    ///     The <see cref="Task" /> that represents the asynchronous operation, containing the
    ///     <see cref="IdentityResult" /> of the operation.
    /// </returns>
    Task<IdentityResult> ResetAccessFailedCountAsync(User user);

    /// <summary>
    ///     Retrieves the current number of failed accesses for the given <paramref name="user" />.
    /// </summary>
    /// <param name="user">The user whose access failed count should be retrieved for.</param>
    /// <returns>
    ///     The <see cref="Task" /> that contains the result the asynchronous operation, the current failed access count
    ///     for the user.
    /// </returns>
    Task<int> GetAccessFailedCountAsync(User user);

    /// <summary>
    ///     Returns a list of users from the user store who have the specified <paramref name="claim" />.
    /// </summary>
    /// <param name="claim">The claim to look for.</param>
    /// <returns>
    ///     A <see cref="Task{TResult}" /> that represents the result of the asynchronous query, a list of
    ///     TUsers who have the specified claim.
    /// </returns>
    Task<IList<User>> GetUsersForClaimAsync(Claim claim);

    /// <summary>
    ///     Returns a list of users from the user store who are members of the specified <paramref name="roleName" />.
    /// </summary>
    /// <param name="roleName">The name of the role whose users should be returned.</param>
    /// <returns>
    ///     A <see cref="Task{TResult}" /> that represents the result of the asynchronous query, a list of
    ///     TUsers who are members of the specified role.
    /// </returns>
    Task<IList<User>> GetUsersInRoleAsync(string roleName);

    /// <summary>
    ///     Returns an authentication token for a user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="loginProvider">The authentication scheme for the provider the token is associated with.</param>
    /// <param name="tokenName">The name of the token.</param>
    /// <returns>The authentication token for a user</returns>
    Task<string> GetAuthenticationTokenAsync(User user, string loginProvider, string tokenName);

    /// <summary>
    ///     Sets an authentication token for a user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="loginProvider">The authentication scheme for the provider the token is associated with.</param>
    /// <param name="tokenName">The name of the token.</param>
    /// <param name="tokenValue">The value of the token.</param>
    /// <returns>Whether the user was successfully updated.</returns>
    Task<IdentityResult> SetAuthenticationTokenAsync(User user, string loginProvider, string tokenName,
        string tokenValue);

    /// <summary>
    ///     Remove an authentication token for a user.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="loginProvider">The authentication scheme for the provider the token is associated with.</param>
    /// <param name="tokenName">The name of the token.</param>
    /// <returns>Whether a token was removed.</returns>
    Task<IdentityResult> RemoveAuthenticationTokenAsync(User user, string loginProvider, string tokenName);

    /// <summary>
    ///     Returns the authenticator key for the user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The authenticator key</returns>
    Task<string> GetAuthenticatorKeyAsync(User user);

    /// <summary>
    ///     Resets the authenticator key for the user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>Whether the user was successfully updated.</returns>
    Task<IdentityResult> ResetAuthenticatorKeyAsync(User user);

    /// <summary>
    ///     Generates a new base32 encoded 160-bit security secret (size of SHA1 hash).
    /// </summary>
    /// <returns>The new security secret.</returns>
    string GenerateNewAuthenticatorKey();

    /// <summary>
    ///     Generates recovery codes for the user, this invalidates any previous recovery codes for the user.
    /// </summary>
    /// <param name="user">The user to generate recovery codes for.</param>
    /// <param name="number">The number of codes to generate.</param>
    /// <returns>
    ///     The new recovery codes for the user.  Note: there may be less than number returned, as duplicates will be
    ///     removed.
    /// </returns>
    Task<IEnumerable<string>> GenerateNewTwoFactorRecoveryCodesAsync(User user, int number);

    /// <summary>
    ///     Generate a new recovery code.
    /// </summary>
    /// <returns></returns>
    string CreateTwoFactorRecoveryCode();

    /// <summary>
    ///     Returns whether a recovery code is valid for a user. Note: recovery codes are only valid
    ///     once, and will be invalid after use.
    /// </summary>
    /// <param name="user">The user who owns the recovery code.</param>
    /// <param name="code">The recovery code to use.</param>
    /// <returns>True if the recovery code was found for the user.</returns>
    Task<IdentityResult> RedeemTwoFactorRecoveryCodeAsync(User user, string code);

    /// <summary>
    ///     Returns how many recovery code are still valid for a user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>How many recovery code are still valid for a user.</returns>
    Task<int> CountRecoveryCodesAsync(User user);

    /// <summary>
    ///     Creates bytes to use as a security token from the user's security stamp.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns>The security token bytes.</returns>
    Task<byte[]> CreateSecurityTokenAsync(User user);

    /// <summary>
    ///     Updates a user's password hash.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="newPassword">The new password.</param>
    /// <param name="validatePassword">Whether to validate the password.</param>
    /// <returns>Whether the password has was successfully updated.</returns>
    Task<IdentityResult> UpdatePasswordHash(User user, string newPassword, bool validatePassword);
}