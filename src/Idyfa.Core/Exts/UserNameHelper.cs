using Idyfa.Core.enums;

namespace Idyfa.Core.Extensions;

public static class UserNameHelper
{

    public static string GetUserName(this User user, UserNameType userNameType)
        => userNameType switch
        {
            UserNameType.Email => user.Email,
            UserNameType.PhoneNumber => user.PhoneNumber,
            UserNameType.UserName => user.UserName,
            _=> string.Empty
        };
}