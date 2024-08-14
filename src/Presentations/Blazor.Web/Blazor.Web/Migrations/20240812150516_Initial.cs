using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Blazor.Web.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Announcements",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RequestId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DrugIdAndQuantity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PaidDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExcessAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UnderPaidAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drugs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Supplier = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FileReference = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drugs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medias",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MediaName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MediaUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TimeUpload = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requests",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Symptoms = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoomId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BillId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ServiceId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserIds = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CalculationType = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BillServices",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BillId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BillServices_Bills_BillId",
                        column: x => x.BillId,
                        principalTable: "Bills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BillServices_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Bills",
                columns: new[] { "Id", "CreatedDate", "Deadline", "DrugIdAndQuantity", "ExcessAmount", "IsDeleted", "PaidDate", "RequestId", "UnderPaidAmount", "UserId" },
                values: new object[,]
                {
                    { new Guid("0d7c455c-2760-4066-ad1d-9e6b06c70d07"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4694), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "cef13006-aed2-49fc-a8ca-8c6d76132aff", 0m, "c54274f0-5603-4f37-a400-c17ac38a9063" },
                    { new Guid("2d125d02-0a2e-4d6e-bd6b-e3fd30228bd2"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4745), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "3b57d787-ad41-41e2-a722-3ff80d715f0a", 0m, "c54274f0-5603-4f37-a400-c17ac38a9063" },
                    { new Guid("33f6e693-2937-4b81-af7c-824bf6f261e1"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4762), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "90b7f2ff-9722-485e-adc8-9beffc464308", 0m, "c54274f0-5603-4f37-a400-c17ac38a9061" },
                    { new Guid("4cbdf427-92c9-4d23-bc91-7e6c9b4aa957"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4677), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "53b0d8dd-7ab7-4dc6-bf74-e4e9af84729a", 0m, "c54274f0-5603-4f37-a400-c17ac38a9062" },
                    { new Guid("832f5d74-0c38-410b-a2f2-e4a68d4c7f2f"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4850), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "b3c4c339-009c-4ea8-aa6c-872bcb856301", 0m, "c54274f0-5603-4f37-a400-c17ac38a9061" },
                    { new Guid("97ee73b5-113c-4028-8cde-e375513f804c"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4831), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "cdba01dc-5f65-46ec-a242-e927cc1ce6d2", 0m, "c54274f0-5603-4f37-a400-c17ac38a9063" },
                    { new Guid("b0476c18-2c93-4d48-8683-49e56c0eabc0"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4814), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "d9a5f3b8-9d40-44c8-9c0f-379ee9306ed9", 0m, "c54274f0-5603-4f37-a400-c17ac38a9062" },
                    { new Guid("bb45386c-1ef8-40f4-b466-84ac27ae6686"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4657), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "baed7369-a1f4-4311-b82f-b9e4a3b0de7b", 0m, "c54274f0-5603-4f37-a400-c17ac38a9061" },
                    { new Guid("df399d17-4fb2-4947-8159-313d53d340d7"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4710), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "da9dc981-ff65-4d43-8e7f-ae66e52b8039", 0m, "c54274f0-5603-4f37-a400-c17ac38a9061" },
                    { new Guid("f1b4da52-8c2b-4859-b32a-47b68ea13c40"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4727), new DateTime(2024, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "", 0m, false, null, "3bb48ace-95ae-448e-8b0e-5cd0d292552a", 0m, "c54274f0-5603-4f37-a400-c17ac38a9062" }
                });

            migrationBuilder.InsertData(
                table: "Drugs",
                columns: new[] { "Id", "CreatedDate", "Description", "FileReference", "IsDeleted", "OrderDate", "Price", "Supplier" },
                values: new object[,]
                {
                    { new Guid("01a7ebc1-21f0-4744-88ec-68861afd37bb"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4333), "Duloxetine", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 120m, "USA" },
                    { new Guid("1152c0cb-a974-462b-8c9c-14987a1debda"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4317), "Insulin Glargine", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 110m, "USA" },
                    { new Guid("242e8bc8-61a9-41c6-8683-4164b8e973c2"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4301), "Epoetin Alfa", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 90m, "USA" },
                    { new Guid("30e96ac3-d243-4de3-9995-94e2bdb2d12c"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4444), "Oseltamivir", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 190m, "USA" },
                    { new Guid("30ee967a-8b14-466c-ad12-06a05493cdd3"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4593), "Lenalidomide", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2000m, "USA" },
                    { new Guid("69516182-4353-4f3d-96d3-c28bb31e34cb"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4413), "Nebivolol", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 170m, "USA" },
                    { new Guid("7550f858-084b-4a47-86ca-c53a3fee8737"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4203), "Atorvastatin", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 10m, "USA" },
                    { new Guid("797c16b3-9f66-4619-8068-3af911e954c2"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4250), "Clopidogrel", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 20m, "USA" },
                    { new Guid("88125653-180b-445c-94bf-51003eb1c5d3"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4492), "Lisinopril", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 220m, "USA" },
                    { new Guid("8dc2966c-86db-4fdb-90ef-f1f4aaa0fbc7"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4507), "Metformin", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 230m, "USA" },
                    { new Guid("8fb48dac-ea0e-468c-8e40-94cbd52d53bc"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4234), "Esomeprazole", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 15m, "USA" },
                    { new Guid("a079289f-4ef6-4463-9884-d931108cb1dc"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4634), "Pembrolizumab", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2200m, "USA" },
                    { new Guid("a52bb0e3-4a2d-4bb9-a7df-7346b39d5ea4"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4428), "Buprenorphine/Naloxone", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 180m, "USA" },
                    { new Guid("abaf2a73-f650-4795-a8d6-7d517857c4c2"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4618), "Nivolumab", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 2100m, "USA" },
                    { new Guid("adcd5b3c-7c25-4e42-90ad-e957e9573b65"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4366), "Pregabalin", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 140m, "USA" },
                    { new Guid("c375d1ed-b87f-4dbb-a79e-0acceba91935"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4266), "Fluticasone/Salmeterol", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 30m, "USA" },
                    { new Guid("c450243b-0bef-422b-9209-0ed5a64ea359"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4459), "Lisinopril", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 200m, "USA" },
                    { new Guid("cd317c57-05b5-4b4f-8e40-314ee3d6992f"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4349), "Lisdexamfetamine", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 130m, "USA" },
                    { new Guid("cd9810ab-4363-48fb-ab22-8326b02b9300"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4474), "Amlodipine", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 210m, "USA" },
                    { new Guid("e4980c62-77a8-488a-9e47-cf8bfa0bf8c9"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4382), "Tiotropium", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 150m, "USA" },
                    { new Guid("ee21ce39-eedb-4873-a705-4f9aa73e9cae"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4398), "Sitagliptin", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 160m, "USA" },
                    { new Guid("ef582be5-01fd-41c7-8d03-a2746f281eff"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4284), "Montelukast", null, false, new DateTime(2024, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 60m, "USA" }
                });

            migrationBuilder.InsertData(
                table: "Requests",
                columns: new[] { "Id", "CreatedDate", "IsDeleted", "Symptoms", "UserId" },
                values: new object[,]
                {
                    { new Guid("3b57d787-ad41-41e2-a722-3ff80d715f0a"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5735), true, "7,24,20", "c54274f0-5603-4f37-a400-c17ac38a9063" },
                    { new Guid("3bb48ace-95ae-448e-8b0e-5cd0d292552a"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5718), true, "6,8,10", "c54274f0-5603-4f37-a400-c17ac38a9062" },
                    { new Guid("53b0d8dd-7ab7-4dc6-bf74-e4e9af84729a"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5618), true, "11,12,13", "c54274f0-5603-4f37-a400-c17ac38a9062" },
                    { new Guid("90b7f2ff-9722-485e-adc8-9beffc464308"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5751), true, "13,14,11", "c54274f0-5603-4f37-a400-c17ac38a9061" },
                    { new Guid("b3c4c339-009c-4ea8-aa6c-872bcb856301"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5799), true, "10,9,8", "c54274f0-5603-4f37-a400-c17ac38a9061" },
                    { new Guid("baed7369-a1f4-4311-b82f-b9e4a3b0de7b"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5598), true, "1,2,8", "c54274f0-5603-4f37-a400-c17ac38a9061" },
                    { new Guid("cdba01dc-5f65-46ec-a242-e927cc1ce6d2"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5783), true, "31,32,26", "c54274f0-5603-4f37-a400-c17ac38a9063" },
                    { new Guid("cef13006-aed2-49fc-a8ca-8c6d76132aff"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5634), true, "17,18,7", "c54274f0-5603-4f37-a400-c17ac38a9063" },
                    { new Guid("d9a5f3b8-9d40-44c8-9c0f-379ee9306ed9"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5768), true, "22,20,17", "c54274f0-5603-4f37-a400-c17ac38a9062" },
                    { new Guid("da9dc981-ff65-4d43-8e7f-ae66e52b8039"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5698), true, "26,27,32", "c54274f0-5603-4f37-a400-c17ac38a9061" }
                });

            migrationBuilder.InsertData(
                table: "RoomEvents",
                columns: new[] { "Id", "BillId", "CreatedDate", "EndDate", "IsDeleted", "RoomId", "ServiceId", "StartDate", "UserIds" },
                values: new object[,]
                {
                    { new Guid("1b8cebf1-ab2a-4823-9306-00ed165949d5"), "4cbdf427-92c9-4d23-bc91-7e6c9b4aa957", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5332), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "201", "7e08903a-ebaa-4be7-a760-cf4511c06db6", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3" },
                    { new Guid("28af7272-b7bd-47df-8c31-3fd481594e3b"), "bb45386c-1ef8-40f4-b466-84ac27ae6686", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5312), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "106", "5f2883f6-0a20-422c-8de5-643ec84baeab", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "c54274f0-5603-4f37-a400-c17ac38a9060" },
                    { new Guid("38a485ff-6540-41ea-b622-fa39b1b21661"), "f1b4da52-8c2b-4859-b32a-47b68ea13c40", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5458), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "105", "20b748be-29f9-4d9d-8fe0-09f9a8cffd7e", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "c54274f0-5603-4f37-a400-c17ac38a9060" },
                    { new Guid("41224e8f-4f2c-4d42-b2c0-12ca428f14a5"), "bb45386c-1ef8-40f4-b466-84ac27ae6686", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5272), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "105", "20b748be-29f9-4d9d-8fe0-09f9a8cffd7e", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "123e4567-e89b-12d3-a456-426614174000" },
                    { new Guid("550089e0-2e9c-4f9e-b0a6-cd2934911390"), "832f5d74-0c38-410b-a2f2-e4a68d4c7f2f", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5548), new DateTime(2024, 1, 31, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "206", "0e1bf384-2ef7-48a8-bfbb-458afa0824ce", new DateTime(2024, 1, 31, 14, 24, 5, 0, DateTimeKind.Unspecified), "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3" },
                    { new Guid("5b054ebf-8605-48f6-92d2-a527a90ff29b"), "4cbdf427-92c9-4d23-bc91-7e6c9b4aa957", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5349), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "202", "26230c2b-2edd-486d-837a-3feca30bcb03", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c" },
                    { new Guid("691f83d7-312d-49d2-be62-479074864112"), "2d125d02-0a2e-4d6e-bd6b-e3fd30228bd2", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5475), new DateTime(2024, 1, 27, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "106", "5f2883f6-0a20-422c-8de5-643ec84baeab", new DateTime(2024, 1, 27, 14, 24, 5, 0, DateTimeKind.Unspecified), "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3" },
                    { new Guid("6bf0b352-d085-46ef-8f21-bf9a2b441c61"), "f1b4da52-8c2b-4859-b32a-47b68ea13c40", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5439), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "404", "86af131d-b2ab-4ad2-9301-e58be72d360f", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "123e4567-e89b-12d3-a456-426614174000" },
                    { new Guid("6dc55127-c828-411f-8492-4f8b423aa57c"), "df399d17-4fb2-4947-8159-313d53d340d7", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5422), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "301", "33454a63-5962-4b58-aab6-7b067015d622", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c" },
                    { new Guid("7f88340c-20cd-470b-82d9-a0dfb5fbff4a"), "df399d17-4fb2-4947-8159-313d53d340d7", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5406), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "105", "b47b3a07-adbd-4ff1-8cd2-8daf847957d7", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "b7ec8772-8d5c-4fec-92e0-dfc7bfa74dd3" },
                    { new Guid("92f49ddb-f751-4d3e-8839-7a4b0e52d8e8"), "0d7c455c-2760-4066-ad1d-9e6b06c70d07", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5388), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "206", "0e1bf384-2ef7-48a8-bfbb-458afa0824ce", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "c54274f0-5603-4f37-a400-c17ac38a9060" },
                    { new Guid("957279de-9814-409d-bc90-068e34435cab"), "b0476c18-2c93-4d48-8683-49e56c0eabc0", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5509), new DateTime(2024, 1, 29, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "202", "26230c2b-2edd-486d-837a-3feca30bcb03", new DateTime(2024, 1, 29, 14, 24, 5, 0, DateTimeKind.Unspecified), "123e4567-e89b-12d3-a456-426614174000" },
                    { new Guid("a3087864-4912-46ca-b429-469a6a360498"), "97ee73b5-113c-4028-8cde-e375513f804c", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5531), new DateTime(2024, 1, 30, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "203", "454696bc-7737-4792-a3ec-f160f2117296", new DateTime(2024, 1, 30, 14, 24, 5, 0, DateTimeKind.Unspecified), "c54274f0-5603-4f37-a400-c17ac38a9060" },
                    { new Guid("a6f133ac-7e8c-4d59-9fb2-088de81d2986"), "33f6e693-2937-4b81-af7c-824bf6f261e1", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5492), new DateTime(2024, 1, 28, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "201", "7e08903a-ebaa-4be7-a760-cf4511c06db6", new DateTime(2024, 1, 28, 14, 24, 5, 0, DateTimeKind.Unspecified), "cbf20cfa-b5f9-43ca-a54c-e2a18f05fd1c" },
                    { new Guid("e5fd1ba3-3b35-472d-9cb5-4d215f73be37"), "0d7c455c-2760-4066-ad1d-9e6b06c70d07", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5366), new DateTime(2024, 1, 17, 15, 24, 5, 0, DateTimeKind.Unspecified), false, "203", "454696bc-7737-4792-a3ec-f160f2117296", new DateTime(2024, 1, 17, 14, 24, 5, 0, DateTimeKind.Unspecified), "123e4567-e89b-12d3-a456-426614174000" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "CalculationType", "Color", "CreatedDate", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("0e1bf384-2ef7-48a8-bfbb-458afa0824ce"), 0, "#92d050", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4084), false, "Physical Therapy Session", 100.00m },
                    { new Guid("20b748be-29f9-4d9d-8fe0-09f9a8cffd7e"), 0, "#92d050", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(3943), false, "Routine Check-up", 80.00m },
                    { new Guid("26230c2b-2edd-486d-837a-3feca30bcb03"), 0, "#ff0000", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4046), false, "MRI Scan", 750.00m },
                    { new Guid("33454a63-5962-4b58-aab6-7b067015d622"), 0, "#92d050", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4159), false, "Eye Examination", 100.00m },
                    { new Guid("454696bc-7737-4792-a3ec-f160f2117296"), 0, "#ffc000", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4063), false, "Ultrasound", 300.00m },
                    { new Guid("5f2883f6-0a20-422c-8de5-643ec84baeab"), 0, "#92d050", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4007), false, "Blood Test", 30.00m },
                    { new Guid("7e08903a-ebaa-4be7-a760-cf4511c06db6"), 0, "#ffc000", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4027), false, "X-ray", 150.00m },
                    { new Guid("86af131d-b2ab-4ad2-9301-e58be72d360f"), 0, "#92d050", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4183), false, "Dermatology Consultation", 120.00m },
                    { new Guid("b47b3a07-adbd-4ff1-8cd2-8daf847957d7"), 0, "#ffc000", new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4101), false, "Dental Cleaning", 150.00m }
                });

            migrationBuilder.InsertData(
                table: "BillServices",
                columns: new[] { "Id", "BillId", "CreatedDate", "IsDeleted", "ServiceId" },
                values: new object[,]
                {
                    { new Guid("150a759a-34dc-4d98-a7fd-688280e35f1c"), new Guid("0d7c455c-2760-4066-ad1d-9e6b06c70d07"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5005), false, new Guid("0e1bf384-2ef7-48a8-bfbb-458afa0824ce") },
                    { new Guid("27ec1b1d-14ce-4d63-a44b-b0552f7c63a5"), new Guid("4cbdf427-92c9-4d23-bc91-7e6c9b4aa957"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4956), false, new Guid("26230c2b-2edd-486d-837a-3feca30bcb03") },
                    { new Guid("2f0203ee-4616-4d47-aa1a-f34d0af4b3a5"), new Guid("b0476c18-2c93-4d48-8683-49e56c0eabc0"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5201), false, new Guid("26230c2b-2edd-486d-837a-3feca30bcb03") },
                    { new Guid("2fa34f5d-c3d6-4b34-a0b2-bbe9184a88a9"), new Guid("bb45386c-1ef8-40f4-b466-84ac27ae6686"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4920), false, new Guid("5f2883f6-0a20-422c-8de5-643ec84baeab") },
                    { new Guid("30f084ff-ff49-4d7f-9e6c-7108254edd04"), new Guid("df399d17-4fb2-4947-8159-313d53d340d7"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5023), false, new Guid("b47b3a07-adbd-4ff1-8cd2-8daf847957d7") },
                    { new Guid("5c928507-55d7-40e9-a8d2-f689e89b8b69"), new Guid("df399d17-4fb2-4947-8159-313d53d340d7"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5041), false, new Guid("33454a63-5962-4b58-aab6-7b067015d622") },
                    { new Guid("60845a54-c54f-4076-b0fc-42a602485a73"), new Guid("97ee73b5-113c-4028-8cde-e375513f804c"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5218), false, new Guid("454696bc-7737-4792-a3ec-f160f2117296") },
                    { new Guid("666fc434-7505-458e-84a9-1f4645b556b4"), new Guid("f1b4da52-8c2b-4859-b32a-47b68ea13c40"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5145), false, new Guid("20b748be-29f9-4d9d-8fe0-09f9a8cffd7e") },
                    { new Guid("89c4ab7f-ff7d-4209-946f-5e56f71fedef"), new Guid("4cbdf427-92c9-4d23-bc91-7e6c9b4aa957"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4938), false, new Guid("7e08903a-ebaa-4be7-a760-cf4511c06db6") },
                    { new Guid("8ec32cab-9ada-4303-9f4e-01b7ffb9187c"), new Guid("0d7c455c-2760-4066-ad1d-9e6b06c70d07"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4985), false, new Guid("454696bc-7737-4792-a3ec-f160f2117296") },
                    { new Guid("92599a92-68cd-4f0f-b2b0-c5d61111f278"), new Guid("f1b4da52-8c2b-4859-b32a-47b68ea13c40"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5058), false, new Guid("86af131d-b2ab-4ad2-9301-e58be72d360f") },
                    { new Guid("94d6ae2b-9e94-4203-a23f-5d9c79c09362"), new Guid("33f6e693-2937-4b81-af7c-824bf6f261e1"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5180), false, new Guid("7e08903a-ebaa-4be7-a760-cf4511c06db6") },
                    { new Guid("afcb56e6-3d30-4f88-96bd-68e89c4bf572"), new Guid("832f5d74-0c38-410b-a2f2-e4a68d4c7f2f"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5236), false, new Guid("0e1bf384-2ef7-48a8-bfbb-458afa0824ce") },
                    { new Guid("c38be7c3-4fe2-4d6d-bce9-299b590e5733"), new Guid("bb45386c-1ef8-40f4-b466-84ac27ae6686"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(4893), false, new Guid("20b748be-29f9-4d9d-8fe0-09f9a8cffd7e") },
                    { new Guid("dbc5c03b-200f-4b28-a5dd-0830dae31f4a"), new Guid("2d125d02-0a2e-4d6e-bd6b-e3fd30228bd2"), new DateTime(2024, 8, 12, 22, 5, 15, 302, DateTimeKind.Local).AddTicks(5164), false, new Guid("5f2883f6-0a20-422c-8de5-643ec84baeab") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_BillServices_BillId",
                table: "BillServices",
                column: "BillId");

            migrationBuilder.CreateIndex(
                name: "IX_BillServices_ServiceId",
                table: "BillServices",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements");

            migrationBuilder.DropTable(
                name: "BillServices");

            migrationBuilder.DropTable(
                name: "Drugs");

            migrationBuilder.DropTable(
                name: "Medias");

            migrationBuilder.DropTable(
                name: "Requests");

            migrationBuilder.DropTable(
                name: "RoomEvents");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
