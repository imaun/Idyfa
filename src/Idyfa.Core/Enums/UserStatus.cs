namespace Idyfa.Core;

public enum UserStatus
{
    /// <summary>
    /// SoftDeleted User
    /// </summary>
    Deleted = -1,
    
    /// <summary>
    /// The Default Status for the <see cref="User"/>
    /// </summary>
    Created = 0,
    
    Enabled = 1,
    
    Locked = 2,
    
    Blocked = 3
}