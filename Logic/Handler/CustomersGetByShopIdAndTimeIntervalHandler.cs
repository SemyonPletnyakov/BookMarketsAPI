using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using SimleCustomer = Models.ForUpdate.Customer;

namespace Logic.Handler;

public sealed class CustomersGetByShopIdAndTimeIntervalHandler :
    IRequestHandler<
        RequestGetManyByIdByTimeIntervalWithPagination<Shop, CustomerSorting>,
        Task<IList<SimleCustomer>>>
{
    /// <summary>
    /// Создаёт объект <see cref="CustomersGetByShopIdAndTimeIntervalHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public CustomersGetByShopIdAndTimeIntervalHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<SimleCustomer>> HandleAsync(
        RequestGetManyByIdByTimeIntervalWithPagination<Shop, CustomerSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Customers.GetCustomersByShopIdByTimeIntervalAsync(
            request.Id,
            request.StartDate,
            request.EndDate,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
