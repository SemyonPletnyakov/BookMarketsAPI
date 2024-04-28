using Logic.Abstractions.Handlers;

using Models.ForUpdate;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class EmployeesGetByLastNameHandler :
    IRequestHandler<
        RequestGetManyByLastNameWithPagination<EmployeeSorting>,
        Task<IList<Employee>>>
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeesGetByLastNameHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public EmployeesGetByLastNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Employee>> HandleAsync(
        RequestGetManyByLastNameWithPagination<EmployeeSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Employees.GetEmployeesByLastNameAsync(
            request.LastName,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
