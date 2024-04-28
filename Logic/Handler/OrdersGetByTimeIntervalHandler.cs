using Logic.Abstractions.Handlers;

using Models.ForUpdate;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class OrdersGetByTimeIntervalHandler :
    IRequestHandler<
        RequestGetManyByTimeIntervalWithPagination<OrderSorting>,
        Task<IList<Order>>>
{
    /// <summary>
    /// Создаёт объект <see cref="OrdersGetByTimeIntervalHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public OrdersGetByTimeIntervalHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Order>> HandleAsync(
        RequestGetManyByTimeIntervalWithPagination<OrderSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Orders.GetOrdersByTimeIntervalAsync(
            request.StartDate,
            request.EndDate,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
