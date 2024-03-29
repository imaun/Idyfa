﻿// <auto-generated />
using System;
using Idyfa.EntityFrameworkCore.SQLite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Idyfa.EntityFrameworkCore.SQLite.Migrations
{
    [DbContext(typeof(IdyfaSQLiteDbContext))]
    [Migration("20221120092812_InitDb")]
    partial class InitDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("Idyfa.Core.Permission", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Category")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("SystemName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Idyfa.Permission", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("AltTitle")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastStatusChanged")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(1);

                    b.Property<string>("Title")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Idyfa.Role", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.RoleClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("Idyfa.RoleClaim", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.User", b =>
                {
                    b.Property<string>("Id")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ApiKey")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CategoryId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasMaxLength(500)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasMaxLength(500)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastStatusChanged")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("LastVisitDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("NationalCode")
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ReferralCode")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("RegisterDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValue(0);

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Idyfa.User", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.UserCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasMaxLength(4000)
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ModifyDate")
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(300)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Idyfa.UserCategory", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.UserClaim", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .IsRequired()
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Idyfa.UserClaim", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.UserLogin", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "ProviderKey");

                    b.ToTable("Idyfa.UserLogin", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.UserLoginRecord", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("ExtraInfo")
                        .HasMaxLength(2000)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("HostName")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("IpAddress")
                        .HasMaxLength(50)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginUrl")
                        .HasMaxLength(4000)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("OsName")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserAgent")
                        .HasMaxLength(1000)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Idyfa.UserLoginRecord", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.UserRole", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("Idyfa.UserRole", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.UserToken", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasMaxLength(256)
                        .IsUnicode(true)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Idyfa.UserToken", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.UserUsedPassword", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("HashedPassword")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Idyfa.UserUsedPassword", (string)null);
                });

            modelBuilder.Entity("Idyfa.Core.Role", b =>
                {
                    b.OwnsMany("Idyfa.Core.RolePermission", "Permissions", b1 =>
                        {
                            b1.Property<Guid>("PermissionId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<string>("RoleId")
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("CreateDate")
                                .HasColumnType("TEXT");

                            b1.HasKey("PermissionId", "RoleId");

                            b1.HasIndex("RoleId");

                            b1.ToTable("Idyfa.RolePermission", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Idyfa.Core.RoleClaim", b =>
                {
                    b.HasOne("Idyfa.Core.Role", null)
                        .WithMany("Claims")
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Idyfa.Core.User", b =>
                {
                    b.HasOne("Idyfa.Core.UserCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.SetNull);

                    b.OwnsMany("Idyfa.Core.UserPermission", "Permissions", b1 =>
                        {
                            b1.Property<string>("UserId")
                                .HasColumnType("TEXT");

                            b1.Property<Guid>("PermissionId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("TEXT");

                            b1.Property<DateTime>("CreateDate")
                                .HasColumnType("TEXT");

                            b1.HasKey("UserId", "PermissionId");

                            b1.ToTable("Idyfa.UserPermission", (string)null);

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Idyfa.Core.UserLogin", b =>
                {
                    b.HasOne("Idyfa.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Idyfa.Core.UserLoginRecord", b =>
                {
                    b.HasOne("Idyfa.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Idyfa.Core.UserRole", b =>
                {
                    b.HasOne("Idyfa.Core.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Idyfa.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Idyfa.Core.UserToken", b =>
                {
                    b.HasOne("Idyfa.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Idyfa.Core.UserUsedPassword", b =>
                {
                    b.HasOne("Idyfa.Core.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Idyfa.Core.Role", b =>
                {
                    b.Navigation("Claims");
                });
#pragma warning restore 612, 618
        }
    }
}
