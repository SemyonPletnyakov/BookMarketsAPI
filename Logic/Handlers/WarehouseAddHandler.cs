using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using WarehouseWithoutId = Models.ForCreate.Warehouse;

namespace Logic.Handlers;

public sealed class WarehouseAddHandler :
    IRequestHandler<RequestAddEntity<WarehouseWithoutId>>
{
    /// <summary>
    /// Создаёт объект <see cref="WarehouseAddHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public WarehouseAddHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestAddEntity<WarehouseWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        _unitOfWork.Warehouses.AddWarehouse(request.Entity);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
