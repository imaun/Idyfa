using Idyfa.Core;
using Idyfa.Core.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Idyfa.EntityFrameworkCore.Configurations;

public static partial class EntityConfigurations
{

    /// <summary>
    /// Configure <see cref="User"/> mapping for the Database Schema.
    /// </summary>
    /// <param name="builder"></param>
    public static void AddUserConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<User>(user =>
        {
            user.ToTable(GetTableName(typeof(User), tablePrefix)).HasKey(u => u.Id);
            user.Property(u => u.UserName).IsUnicode().HasMaxLength(256).IsRequired();
            user.Property(u => u.Email).IsUnicode().HasMaxLength(1000);
            user.Property(u => u.PhoneNumber).IsUnicode().HasMaxLength(20);
            user.Property(u => u.NationalCode).IsUnicode().HasMaxLength(10);
            user.Property(u => u.FirstName).IsUnicode().HasMaxLength(256);
            user.Property(u => u.LastName).IsUnicode().HasMaxLength(256);
            user.Property(u => u.Status).HasDefaultValue(UserStatus.Created);
            
        });
    }
}