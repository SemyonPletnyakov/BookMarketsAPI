using Models.Pagination.Sorting;
using Models.Requests.Operations;
using Models.Requests.Operations.Types;

namespace Models.Requests.BaseRequests;

/// <summary>
/// Базовый класс всех запросов.
/// </summary>
public abstract record RequestBase
{
    /// <summary>
    /// Описание операции.
    /// </summary>
    public OperationDescriprion OperationDescriprion { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestBase"/>.
    /// </summary>
    /// <param name="operationDescriprion">
    /// Описание операции.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="operationDescriprion"/> равен <see langword="null"/>.
    /// </exception>
    public RequestBase(OperationDescriprion operationDescriprion)
    {
        OperationDescriprion = operationDescriprion
            ?? throw new ArgumentNullException(nameof(operationDescriprion));
    }

    /// <summary>
    /// Получить тип сущности по типу сортировки.
    /// </summary>
    /// <typeparam name="TSorting">
    /// Тип сортровки.
    /// </typeparam>
    /// <returns>
    /// Тип сущности.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Если тип сортировки не поддерживается.
    /// </exception>
    protected static EntityType GetEntityTypeBySortingType<TSorting>()
        where TSorting : Enum
        => _sortingTypesMapping.TryGetValue(typeof(TSorting), out var entityType)
            ? entityType
            : throw new NotImplementedException("Тип сортировки не поддерживается.");

    /// <summary>
    /// Получить тип сущности по типу сортировки.
    /// </summary>
    /// <typeparam name="TEntity">
    /// Класс сущности.
    /// </typeparam>
    /// <returns>
    /// Тип сущности.
    /// </returns>
    /// <exception cref="NotImplementedException">
    /// Если тип сортировки не поддерживается.
    /// </exception>
    protected static EntityType GetEntityTypeByEntity<TEntity>()
        => _entityTypesMapping.TryGetValue(typeof(TEntity), out var entityType)
            ? entityType
            : throw new NotImplementedException("Класс сущности не поддерживается.");

    private static readonly IReadOnlyDictionary<Type, EntityType> _sortingTypesMapping = 
        new Dictionary<Type, EntityType>()
        {
            { typeof(AuthorSorting), EntityType.Author },
            { typeof(BookSorting), EntityType.Book },
            { typeof(CustomerSorting), EntityType.Customer },
            { typeof(EmployeeSorting), EntityType.Employee },
            { typeof(OrderSorting), EntityType.Order },
            { typeof(ProductCountSorting), EntityType.ProductCount },
            { typeof(ProductSorting), EntityType.Product },
            { typeof(ShopSorting), EntityType.Shop },
            { typeof(WarehouseSorting), EntityType.Warehouse },
        };

    private static readonly IReadOnlyDictionary<Type, EntityType> _entityTypesMapping = 
        new Dictionary<Type, EntityType>()
        {
            { typeof(Id<FullEntities.Address>), EntityType.Address },
            { typeof(ForCreate.Author), EntityType.Author },
            { typeof(FullEntities.Author), EntityType.Author },
            { typeof(ForCreate.Book), EntityType.Book },
            { typeof(ForUpdate.Book), EntityType.Book },
            { typeof(FullEntities.Book), EntityType.Book },
            { typeof(SimpleEntities.Book), EntityType.Book },
            { typeof(ForCreate.Customer), EntityType.Customer },
            { typeof(ForUpdate.Customer), EntityType.Customer },
            { typeof(FullEntities.Customer), EntityType.Customer },
            { typeof(ForCreate.Employee), EntityType.Employee },
            { typeof(ForUpdate.Employee), EntityType.Employee },
            { typeof(FullEntities.Employee), EntityType.Employee },
            { typeof(ForCreate.Order), EntityType.Order },
            { typeof(ForUpdate.Order), EntityType.Order },
            { typeof(FullEntities.Order), EntityType.Order },
            { typeof(ForCreate.ProductCount), EntityType.ProductCount },
            { typeof(SimpleEntities.ProductCount), EntityType.ProductCount },
            { typeof(ForCreate.Product), EntityType.Product },
            { typeof(FullEntities.Product), EntityType.Product },
            { typeof(SimpleEntities.Product), EntityType.Product },
            { typeof(ForCreate.Shop), EntityType.Shop },
            { typeof(ForUpdate.Shop), EntityType.Shop },
            { typeof(FullEntities.Shop), EntityType.Shop },
            { typeof(ForCreate.Warehouse), EntityType.Warehouse },
            { typeof(ForUpdate.Warehouse), EntityType.Warehouse },
            { typeof(FullEntities.Warehouse), EntityType.Warehouse },
        };
}
