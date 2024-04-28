using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class EmployeeUpdatePasswordHandler :
    IRequestHandler<RequestUpdateEmployeePassword, Task>
{
    /// <summary>
    /// Создаёт объект <see cref="EmployeeUpdatePasswordHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public EmployeeUpdatePasswordHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestUpdateEmployeePassword request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Employees.UpdateEmployeePasswordAsync(
            request.Login,
            request.Password,
            token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
