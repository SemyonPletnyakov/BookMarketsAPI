﻿using Logic.Abstractions.Handlers;

using Models.ForUpdate;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class CustomersGetHandler :
    IRequestHandler<
        RequestGetManyWithPagination<CustomerSorting>,
        IList<Customer>>
{
    /// <summary>
    /// Создаёт объект <see cref="CustomersGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public CustomersGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Customer>> HandleAsync(
        RequestGetManyWithPagination<CustomerSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Customers.GetCustomersAsync(request.PaginationInfo, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
