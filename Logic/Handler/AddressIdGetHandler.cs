using Logic.Abstractions.Handlers;

using Models;
using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

using AddressWithoutId = Models.ForCreate.Address;

namespace Logic.Handler;

internal class AddressIdGetHandler :
    IRequestHandler<
        RequestGetOneByAddress<Id<Address>>,
        Task<Id<Address>>>
{
    /// <summary>
    /// Создаёт объект <see cref="AddressIdGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public AddressIdGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task<Id<Address>> HandleAsync(
        RequestGetOneByAddress<Id<Address>> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        var id = await _unitOfWork.Addresses.GetIdOrAddAddressAsync(
            request.Address, 
            token);

        await _unitOfWork.SaveChangesAsync(token);

        return id;
    }

    private readonly IUnitOfWork _unitOfWork;
}
