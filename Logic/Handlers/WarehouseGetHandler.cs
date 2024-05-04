using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handlers;

public sealed class WarehouseGetHandler :
    IRequestHandler<
        RequestGetManyWithPagination<WarehouseSorting>,
        IList<Warehouse>>
{
    /// <summary>
    /// Создаёт объект <see cref="WarehouseGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public WarehouseGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Warehouse>> HandleAsync(
        RequestGetManyWithPagination<WarehouseSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Warehouses.GetWarehousesAsync(request.PaginationInfo, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
