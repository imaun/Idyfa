using Idyfa.Core;
using Idyfa.Core.Contracts;
using Idyfa.Core.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Idyfa.EntityFrameworkCore;


public class IdyfaUserUsedPasswordRepository : 
    IdyfaBaseRepository<UserUsedPassword, long>, IIdyfaUserUsedPasswordRepository
{
    public IdyfaUserUsedPasswordRepository(
        IdyfaDbContext db, ILogger<IdyfaUserUsedPasswordRepository> logger) : base(db, logger)
    {
    }
    
    public async Task<IReadOnlyCollection<UserUsedPassword>> GetByUserIdAsync(string userId)
    {
        if (userId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userId));
        
        return await _set.Where(_ => _.UserId == userId).ToListAsync().ConfigureAwait(false);
    }

    public async Task<UserUsedPassword?> GetLastByUserIdAsync(string userId)
    {
        if (userId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userId));

        return await _set.LastOrDefaultAsync(_ => _.UserId == userId).ConfigureAwait(false);
    }

    public async Task<bool> IsPasswordExistedAsync(string userId, string hashedPassword)
    {
        if (userId.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(userId));
        
        if (hashedPassword.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(hashedPassword));

        return await _set.AnyAsync(_ => _.UserId == userId &&
                                        _.HashedPassword == hashedPassword).ConfigureAwait(false);
    }
}