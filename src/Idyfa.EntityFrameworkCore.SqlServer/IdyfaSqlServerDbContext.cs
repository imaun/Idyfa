using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore.SqlServer
{

    public class IdyfaSqlServerDbContext : IdyfaDbContext
    {

        public IdyfaSqlServerDbContext(DbContextOptions options) : base(options) 
        {
        }
    }
}
