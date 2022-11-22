using Microsoft.AspNetCore.Identity;

namespace Idyfa.Core.Contracts;

public interface IIdyfaUserValidator : IUserValidator<User>
{

    IdentityError[] ValidateUserName(string userName);
}