using Idyfa.Core.Contracts;
using Microsoft.AspNetCore.Identity;
using Idyfa.Core.Extensions;

namespace Idyfa.Core.Services;

public class IdyfaUserValidator : UserValidator<User>, IIdyfaUserValidator
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
        
        errors.AddRange(ValidateUserName(user.UserName));
        
        return errors.Any()
            ? IdentityResult.Failed(errors.ToArray())
            : IdentityResult.Success;
        
        if (_options.UserOptions.UserNameType == UserNameType.UserName &&
            _options.Registration.UserNameIsRequired)
        {
            if (_options.Registration.UserNameMinLength.HasValue &&
                _options.Registration.UserNameMinLength < user.UserName.Length)
            {
                errors.Add(_errorDescriber.MinUserNameLength(
                    _options.Registration.UserNameMinLength.Value));
                return IdentityResult.Failed(errors.ToArray());
            }
            
            //TODO : check valid username chars
            
            if (_userOptions.BannedUserNames.Any() &&
                _userOptions.BannedUserNames.Contains(user.UserName))
            {
                errors.Add(_errorDescriber.UserNameIsBanned(user.UserName));
                return IdentityResult.Failed(errors.ToArray());
            }
        }

        if (_options.UserOptions.UserNameType == UserNameType.Email)
        {
            if (_options.Registration.EmailIsRequired && user.Email.IsNullOrEmpty())
            {
                errors.Add(_errorDescriber.EmailIsRequired());
                return IdentityResult.Failed(errors.ToArray());
            }

            if (_userOptions.BannedEmails.Any() &&
                _userOptions.BannedUserNames.Contains(user.Email))
            {
                errors.Add(_errorDescriber.EmailIsBanned(user.Email));
                return IdentityResult.Failed(errors.ToArray());
            }
        }

        if (_options.UserOptions.UserNameType == UserNameType.PhoneNumber)
        {
            if (_options.Registration.PhoneNumberIsRequired &&
                string.IsNullOrWhiteSpace(user.PhoneNumber))
            {
                errors.Add(_errorDescriber.PhoneNumberIsRequired());
                return IdentityResult.Failed(errors.ToArray());
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
    
    public IdentityError[] ValidateUserName(string userName)
    {
        var errors = new List<IdentityError>();

        if (string.IsNullOrWhiteSpace(userName))
        {
            errors.Add(_errorDescriber.UserNameIsNotSet());
            return errors.ToArray();
        }
        
        if (_options.UserOptions.UserNameType == UserNameType.UserName &&
            _options.Registration.UserNameIsRequired)
        {
            if (_options.Registration.UserNameMinLength.HasValue &&
                _options.Registration.UserNameMinLength < userName.Length)
            {
                errors.Add(_errorDescriber.MinUserNameLength(
                    _options.Registration.UserNameMinLength.Value));
                return errors.ToArray();
            }
            
            //TODO : check valid username chars
            
            if (_userOptions.BannedUserNames != null! && _userOptions.BannedUserNames.Any() &&
                _userOptions.BannedUserNames.Contains(userName))
            {
                errors.Add(_errorDescriber.UserNameIsBanned(userName));
                return errors.ToArray();
            }
        }
        
        if (_options.UserOptions.UserNameType == UserNameType.Email)
        {
            if (_options.Registration.EmailIsRequired && userName.IsNullOrEmpty())
            {
                errors.Add(_errorDescriber.EmailIsRequired());
                return errors.ToArray();
            }

            if (_userOptions.BannedEmails != null! && _userOptions.BannedEmails.Any() &&
                _userOptions.BannedEmails.Contains(userName))
            {
                errors.Add(_errorDescriber.EmailIsBanned(userName));
                return errors.ToArray();
            }
        }
        
        if (_options.UserOptions.UserNameType == UserNameType.PhoneNumber)
        {
            if (_options.Registration.PhoneNumberIsRequired &&
                string.IsNullOrWhiteSpace(userName))
            {
                errors.Add(_errorDescriber.PhoneNumberIsRequired());
                return errors.ToArray();
            }

            if (!userName.IsDigit() ||
                !userName.IsAValidIranianPhoneNumber())
            {
                errors.Add(_errorDescriber.InvalidPhoneNumberFormat);
                return errors.ToArray();
            }
        }
        
        return errors.ToArray();
    }
    
}