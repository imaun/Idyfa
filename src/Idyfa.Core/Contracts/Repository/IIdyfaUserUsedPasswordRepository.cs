namespace Idyfa.Core.Contracts.Repository;

public interface IIdyfaUserUsedPasswordRepository : IIdyfaBaseRepository<UserUsedPassword, string>
{

    Task<IReadOnlyCollection<UserUsedPassword>> GetByUserIdAsync(string userId);
    
    Task<UserUsedPassword?> GetLastByUserIdAsync(string userId);
}