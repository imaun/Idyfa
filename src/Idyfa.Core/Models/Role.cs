using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class Role : IdentityRole<string>
{
    
    public string Title { get; protected set; }
    
    public IEnumerable<RoleClaim> Claims { get; protected set; }
}