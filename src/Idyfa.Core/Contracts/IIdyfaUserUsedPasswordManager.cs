namespace Idyfa.Core.Contracts;

public interface IIdyfaUserUsedPasswordManager
{

    Task<bool> IsPasswordUsedBeforeAsync(User user, string password);

    Task AddToUsedPasswordsAsync(User user, UserUsedPassword password);

    Task<bool> IsUserPasswordTooOldAsync(User user);

    Task<DateTime?> GetLastUserChangePasswordDateAsync(User user);
}