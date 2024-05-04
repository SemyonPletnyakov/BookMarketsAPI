using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handlers;

public sealed class OrderUpdateStatusHandler :
    IRequestHandler<RequestUpdateOrderStatus>
{
    /// <summary>
    /// Создаёт объект <see cref="OrderUpdateStatusHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public OrderUpdateStatusHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestUpdateOrderStatus request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Orders.UpdateOrderStatusAsync(
            request.Id,
            request.Status,
            token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
