using Logic.Abstractions.Handlers;

using Models.Pagination.Sorting;
using Models.Requests;
using Models.SimpleEntities;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class ProductsGetByKeyWordsHandler :
    IRequestHandler<
        RequestGetManyByKeyWordsWithPagination<ProductSorting>,
        Task<IList<Product>>>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductsGetByKeyWordsHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductsGetByKeyWordsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Product>> HandleAsync(
        RequestGetManyByKeyWordsWithPagination<ProductSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Products.GetProductsByKeyWordsAsync(
            request.KeyWords, 
            request.PaginationInfo, 
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
