using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class UserLogin : IdentityUserLogin<string>
{
    protected UserLogin()
    {
    }
    
}