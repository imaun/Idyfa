using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class UserRole : IdentityUserRole<string>
{
    
    public Role Role { get; protected set; }
    
    public User User { get; protected set; }
}