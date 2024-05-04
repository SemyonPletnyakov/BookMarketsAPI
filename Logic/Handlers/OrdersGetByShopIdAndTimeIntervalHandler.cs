using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using SimleOrder = Models.ForUpdate.Order;

namespace Logic.Handlers;

public sealed class OrdersGetByShopIdAndTimeIntervalHandler :
    IRequestHandler<
        RequestGetManyByIdByTimeIntervalWithPagination<Shop, OrderSorting>,
        IList<SimleOrder>>
{
    /// <summary>
    /// Создаёт объект <see cref="OrdersGetByShopIdAndTimeIntervalHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public OrdersGetByShopIdAndTimeIntervalHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<SimleOrder>> HandleAsync(
        RequestGetManyByIdByTimeIntervalWithPagination<Shop, OrderSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Orders.GetOrdersByShopIdByTimeIntervalAsync(
            request.Id,
            request.StartDate,
            request.EndDate,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
