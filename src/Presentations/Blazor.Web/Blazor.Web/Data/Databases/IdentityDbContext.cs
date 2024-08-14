using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blazor.Web;

public class IdentityDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
{
    #region [ Fields ]

    private readonly IServiceScopeFactory serviceScopeFactory;
    #endregion

    #region [ CTors ]

    public IdentityDbContext(DbContextOptions options, IServiceScopeFactory serviceScopeFactory) : base(options)
    {
        this.serviceScopeFactory = serviceScopeFactory;
    }
    #endregion

    #region [ DbSets ]

    public override DbSet<User> Users { get; set; } = null!;
    public override DbSet<Role> Roles { get; set; } = null!;
    public override DbSet<UserRole> UserRoles { get; set; } = null!;
    #endregion

    #region [ Overrides ]

    protected override void OnModelCreating(ModelBuilder builder)
    {

        base.OnModelCreating(builder);

        builder.Entity<UserRole>(entity =>
        {
            entity.HasOne(ur => ur.User)
                  .WithMany(u => u.UserRoles)
                  .HasForeignKey(ur => ur.UserId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
            entity.HasOne(ur => ur.Role)
                  .WithMany(r => r.UserRoles)
                  .HasForeignKey(ur => ur.RoleId)
                  .IsRequired()
                  .OnDelete(DeleteBehavior.Cascade);
        });


        #region [ Roles ]

        builder.Entity<Role>().HasData(
            new Role
            {
                Id = MasterDataConstants.AdminRoleId,
                Name = "Admin",
                NormalizedName = "ADMIN",
                RoleIcon = "default-icon",
                Summary = "Administrator role",
                Mission = "Manage the entire system",
                MainTasks = "Manage users, roles, and system settings"
            }
        );

        builder.Entity<Role>().HasData(
            new Role
            {
                Id = MasterDataConstants.MemberRoleId,
                Name = "Member",
                NormalizedName = "MEMBER",
                RoleIcon = "member-icon",
                Summary = "Member role",
                Mission = "Obey the rules",
                MainTasks = "Don't cause any troubles and damage"
            }
        );
        #endregion

        #region [ Admins and members ]

        using (var scope = serviceScopeFactory.CreateScope())
        {
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

            #region [ Doctor ]
            var mpGatdula = new User
            {
                Id = MasterDataConstants.DoctorIds[0],
                UserName = "MpGatdula",
                NormalizedUserName = "MPGATDULA",
                Email = "mpgatdula@gmail.com",
                NormalizedEmail = "MPGATDULA@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234567",
                PhoneNumberConfirmed = true,
                FirstName = "Mark",
                MiddleName = "Pogi",
                LastName = "Gatdula",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHash = userManager.PasswordHasher.HashPassword(mpGatdula, MasterDataConstants.DefaultPassword);
            mpGatdula.PasswordHash = passwordHash;

            builder.Entity<User>().HasData(mpGatdula);

            var jpHizon = new User
            {
                Id = MasterDataConstants.DoctorIds[1],
                UserName = "JpHizon",
                NormalizedUserName = "JPHIZON",
                Email = "jphizon@gmail.com",
                NormalizedEmail = "JPHIZON@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234568",
                PhoneNumberConfirmed = true,
                FirstName = "Jason Paul",
                MiddleName = "Swabe",
                LastName = "Hizon",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashHizon = userManager.PasswordHasher.HashPassword(jpHizon, MasterDataConstants.DefaultPassword);
            jpHizon.PasswordHash = passwordHashHizon;

            builder.Entity<User>().HasData(jpHizon);

            var drSmith = new User
            {
                Id = MasterDataConstants.DoctorIds[2],
                UserName = "DrSmith",
                NormalizedUserName = "DRSMITH",
                Email = "drsmith@gmail.com",
                NormalizedEmail = "DRSMITH@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234569",
                PhoneNumberConfirmed = true,
                FirstName = "John",
                MiddleName = "A.",
                LastName = "Smith",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashSmith = userManager.PasswordHasher.HashPassword(drSmith, MasterDataConstants.DefaultPassword);
            drSmith.PasswordHash = passwordHashSmith;

            builder.Entity<User>().HasData(drSmith);

            var drDoe = new User
            {
                Id = MasterDataConstants.DoctorIds[3],
                UserName = "DrDoe",
                NormalizedUserName = "DRDOE",
                Email = "drdoe@gmail.com",
                NormalizedEmail = "DRDOE@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234570",
                PhoneNumberConfirmed = true,
                FirstName = "Jane",
                MiddleName = "B.",
                LastName = "Doe",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashDoe = userManager.PasswordHasher.HashPassword(drDoe, MasterDataConstants.DefaultPassword);
            drDoe.PasswordHash = passwordHashDoe;

            builder.Entity<User>().HasData(drDoe);
            #endregion

            #region [ Nurse ]
            var emilyBrown = new User
            {
                Id = MasterDataConstants.NurseIds[0],
                UserName = "EmilyBrown",
                NormalizedUserName = "EMILYBROWN",
                Email = "emilybrown@gmail.com",
                NormalizedEmail = "EMILYBROWN@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234571",
                PhoneNumberConfirmed = true,
                FirstName = "Emily",
                MiddleName = "C.",
                LastName = "Brown",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashBrown = userManager.PasswordHasher.HashPassword(emilyBrown, MasterDataConstants.DefaultPassword);
            emilyBrown.PasswordHash = passwordHashBrown;

            builder.Entity<User>().HasData(emilyBrown);

            var michaelGreen = new User
            {
                Id = MasterDataConstants.NurseIds[1],
                UserName = "MichaelGreen",
                NormalizedUserName = "MICHAELGREEN",
                Email = "michaelgreen@gmail.com",
                NormalizedEmail = "MICHAELGREEN@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234572",
                PhoneNumberConfirmed = true,
                FirstName = "Michael",
                MiddleName = "D.",
                LastName = "Green",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashGreen = userManager.PasswordHasher.HashPassword(michaelGreen, MasterDataConstants.DefaultPassword);
            michaelGreen.PasswordHash = passwordHashGreen;

            builder.Entity<User>().HasData(michaelGreen);

            var sarahWhite = new User
            {
                Id = MasterDataConstants.NurseIds[2],
                UserName = "SarahWhite",
                NormalizedUserName = "SARAHWHITE",
                Email = "sarahwhite@gmail.com",
                NormalizedEmail = "SARAHWHITE@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234573",
                PhoneNumberConfirmed = true,
                FirstName = "Sarah",
                MiddleName = "E.",
                LastName = "White",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashWhite = userManager.PasswordHasher.HashPassword(sarahWhite, MasterDataConstants.DefaultPassword);
            sarahWhite.PasswordHash = passwordHashWhite;

            builder.Entity<User>().HasData(sarahWhite);

            var davidBlack = new User
            {
                Id = MasterDataConstants.NurseIds[3],
                UserName = "DavidBlack",
                NormalizedUserName = "DAVIDBLACK",
                Email = "davidblack@gmail.com",
                NormalizedEmail = "DAVIDBLACK@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234574",
                PhoneNumberConfirmed = true,
                FirstName = "David",
                MiddleName = "F.",
                LastName = "Black",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashBlack = userManager.PasswordHasher.HashPassword(davidBlack, MasterDataConstants.DefaultPassword);
            davidBlack.PasswordHash = passwordHashBlack;

            builder.Entity<User>().HasData(davidBlack);
            #endregion

            #region [ Financist ]
            var alexJohnson = new User
            {
                Id = MasterDataConstants.FinancistIds[0],
                UserName = "AlexJohnson",
                NormalizedUserName = "ALEXJOHNSON",
                Email = "alexjohnson@gmail.com",
                NormalizedEmail = "ALEXJOHNSON@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234569",
                PhoneNumberConfirmed = true,
                FirstName = "Alex",
                MiddleName = "M.",
                LastName = "Johnson",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashAlex = userManager.PasswordHasher.HashPassword(alexJohnson, MasterDataConstants.DefaultPassword);
            alexJohnson.PasswordHash = passwordHashAlex;

            builder.Entity<User>().HasData(alexJohnson);

            var emmaWilliams = new User
            {
                Id = MasterDataConstants.FinancistIds[1],
                UserName = "EmmaWilliams",
                NormalizedUserName = "EMMAWILLIAMS",
                Email = "emmawilliams@gmail.com",
                NormalizedEmail = "EMMAWILLIAMS@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234570",
                PhoneNumberConfirmed = true,
                FirstName = "Emma",
                MiddleName = "L.",
                LastName = "Williams",
                CreatedAt = new DateTime(2024, 1, 17),
            };

            var passwordHashEmma = userManager.PasswordHasher.HashPassword(emmaWilliams, MasterDataConstants.DefaultPassword);
            emmaWilliams.PasswordHash = passwordHashEmma;

            builder.Entity<User>().HasData(emmaWilliams);
            #endregion

            #region [ Patient ]
            // User 1: Apple Sy Ramos
            var appleRamos = new User
            {
                Id = MasterDataConstants.PatientsIds[0],
                UserName = "AsRamos",
                NormalizedUserName = "ASRAMOS",
                Email = "asramos@gmail.com",
                NormalizedEmail = "ASRAMOS@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234569",
                PhoneNumberConfirmed = true,
                FirstName = "Apple",
                MiddleName = "Sy",
                LastName = "Ramos",
                Phase = 1,
                Block = 1,
                Lot = 1,
                Street = "Orange",
                LotSize = 200,
                CreatedAt = new DateTime(2024, 1, 10),
            };

            var passwordHashApple = userManager.PasswordHasher.HashPassword(appleRamos, MasterDataConstants.DefaultPassword);
            appleRamos.PasswordHash = passwordHashApple;

            builder.Entity<User>().HasData(appleRamos);

            // User 2: Ronda Kidman Dela Cruz
            var rondaDelacruz = new User
            {
                Id = MasterDataConstants.PatientsIds[1],
                UserName = "RkDelacruz",
                NormalizedUserName = "RKDELACRUZ",
                Email = "rkdelacruz@gmail.com",
                NormalizedEmail = "RKDELACRUZ@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234560",
                PhoneNumberConfirmed = true,
                FirstName = "Ronda",
                MiddleName = "Kidman",
                LastName = "Dela Cruz",
                Phase = 2,
                Block = 2,
                Lot = 2,
                Street = "Lemon",
                LotSize = 250,
                CreatedAt = new DateTime(2024, 1, 2),
            };

            var passwordHashRonda = userManager.PasswordHasher.HashPassword(rondaDelacruz, MasterDataConstants.DefaultPassword);
            rondaDelacruz.PasswordHash = passwordHashRonda;

            builder.Entity<User>().HasData(rondaDelacruz);

            // User 3: Mary Mar Dantes
            var maryDantes = new User
            {
                Id = MasterDataConstants.PatientsIds[2],
                UserName = "MmDantes",
                NormalizedUserName = "MMDANTES",
                Email = "mmdantes@gmail.com",
                NormalizedEmail = "MMDANTES@GMAIL.COM",
                EmailConfirmed = true,
                PhoneNumber = "09191234560",
                PhoneNumberConfirmed = true,
                FirstName = "Mary",
                MiddleName = "Mar",
                LastName = "Dantes",
                Phase = 3,
                Block = 3,
                Lot = 3,
                Street = "Strawberry",
                LotSize = 280,
                CreatedAt = new DateTime(2024, 1, 5),
            };

            var passwordHashMary = userManager.PasswordHasher.HashPassword(maryDantes, MasterDataConstants.DefaultPassword);
            maryDantes.PasswordHash = passwordHashMary;

            builder.Entity<User>().HasData(maryDantes);
            #endregion
        }
        #endregion

        #region [ UserRoles ]
        #region[ Doctor ]
        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.DoctorIds[0],
                RoleId = MasterDataConstants.AdminRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.DoctorIds[1],
                RoleId = MasterDataConstants.AdminRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.DoctorIds[2],
                RoleId = MasterDataConstants.AdminRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.DoctorIds[3],
                RoleId = MasterDataConstants.AdminRoleId
            });
        #endregion

        #region [ Nurse ]
        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.NurseIds[0],
                RoleId = MasterDataConstants.AdminRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.NurseIds[1],
                RoleId = MasterDataConstants.AdminRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.NurseIds[2],
                RoleId = MasterDataConstants.AdminRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.NurseIds[3],
                RoleId = MasterDataConstants.AdminRoleId
            });
        #endregion

        #region [ Financist ]
        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.FinancistIds[0],
                RoleId = MasterDataConstants.AdminRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.FinancistIds[1],
                RoleId = MasterDataConstants.AdminRoleId
            });
        #endregion

        #region [ Patient ]
        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.PatientsIds[0],
                RoleId = MasterDataConstants.MemberRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.PatientsIds[1],
                RoleId = MasterDataConstants.MemberRoleId
            });

        builder.Entity<UserRole>().HasData(
            new UserRole
            {
                UserId = MasterDataConstants.PatientsIds[2],
                RoleId = MasterDataConstants.MemberRoleId
            });
        #endregion
        #endregion
    }
    #endregion
}