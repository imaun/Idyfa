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

    public override IdentityError InvalidEmail(string email)
        => new IdentityError
        {
            Code = nameof(InvalidEmail),
            Description = string.Format(IdyfaIdentityErrors.InvalidEmail, email)
        };

    public override IdentityError InvalidRoleName(string role)
        => new IdentityError
        {
            Code = nameof(InvalidRoleName),
            Description = string.Format(IdyfaIdentityErrors.InvalidRoleName, role)
        };

    public override IdentityError InvalidToken()
        => new IdentityError
        {
            Code = nameof(InvalidToken),
            Description = IdyfaIdentityErrors.InvalidToken
        };

    public override IdentityError InvalidUserName(string userName)
        => new IdentityError
        {
            Code = nameof(InvalidUserName),
            Description = string.Format(IdyfaIdentityErrors.InvalidUserName, userName)
        };

    public override IdentityError LoginAlreadyAssociated()
        => new IdentityError
        {
            Code = nameof(LoginAlreadyAssociated),
            Description = IdyfaIdentityErrors.LoginAlreadyAssociated
        };

    public override IdentityError PasswordMismatch()
        => new IdentityError
        {
            Code = nameof(PasswordMismatch),
            Description = IdyfaIdentityErrors.PasswordMismatch
        };

    public override IdentityError PasswordRequiresDigit()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresDigit),
            Description = IdyfaIdentityErrors.PasswordRequiresDigit
        };

    public override IdentityError PasswordRequiresLower()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresLower),
            Description = IdyfaIdentityErrors.PasswordRequiresLower
        };

    public override IdentityError PasswordRequiresNonAlphanumeric()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresNonAlphanumeric),
            Description = IdyfaIdentityErrors.PasswordRequiresNonAlphanumeric
        };

    public override IdentityError PasswordRequiresUniqueChars(int uniqueChars)
        => new IdentityError
        {
            Code = nameof(PasswordRequiresUniqueChars),
            Description = IdyfaIdentityErrors.PasswordRequiresUniqueChars
        };
}