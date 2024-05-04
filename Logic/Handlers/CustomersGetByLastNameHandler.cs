using Logic.Abstractions.Handlers;

using Models.ForUpdate;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handlers;

public sealed class CustomersGetByLastNameHandler :
    IRequestHandler<
        RequestGetManyByLastNameWithPagination<CustomerSorting>,
        IList<Customer>>
{
    /// <summary>
    /// Создаёт объект <see cref="CustomersGetByLastNameHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public CustomersGetByLastNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Customer>> HandleAsync(
        RequestGetManyByLastNameWithPagination<CustomerSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Customers.GetCustomersByLastNameAsync(
            request.LastName,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
