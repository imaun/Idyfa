using System.Globalization;
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
            Description = string.Format(IdyfaIdentityErrors.PasswordRequiresUniqueChars, uniqueChars)
        };

    public override IdentityError PasswordRequiresUpper()
        => new IdentityError
        {
            Code = nameof(PasswordRequiresUpper),
            Description = IdyfaIdentityErrors.PasswordRequiresUpper
        };

    public override IdentityError PasswordTooShort(int length)
        => new IdentityError
        {
            Code = nameof(PasswordTooShort),
            Description = string.Format(
                CultureInfo.InvariantCulture,
                IdyfaIdentityErrors.PasswordTooShort, length)
        };

    public override IdentityError RecoveryCodeRedemptionFailed()
        => new IdentityError
        {
            Code = nameof(RecoveryCodeRedemptionFailed),
            Description = IdyfaIdentityErrors.RecoveryCodeRedemptionFailed
        };

    public override IdentityError UserAlreadyHasPassword()
        => new IdentityError
        {
            Code = nameof(UserAlreadyHasPassword),
            Description = IdyfaIdentityErrors.UserAlreadyHasPassword
        };

    public override IdentityError UserAlreadyInRole(string role)
        => new IdentityError
        {
            Code = nameof(UserAlreadyInRole),
            Description = string.Format(
                CultureInfo.InvariantCulture,
                IdyfaIdentityErrors.UserAlreadyInRole, role)
        };

    public override IdentityError UserLockoutNotEnabled()
        => new IdentityError
        {
            Code = nameof(UserLockoutNotEnabled),
            Description = string.Format(IdyfaIdentityErrors.UserLockoutNotEnabled)
        };

    public override IdentityError UserNotInRole(string role)
        => new IdentityError
        {
            Code = nameof(UserNotInRole),
            Description = string.Format(
                CultureInfo.InvariantCulture,
                IdyfaIdentityErrors.UserNotInRole, role)
        };

    public IdentityError MinUserNameLength(int minLength)
        => new IdentityError
        {
            Code = nameof(MinUserNameLength),
            Description = string.Format(IdyfaIdentityErrors.MinUserNameLength, minLength)
        };

    public IdentityError UserNameIsBanned(string userName)
        => new IdentityError
        {
            Code = nameof(UserNameIsBanned),
            Description = string.Format(IdyfaIdentityErrors.UserNameIsBanned, userName)
        };

    public IdentityError EmailIsBanned(string email)
        => new IdentityError
        {
            Code = nameof(EmailIsBanned),
            Description = string.Format(IdyfaIdentityErrors.EmailIsBanned, email)
        };

    public IdentityError EmailIsRequired()
        => new IdentityError
        {
            Code = nameof(EmailIsRequired),
            Description = IdyfaIdentityErrors.EmailIsRequired
        };

    public IdentityError PhoneNumberIsRequired()
        => new IdentityError
        {
            Code = nameof(PhoneNumberIsRequired),
            Description = IdyfaIdentityErrors.PhoneNumberIsRequired
        };
}

public static class IdyfaIdentityErrorProvider
{

    public static IdentityError PasswordIsNotSet
        => new IdentityError
        {
            Code = nameof(PasswordIsNotSet),
            Description = IdyfaIdentityErrors.PasswordIsNotSet
        };

    public static IdentityError UserNameIsNotSet
        => new IdentityError
        {
            Code = nameof(UserNameIsNotSet),
            Description = IdyfaIdentityErrors.UserNameIsNotSet
        };

    public static IdentityError PasswordContainsUserName
        => new IdentityError
        {
            Code = nameof(PasswordContainsUserName),
            Description = IdyfaIdentityErrors.PasswordContainsUserName
        };

    public static IdentityError PasswordIsTooSimple
        => new IdentityError
        {
            Code = nameof(PasswordIsTooSimple),
            Description = IdyfaIdentityErrors.PasswordIsTooSimple
        };

    public static IdentityError IsPreviouslyUsedPassword
        => new IdentityError
        {
            Code = nameof(IsPreviouslyUsedPassword),
            Description = IdyfaIdentityErrors.IsPreviouslyUsedPassword
        };

    public static IdentityError UseOfBannedPassword
        => new IdentityError
        {
            Code = nameof(UseOfBannedPassword),
            Description = IdyfaIdentityErrors.UseOfBannedPassword
        };

    public static IdentityError PasswordIsTooLong
        => new IdentityError
        {
            Code = nameof(PasswordIsTooLong),
            Description = IdyfaIdentityErrors.PasswordIsTooLong
        };
}