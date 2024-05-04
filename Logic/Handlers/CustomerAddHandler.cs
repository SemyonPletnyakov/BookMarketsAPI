using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using CustomerWithoutId = Models.ForCreate.Customer;

namespace Logic.Handlers;

public sealed class CustomerAddHandler :
    IRequestHandler<RequestAddEntity<CustomerWithoutId>>
{
    /// <summary>
    /// Создаёт объект <see cref="CustomerAddHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public CustomerAddHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestAddEntity<CustomerWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Customers.AddCustomerAsync(request.Entity, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}

