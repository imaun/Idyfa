using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class RoleClaim : IdentityRoleClaim<string>
{

    protected RoleClaim() : base()
    {
    }

    public static RoleClaim New(string roleId, string claimType, string claimValue)
        => new RoleClaim
        {
            RoleId = roleId,
            ClaimType = claimType,
            ClaimValue = claimValue
        };
}