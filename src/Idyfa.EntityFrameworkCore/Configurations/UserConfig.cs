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
    /// <param name="tablePrefix">The prefix used in naming the entity Table.</param>
    public static void AddUserConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<User>(user =>
        {
            user.ToTable(GetTableName(typeof(User), tablePrefix)).HasKey(u => u.Id);
            user.Property(u => u.Id).HasMaxLength(256).IsUnicode().IsRequired();
            user.Property(u => u.UserName).IsUnicode().HasMaxLength(256).IsRequired();
            user.Property(u => u.Email).IsUnicode().HasMaxLength(1000).IsRequired(false);
            user.Property(u => u.PhoneNumber).IsUnicode().HasMaxLength(20).IsRequired(false);
            user.Property(u => u.NationalCode).IsUnicode().HasMaxLength(10).IsRequired(false);
            user.Property(u => u.FirstName).IsUnicode().HasMaxLength(256).IsRequired(false);
            user.Property(u => u.LastName).IsUnicode().HasMaxLength(256).IsRequired(false);
            user.Property(u => u.Status).HasDefaultValue(UserStatus.Created);
            user.Property(u => u.DisplayName).IsUnicode().HasMaxLength(500).IsRequired(false);
            user.Property(u => u.ConcurrencyStamp).HasMaxLength(500).IsConcurrencyToken();
            user.Property(u => u.NormalizedUserName).IsUnicode().HasMaxLength(256);
            user.Property(u => u.NormalizedEmail).IsUnicode().HasMaxLength(1000).IsRequired(false);
            user.Property(u => u.ApiKey).IsUnicode().HasMaxLength(50).IsRequired();
            user.Property(u => u.ReferralCode).IsUnicode().HasMaxLength(10).IsRequired();
            user.Property(u => u.LastTwoFactorCode).HasMaxLength(20).IsRequired(false);
            user.Property(u => u.TwoFactorProvider).HasMaxLength(50).IsRequired().HasDefaultValue("email");

            user.HasOne<UserCategory>()
                .WithMany()
                .HasForeignKey(_ => _.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            user.OwnsMany(_ => _.Permissions, p =>
            {
                p.ToTable(GetTableName(typeof(UserPermission), tablePrefix)).HasKey(_ => new
                {
                    _.UserId, _.PermissionId
                });
                p.Property(_ => _.UserId).IsRequired();
            });
        });
    }

    /// <summary>
    /// Configures <see cref="UserRole"/> mapping for the Database schema. 
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tablePrefix"></param>
    public static void AddUserRoleConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<UserRole>(ur =>
        {
            ur.ToTable(GetTableName(typeof(UserRole), tablePrefix))
                .HasKey(_ => new { _.RoleId, _.UserId });

            ur.HasOne<User>()
                .WithMany()
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            ur.HasOne<Role>()
                .WithMany()
                .HasForeignKey(_ => _.RoleId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
    
    /// <summary>
    /// Configures <see cref="UserClaim"/> mapping for the Database schema.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tablePrefix"></param>
    public static void AddUserClaimConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<UserClaim>(claim =>
        {
            claim.ToTable(GetTableName(typeof(UserClaim), tablePrefix))
                .HasKey(_=> _.Id);
            claim.Property(_ => _.ClaimType).HasMaxLength(256).IsUnicode().IsRequired();
            claim.Property(_ => _.ClaimValue).HasMaxLength(1000).IsUnicode();
        });
    }

    /// <summary>
    /// Configures <see cref="UserLogin"/> mapping for the Database schema.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tablePrefix"></param>
    public static void AddUserLoginConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<UserLogin>(login =>
        {
            login.ToTable(GetTableName(typeof(UserLogin), tablePrefix))
                .HasKey(_=> new
                {
                    _.UserId, _.ProviderKey
                });

            login.Property(l => l.LoginProvider).IsUnicode().HasMaxLength(256);
            login.Property(l => l.ProviderKey).IsUnicode().HasMaxLength(256).IsRequired();
            login.Property(l => l.ProviderDisplayName).IsUnicode().HasMaxLength(256);
            login.Property(l => l.UserId).IsRequired();

            login.HasOne<User>()
                .WithMany()
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    /// <summary>
    /// Configures <see cref="UserToken"/> mapping for the Database schema.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tablePrefix"></param>
    public static void AddUserTokenConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<UserToken>(token =>
        {
            token.ToTable(GetTableName(typeof(UserToken), tablePrefix))
                .HasKey(_ => _.Id);

            token.Property(t => t.Name).HasMaxLength(256).IsUnicode();
            token.Property(t => t.Value).HasMaxLength(256).IsUnicode();
            token.Property(t => t.LoginProvider).HasMaxLength(256).IsUnicode();
            token.Property(t => t.UserId).IsRequired();

            token.HasOne<User>()
                .WithMany()
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    /// <summary>
    /// Configures <see cref="UserLoginRecord"/> mapping for the Database schema.
    /// </summary>
    /// <param name="builder"></param>
    /// <param name="tablePrefix"></param>
    public static void AddUserLoginRecordConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<UserLoginRecord>(rec =>
        {
            rec.ToTable(GetTableName(typeof(UserLoginRecord), tablePrefix))
                .HasKey(_ => _.Id);

            rec.Property(r => r.UserId).HasMaxLength(256).IsRequired();
            rec.Property(r => r.City).HasMaxLength(256).IsUnicode().IsRequired(false);
            rec.Property(r => r.Country).HasMaxLength(256).IsUnicode().IsRequired(false);
            rec.Property(r => r.IpAddress).HasMaxLength(50).IsUnicode().IsRequired(false);
            rec.Property(r => r.HostName).HasMaxLength(256).IsUnicode().IsRequired(false);
            rec.Property(r => r.LoginUrl).HasMaxLength(4000).IsUnicode().IsRequired(false);
            rec.Property(r => r.OsName).HasMaxLength(256).IsUnicode().IsRequired(false);
            rec.Property(r => r.UserAgent).HasMaxLength(1000).IsUnicode().IsRequired(false);
            rec.Property(r => r.ExtraInfo).HasMaxLength(2000).IsUnicode().IsRequired(false);

            rec.HasOne<User>()
                .WithMany()
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }


    public static void AddUserUsedPasswordConfiguration(this ModelBuilder builder, string tablePrefix = "")
    {
        builder.CheckArgumentIsNull(nameof(builder));

        builder.Entity<UserUsedPassword>(password =>
        {
            password.ToTable(GetTableName(typeof(UserUsedPassword), tablePrefix))
                .HasKey(_ => _.Id);
            password.Property(p => p.UserId).HasMaxLength(256).IsRequired();
            password.Property(p => p.HashedPassword).HasMaxLength(256).IsRequired();

            password.HasOne<User>()
                .WithMany()
                .HasForeignKey(_ => _.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}