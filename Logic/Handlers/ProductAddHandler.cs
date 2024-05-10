using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using ProductWithoutId = Models.ForCreate.Product;

namespace Logic.Handlers;

public sealed class ProductAddHandler :
    IRequestHandler<RequestAddEntity<ProductWithoutId>>
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
    public Task HandleAsync(
        RequestAddEntity<ProductWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        _unitOfWork.Products.AddProduct(request.Entity);

        return _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
