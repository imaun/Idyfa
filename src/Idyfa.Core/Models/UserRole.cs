using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class UserRole : IdentityUserRole<string>
{
    protected UserRole() { }

    public static UserRole New() => new();

    public static UserRole New(string userId, string roleId)
        => New().WithUserId(userId).WithRoleId(roleId);
    
    public UserRole WithUserId(string userId)
    {
        UserId = userId;
        return this;
    }

    public UserRole WithRoleId(string roleId)
    {
        RoleId = roleId;
        return this;
    }
    
    
}