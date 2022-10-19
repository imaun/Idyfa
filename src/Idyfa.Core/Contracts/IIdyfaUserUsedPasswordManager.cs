namespace Idyfa.Core.Contracts;

public interface IIdyfaUserUsedPasswordManager
{

    Task<bool> IsPasswordUsedBeforeAsync(User user, string password);

    Task AddToUsedPasswordsAsync(User user, UserUsedPassword password);

    /// <summary>
    /// Adds a Password to the <see cref="User"/>s used password history and
    /// returns it to the caller.
    /// </summary>
    /// <param name="user"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    Task<UserUsedPassword> AddToUsedPasswordsAsync(User user, string password);

    Task<bool> IsUserPasswordTooOldAsync(User user);

    Task<DateTime?> GetLastUserChangePasswordDateAsync(User user);
}