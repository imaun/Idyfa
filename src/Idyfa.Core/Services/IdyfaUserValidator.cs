using Idyfa.Core.enums;
using Microsoft.AspNetCore.Identity;

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
        
        if (_options.UserNameType == UserNameType.UserName &&
            _options.Registration.UserNameIsRequired)
        {
            if (_options.Registration.UserNameMinLength.HasValue &&
                _options.Registration.UserNameMinLength < user.UserName.Length)
            {
                errors.Add(_errorDescriber.MinUserNameLength(_options.Registration.UserNameMinLength.Value));
                return IdentityResult.Failed(errors.ToArray());
            }

            if (_options.UserOptions.BannedUserNames.Any() &&
                _options.UserOptions.BannedUserNames.Contains(user.UserName))
            {
                errors.Add(_errorDescriber.UserNameIsBanned(user.UserName));
                return IdentityResult.Failed(errors.ToArray());
            }
        }

        return errors.Any()
            ? IdentityResult.Failed(errors.ToArray())
            : IdentityResult.Success;
    }
}