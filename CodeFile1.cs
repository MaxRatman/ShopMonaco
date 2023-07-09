using Microsoft.EntityFrameworkCore;
public class Employees
{
    public int Id { get; set; }
    public string EmployeeName { get; set; } = null!;
    public string EmployeeSurname { get; set; } = null!;
}
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double PriceForBuy { get; set; }
    public double PriceForSell { get; set; }
}
public class ProductInWarehouse
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int Amount { get; set; }
}
public class Sales
{
    public int Id { get; set; }
    public string NameProduct { get; set; } = null!;
    public int Amount { get; set; }
    public double Price { get; set; }
    public DateTime dateTime { get; set; }
    public string NameEmployees { get; set; } = null!;
}
public class DataBaseContext:DbContext
{
    public virtual DbSet<Employees> Employees { get; set; }
    public virtual DbSet<ProductInWarehouse> productInWarehouses { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Sales> Sales { get; set; }
    public DataBaseContext() { Database.EnsureCreated(); }
    public DataBaseContext(DbContextOptions<DataBaseContext> dbContextOptions):base (dbContextOptions) { }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DataBaseShopMonaco;");

    }
}