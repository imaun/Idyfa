using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts;

public interface IIdyfaUserRepository : IIdyfaBaseRepository<User, string>, IUserStore<User>, IUserEmailStore<User>
{
    Task<User> FindByUserNameAsync(
        string userName, CancellationToken cancellationToken = default);
    
    Task<UserRole> FindUserRoleAsync(
        string userId, string roleId, CancellationToken cancellationToken);
    
    Task<UserLogin> FindUserLoginAsync(
        string userId, string loginProvider, string providerKey, CancellationToken cancellationToken);
    
    Task AddToRoleAsync(
        User user, string normalizedRoleName, CancellationToken cancellationToken = default);
    
    Task RemoveFromRoleAsync(
        User user, string normalizedRoleName, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<string>> GetRolesAsync(User user, CancellationToken cancellationToken = default);
    
    Task<bool> IsInRoleAsync(
        User user, string normalizedRoleName, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<UserClaim>> GetClaimsAsync(
        User user, CancellationToken cancellationToken = default);
    
    Task AddClaimsAsync(
        User user, IEnumerable<UserClaim> claims, CancellationToken cancellationToken = default);
    
    Task ReplaceClaimAsync(
        User user, UserClaim claim, UserClaim newClaim, CancellationToken cancellationToken = default);
    
    Task RemoveClaimsAsync(
        User user, IEnumerable<UserClaim> claims, CancellationToken cancellationToken = default);
    
    Task<User> FindByEmailAsync(
        string email, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<User>> GetUsersInRoleAsync(
        string roleName, CancellationToken cancellationToken = default);

    Task AddUserTokenAsync(User user, UserToken token, CancellationToken cancellationToken = default);
    
    Task RemoveUserTokenAsync(UserToken token, CancellationToken cancellationToken = default);
}