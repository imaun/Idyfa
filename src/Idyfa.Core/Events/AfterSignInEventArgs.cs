namespace Idyfa.Core.Events;

public class AfterSignInEventArgs
{
    public AfterSignInEventArgs(
        string id, string userName, UserStatus status,
        string? phoneNumber, string? email, string? displayName)
    {
        Id = id;
        UserName = userName;
        Status = status;
        PhoneNumber = phoneNumber;
        Email = email;
        DisplayName = displayName;
    }
    
    public string Id { get; }
    public string UserName { get; }
    public string? PhoneNumber { get; }
    public string? Email { get; }
    public string? DisplayName { get; }
    public UserStatus Status { get; }
}