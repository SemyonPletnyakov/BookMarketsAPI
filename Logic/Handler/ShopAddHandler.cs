using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using ShopWithoutId = Models.ForCreate.Shop;

namespace Logic.Handler;

public sealed class ShopAddHandler :
    IRequestHandler<RequestAddEntity<ShopWithoutId>, Task>
{
    /// <summary>
    /// Создаёт объект <see cref="ShopAddHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ShopAddHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestAddEntity<ShopWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        _unitOfWork.Shops.AddShop(request.Entity);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
