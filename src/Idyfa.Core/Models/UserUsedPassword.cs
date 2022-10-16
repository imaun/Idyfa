namespace Idyfa.Core;

public class UserUsedPassword
{
    private UserUsedPassword() { }

    #region props

    public long Id { get; protected set; }
    
    public string UserId { get; protected set; }
    
    public string HashedPassword { get; protected set; }
    
    public DateTime CreateDate { get; protected set; }

    #endregion

    #region Builder
    
    public static UserUsedPassword New()
    {
        var userUsedPassword = new UserUsedPassword();
        userUsedPassword.CreateDate = DateTime.UtcNow;
        return userUsedPassword;
    }

    public UserUsedPassword WithHashedPassword(string hashedPassword)
    {
        HashedPassword = hashedPassword;
        return this;
    }

    public UserUsedPassword WithUserId(string userId)
    {
        UserId = userId;
        return this;
    }

    public static UserUsedPassword New(string userId, string hashedPassword) 
        => New().WithUserId(userId).WithHashedPassword(hashedPassword);

    #endregion
    
}