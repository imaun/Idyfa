namespace Idyfa.Core;

public class UserLoginRecord
{

    public UserLoginRecord()
    {
    }
    
    
    #region props

    public long Id { get; protected set; }
    
    public string UserId { get; protected set; }
    
    public string? LoginUrl { get; protected set; }
    
    public string? IpAddress { get; protected set; }
    
    public string? HostName { get; protected set; }
    
    public string? UserAgent { get; protected set; }
    
    public string? OsName { get; protected set; }
    
    public string? Country { get; protected set; }
    
    public string? City { get; protected set; }

    public string? ExtraInfo { get; protected set; }
    
    
    #endregion
}