using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using SimleEmployee = Models.ForUpdate.Employee;

namespace Logic.Handler;

public sealed class EmployeesGetByShopIdHandler :
    IRequestHandler<
        RequestGetManyByIdWithPagination<Shop, EmployeeSorting>,
        IList<SimleEmployee>>
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeesGetByShopIdHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public EmployeesGetByShopIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<SimleEmployee>> HandleAsync(
        RequestGetManyByIdWithPagination<Shop, EmployeeSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Employees.GetEmployeesByShopAsync(
            request.Id,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
