using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handlers;

public sealed class ProductGetByIdHandler :
    IRequestHandler<
        RequestGetOneById<Product, Product>,
        Product>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductGetByIdHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductGetByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<Product> HandleAsync(
        RequestGetOneById<Product, Product> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Products.GetProductByIdAsync(request.Id, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
