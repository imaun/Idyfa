using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Services;

public class IdyfaTwoFactorPhoneTokenProvider<TUser> 
    : PhoneNumberTokenProvider<TUser> where TUser : class
{
    
    public IdyfaTwoFactorPhoneTokenProvider()
    {
    }
}

public class IdyfaTwoFactorPhoneTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public IdyfaTwoFactorPhoneTokenProviderOptions()
    {
        Name = "IdyfaSms";
        TokenLifespan = TimeSpan.FromMinutes(2);
    }
}


