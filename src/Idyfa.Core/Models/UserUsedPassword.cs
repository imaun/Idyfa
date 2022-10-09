namespace Idyfa.Core;

public class UserUsedPassword
{
    protected UserUsedPassword() { }
    
    public string UserId { get; protected set; }
    
    public string HashedPassword { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }
}