using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using ProductCount = Models.SimpleEntities.ProductCount;

namespace Logic.Handler;

public sealed class ProductsCountsInShopGetHandler :
    IRequestHandler<
        RequestGetManyByIdWithPagination<Shop, ProductCountSorting>,
        Task<IList<ProductCount>>>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductsCountsInShopGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductsCountsInShopGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<ProductCount>> HandleAsync(
        RequestGetManyByIdWithPagination<Shop, ProductCountSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Shops.GetProductsCountInShopByIdAsync(
            request.Id,
            request.PaginationInfo, 
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
