using Storage.Abstractions.Repositories;

namespace Storage.Abstractions;

/// <summary>
/// Интерфейс единицы работы с репозиториями.
/// </summary>
public interface IUnitOfWork : IDisposable
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
    Task SaveChangesAsync(CancellationToken token);
}
