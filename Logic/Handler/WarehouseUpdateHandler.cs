using Logic.Abstractions.Handlers;

using Models.ForUpdate;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class WarehouseUpdateHandler :
    IRequestHandler<RequestUpdateEntity<Warehouse>, Task>
{
    /// <summary>
    /// Создаёт объект <see cref="WarehouseUpdateHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public WarehouseUpdateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestUpdateEntity<Warehouse> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Warehouses.UpdateWarehouseAsync(request.Entity, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
