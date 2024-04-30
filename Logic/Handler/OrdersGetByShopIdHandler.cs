using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using SimleOrder = Models.ForUpdate.Order;

namespace Logic.Handler;

public sealed class OrdersGetByShopIdHandler :
    IRequestHandler<
        RequestGetManyByIdWithPagination<Shop, OrderSorting>,
        IList<SimleOrder>>
{
    /// <summary>
    /// Создаёт объект <see cref="OrdersGetByShopIdHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public OrdersGetByShopIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<SimleOrder>> HandleAsync(
        RequestGetManyByIdWithPagination<Shop, OrderSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Orders.GetOrdersByShopIdAsync(
            request.Id,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
