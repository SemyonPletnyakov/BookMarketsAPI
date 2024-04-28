using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class ProductDeleteHandler :
    IRequestHandler<RequestDeleteEntityById<Product, Product>, Task>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductDeleteHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductDeleteHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestDeleteEntityById<Product, Product> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Products.DeleteProductAsync(request.EntityId, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
