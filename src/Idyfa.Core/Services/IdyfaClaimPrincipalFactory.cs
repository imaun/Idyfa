using System.Security.Claims;
using System.Security.Principal;
using Idyfa.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Idyfa.Core.Services;

/// <summary>
/// Customize claims for <see cref="User"/>
/// </summary>
public class IdyfaClaimPrincipalFactory : UserClaimsPrincipalFactory<User, Role>
{
    public IdyfaClaimPrincipalFactory(
        IIdyfaUserManager userManager, 
        IIdyfaRoleManager roleManager, 
        IOptions<IdentityOptions> options) 
        : base((UserManager<User>)userManager, (RoleManager<Role>)roleManager, options)
    {
    }
    
    public override async Task<ClaimsPrincipal> CreateAsync(User user)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        var principal = await base.CreateAsync(user);
        addCustomClaims(user, principal);

        return principal;
    }
    
    private static void addCustomClaims(User user, IPrincipal principal)
    {
        if (user is null)
            throw new ArgumentNullException(nameof(user));

        if (principal is null)
            throw new ArgumentNullException(nameof(principal));

        if (principal.Identity is null)
            throw new ArgumentNullException(nameof(principal.Identity));
        
        ((ClaimsIdentity)principal.Identity)?.AddClaims(new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.GivenName, user.FirstName!),
            new Claim(ClaimTypes.Surname, user.LastName!),
        });
    }
    
}