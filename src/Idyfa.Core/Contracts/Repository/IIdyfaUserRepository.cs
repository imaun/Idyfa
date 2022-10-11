using System.Security.Claims;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts.Repository;

public interface IIdyfaUserRepository : IIdyfaBaseRepository<User, string>, IUserStore<User>
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
    
    Task<IReadOnlyCollection<Claim>> GetClaimsAsync(
        User user, CancellationToken cancellationToken = default);
    
    Task AddClaimsAsync(
        User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default);
    
    Task ReplaceClaimAsync(
        User user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default);
    
    Task RemoveClaimsAsync(
        User user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default);
    
    Task<User> FindByEmailAsync(
        string email, CancellationToken cancellationToken = default);
    
    Task<IReadOnlyCollection<User>> GetUsersInRoleAsync(
        string roleName, CancellationToken cancellationToken = default);

    Task AddUserTokenAsync(UserToken token);
    
    Task RemoveUserTokenAsync(UserToken token);
}