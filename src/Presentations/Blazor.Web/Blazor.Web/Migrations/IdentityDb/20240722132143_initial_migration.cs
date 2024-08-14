using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blazor.Web.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class initial_migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleIcon = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Mission = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MainTasks = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CoverImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    SignalRConnectionId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase = table.Column<int>(type: "int", nullable: false),
                    Block = table.Column<int>(type: "int", nullable: false),
                    Lot = table.Column<int>(type: "int", nullable: false),
                    LotSize = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Id = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "MainTasks", "Mission", "Name", "NormalizedName", "RoleIcon", "Summary" },
                values: new object[,]
                {
                    { "123e4567-e89b-12d3-a456-426614174000", null, "Manage users, roles, and system settings", "Manage the entire system", "Admin", "ADMIN", "default-icon", "Administrator role" },
                    { "c54274f0-5603-4f37-a400-c17ac38a9060", null, "Don't cause any troubles and damage", "Obey the rules", "Member", "MEMBER", "member-icon", "Member role" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Block", "ConcurrencyStamp", "CoverImageUrl", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "Lot", "LotSize", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phase", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImageUrl", "SecurityStamp", "SignalRConnectionId", "Street", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "123e4567-e89b-12d3-a456-426614174000", 0, 0, "836f385c-6c03-4cb9-b011-311692015336", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "mpgatdula@gmail.com", true, "Mark", null, false, "Gatdula", false, null, 0, 0, "Pogi", "MPGATDULA@GMAIL.COM", "MPGATDULA", "AQAAAAIAAYagAAAAENhiE5RVn6UAm/hDVmxKnFY2jzbFXR0UH8BUlT2OrfGbaUexelzqPlm6qM8l4ZSgaQ==", 0, "09191234567", true, null, "a6b31e15-6024-408a-9955-20790cfa2d56", null, null, false, "MpGatdula" },
                    { "9e60babb-15a9-4efe-837e-2ba0c1516afb", 0, 0, "01c5ef30-595a-43d5-b8fc-4e759ae45f52", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "michaelgreen@gmail.com", true, "Michael", null, false, "Green", false, null, 0, 0, "D.", "MICHAELGREEN@GMAIL.COM", "MICHAELGREEN", "AQAAAAIAAYagAAAAEPVOBMSotvMFLnX4mjcbrQgpD2Ek+p7LaOc/gvuVy6MjlFL9lrLZQXIJhlKEVDhB0w==", 0, "09191234572", true, null, "cc112e70-27d6-47fe-954d-763e174e151d", null, null, false, "MichaelGreen" },
                    { "acbf592e-ed02-4de5-af78-2fb17f2dc872", 0, 0, "e60f37f4-9f9e-4946-b997-36215c0758df", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "davidblack@gmail.com", true, "David", null, false, "Black", false, null, 0, 0, "F.", "DAVIDBLACK@GMAIL.COM", "DAVIDBLACK", "AQAAAAIAAYagAAAAEAAp0NPA3wszbP0Uw7vnjjSTjKf4qSnRtjuNam8i/oplYlgbmSl4JPV4KrEi60Syqg==", 0, "09191234574", true, null, "d1ce923f-db0b-4aa6-98c7-1c1d58d55180", null, null, false, "DavidBlack" },
                    { "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3", 0, 0, "fdcd9c2f-e341-41e1-b079-d12449f19172", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "drsmith@gmail.com", true, "John", null, false, "Smith", false, null, 0, 0, "A.", "DRSMITH@GMAIL.COM", "DRSMITH", "AQAAAAIAAYagAAAAEDGc7DurooH2/Pa68SHOBH5ILUuI2mWV6B9ZQQVz9e+KLg5zddygH66ev+vN3xZLww==", 0, "09191234569", true, null, "07c0dfeb-2917-4d7c-a2f8-4d8e455eda0c", null, null, false, "DrSmith" },
                    { "c54274f0-5603-4f37-a400-c17ac38a9060", 0, 0, "fe24abfa-7776-48fe-9ed7-07c66fdeb9a1", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "jphizon@gmail.com", true, "Jason Paul", null, false, "Hizon", false, null, 0, 0, "Swabe", "JPHIZON@GMAIL.COM", "JPHIZON", "AQAAAAIAAYagAAAAELkF3PTW23ZAAqii17rSztmVxCnhjj0TrGYU0Cfvsu5RSB48Uxza42TP3oJpa5eXZw==", 0, "09191234568", true, null, "ef9b35a8-4b91-4c8c-acbd-21ed39ae94c1", null, null, false, "JpHizon" },
                    { "c54274f0-5603-4f37-a400-c17ac38a9061", 0, 1, "f0759c06-17e2-419c-930b-c28be8e880b6", null, new DateTime(2024, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "asramos@gmail.com", true, "Apple", null, false, "Ramos", false, null, 1, 200, "Sy", "ASRAMOS@GMAIL.COM", "ASRAMOS", "AQAAAAIAAYagAAAAECjIVLCS+rVlIwSi3PcjCLki4Iblo9HKXVSUUQptdc79+xdDQdDV4DYd8VyWZmuOJA==", 1, "09191234569", true, null, "a10c4f7d-e1d3-423d-9c29-0bf40de3559a", null, "Orange", false, "AsRamos" },
                    { "c54274f0-5603-4f37-a400-c17ac38a9062", 0, 2, "e6452a79-1424-4aec-8d01-745aa151eaf0", null, new DateTime(2024, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "rkdelacruz@gmail.com", true, "Ronda", null, false, "Dela Cruz", false, null, 2, 250, "Kidman", "RKDELACRUZ@GMAIL.COM", "RKDELACRUZ", "AQAAAAIAAYagAAAAED85cg3kAJmR+MXGXof+1UhcrCeqQw7jWS90qS2IDFADemcKgrU00i2+YnD+JoGavg==", 2, "09191234560", true, null, "6681e967-aee9-4514-904d-ae3c40de2ea5", null, "Lemon", false, "RkDelacruz" },
                    { "c54274f0-5603-4f37-a400-c17ac38a9063", 0, 3, "f7f7bb26-9811-4223-a30f-759f7c48743d", null, new DateTime(2024, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "mmdantes@gmail.com", true, "Mary", null, false, "Dantes", false, null, 3, 280, "Mar", "MMDANTES@GMAIL.COM", "MMDANTES", "AQAAAAIAAYagAAAAEPX8iFLaiZ93oDzqiSMnYG9UVhoTGoz2hVpi/ledoZYC/F8Rc0UTEGaslPZNwr//Yw==", 3, "09191234560", true, null, "aaaaa32a-a2d6-425d-bf7a-2e855f87e442", null, "Strawberry", false, "MmDantes" },
                    { "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c", 0, 0, "8b9ac4c3-4ac9-45b8-ae4e-dd7466e7ef93", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "drdoe@gmail.com", true, "Jane", null, false, "Doe", false, null, 0, 0, "B.", "DRDOE@GMAIL.COM", "DRDOE", "AQAAAAIAAYagAAAAEIBd4rNQhzw+fgxi87SRTdCEguKEA4yPW0sqetOA+JTdOMWEXyFQB99/YKNwVNCe+w==", 0, "09191234570", true, null, "d455106d-c6da-4eae-bd96-0a1c8975023c", null, null, false, "DrDoe" },
                    { "ebdaa999-2966-4227-96e2-5a72582c0566", 0, 0, "796abecd-283e-4001-85c9-d52c3a151146", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "sarahwhite@gmail.com", true, "Sarah", null, false, "White", false, null, 0, 0, "E.", "SARAHWHITE@GMAIL.COM", "SARAHWHITE", "AQAAAAIAAYagAAAAEBIhm0oxefVOccNdm0XaFG2pieUc3YTT1Q7fR4YBlG/BYWXY46GnmxMFeOSglLIBLg==", 0, "09191234573", true, null, "0effefe8-e62e-4c4f-b64a-863ecf177432", null, null, false, "SarahWhite" },
                    { "ffbfde38-3b88-48b4-910d-86dbcab5ba02", 0, 0, "bc2bb453-1c49-4a2d-ab5e-c26bd78c8721", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "emilybrown@gmail.com", true, "Emily", null, false, "Brown", false, null, 0, 0, "C.", "EMILYBROWN@GMAIL.COM", "EMILYBROWN", "AQAAAAIAAYagAAAAEC3ebAej+VFgwSWOyLj7lOlo0upLIqZHaZm9qI7u9+xbej9ZPeOu9nRDT0iCgF7RVQ==", 0, "09191234571", true, null, "66ef9e05-aa39-4f84-a483-b6f34b8624d3", null, null, false, "EmilyBrown" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Id" },
                values: new object[,]
                {
                    { "123e4567-e89b-12d3-a456-426614174000", "123e4567-e89b-12d3-a456-426614174000", null },
                    { "123e4567-e89b-12d3-a456-426614174000", "9e60babb-15a9-4efe-837e-2ba0c1516afb", null },
                    { "123e4567-e89b-12d3-a456-426614174000", "acbf592e-ed02-4de5-af78-2fb17f2dc872", null },
                    { "123e4567-e89b-12d3-a456-426614174000", "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3", null },
                    { "123e4567-e89b-12d3-a456-426614174000", "c54274f0-5603-4f37-a400-c17ac38a9060", null },
                    { "c54274f0-5603-4f37-a400-c17ac38a9060", "c54274f0-5603-4f37-a400-c17ac38a9061", null },
                    { "c54274f0-5603-4f37-a400-c17ac38a9060", "c54274f0-5603-4f37-a400-c17ac38a9062", null },
                    { "c54274f0-5603-4f37-a400-c17ac38a9060", "c54274f0-5603-4f37-a400-c17ac38a9063", null },
                    { "123e4567-e89b-12d3-a456-426614174000", "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c", null },
                    { "123e4567-e89b-12d3-a456-426614174000", "ebdaa999-2966-4227-96e2-5a72582c0566", null },
                    { "123e4567-e89b-12d3-a456-426614174000", "ffbfde38-3b88-48b4-910d-86dbcab5ba02", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
