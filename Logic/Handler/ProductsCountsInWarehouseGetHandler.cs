using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using ProductCount = Models.SimpleEntities.ProductCount;

namespace Logic.Handler;

public sealed class ProductsCountsInWarehouseGetHandler :
    IRequestHandler<
        RequestGetManyByIdWithPagination<Warehouse, ProductCountSorting>,
        IList<ProductCount>>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductsCountsInWarehouseGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductsCountsInWarehouseGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<ProductCount>> HandleAsync(
        RequestGetManyByIdWithPagination<Warehouse, ProductCountSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Warehouses.GetProductsCountInWarehouseByIdAsync(
            request.Id,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
