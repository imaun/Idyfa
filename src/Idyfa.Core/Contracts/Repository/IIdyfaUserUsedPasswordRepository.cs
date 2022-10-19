namespace Idyfa.Core.Contracts;

public interface IIdyfaUserUsedPasswordRepository : IIdyfaBaseRepository<UserUsedPassword, long>
{

    Task<IReadOnlyCollection<UserUsedPassword>> GetByUserIdAsync(string userId);
    
    Task<UserUsedPassword?> GetLastByUserIdAsync(string userId);

    Task<bool> IsPasswordExistedAsync(string userId, string hashedPassword);
}