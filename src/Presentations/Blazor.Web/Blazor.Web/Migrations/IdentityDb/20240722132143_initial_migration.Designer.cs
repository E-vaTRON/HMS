﻿// <auto-generated />
using System;
using Blazor.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Blazor.Web.Migrations.IdentityDb
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20240722132143_initial_migration")]
    partial class initial_migration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Blazor.Web.Role", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MainTasks")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Mission")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("RoleIcon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Summary")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "123e4567-e89b-12d3-a456-426614174000",
                            MainTasks = "Manage users, roles, and system settings",
                            Mission = "Manage the entire system",
                            Name = "Admin",
                            NormalizedName = "ADMIN",
                            RoleIcon = "default-icon",
                            Summary = "Administrator role"
                        },
                        new
                        {
                            Id = "c54274f0-5603-4f37-a400-c17ac38a9060",
                            MainTasks = "Don't cause any troubles and damage",
                            Mission = "Obey the rules",
                            Name = "Member",
                            NormalizedName = "MEMBER",
                            RoleIcon = "member-icon",
                            Summary = "Member role"
                        });
                });

            modelBuilder.Entity("Blazor.Web.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<int>("Block")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CoverImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<int>("Lot")
                        .HasColumnType("int");

                    b.Property<int>("LotSize")
                        .HasColumnType("int");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Phase")
                        .HasColumnType("int");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("ProfileImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SignalRConnectionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Street")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "123e4567-e89b-12d3-a456-426614174000",
                            AccessFailedCount = 0,
                            Block = 0,
                            ConcurrencyStamp = "836f385c-6c03-4cb9-b011-311692015336",
                            CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mpgatdula@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Mark",
                            IsDeleted = false,
                            LastName = "Gatdula",
                            LockoutEnabled = false,
                            Lot = 0,
                            LotSize = 0,
                            MiddleName = "Pogi",
                            NormalizedEmail = "MPGATDULA@GMAIL.COM",
                            NormalizedUserName = "MPGATDULA",
                            PasswordHash = "AQAAAAIAAYagAAAAENhiE5RVn6UAm/hDVmxKnFY2jzbFXR0UH8BUlT2OrfGbaUexelzqPlm6qM8l4ZSgaQ==",
                            Phase = 0,
                            PhoneNumber = "09191234567",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "a6b31e15-6024-408a-9955-20790cfa2d56",
                            TwoFactorEnabled = false,
                            UserName = "MpGatdula"
                        },
                        new
                        {
                            Id = "c54274f0-5603-4f37-a400-c17ac38a9060",
                            AccessFailedCount = 0,
                            Block = 0,
                            ConcurrencyStamp = "fe24abfa-7776-48fe-9ed7-07c66fdeb9a1",
                            CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jphizon@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Jason Paul",
                            IsDeleted = false,
                            LastName = "Hizon",
                            LockoutEnabled = false,
                            Lot = 0,
                            LotSize = 0,
                            MiddleName = "Swabe",
                            NormalizedEmail = "JPHIZON@GMAIL.COM",
                            NormalizedUserName = "JPHIZON",
                            PasswordHash = "AQAAAAIAAYagAAAAELkF3PTW23ZAAqii17rSztmVxCnhjj0TrGYU0Cfvsu5RSB48Uxza42TP3oJpa5eXZw==",
                            Phase = 0,
                            PhoneNumber = "09191234568",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "ef9b35a8-4b91-4c8c-acbd-21ed39ae94c1",
                            TwoFactorEnabled = false,
                            UserName = "JpHizon"
                        },
                        new
                        {
                            Id = "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3",
                            AccessFailedCount = 0,
                            Block = 0,
                            ConcurrencyStamp = "fdcd9c2f-e341-41e1-b079-d12449f19172",
                            CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "drsmith@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "John",
                            IsDeleted = false,
                            LastName = "Smith",
                            LockoutEnabled = false,
                            Lot = 0,
                            LotSize = 0,
                            MiddleName = "A.",
                            NormalizedEmail = "DRSMITH@GMAIL.COM",
                            NormalizedUserName = "DRSMITH",
                            PasswordHash = "AQAAAAIAAYagAAAAEDGc7DurooH2/Pa68SHOBH5ILUuI2mWV6B9ZQQVz9e+KLg5zddygH66ev+vN3xZLww==",
                            Phase = 0,
                            PhoneNumber = "09191234569",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "07c0dfeb-2917-4d7c-a2f8-4d8e455eda0c",
                            TwoFactorEnabled = false,
                            UserName = "DrSmith"
                        },
                        new
                        {
                            Id = "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c",
                            AccessFailedCount = 0,
                            Block = 0,
                            ConcurrencyStamp = "8b9ac4c3-4ac9-45b8-ae4e-dd7466e7ef93",
                            CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "drdoe@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Jane",
                            IsDeleted = false,
                            LastName = "Doe",
                            LockoutEnabled = false,
                            Lot = 0,
                            LotSize = 0,
                            MiddleName = "B.",
                            NormalizedEmail = "DRDOE@GMAIL.COM",
                            NormalizedUserName = "DRDOE",
                            PasswordHash = "AQAAAAIAAYagAAAAEIBd4rNQhzw+fgxi87SRTdCEguKEA4yPW0sqetOA+JTdOMWEXyFQB99/YKNwVNCe+w==",
                            Phase = 0,
                            PhoneNumber = "09191234570",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "d455106d-c6da-4eae-bd96-0a1c8975023c",
                            TwoFactorEnabled = false,
                            UserName = "DrDoe"
                        },
                        new
                        {
                            Id = "ffbfde38-3b88-48b4-910d-86dbcab5ba02",
                            AccessFailedCount = 0,
                            Block = 0,
                            ConcurrencyStamp = "bc2bb453-1c49-4a2d-ab5e-c26bd78c8721",
                            CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "emilybrown@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Emily",
                            IsDeleted = false,
                            LastName = "Brown",
                            LockoutEnabled = false,
                            Lot = 0,
                            LotSize = 0,
                            MiddleName = "C.",
                            NormalizedEmail = "EMILYBROWN@GMAIL.COM",
                            NormalizedUserName = "EMILYBROWN",
                            PasswordHash = "AQAAAAIAAYagAAAAEC3ebAej+VFgwSWOyLj7lOlo0upLIqZHaZm9qI7u9+xbej9ZPeOu9nRDT0iCgF7RVQ==",
                            Phase = 0,
                            PhoneNumber = "09191234571",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "66ef9e05-aa39-4f84-a483-b6f34b8624d3",
                            TwoFactorEnabled = false,
                            UserName = "EmilyBrown"
                        },
                        new
                        {
                            Id = "9e60babb-15a9-4efe-837e-2ba0c1516afb",
                            AccessFailedCount = 0,
                            Block = 0,
                            ConcurrencyStamp = "01c5ef30-595a-43d5-b8fc-4e759ae45f52",
                            CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "michaelgreen@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Michael",
                            IsDeleted = false,
                            LastName = "Green",
                            LockoutEnabled = false,
                            Lot = 0,
                            LotSize = 0,
                            MiddleName = "D.",
                            NormalizedEmail = "MICHAELGREEN@GMAIL.COM",
                            NormalizedUserName = "MICHAELGREEN",
                            PasswordHash = "AQAAAAIAAYagAAAAEPVOBMSotvMFLnX4mjcbrQgpD2Ek+p7LaOc/gvuVy6MjlFL9lrLZQXIJhlKEVDhB0w==",
                            Phase = 0,
                            PhoneNumber = "09191234572",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "cc112e70-27d6-47fe-954d-763e174e151d",
                            TwoFactorEnabled = false,
                            UserName = "MichaelGreen"
                        },
                        new
                        {
                            Id = "ebdaa999-2966-4227-96e2-5a72582c0566",
                            AccessFailedCount = 0,
                            Block = 0,
                            ConcurrencyStamp = "796abecd-283e-4001-85c9-d52c3a151146",
                            CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "sarahwhite@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Sarah",
                            IsDeleted = false,
                            LastName = "White",
                            LockoutEnabled = false,
                            Lot = 0,
                            LotSize = 0,
                            MiddleName = "E.",
                            NormalizedEmail = "SARAHWHITE@GMAIL.COM",
                            NormalizedUserName = "SARAHWHITE",
                            PasswordHash = "AQAAAAIAAYagAAAAEBIhm0oxefVOccNdm0XaFG2pieUc3YTT1Q7fR4YBlG/BYWXY46GnmxMFeOSglLIBLg==",
                            Phase = 0,
                            PhoneNumber = "09191234573",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "0effefe8-e62e-4c4f-b64a-863ecf177432",
                            TwoFactorEnabled = false,
                            UserName = "SarahWhite"
                        },
                        new
                        {
                            Id = "acbf592e-ed02-4de5-af78-2fb17f2dc872",
                            AccessFailedCount = 0,
                            Block = 0,
                            ConcurrencyStamp = "e60f37f4-9f9e-4946-b997-36215c0758df",
                            CreatedAt = new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "davidblack@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "David",
                            IsDeleted = false,
                            LastName = "Black",
                            LockoutEnabled = false,
                            Lot = 0,
                            LotSize = 0,
                            MiddleName = "F.",
                            NormalizedEmail = "DAVIDBLACK@GMAIL.COM",
                            NormalizedUserName = "DAVIDBLACK",
                            PasswordHash = "AQAAAAIAAYagAAAAEAAp0NPA3wszbP0Uw7vnjjSTjKf4qSnRtjuNam8i/oplYlgbmSl4JPV4KrEi60Syqg==",
                            Phase = 0,
                            PhoneNumber = "09191234574",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "d1ce923f-db0b-4aa6-98c7-1c1d58d55180",
                            TwoFactorEnabled = false,
                            UserName = "DavidBlack"
                        },
                        new
                        {
                            Id = "c54274f0-5603-4f37-a400-c17ac38a9061",
                            AccessFailedCount = 0,
                            Block = 1,
                            ConcurrencyStamp = "f0759c06-17e2-419c-930b-c28be8e880b6",
                            CreatedAt = new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "asramos@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Apple",
                            IsDeleted = false,
                            LastName = "Ramos",
                            LockoutEnabled = false,
                            Lot = 1,
                            LotSize = 200,
                            MiddleName = "Sy",
                            NormalizedEmail = "ASRAMOS@GMAIL.COM",
                            NormalizedUserName = "ASRAMOS",
                            PasswordHash = "AQAAAAIAAYagAAAAECjIVLCS+rVlIwSi3PcjCLki4Iblo9HKXVSUUQptdc79+xdDQdDV4DYd8VyWZmuOJA==",
                            Phase = 1,
                            PhoneNumber = "09191234569",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "a10c4f7d-e1d3-423d-9c29-0bf40de3559a",
                            Street = "Orange",
                            TwoFactorEnabled = false,
                            UserName = "AsRamos"
                        },
                        new
                        {
                            Id = "c54274f0-5603-4f37-a400-c17ac38a9062",
                            AccessFailedCount = 0,
                            Block = 2,
                            ConcurrencyStamp = "e6452a79-1424-4aec-8d01-745aa151eaf0",
                            CreatedAt = new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "rkdelacruz@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Ronda",
                            IsDeleted = false,
                            LastName = "Dela Cruz",
                            LockoutEnabled = false,
                            Lot = 2,
                            LotSize = 250,
                            MiddleName = "Kidman",
                            NormalizedEmail = "RKDELACRUZ@GMAIL.COM",
                            NormalizedUserName = "RKDELACRUZ",
                            PasswordHash = "AQAAAAIAAYagAAAAED85cg3kAJmR+MXGXof+1UhcrCeqQw7jWS90qS2IDFADemcKgrU00i2+YnD+JoGavg==",
                            Phase = 2,
                            PhoneNumber = "09191234560",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "6681e967-aee9-4514-904d-ae3c40de2ea5",
                            Street = "Lemon",
                            TwoFactorEnabled = false,
                            UserName = "RkDelacruz"
                        },
                        new
                        {
                            Id = "c54274f0-5603-4f37-a400-c17ac38a9063",
                            AccessFailedCount = 0,
                            Block = 3,
                            ConcurrencyStamp = "f7f7bb26-9811-4223-a30f-759f7c48743d",
                            CreatedAt = new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "mmdantes@gmail.com",
                            EmailConfirmed = true,
                            FirstName = "Mary",
                            IsDeleted = false,
                            LastName = "Dantes",
                            LockoutEnabled = false,
                            Lot = 3,
                            LotSize = 280,
                            MiddleName = "Mar",
                            NormalizedEmail = "MMDANTES@GMAIL.COM",
                            NormalizedUserName = "MMDANTES",
                            PasswordHash = "AQAAAAIAAYagAAAAEPX8iFLaiZ93oDzqiSMnYG9UVhoTGoz2hVpi/ledoZYC/F8Rc0UTEGaslPZNwr//Yw==",
                            Phase = 3,
                            PhoneNumber = "09191234560",
                            PhoneNumberConfirmed = true,
                            SecurityStamp = "aaaaa32a-a2d6-425d-bf7a-2e855f87e442",
                            Street = "Strawberry",
                            TwoFactorEnabled = false,
                            UserName = "MmDantes"
                        });
                });

            modelBuilder.Entity("Blazor.Web.UserRole", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "123e4567-e89b-12d3-a456-426614174000",
                            RoleId = "123e4567-e89b-12d3-a456-426614174000"
                        },
                        new
                        {
                            UserId = "c54274f0-5603-4f37-a400-c17ac38a9060",
                            RoleId = "123e4567-e89b-12d3-a456-426614174000"
                        },
                        new
                        {
                            UserId = "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3",
                            RoleId = "123e4567-e89b-12d3-a456-426614174000"
                        },
                        new
                        {
                            UserId = "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c",
                            RoleId = "123e4567-e89b-12d3-a456-426614174000"
                        },
                        new
                        {
                            UserId = "ffbfde38-3b88-48b4-910d-86dbcab5ba02",
                            RoleId = "123e4567-e89b-12d3-a456-426614174000"
                        },
                        new
                        {
                            UserId = "9e60babb-15a9-4efe-837e-2ba0c1516afb",
                            RoleId = "123e4567-e89b-12d3-a456-426614174000"
                        },
                        new
                        {
                            UserId = "ebdaa999-2966-4227-96e2-5a72582c0566",
                            RoleId = "123e4567-e89b-12d3-a456-426614174000"
                        },
                        new
                        {
                            UserId = "acbf592e-ed02-4de5-af78-2fb17f2dc872",
                            RoleId = "123e4567-e89b-12d3-a456-426614174000"
                        },
                        new
                        {
                            UserId = "c54274f0-5603-4f37-a400-c17ac38a9061",
                            RoleId = "c54274f0-5603-4f37-a400-c17ac38a9060"
                        },
                        new
                        {
                            UserId = "c54274f0-5603-4f37-a400-c17ac38a9062",
                            RoleId = "c54274f0-5603-4f37-a400-c17ac38a9060"
                        },
                        new
                        {
                            UserId = "c54274f0-5603-4f37-a400-c17ac38a9063",
                            RoleId = "c54274f0-5603-4f37-a400-c17ac38a9060"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("Blazor.Web.UserRole", b =>
                {
                    b.HasOne("Blazor.Web.Role", "Role")
                        .WithMany("UserRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Blazor.Web.User", "User")
                        .WithMany("UserRoles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Blazor.Web.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Blazor.Web.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Blazor.Web.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Blazor.Web.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Blazor.Web.Role", b =>
                {
                    b.Navigation("UserRoles");
                });

            modelBuilder.Entity("Blazor.Web.User", b =>
                {
                    b.Navigation("UserRoles");
                });
#pragma warning restore 612, 618
        }
    }
}
