using Idyfa.Core;
using Idyfa.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    /// <summary>
    /// Configures <see cref="Role"/> mapping for the Database Schema.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tablePrefix">The prefix used in naming the entity Table.</param>
    public static void AddRoleConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));
        builder.Entity<Role>(role => {
            role.ToTable(GetTableName(typeof(Role), tablePrefix)).HasKey(_ => _.Id);
            role.Property(r => r.Name).IsUnicode().HasMaxLength(256).IsRequired();
            role.Property(r => r.NormalizedName).HasMaxLength(256);
            role.Property(r => r.Title).IsUnicode().HasMaxLength(256);
            role.Property(r => r.AltTitle).HasMaxLength(256).IsUnicode();
            role.Property(r => r.ConcurrencyStamp).HasMaxLength(500).IsConcurrencyToken();
            role.Property(r => r.Status).HasDefaultValue(RoleStatus.Enabled);
            
        });
    }
}