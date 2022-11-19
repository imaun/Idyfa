using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore.SQLite;

public class IdyfaSQLiteDbContext : IdyfaDbContext
{
    
    public IdyfaSQLiteDbContext(DbContextOptions options) : base(options)
    {
    }
    
}