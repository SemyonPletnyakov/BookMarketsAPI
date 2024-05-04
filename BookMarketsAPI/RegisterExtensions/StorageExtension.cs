using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Storage;
using Storage.Abstractions;
using Storage.Abstractions.Repositories;
using Storage.Repositories;

namespace BookMarketsAPI.RegisterExtensions;

public static class StorageExtension
{
    public static IServiceCollection AddStorage(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        return services.AddDbContext<ApplicationContext>(
                optionsBuilder => optionsBuilder.UseNpgsql(configuration.GetConnectionString(nameof(ApplicationContext))))
            .AddScoped<IAddressesRepository, AddressesRepository>()
            .AddScoped<IAuthorsRepository, AuthorsRepository>()
            .AddScoped<IBooksRepository, BooksRepository>()
            .AddScoped<ICustomersRepository, CustomersRepository>()
            .AddScoped<IEmployeesRepository, EmployeesRepository>()
            .AddScoped<IOrdersRepository, OrdersRepository>()
            .AddScoped<IProductsRepository, ProductsRepository>()
            .AddScoped<IShopsRepository, ShopsRepository>()
            .AddScoped<IWarehousesRepository, WarehousesRepository>()
            .AddScoped<IUnitOfWork, UnitOfWork>();
    }
}
