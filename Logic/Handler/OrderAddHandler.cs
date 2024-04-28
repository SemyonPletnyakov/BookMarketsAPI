using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using OrderWithoutId = Models.ForCreate.Order;

namespace Logic.Handler;

public sealed class OrderAddHandler :
    IRequestHandler<RequestAddEntity<OrderWithoutId>, Task>
{
    /// <summary>
    /// Создаёт объект <see cref="OrderAddHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public OrderAddHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestAddEntity<OrderWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Orders.AddOrderAsync(request.Entity, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}

