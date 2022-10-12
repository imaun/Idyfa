using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts;

/// <summary>
/// Main service to manager <see cref="User"/>s
/// </summary>
public interface IIdyfaUserManager
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
}