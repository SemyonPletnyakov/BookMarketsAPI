using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class ProductCountInShopUpdateHandler :
    IRequestHandler<
        RequestUpdateProductCountInEntity<Shop>,
        Task>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductCountInShopUpdateHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductCountInShopUpdateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestUpdateProductCountInEntity<Shop> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Shops.UpdateProductCountInShopAsync(
            request.EntityId,
            request.ProductId,
            request.Count,
            token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}

