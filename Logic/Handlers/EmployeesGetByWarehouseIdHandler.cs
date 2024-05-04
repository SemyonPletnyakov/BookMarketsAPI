using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using SimleEmployee = Models.ForUpdate.Employee;

namespace Logic.Handlers;

public sealed class EmployeesGetByWarehouseIdHandler :
    IRequestHandler<
        RequestGetManyByIdWithPagination<Warehouse, EmployeeSorting>,
        IList<SimleEmployee>>
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeesGetByWarehouseIdHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public EmployeesGetByWarehouseIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<SimleEmployee>> HandleAsync(
        RequestGetManyByIdWithPagination<Warehouse, EmployeeSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Employees.GetEmployeesByWarehouseAsync(
            request.Id,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
