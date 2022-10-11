using Microsoft.AspNetCore.Identity;
using Idyfa.Core.Extensions;

namespace Idyfa.Core.Services;

public class IdyfaUserValidator : UserValidator<User>
{
    private readonly IdyfaOptions _options;
    private readonly IdyfaErrorDescriber _errorDescriber;
    private readonly IdyfaUserOptions _userOptions;
    
    public IdyfaUserValidator(
        IdyfaErrorDescriber errorDescriber,
        IdyfaOptions options): base(errorDescriber)
    {
        _errorDescriber = errorDescriber ?? throw new ArgumentNullException(nameof(errorDescriber));
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _userOptions = options.UserOptions;
    }
    
    public override async Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user)
    {
        var result = await base.ValidateAsync(manager, user);
        var errors = result.Succeeded ? new List<IdentityError>() : result.Errors.ToList();
        var invalidChars = _userOptions.InvalidCharacters.ToArray();
        
        if (_options.UserNameType == UserNameType.UserName &&
            _options.Registration.UserNameIsRequired)
        {
            if (_options.Registration.UserNameMinLength.HasValue &&
                _options.Registration.UserNameMinLength < user.UserName.Length)
            {
                errors.Add(_errorDescriber.MinUserNameLength(
                    _options.Registration.UserNameMinLength.Value));
                return IdentityResult.Failed(errors.ToArray());
            }

            if (invalidChars.Any(user.UserName.Contains))
            {
                //TODO : error invalid chars
            }

            if (_userOptions.BannedUserNames.Any() &&
                _userOptions.BannedUserNames.Contains(user.UserName))
            {
                errors.Add(_errorDescriber.UserNameIsBanned(user.UserName));
                return IdentityResult.Failed(errors.ToArray());
            }
        }

        if (_options.UserNameType == UserNameType.Email)
        {
            if (_options.Registration.EmailIsRequired && user.Email.IsNullOrEmpty())
            {
                errors.Add(_errorDescriber.EmailIsRequired());
                return IdentityResult.Failed(errors.ToArray());
            }

            if (invalidChars.Any(user.Email.Contains))
            {
                //TODO : Error
            }
            
            if (_userOptions.BannedEmails.Any() &&
                _userOptions.BannedUserNames.Contains(user.Email))
            {
                errors.Add(_errorDescriber.EmailIsBanned(user.Email));
                return IdentityResult.Failed(errors.ToArray());
            }
        }

        if (_options.UserNameType == UserNameType.PhoneNumber)
        {
            if (_options.Registration.PhoneNumberIsRequired &&
                string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                errors.Add(_errorDescriber.PhoneNumberIsRequired());
                return IdentityResult.Failed(errors.ToArray());
            }

            if (invalidChars.Any(user.PhoneNumber.Contains))
            {
                //TODO : error
            }
            
            if (!user.PhoneNumber.IsDigit() ||
                !user.PhoneNumber.IsAValidIranianPhoneNumber())
            {
                errors.Add(_errorDescriber.InvalidPhoneNumberFormat);
                return IdentityResult.Failed(errors.ToArray());
            }
        }
        
        return errors.Any()
            ? IdentityResult.Failed(errors.ToArray())
            : IdentityResult.Success;
    }
    
}