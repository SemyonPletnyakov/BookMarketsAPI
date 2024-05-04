using Logic.Abstractions.Handlers;

using Models.ForUpdate;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handlers;

public sealed class OrdersGetHandler :
    IRequestHandler<
        RequestGetManyWithPagination<OrderSorting>,
        IList<Order>>
{
    /// <summary>
    /// Создаёт объект <see cref="OrdersGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public OrdersGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Order>> HandleAsync(
        RequestGetManyWithPagination<OrderSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Orders.GetOrdersAsync(request.PaginationInfo, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
