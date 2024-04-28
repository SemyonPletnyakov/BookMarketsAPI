using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class ProductCountInWarehouseUpdateHandler :
    IRequestHandler<
        RequestUpdateProductCountInEntity<Warehouse>,
        Task>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductCountInWarehouseUpdateHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductCountInWarehouseUpdateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestUpdateProductCountInEntity<Warehouse> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Warehouses.UpdateProductCountInWarehouseAsync(
            request.EntityId,
            request.ProductId,
            request.Count,
            token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}

