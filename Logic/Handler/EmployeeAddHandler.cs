using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using EmployeeWithoutId = Models.ForCreate.Employee;

namespace Logic.Handler;

public sealed class EmployeeAddHandler :
    IRequestHandler<RequestAddEntity<EmployeeWithoutId>, Task>
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeeAddHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public EmployeeAddHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestAddEntity<EmployeeWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Employees.AddEmployeeAsync(request.Entity, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}

