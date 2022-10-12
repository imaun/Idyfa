namespace Idyfa.Core.Contracts;

public interface IIdyfaUserUsedPasswordRepository : IIdyfaBaseRepository<UserUsedPassword, string>
{

    Task<IReadOnlyCollection<UserUsedPassword>> GetByUserIdAsync(string userId);
    
    Task<UserUsedPassword?> GetLastByUserIdAsync(string userId);

    Task<bool> IsPasswordExistedAsync(string userId, string password);
}