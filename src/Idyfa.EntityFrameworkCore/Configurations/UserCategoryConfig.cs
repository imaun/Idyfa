using Idyfa.Core;
using Idyfa.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    /// <summary>
    /// Configures <see cref="UserCategory"/> for the Database schema.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tablePrefix"></param>
    public static void AddUserCategoryConfiguration(
        this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<UserCategory>(cat =>
        {
            cat.ToTable(GetTableName(typeof(UserCategory), tablePrefix))
                .HasKey(_ => _.Id);
            cat.Property(_ => _.Title).HasMaxLength(300).IsUnicode().IsRequired();
            cat.Property(_ => _.Description).HasMaxLength(4000);
        });
    }
}