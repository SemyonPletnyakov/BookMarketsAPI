using Microsoft.EntityFrameworkCore;
using Storage.Models;

namespace Storage;

/// <summary>
/// Контекст приложения для работы с базой данных.
/// </summary>
public sealed class ApplicationContext : DbContext
{
    /// <summary>
    /// Адреса.
    /// </summary>
    public DbSet<Address> Addresses { get; set; }

    /// <summary>
    /// Магазины.
    /// </summary>
    public DbSet<Shop> Shops { get; set; }

    /// <summary>
    /// Склады.
    /// </summary>
    public DbSet<Warehouse> Warehouses { get; set; }

    /// <summary>
    /// Товары.
    /// </summary>
    public DbSet<Product> Products { get; set; }

    /// <summary>
    /// Авторы.
    /// </summary>
    public DbSet<Author> Authors { get; set; }

    /// <summary>
    /// Книги.
    /// </summary>
    public DbSet<Book> Books { get; set; }

    /// <summary>
    /// Сущности, показывающие количество определённого товара 
    /// в определённом магазине.
    /// </summary>
    public DbSet<ProductsInShop> ProductsInShops { get; set; }

    /// <summary>
    /// Сущности, показывающие количество определённого товара 
    /// на определённом складе.
    /// </summary>
    public DbSet<ProductsInWarehouse> ProductsInWarehouses { get; set; }

    /// <summary>
    /// Работники.
    /// </summary>
    public DbSet<Employee> Employees { get; set; }

    /// <summary>
    /// Сущности, показывающие магазины и работающих в них сотрудников.
    /// </summary>
    public DbSet<LinksEmployeeAndShop> LinksEmployeeAndShops { get; set; }

    /// <summary>
    /// Сущности, показывающие склады и работающих в них сотрудников.
    /// </summary>
    public DbSet<LinksEmployeeAndWarehouse> LinksEmployeeAndWarehouses { get; set; }

    /// <summary>
    /// Покупатели.
    /// </summary>
    public DbSet<Customer> Customers { get; set; }

    /// <summary>
    /// Заказы.
    /// </summary>
    public DbSet<Order> Orders { get; set; }

    /// <summary>
    /// Сущности, показывающие товары в заказе.
    /// </summary>
    public DbSet<ProductsInOrder> ProductsInOrders { get; set; }

    /// <summary>
    /// Создает новый экземпляр <see cref="ApplicationContext"/>.
    /// </summary>
    /// <param name="options">
    /// Опции контекста БД.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="options"/> равен <see langword="null"/>.
    /// </exception>
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options ?? throw new ArgumentNullException(nameof(options))) 
    { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Shop>()
            .HasOne(p => p.Address)
            .WithMany()
            .HasForeignKey(p => p.AddressId);

        modelBuilder.Entity<Warehouse>()
            .HasOne(p => p.Address)
            .WithMany()
            .HasForeignKey(p => p.AddressId);

        modelBuilder.Entity<Book>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId);

        modelBuilder.Entity<Book>()
            .HasOne(p => p.Author)
            .WithMany()
            .HasForeignKey(p => p.AuthorId);

        modelBuilder.Entity<Book>().HasKey(u => new { u.ProductId });

        modelBuilder.Entity<ProductsInShop>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId);

        modelBuilder.Entity<ProductsInShop>()
            .HasOne(p => p.Shop)
            .WithMany()
            .HasForeignKey(p => p.ShopId);

        modelBuilder.Entity<ProductsInShop>().HasKey(u => new { u.ProductId, u.ShopId });

        modelBuilder.Entity<ProductsInWarehouse>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId);

        modelBuilder.Entity<ProductsInWarehouse>()
            .HasOne(p => p.Warehouse)
            .WithMany()
            .HasForeignKey(p => p.WarehouseId);

        modelBuilder.Entity<ProductsInWarehouse>().HasKey(u => new { u.ProductId, u.WarehouseId });

        modelBuilder.Entity<LinksEmployeeAndShop>()
            .HasOne(p => p.Employee)
            .WithMany()
            .HasForeignKey(p => p.EmployeeId);

        modelBuilder.Entity<LinksEmployeeAndShop>()
            .HasOne(p => p.Shop)
            .WithMany()
            .HasForeignKey(p => p.ShopId);

        modelBuilder.Entity<LinksEmployeeAndShop>().HasKey(u => new { u.EmployeeId, u.ShopId });

        modelBuilder.Entity<LinksEmployeeAndWarehouse>()
            .HasOne(p => p.Employee)
            .WithMany()
            .HasForeignKey(p => p.EmployeeId);

        modelBuilder.Entity<LinksEmployeeAndWarehouse>()
            .HasOne(p => p.Warehouse)
            .WithMany()
            .HasForeignKey(p => p.WarehouseId);

        modelBuilder.Entity<LinksEmployeeAndWarehouse>().HasKey(u => new { u.EmployeeId, u.WarehouseId });

        modelBuilder.Entity<Order>()
            .HasOne(p => p.Customer)
            .WithMany()
            .HasForeignKey(p => p.CustomerId);

        modelBuilder.Entity<ProductsInOrder>()
            .HasOne(p => p.Product)
            .WithMany()
            .HasForeignKey(p => p.ProductId);

        modelBuilder.Entity<ProductsInOrder>().HasKey(u => new { u.OrderId, u.ProductId});
    }
}
