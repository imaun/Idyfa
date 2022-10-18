using Idyfa.Core;
using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore.SQLite;

public class IdyfaSQLiteDbContext : IdyfaDbContext
{
    public IdyfaSQLiteDbContext(
            DbContextOptions options, IdyfaOptions idyfaOptions
        ) : base(options, idyfaOptions)
    {
    }
    
    
}