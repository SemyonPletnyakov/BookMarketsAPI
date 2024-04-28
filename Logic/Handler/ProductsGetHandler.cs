using Logic.Abstractions.Handlers;

using Models.Pagination.Sorting;
using Models.Requests;
using Models.SimpleEntities;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class ProductsGetHandler :
    IRequestHandler<
        RequestGetManyWithPagination<ProductSorting>,
        Task<IList<Product>>>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductsGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductsGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Product>> HandleAsync(
        RequestGetManyWithPagination<ProductSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Products.GetProductsAsync(request.PaginationInfo, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
