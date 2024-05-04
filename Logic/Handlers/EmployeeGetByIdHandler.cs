using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

using SimleEmployee = Models.ForUpdate.Employee;

namespace Logic.Handlers;

public sealed class EmployeeGetByIdHandler :
    IRequestHandler<
        RequestGetOneById<Employee, SimleEmployee>,
        SimleEmployee>
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeeGetByIdHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public EmployeeGetByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<SimleEmployee> HandleAsync(
        RequestGetOneById<Employee, SimleEmployee> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Employees.GetEmployeeByIdAsync(request.Id, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
