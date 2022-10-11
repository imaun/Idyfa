using Idyfa.Core.Resources;
using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

/// <summary>
/// Customizing Error messages for the ASP.NET Identity.
/// </summary>
public class IdyfaErrorDescriber : IdentityErrorDescriber
{
    public override IdentityError ConcurrencyFailure()
        => new IdentityError
        {
            Code = nameof(ConcurrencyFailure),
            Description = IdyfaIdentityErrors.ConcurrencyFailure
        };

    public override IdentityError DefaultError()
        => new IdentityError
        {
            Code = nameof(DefaultError),
            Description = IdyfaIdentityErrors.DefaultError
        };

    public override IdentityError DuplicateEmail(string email)
        => new IdentityError
        {
            Code = nameof(DuplicateEmail),
            Description = string.Format(IdyfaIdentityErrors.DuplicateEmail, email)
        };

    public override IdentityError DuplicateRoleName(string role)
        => new IdentityError
        {
            Code = nameof(DuplicateRoleName),
            Description = string.Format(IdyfaIdentityErrors.DuplicateRoleName, role)
        };

    public override IdentityError DuplicateUserName(string userName)
        => new IdentityError
        {
            Code = nameof(DuplicateUserName),
            Description = string.Format(IdyfaIdentityErrors.DuplicateUserName, userName)
        };
    
}