using Microsoft.EntityFrameworkCore;
using Reports.Persistence.Entities;
using Reports.Persistence.EntityConfigurations;

namespace Reports.Persistence;

public class ReportsDbContext : DbContext
{
    public ReportsDbContext(DbContextOptions<ReportsDbContext> options) : base(options) {
        Initialize(this);
    }

    public DbSet<Order> Orders { get; set; }

    public static void Initialize(ReportsDbContext context)
    {
        context.Database.EnsureCreated();
        if (context.Orders.Any())
        {
            return;
        }
        else
        {
            context.Orders.AddRange(new List<Order>
            {
                new Order { OrderNumber = "ORD001", Discription = "Order from HK Organisation", TotalAmount = 100.00m, OrderDate = DateTime.Now.AddDays(-1).Date },
                new Order { OrderNumber = "ORD002", Discription = "Order from US Organisation", TotalAmount = 200.00m, OrderDate = DateTime.Now.AddDays(-2).Date },
                new Order { OrderNumber = "ORD003", Discription = "Order from UK Organisation", TotalAmount = 300.00m, OrderDate = DateTime.Now.AddDays(-3).Date },
                new Order { OrderNumber = "ORD004", Discription = "Order from CA Organisation", TotalAmount = 400.00m, OrderDate = DateTime.Now.AddDays(-4).Date },
                new Order { OrderNumber = "ORD005", Discription = "Order from AU Organisation", TotalAmount = 500.00m, OrderDate = DateTime.Now.AddDays(-5).Date },
                new Order { OrderNumber = "ORD006", Discription = "Order from IN Organisation", TotalAmount = 600.00m, OrderDate = DateTime.Now.AddDays(-1).Date },
                new Order { OrderNumber = "ORD007", Discription = "Order from JP Organisation", TotalAmount = 700.00m, OrderDate = DateTime.Now.AddDays(-2).Date },
                new Order { OrderNumber = "ORD008", Discription = "Order from DE Organisation", TotalAmount = 800.00m, OrderDate = DateTime.Now.AddDays(-3).Date },
                new Order { OrderNumber = "ORD009", Discription = "Order from FR Organisation", TotalAmount = 900.00m, OrderDate = DateTime.Now.AddDays(-4).Date },
                new Order { OrderNumber = "ORD010", Discription = "Order from IT Organisation", TotalAmount = 1000.00m, OrderDate = DateTime.Now.AddDays(-5).Date },
                new Order { OrderNumber = "ORD011", Discription = "Order from BR Organisation", TotalAmount = 1100.00m, OrderDate = DateTime.Now.AddDays(-1).Date },
                new Order { OrderNumber = "ORD012", Discription = "Order from RU Organisation", TotalAmount = 1200.00m, OrderDate = DateTime.Now.AddDays(-2).Date },
                new Order { OrderNumber = "ORD013", Discription = "Order from CN Organisation", TotalAmount = 1300.00m, OrderDate = DateTime.Now.AddDays(-3).Date },
                new Order { OrderNumber = "ORD014", Discription = "Order from ZA Organisation", TotalAmount = 1400.00m, OrderDate = DateTime.Now.AddDays(-4).Date },
                new Order { OrderNumber = "ORD015", Discription = "Order from MX Organisation", TotalAmount = 1500.00m, OrderDate = DateTime.Now.AddDays(-5).Date },
                new Order { OrderNumber = "ORD016", Discription = "Order from KR Organisation", TotalAmount = 1600.00m, OrderDate = DateTime.Now.AddDays(-1).Date },
                new Order { OrderNumber = "ORD017", Discription = "Order from SG Organisation", TotalAmount = 1700.00m, OrderDate = DateTime.Now.AddDays(-2).Date },
                new Order { OrderNumber = "ORD018", Discription = "Order from MY Organisation", TotalAmount = 1800.00m, OrderDate = DateTime.Now.AddDays(-3).Date },
                new Order { OrderNumber = "ORD019", Discription = "Order from TH Organisation", TotalAmount = 1900.00m, OrderDate = DateTime.Now.AddDays(-4).Date },
                new Order { OrderNumber = "ORD020", Discription = "Order from VN Organisation", TotalAmount = 2000.00m, OrderDate = DateTime.Now.AddDays(-5).Date }
            });
            context.SaveChanges();
        }

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReportsDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
        
    }
}
