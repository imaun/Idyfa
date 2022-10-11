using Idyfa.Core.Contracts;
using Idyfa.Core.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Idyfa.Core.Services;

public class IdyfaPasswordValidator : PasswordValidator<User>
{
    private readonly ISet<string> _bannedPasswords;
    private readonly IIdyfaUsedPasswordManager _usedPasswordManager;

    public IdyfaPasswordValidator(
        IdentityErrorDescriber errorDescriber,
        IdyfaSetting idyfaSetting,
        IIdyfaUsedPasswordManager usedPasswordManager)
    {
        if (idyfaSetting is null)
            throw new IdyfaSettingNotFoundException();
        
        _bannedPasswords = idyfaSetting.BannedPasswords;
        _usedPasswordManager = usedPasswordManager ?? throw new ArgumentNullException(nameof(usedPasswordManager));
    }
    
    public override Task<IdentityResult> ValidateAsync(UserManager<User> manager, User user, string password)
    {
        var errors = new List<IdentityError>();
        
        return base.ValidateAsync(manager, user, password);
    }
}