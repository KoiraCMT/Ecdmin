﻿// <auto-generated />
using System;
using Ecdmin.EntityFramework.Core.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Ecdmin.Database.Migrations.Migrations
{
    [DbContext(typeof(SqlServerDbContext))]
    partial class SqlServerDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.1");

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.Administrator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Avatar")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("Username");

                    b.ToTable("administrator");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTimeOffset(new DateTime(2021, 2, 5, 10, 3, 49, 153, DateTimeKind.Unspecified).AddTicks(3070), new TimeSpan(0, 8, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "echo",
                            Password = "AQAAAAEAACcQAAAAELVd73DIDUHSsJ2nIe6KN/OtHCkTeU72hTxixsainCkt5hnUMfgTStyC6AhlH3+nmA==",
                            Username = "echo"
                        });
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.AdministratorRole", b =>
                {
                    b.Property<int>("AdministratorId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("AdministratorId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("administrator_role");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("PermissionGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PermissionGroupId");

                    b.ToTable("permission");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayName = "首页",
                            Name = "administrator.index",
                            PermissionGroupId = 1
                        },
                        new
                        {
                            Id = 2,
                            DisplayName = "新增",
                            Name = "administrator.add",
                            PermissionGroupId = 1
                        },
                        new
                        {
                            Id = 3,
                            DisplayName = "修改",
                            Name = "administrator.update",
                            PermissionGroupId = 1
                        },
                        new
                        {
                            Id = 4,
                            DisplayName = "删除",
                            Name = "administrator.delete",
                            PermissionGroupId = 1
                        },
                        new
                        {
                            Id = 5,
                            DisplayName = "首页",
                            Name = "permission.index",
                            PermissionGroupId = 2
                        });
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.PermissionGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("permission_group");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DisplayName = "用户管理",
                            Name = "administrator"
                        },
                        new
                        {
                            Id = 2,
                            DisplayName = "权限管理",
                            Name = "permission"
                        },
                        new
                        {
                            Id = 3,
                            DisplayName = "角色管理",
                            Name = "role"
                        });
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<DateTimeOffset>("CreatedTime")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTimeOffset?>("UpdatedTime")
                        .HasColumnType("datetimeoffset");

                    b.HasKey("Id");

                    b.ToTable("role");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.RolePermission", b =>
                {
                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("PermissionId")
                        .HasColumnType("int");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("role_permission");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.AdministratorRole", b =>
                {
                    b.HasOne("Ecdmin.Core.Entities.Admin.Administrator", "Administrator")
                        .WithMany("AdministratorRoles")
                        .HasForeignKey("AdministratorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecdmin.Core.Entities.Admin.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Administrator");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.Permission", b =>
                {
                    b.HasOne("Ecdmin.Core.Entities.Admin.PermissionGroup", null)
                        .WithMany("Permissions")
                        .HasForeignKey("PermissionGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.RolePermission", b =>
                {
                    b.HasOne("Ecdmin.Core.Entities.Admin.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecdmin.Core.Entities.Admin.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.Administrator", b =>
                {
                    b.Navigation("AdministratorRoles");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.PermissionGroup", b =>
                {
                    b.Navigation("Permissions");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.Role", b =>
                {
                    b.Navigation("RolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
