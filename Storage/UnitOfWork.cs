using Storage.Abstractions;
using Storage.Abstractions.Repositories;

namespace Storage;

/// <summary>
/// Интерфейс единицы работы с репозиториями.
/// </summary>
public sealed class UnitOfWork : IUnitOfWork
{
    /// <summary>
    /// Репозиторий авторов.
    /// </summary>
    public IAuthorsRepository Authors { get; }

    /// <summary>
    /// Репозиторий книг.
    /// </summary>
    public IBooksRepository Books { get; }

    /// <summary>
    /// Репозиторий покупателей.
    /// </summary>
    public ICustomersRepository Customers { get; }

    /// <summary>
    /// Репозиторий сотрудников.
    /// </summary>
    public IEmployeesRepository Employees { get; }

    /// <summary>
    /// Репозиторий заказов.
    /// </summary>
    public IOrdersRepository Orders { get; }

    /// <summary>
    /// Репозиторий товаров.
    /// </summary>
    public IProductsRepository Products { get; }

    /// <summary>
    /// Репозиторий магазинов.
    /// </summary>
    public IShopsRepository Shops { get; }

    /// <summary>
    /// Репозиторий складов.
    /// </summary>
    public IWarehousesRepository Warehouses { get; }

    /// <summary>
    /// Репозиторий адресов.
    /// </summary>
    public IAddressesRepository Addresses { get; }

    /// <summary>
    /// Отправляет транзакцию изменения в репозиториях.
    /// </summary>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача асинхронного выполнения.
    /// </returns>
    public Task SaveChangesAsync(CancellationToken token) 
        => _context.SaveChangesAsync(token);

    /// <summary>
    /// Создаёт объект <see cref="UnitOfWork"/>.
    /// </summary>
    /// <param name="authors">
    /// Репозиторий авторов.
    /// </param>
    /// <param name="books">
    /// Репозиторий книг.
    /// </param>
    /// <param name="customers">
    /// Репозиторий покупателей.
    /// </param>
    /// <param name="employees">
    /// Репозиторий сотрудников.
    /// </param>
    /// <param name="orders">
    /// Репозиторий заказов.
    /// </param>
    /// <param name="products">
    /// Репозиторий товаров.
    /// </param>
    /// <param name="shops">
    /// Репозиторий магазинов.
    /// </param>
    /// <param name="warehouses">
    /// Репозиторий складов.
    /// </param>
    /// <param name="addresses">
    /// Репозиторий адресов.
    /// </param>
    /// <param name="context">
    /// Контекст приложения для работы с базой данных.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из аргументов равен <see langword="null"/>.
    /// </exception>
    public UnitOfWork(
        IAuthorsRepository authors, 
        IBooksRepository books, 
        ICustomersRepository customers, 
        IEmployeesRepository employees, 
        IOrdersRepository orders, 
        IProductsRepository products, 
        IShopsRepository shops, 
        IWarehousesRepository warehouses, 
        IAddressesRepository addresses, 
        ApplicationContext context)
    {
        Authors = authors ?? throw new ArgumentNullException(nameof(authors));
        Books = books ?? throw new ArgumentNullException(nameof(books));
        Customers = customers ?? throw new ArgumentNullException(nameof(customers));
        Employees = employees ?? throw new ArgumentNullException(nameof(employees));
        Orders = orders ?? throw new ArgumentNullException(nameof(orders));
        Products = products ?? throw new ArgumentNullException(nameof(products));
        Shops = shops ?? throw new ArgumentNullException(nameof(shops));
        Warehouses = warehouses ?? throw new ArgumentNullException(nameof(warehouses));
        Addresses = addresses ?? throw new ArgumentNullException(nameof(addresses));
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    private readonly ApplicationContext _context;
}
