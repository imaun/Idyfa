using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class UserClaim : IdentityUserClaim<string>
{
    public UserClaim() { }

    public static UserClaim New(string userId, string claimType, string claimValue)
    {
        return new UserClaim
        {
            UserId = userId,
            ClaimType = claimType,
            ClaimValue = claimValue
        };
    }
    
}