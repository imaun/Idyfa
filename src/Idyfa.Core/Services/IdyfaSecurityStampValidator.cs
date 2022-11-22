using Idyfa.Core.Contracts;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Idyfa.Core.Services;

public class IdyfaSecurityStampValidator : SecurityStampValidator<User>
{
    private readonly ISystemClock _clock;
    private readonly IOptions<SecurityStampValidatorOptions> _options;
    private readonly IIdyfaSignInManager _signInManager;
    
    public IdyfaSecurityStampValidator(
        IOptions<SecurityStampValidatorOptions> options, IIdyfaSignInManager signInManager, 
        ISystemClock clock, ILoggerFactory loggerFactory
        ) : base(options, (SignInManager<User>)signInManager, clock, loggerFactory)
    {
        _options = options ?? throw new ArgumentNullException(nameof(options));
        _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        _clock = clock ?? throw new ArgumentNullException(nameof(clock));
    }

    public override async Task ValidateAsync(CookieValidatePrincipalContext context)
    {
        await base.ValidateAsync(context);
        //TODO : Update User's LastVisitDate here to keep record of online users.
    }
    
}