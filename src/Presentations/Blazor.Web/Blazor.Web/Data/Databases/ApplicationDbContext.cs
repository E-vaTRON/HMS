using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Blazor.Web;

public class ApplicationDbContext : DbContext
{
    #region [ CTors ]

    public ApplicationDbContext(DbContextOptions options) : base(options) { }
    #endregion

    #region [ DbSets ]

    public virtual DbSet<Bill> Bills { get; set; } = null!;
    public virtual DbSet<Media> Medias { get; set; } = null!;
    public virtual DbSet<Drug> Drugs { get; set; } = null!;
    public virtual DbSet<Service> Services { get; set; } = null!;
    public virtual DbSet<BillServices> BillServices { get; set; } = null!;
    public virtual DbSet<Announcement> Announcements { get; set; } = null!;
    public virtual DbSet<RoomEvent> RoomEvents { get; set; } = null!;
    public virtual DbSet<Request> Requests { get; set; } = null!;
    #endregion

    #region [ Overrides ]

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<BillServices>()
               .Property(b => b.Quantity)
               .HasDefaultValue(1);

        builder.Entity<Drug>()
               .Property(b => b.Quantity)
               .HasDefaultValue(1);

        base.OnModelCreating(builder);

        #region [ Services ]

        //builder.Entity<Service>().HasData(new Service
        //{
        //    Id = Guid.Parse(MasterDataConstants.MonthlyDueServiceId),
        //    Name = "Monthly Due",
        //    Color = "#ffc000",
        //    Price = 3.25m,
        //    CalculationType = CalculationType.LotSizeMultiplication,
        //});

        //builder.Entity<Service>().HasData(new Service
        //{
        //    Id = Guid.Parse(MasterDataConstants.PenaltyServiceId),
        //    Name = "Penalty",
        //    Color = "#ff0000",
        //    Price = 0.325m,
        //    CalculationType = CalculationType.LotSizeMultiplication,
        //});

        //builder.Entity<Service>().HasData(new Service
        //{
        //    Id = Guid.Parse(MasterDataConstants.CarStickerServiceId),
        //    Name = "Car Sticker",
        //    Color = "#92d050",
        //    Price = 100.00m,
        //    CalculationType = CalculationType.DirectAddition,
        //});

        //var medicalAdviceService = new Service
        //{
        //    Id = Guid.Parse(MasterDataConstants.MedicalAdviceId),
        //    Name = "Medical Advice",
        //    Color = "#ffd52a",
        //    Price = 150.00m,
        //    CalculationType = CalculationType.DirectAddition,
        //};

        //builder.Entity<Service>().HasData(medicalAdviceService);

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[0]),
            Name = "Routine Check-up",
            Color = "#92d050", // Green for simple services
            Price = 80.00m,
            CalculationType = CalculationType.DirectAddition,
        });

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[1]),
            Name = "Blood Test",
            Color = "#92d050", // Green for simple services
            Price = 30.00m,
            CalculationType = CalculationType.DirectAddition,
        });

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[2]),
            Name = "X-ray",
            Color = "#ffc000", // Yellow for moderately complex services
            Price = 150.00m,
            CalculationType = CalculationType.DirectAddition,
        });

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[3]),
            Name = "MRI Scan",
            Color = "#ff0000", // Red for complex services
            Price = 750.00m,
            CalculationType = CalculationType.DirectAddition,
        });

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[4]),
            Name = "Ultrasound",
            Color = "#ffc000", // Yellow for moderately complex services
            Price = 300.00m,
            CalculationType = CalculationType.DirectAddition,
        });

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[5]),
            Name = "Physical Therapy Session",
            Color = "#92d050", // Green for simple services
            Price = 100.00m,
            CalculationType = CalculationType.DirectAddition,
        });

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[7]),
            Name = "Dental Cleaning",
            Color = "#ffc000", // Yellow for moderately complex services
            Price = 150.00m,
            CalculationType = CalculationType.DirectAddition,
        });

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[8]),
            Name = "Eye Examination",
            Color = "#92d050", // Green for simple services
            Price = 100.00m,
            CalculationType = CalculationType.DirectAddition,
        });

        builder.Entity<Service>().HasData(new Service
        {
            Id = Guid.Parse(MasterDataConstants.ServiceIds[9]),
            Name = "Dermatology Consultation",
            Color = "#92d050", // Green for simple services
            Price = 120.00m,
            CalculationType = CalculationType.DirectAddition,
        });
        #endregion

        #region [ Drugs ]

        //builder.Entity<Expense>().HasData(new Expense
        //{
        //    Id = Guid.Parse(MasterDataConstants.BallpenId),
        //    Description = "Ballpen",
        //    Supplier = "Paseo Mall Sta. Rosa Laguna",
        //    PhoneNumber = "09171234561",
        //    Email = "nbs@gmail.com",
        //    Address = "Paseo Mall Sta. Rosa Laguna",
        //    UserReference = "Mark",
        //    Price = 20.00m,
        //    Quantity = 10,
        //    OrderDate = new DateTime(2024, 1, 17),
        //});

        //builder.Entity<Expense>().HasData(new Expense
        //{
        //    Id = Guid.Parse(MasterDataConstants.ElectricStandFanId),
        //    Description = "Electric stand fan",
        //    Supplier = "Abenson",
        //    PhoneNumber = "09171234562",
        //    Email = "abenson@gmail.com",
        //    Address = "Solenad Mall Sta. Rosa Laguna",
        //    UserReference = "Jayson Paul",
        //    Price = decimal.Parse("1,500.00", CultureInfo.GetCultureInfo("en-PH")),
        //    Quantity = 2,
        //    OrderDate = new DateTime(2024, 1, 17),
        //});
        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[0]),
            Description = "Atorvastatin",
            Supplier = "USA",
            Price = 10m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[1]),
            Description = "Esomeprazole",
            Supplier = "USA",
            Price = 15m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[2]),
            Description = "Clopidogrel",
            Supplier = "USA",
            Price = 20m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[3]),
            Description = "Fluticasone/Salmeterol",
            Supplier = "USA",
            Price = 30m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[4]),
            Description = "Montelukast",
            Supplier = "USA",
            Price = 60m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[5]),
            Description = "Epoetin Alfa",
            Supplier = "USA",
            Price = 90m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[6]),
            Description = "Insulin Glargine",
            Supplier = "USA",
            Price = 110m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[7]),
            Description = "Duloxetine",
            Supplier = "USA",
            Price = 120m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[8]),
            Description = "Lisdexamfetamine",
            Supplier = "USA",
            Price = 130m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[9]),
            Description = "Pregabalin",
            Supplier = "USA",
            Price = 140m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[10]),
            Description = "Tiotropium",
            Supplier = "USA",
            Price = 150m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[11]),
            Description = "Sitagliptin",
            Supplier = "USA",
            Price = 160m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[12]),
            Description = "Nebivolol",
            Supplier = "USA",
            Price = 170m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[13]),
            Description = "Buprenorphine/Naloxone",
            Supplier = "USA",
            Price = 180m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[14]),
            Description = "Oseltamivir",
            Supplier = "USA",
            Price = 190m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[15]),
            Description = "Lisinopril",
            Supplier = "USA",
            Price = 200m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[16]),
            Description = "Amlodipine",
            Supplier = "USA",
            Price = 210m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[17]),
            Description = "Lisinopril",
            Supplier = "USA",
            Price = 220m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[18]),
            Description = "Metformin",
            Supplier = "USA",
            Price = 230m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[19]),
            Description = "Lenalidomide",
            Supplier = "USA",
            Price = 2000m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[20]),
            Description = "Nivolumab",
            Supplier = "USA",
            Price = 2100m,
            OrderDate = new DateTime(2024, 1, 17),
        });

        builder.Entity<Drug>().HasData(new Drug
        {
            Id = Guid.Parse(MasterDataConstants.DrugIds[21]),
            Description = "Pembrolizumab",
            Supplier = "USA",
            Price = 2200m,
            OrderDate = new DateTime(2024, 1, 17),
        });
        #endregion

        #region [ Bills ]

        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[0]),
            UserId = MasterDataConstants.PatientsIds[0],
            RequestId = MasterDataConstants.FirstTenRequestIds[0],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[1]),
            UserId = MasterDataConstants.PatientsIds[1],
            RequestId = MasterDataConstants.FirstTenRequestIds[1],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[2]),
            UserId = MasterDataConstants.PatientsIds[2],
            RequestId = MasterDataConstants.FirstTenRequestIds[2],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[3]),
            UserId = MasterDataConstants.PatientsIds[0],
            RequestId = MasterDataConstants.FirstTenRequestIds[3],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[4]),
            UserId = MasterDataConstants.PatientsIds[1],
            RequestId = MasterDataConstants.FirstTenRequestIds[4],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[5]),
            UserId = MasterDataConstants.PatientsIds[2],
            RequestId = MasterDataConstants.FirstTenRequestIds[5],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[6]),
            UserId = MasterDataConstants.PatientsIds[0],
            RequestId = MasterDataConstants.FirstTenRequestIds[6],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[7]),
            UserId = MasterDataConstants.PatientsIds[1],
            RequestId = MasterDataConstants.FirstTenRequestIds[7],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[8]),
            UserId = MasterDataConstants.PatientsIds[2],
            RequestId = MasterDataConstants.FirstTenRequestIds[8],
            Deadline = new DateTime(2024, 06, 30)
        });
        builder.Entity<Bill>().HasData(new Bill
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenBillIds[9]),
            UserId = MasterDataConstants.PatientsIds[0],
            RequestId = MasterDataConstants.FirstTenRequestIds[9],
            Deadline = new DateTime(2024, 06, 30)
        });
        #endregion

        #region [ Bill Service ]

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[0]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[0]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });
        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[0]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[1]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[1]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[2]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });
        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[1]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[3]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[2]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[4]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });
        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[2]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[5]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[3]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[7]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });
        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[3]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[8]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[4]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[9]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });
        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[4]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[0]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[5]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[1]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[6]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[2]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[7]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[3]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[8]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[4]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<BillServices>().HasData(new
        {
            Id = Guid.NewGuid(),
            BillId = Guid.Parse(MasterDataConstants.FirstTenBillIds[9]),
            ServiceId = Guid.Parse(MasterDataConstants.ServiceIds[5]),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });
        #endregion

        #region [ Room Events ]
        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "105", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[0],
            ServiceId = MasterDataConstants.ServiceIds[0],
            UserIds = MasterDataConstants.DoctorIds[0],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "106", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[0],
            ServiceId = MasterDataConstants.ServiceIds[1],
            UserIds = MasterDataConstants.DoctorIds[1],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "201", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[1],
            ServiceId = MasterDataConstants.ServiceIds[2],
            UserIds = MasterDataConstants.DoctorIds[2],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "202", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[1],
            ServiceId = MasterDataConstants.ServiceIds[3],
            UserIds = MasterDataConstants.DoctorIds[3],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "203", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[2],
            ServiceId = MasterDataConstants.ServiceIds[4],
            UserIds = MasterDataConstants.DoctorIds[0],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "206", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[2],
            ServiceId = MasterDataConstants.ServiceIds[5],
            UserIds = MasterDataConstants.DoctorIds[1],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "105", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[3],
            ServiceId = MasterDataConstants.ServiceIds[7],
            UserIds = MasterDataConstants.DoctorIds[2],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "301", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[3],
            ServiceId = MasterDataConstants.ServiceIds[8],
            UserIds = MasterDataConstants.DoctorIds[3],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "404", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[4],
            ServiceId = MasterDataConstants.ServiceIds[9],
            UserIds = MasterDataConstants.DoctorIds[0],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "105", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[4],
            ServiceId = MasterDataConstants.ServiceIds[0],
            UserIds = MasterDataConstants.DoctorIds[1],
            StartDate = new DateTime(2024, 1, 17, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 17, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "106", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[5],
            ServiceId = MasterDataConstants.ServiceIds[1],
            UserIds = MasterDataConstants.DoctorIds[2],
            StartDate = new DateTime(2024, 1, 27, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 27, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "201", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[6],
            ServiceId = MasterDataConstants.ServiceIds[2],
            UserIds = MasterDataConstants.DoctorIds[3],
            StartDate = new DateTime(2024, 1, 28, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 28, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "202", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[7],
            ServiceId = MasterDataConstants.ServiceIds[3],
            UserIds = MasterDataConstants.DoctorIds[0],
            StartDate = new DateTime(2024, 1, 29, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 29, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "203", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[8],
            ServiceId = MasterDataConstants.ServiceIds[4],
            UserIds = MasterDataConstants.DoctorIds[1],
            StartDate = new DateTime(2024, 1, 30, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 30, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });

        builder.Entity<RoomEvent>().HasData(new
        {
            Id = Guid.NewGuid(),
            RoomId = "206", // Replace with actual room ID
            BillId = MasterDataConstants.FirstTenBillIds[9],
            ServiceId = MasterDataConstants.ServiceIds[5],
            UserIds = MasterDataConstants.DoctorIds[2],
            StartDate = new DateTime(2024, 1, 31, 14, 24, 05),
            EndDate = new DateTime(2024, 1, 31, 15, 24, 05),
            CreatedDate = DateTime.Now,
            IsDeleted = false
        });
        #endregion

        #region[ Request ]
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[0]),
            UserId = MasterDataConstants.PatientsIds[0],
            Symptoms = "1,2,8", // Matching Routine Check-up and Blood Test
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[1]),
            UserId = MasterDataConstants.PatientsIds[1],
            Symptoms = "11,12,13", // Matching X-ray and MRI Scan
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[2]),
            UserId = MasterDataConstants.PatientsIds[2],
            Symptoms = "17,18,7", // Matching Ultrasound and Physical Therapy Session
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[3]),
            UserId = MasterDataConstants.PatientsIds[0],
            Symptoms = "26,27,32", // Matching Dental Cleaning and Eye Examination
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[4]),
            UserId = MasterDataConstants.PatientsIds[1],
            Symptoms = "6,8,10", // Matching Dermatology Consultation and Routine Check-up
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[5]),
            UserId = MasterDataConstants.PatientsIds[2],
            Symptoms = "7,24,20", // Matching Blood Test
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[6]),
            UserId = MasterDataConstants.PatientsIds[0],
            Symptoms = "13,14,11", // Matching X-ray and MRI Scan
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[7]),
            UserId = MasterDataConstants.PatientsIds[1],
            Symptoms = "22,20,17", // Matching Ultrasound and Physical Therapy Session
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[8]),
            UserId = MasterDataConstants.PatientsIds[2],
            Symptoms = "31,32,26", // Matching Dental Cleaning and Eye Examination
            IsDeleted = true
        });
        builder.Entity<Request>().HasData(new Request
        {
            Id = Guid.Parse(MasterDataConstants.FirstTenRequestIds[9]),
            UserId = MasterDataConstants.PatientsIds[0],
            Symptoms = "10,9,8", // Matching Dermatology Consultation and Routine Check-up
            IsDeleted = true
        });
        #endregion
    }
    #endregion

}
