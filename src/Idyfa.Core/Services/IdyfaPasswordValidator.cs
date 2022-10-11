using Idyfa.Core.Contracts;
using Idyfa.Core.enums;
using Idyfa.Core.Exceptions;
using Idyfa.Core.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Idyfa.Core.Services;

public class IdyfaPasswordValidator : PasswordValidator<User>
{
    private readonly IdyfaOptions _options;
    private readonly IIdyfaUsedPasswordManager _usedPasswordManager;

    public IdyfaPasswordValidator(
        IdentityErrorDescriber errorDescriber,
        IdyfaOptions options,
        IIdyfaUsedPasswordManager usedPasswordManager)
    {
        _options = options ?? throw new IdyfaOptionsNotFoundException();
        _usedPasswordManager = usedPasswordManager ?? throw new ArgumentNullException(nameof(usedPasswordManager));
    }
    
    public override async Task<IdentityResult> ValidateAsync(
        UserManager<User> manager, User user, string password)
    {
        var errors = new List<IdentityError>();

        if (_options.Registration.PasswordIsRequired && password.IsNullOrEmpty())
        {
            errors.Add(IdyfaIdentityErrorProvider.PasswordIsNotSet);
            return IdentityResult.Failed(errors.ToArray());
        }

        if (_options.Registration.UserNameIsRequired)
        {
            if (user.GetUserName(UserNameType.UserName).IsNullOrEmpty() ||
                user.GetUserName(UserNameType.Email).IsNullOrEmpty() ||
                user.GetUserName(UserNameType.PhoneNumber).IsNullOrEmpty())
            {
                errors.Add(IdyfaIdentityErrorProvider.UserNameIsNotSet);
                return IdentityResult.Failed(errors.ToArray());
            }
        }
        
        var result = await base.ValidateAsync(manager, user, password);
        errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();
        
        var passwordOptions = _options.PasswordOptions;
        if (!passwordOptions.CanIncludeUserName && 
            user.GetUserName(_options.UserNameType).IsNullOrEmpty())
        {
            errors.Add(IdyfaIdentityErrorProvider.PasswordContainsUserName);
            return IdentityResult.Failed(errors.ToArray());
        }

        if (passwordOptions.BannedPasswords.Any() && 
            passwordOptions.BannedPasswords.Contains(password))
        {
            errors.Add(IdyfaIdentityErrorProvider.UseOfBannedPassword);
            return IdentityResult.Failed(errors.ToArray());
        }

        if (passwordOptions.MaxLength.HasValue &&
            password.Length > passwordOptions.MaxLength.Value)
        {
            errors.Add(IdyfaIdentityErrorProvider.PasswordIsTooLong);
            return IdentityResult.Failed(errors.ToArray());
        }

        if (IsPasswordToSimple(password))
        {
            errors.Add(IdyfaIdentityErrorProvider.PasswordIsTooSimple);
            return IdentityResult.Failed(errors.ToArray());
        }
        
        return errors.Any() 
            ? IdentityResult.Failed(errors.ToArray()) 
            : IdentityResult.Success;
    }

    private bool IsAllCharsTheSame(string password)
    {
        if (password.IsNullOrEmpty()) return false;
        password = password.ToLowerInvariant();
        
        return password.ToCharArray()
            .Count(_ => _ == password.ElementAt(0)) == password.Length;
    }

    private bool IsPasswordToSimple(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            return true;

        if (IsAllCharsTheSame(password))
            return true;

        if (_options.Password.RequiredLength < password.Length)
            return true;

        return false;
    }
}