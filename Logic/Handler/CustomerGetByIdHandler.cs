using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

using SimleCustomer = Models.ForUpdate.Customer;

namespace Logic.Handler;

public sealed class CustomerGetByIdHandler :
    IRequestHandler<
        RequestGetOneById<Customer, SimleCustomer>,
        SimleCustomer>
{
    /// <summary>
    /// Создаёт объект <see cref="CustomerGetByIdHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public CustomerGetByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<SimleCustomer> HandleAsync(
        RequestGetOneById<Customer, SimleCustomer> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Customers.GetCustomerByIdAsync(request.Id, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
