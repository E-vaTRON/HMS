using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blazor.Web.Migrations.IdentityDb
{
    /// <inheritdoc />
    public partial class Add_Financists : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "123e4567-e89b-12d3-a456-426614174000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "23f5a3fa-0ca2-410d-95c5-ba9fc96e3e0d", "AQAAAAIAAYagAAAAEAOhZtaate22pQcObzi50TBSIe0sGfRkgiTPLrws5saH550ESe0XfzPA7scdD3LSVQ==", "e7c2df88-227e-4b8e-9414-7c89b5e8443f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e60babb-15a9-4efe-837e-2ba0c1516afb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0ae088e-dd34-48ba-b9c7-d750d31a8b7c", "AQAAAAIAAYagAAAAEECHFUnJIJn9/q/ZFopW7hReMZw7+6y/l/SPCRfJTQsW9p7ucYpwv4nPO20gKl602w==", "c5023ac1-6c48-4a2a-80fe-0202f59d2ea1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acbf592e-ed02-4de5-af78-2fb17f2dc872",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "87e8feb6-b80f-4481-b78c-10d44401b18f", "AQAAAAIAAYagAAAAEJ+QxIvjkR1YhFVpsmxCQgkVRII06GcQ5EQMYmLvpZlXkpL3zdcKUWjx2lasjOQz6Q==", "7d493946-b259-470c-ba16-2561f791bcb3" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d889d787-4865-47d4-bdbf-bd830be88093", "AQAAAAIAAYagAAAAEBw4G6PzjbdwiO488yfoAbvn9W3IvZjetjKVJDc6j1YtYvh1Q/1VrJRQ+9OBhxaSyQ==", "72f5cb75-fd06-4516-8ebe-fa430d9951dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c54274f0-5603-4f37-a400-c17ac38a9060",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f1da4e1c-147d-4db4-ab2e-a8f7fe05392d", "AQAAAAIAAYagAAAAENJARwCxnKCDtqK/NdLVdK7HJX4ke8ep4B5e0I5CcB0/MTY4c9CB/LNxsPtu8nvZmA==", "fdeae6fb-8c43-44f1-bd9f-ccdd29fa2210" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c54274f0-5603-4f37-a400-c17ac38a9061",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c2f6c27e-f2de-48aa-8c45-7a30a733519b", "AQAAAAIAAYagAAAAEAaiXCP1A+PTghi0P1YJG1q/NLVl5fzQkl3kYG3wFaIUz7nekcXCElBi7q24A6YBTQ==", "44255684-712f-4f4f-a999-2f797b896098" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c54274f0-5603-4f37-a400-c17ac38a9062",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac2e27f1-7f02-437a-8fd3-43d4db0c7e7c", "AQAAAAIAAYagAAAAEFjp90GLAe9nXfc+DuTwvOZIP7RFBgaxWO6u6sqwaUJekvAKxW7QRCtiGoOBlF8OjA==", "888cf20a-a503-4179-9b4e-549177d5c4ee" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c54274f0-5603-4f37-a400-c17ac38a9063",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7afd14e6-bbd8-4ae7-915d-7ed1f16a58fc", "AQAAAAIAAYagAAAAEH7EPtlBvSJl7zziGxoCV26vNOGE32H8OSqhQKSWXyJtgiuGJSZAk8deYoUmKBtT/w==", "5b2abe14-8145-4ca6-bbf1-b95ed471946d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "680ec474-f0de-4f42-8cd7-03c19a619222", "AQAAAAIAAYagAAAAEL/I3RLl/pC+7DYwIdPfo5wBRJJpuytAvhfOOM0F4myEqCXaHuucZd2iRkVZC4Daew==", "eb22171f-9e35-4807-9c32-14986c6138d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ebdaa999-2966-4227-96e2-5a72582c0566",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d58f601b-aeb9-4a39-809c-3376bed5a6be", "AQAAAAIAAYagAAAAED4eziVaNZQ/FOmUrE1ESsu9nGaTsbcVYP2r/vbkszddQ4MA+bhIm9nv7pAapeT7iQ==", "a5f7b5f3-bd7d-43d5-8127-eca1751c9b3f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffbfde38-3b88-48b4-910d-86dbcab5ba02",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d9a55bcd-e5ac-4d4f-8b03-4e70ce53cdef", "AQAAAAIAAYagAAAAEJvkgJxq7cqrQLVL1cwWlBvTmd9uEJT+VWAug2GtrdJHQySwNHir/YLMBKz1s1xukw==", "38641f2d-ffa1-4263-92b6-589b73000d00" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "Block", "ConcurrencyStamp", "CoverImageUrl", "CreatedAt", "Email", "EmailConfirmed", "FirstName", "IsActive", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "Lot", "LotSize", "MiddleName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "Phase", "PhoneNumber", "PhoneNumberConfirmed", "ProfileImageUrl", "SecurityStamp", "SignalRConnectionId", "Street", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "884241b7-bbe3-43df-a18c-ba082a9ed9f5", 0, 0, "7cb85639-66a1-4ae2-a74a-23c5fd63c095", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "alexjohnson@gmail.com", true, "Alex", null, false, "Johnson", false, null, 0, 0, "M.", "ALEXJOHNSON@GMAIL.COM", "ALEXJOHNSON", "AQAAAAIAAYagAAAAEITCMLZ7G78nR4z2mR4cCpTYIfSRV+BHE5Py9iiefUPfetEqRrA5fvEJKHhnA5KiMw==", 0, "09191234569", true, null, "ba5d0714-f434-4059-818e-1c1c61eaf428", null, null, false, "AlexJohnson" },
                    { "cae4a30d-112b-4e59-8e25-e5b565262c09", 0, 0, "7b5d782c-3126-46d1-a565-8a9f5588afc3", null, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "emmawilliams@gmail.com", true, "Emma", null, false, "Williams", false, null, 0, 0, "L.", "EMMAWILLIAMS@GMAIL.COM", "EMMAWILLIAMS", "AQAAAAIAAYagAAAAEHfglvAgBLIT+3AjCMmB/Q2nR1UPUZMTUAf9Xb0CjOTQPBUhNYiVZvwcrXiHEwhqiQ==", 0, "09191234570", true, null, "0b3e1e77-4340-400b-bad1-2ea92219ab53", null, null, false, "EmmaWilliams" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId", "Id" },
                values: new object[,]
                {
                    { "123e4567-e89b-12d3-a456-426614174000", "884241b7-bbe3-43df-a18c-ba082a9ed9f5", null },
                    { "123e4567-e89b-12d3-a456-426614174000", "cae4a30d-112b-4e59-8e25-e5b565262c09", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "123e4567-e89b-12d3-a456-426614174000", "884241b7-bbe3-43df-a18c-ba082a9ed9f5" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "123e4567-e89b-12d3-a456-426614174000", "cae4a30d-112b-4e59-8e25-e5b565262c09" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "884241b7-bbe3-43df-a18c-ba082a9ed9f5");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cae4a30d-112b-4e59-8e25-e5b565262c09");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "123e4567-e89b-12d3-a456-426614174000",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "836f385c-6c03-4cb9-b011-311692015336", "AQAAAAIAAYagAAAAENhiE5RVn6UAm/hDVmxKnFY2jzbFXR0UH8BUlT2OrfGbaUexelzqPlm6qM8l4ZSgaQ==", "a6b31e15-6024-408a-9955-20790cfa2d56" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "9e60babb-15a9-4efe-837e-2ba0c1516afb",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "01c5ef30-595a-43d5-b8fc-4e759ae45f52", "AQAAAAIAAYagAAAAEPVOBMSotvMFLnX4mjcbrQgpD2Ek+p7LaOc/gvuVy6MjlFL9lrLZQXIJhlKEVDhB0w==", "cc112e70-27d6-47fe-954d-763e174e151d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acbf592e-ed02-4de5-af78-2fb17f2dc872",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e60f37f4-9f9e-4946-b997-36215c0758df", "AQAAAAIAAYagAAAAEAAp0NPA3wszbP0Uw7vnjjSTjKf4qSnRtjuNam8i/oplYlgbmSl4JPV4KrEi60Syqg==", "d1ce923f-db0b-4aa6-98c7-1c1d58d55180" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fdcd9c2f-e341-41e1-b079-d12449f19172", "AQAAAAIAAYagAAAAEDGc7DurooH2/Pa68SHOBH5ILUuI2mWV6B9ZQQVz9e+KLg5zddygH66ev+vN3xZLww==", "07c0dfeb-2917-4d7c-a2f8-4d8e455eda0c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c54274f0-5603-4f37-a400-c17ac38a9060",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "fe24abfa-7776-48fe-9ed7-07c66fdeb9a1", "AQAAAAIAAYagAAAAELkF3PTW23ZAAqii17rSztmVxCnhjj0TrGYU0Cfvsu5RSB48Uxza42TP3oJpa5eXZw==", "ef9b35a8-4b91-4c8c-acbd-21ed39ae94c1" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c54274f0-5603-4f37-a400-c17ac38a9061",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f0759c06-17e2-419c-930b-c28be8e880b6", "AQAAAAIAAYagAAAAECjIVLCS+rVlIwSi3PcjCLki4Iblo9HKXVSUUQptdc79+xdDQdDV4DYd8VyWZmuOJA==", "a10c4f7d-e1d3-423d-9c29-0bf40de3559a" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c54274f0-5603-4f37-a400-c17ac38a9062",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e6452a79-1424-4aec-8d01-745aa151eaf0", "AQAAAAIAAYagAAAAED85cg3kAJmR+MXGXof+1UhcrCeqQw7jWS90qS2IDFADemcKgrU00i2+YnD+JoGavg==", "6681e967-aee9-4514-904d-ae3c40de2ea5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "c54274f0-5603-4f37-a400-c17ac38a9063",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f7f7bb26-9811-4223-a30f-759f7c48743d", "AQAAAAIAAYagAAAAEPX8iFLaiZ93oDzqiSMnYG9UVhoTGoz2hVpi/ledoZYC/F8Rc0UTEGaslPZNwr//Yw==", "aaaaa32a-a2d6-425d-bf7a-2e855f87e442" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8b9ac4c3-4ac9-45b8-ae4e-dd7466e7ef93", "AQAAAAIAAYagAAAAEIBd4rNQhzw+fgxi87SRTdCEguKEA4yPW0sqetOA+JTdOMWEXyFQB99/YKNwVNCe+w==", "d455106d-c6da-4eae-bd96-0a1c8975023c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ebdaa999-2966-4227-96e2-5a72582c0566",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "796abecd-283e-4001-85c9-d52c3a151146", "AQAAAAIAAYagAAAAEBIhm0oxefVOccNdm0XaFG2pieUc3YTT1Q7fR4YBlG/BYWXY46GnmxMFeOSglLIBLg==", "0effefe8-e62e-4c4f-b64a-863ecf177432" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "ffbfde38-3b88-48b4-910d-86dbcab5ba02",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bc2bb453-1c49-4a2d-ab5e-c26bd78c8721", "AQAAAAIAAYagAAAAEC3ebAej+VFgwSWOyLj7lOlo0upLIqZHaZm9qI7u9+xbej9ZPeOu9nRDT0iCgF7RVQ==", "66ef9e05-aa39-4f84-a483-b6f34b8624d3" });
        }
    }
}
