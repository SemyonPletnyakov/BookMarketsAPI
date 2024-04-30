using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class ShopsGetHandler :
    IRequestHandler<
        RequestGetManyWithPagination<ShopSorting>,
        IList<Shop>>
{
    /// <summary>
    /// Создаёт объект <see cref="ShopsGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public ShopsGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Shop>> HandleAsync(
        RequestGetManyWithPagination<ShopSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Shops.GetShopsAsync(request.PaginationInfo, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
