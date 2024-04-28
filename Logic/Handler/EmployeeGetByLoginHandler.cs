using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class EmployeeGetByLoginHandler :
    IRequestHandler<
        RequestGetOneByLogin<Employee>,
        Task<Employee>>
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeeGetByLoginHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public EmployeeGetByLoginHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<Employee> HandleAsync(
        RequestGetOneByLogin<Employee> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Employees.GetEmployeeByLoginAsync(request.Login, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
