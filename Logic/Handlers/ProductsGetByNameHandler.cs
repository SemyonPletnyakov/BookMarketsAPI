using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using SimpleProduct = Models.SimpleEntities.Product;

namespace Logic.Handlers;

public sealed class ProductsGetByNameHandler :
    IRequestHandler<
        RequestGetManyByNameWithPagination<Product, ProductSorting>,
        IList<SimpleProduct>>
{
    /// <summary>
    /// Создаёт объект <see cref="ProductsGetByNameHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ProductsGetByNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<SimpleProduct>> HandleAsync(
        RequestGetManyByNameWithPagination<Product, ProductSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Products.GetProductsByNameAsync(
            request.Name,
            request.PaginationInfo, 
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
