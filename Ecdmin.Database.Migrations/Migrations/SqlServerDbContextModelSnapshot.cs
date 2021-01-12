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

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.AdminUser", b =>
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

                    b.ToTable("admin_user");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedTime = new DateTimeOffset(new DateTime(2021, 1, 8, 16, 51, 9, 639, DateTimeKind.Unspecified).AddTicks(6690), new TimeSpan(0, 8, 0, 0, 0)),
                            IsDeleted = false,
                            Name = "echo",
                            Password = "AQAAAAEAACcQAAAAEMpry8Lr57SQZCcJZJyBOU3CaECBsADyjWRvl/PiT7jhlvWNWIL78QbVyC4jGIVkNw==",
                            Username = "echo"
                        });
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
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.PermissionGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .UseIdentityColumn();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.ToTable("permission_group");
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

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.Permission", b =>
                {
                    b.HasOne("Ecdmin.Core.Entities.Admin.PermissionGroup", null)
                        .WithMany("Permissions")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.RolePermission", b =>
                {
                    b.HasOne("Ecdmin.Core.Entities.Admin.Permission", "Permission")
                        .WithMany()
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Ecdmin.Core.Entities.Admin.Role", "Role")
                        .WithMany()
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Ecdmin.Core.Entities.Admin.PermissionGroup", b =>
                {
                    b.Navigation("Permissions");
                });
#pragma warning restore 612, 618
        }
    }
}
