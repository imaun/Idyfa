using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core;

public class UserToken : IdentityUserToken<string>
{
    private UserToken(): base() { }


    public static UserToken New() => new UserToken();

    public static UserToken New(
        string userId, string name,
        string loginProvider, string value)
    => New().WithUserId(userId)
            .WithName(name)
            .WithLoginProvider(loginProvider)
            .WithValue(value);
    
    public UserToken WithUserId(string userId)
    {
        UserId = userId;
        return this;
    }

    public UserToken WithLoginProvider(string loginProvider)
    {
        LoginProvider = loginProvider;
        return this;
    }

    public UserToken WithName(string name)
    {
        Name = name;
        return this;
    }

    public UserToken WithValue(string value)
    {
        Value = value;
        return this;
    }
}