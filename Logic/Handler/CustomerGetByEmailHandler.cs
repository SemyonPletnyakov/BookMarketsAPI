using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class CustomerGetByEmailHandler :
    IRequestHandler<
        RequestGetOneByEmail<Customer>,
        Task<Customer>>
{
    /// <summary>
    /// Создаёт объект <see cref="CustomerGetByEmailHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public CustomerGetByEmailHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<Customer> HandleAsync(
        RequestGetOneByEmail<Customer> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Customers.GetCustomerByEmailAsync(request.Email, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
