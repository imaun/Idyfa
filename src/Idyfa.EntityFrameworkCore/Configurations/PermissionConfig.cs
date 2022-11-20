using Idyfa.Core;
using Idyfa.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    /// <summary>
    /// Configures <see cref="Permission"/> for the Database schema.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tablePrefix"></param>
    public static void AddPermissionConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<Permission>(per =>
        {
            per.ToTable(GetTableName(typeof(Permission), tablePrefix))
                .HasKey(_ => _.Id);

            per.Property(p => p.Title).HasMaxLength(256).IsUnicode().IsRequired();
            per.Property(p => p.SystemName).HasMaxLength(256).IsUnicode().IsRequired();
            per.Property(p => p.Category).HasMaxLength(256).IsUnicode();
            per.Property(p => p.Description).HasMaxLength(2000).IsUnicode();
        });
    }
}