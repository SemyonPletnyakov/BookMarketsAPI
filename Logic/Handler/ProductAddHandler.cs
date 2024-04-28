using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using ProductWithoutId = Models.ForCreate.Product;

namespace Logic.Handler;

public sealed class ProductAddHandler :
    IRequestHandler<RequestAddEntity<ProductWithoutId>, Task>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductAddHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductAddHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestAddEntity<ProductWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Products.AddProductAsync(request.Entity, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
