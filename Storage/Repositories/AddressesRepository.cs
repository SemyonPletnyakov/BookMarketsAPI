using Microsoft.EntityFrameworkCore;
using Models;
using Models.FullEntities;

using Storage.Abstractions.Repositories;

using AddressWithoutId = Models.ForCreate.Address;

namespace Storage.Repositories;

/// <summary>
/// Репозиторий адресов.
/// </summary>
public sealed class AddressesRepository : IAddressesRepository
{
    /// <summary>
    /// Создаёт объект <see cref="AddressesRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public AddressesRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<Id<Address>> GetAddressIdOrAddAddressAsync(
        AddressWithoutId address,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(address);
        token.ThrowIfCancellationRequested();

        var addressId = (await _context.Addresses
            .SingleOrDefaultAsync(
                a =>
                    a.Country == address.Country &&
                    a.RegionNumber == address.RegionNumber &&
                    a.RegionName == address.RegionName &&
                    a.City == address.City &&
                    a.District == address.District &&
                    a.Street == address.Street &&
                    a.House == address.House &&
                    a.Room == address.Room, 
                token))?.AddressId;

        if (addressId is not null)
        {
            return new(addressId.Value);
        }

        var addedAddress = await _context.Addresses.AddAsync(
            new Models.Address
            {
                Country = address.Country,
                RegionNumber = address.RegionNumber,
                RegionName = address.RegionName,
                City = address.City,
                District = address.District,
                Street = address.Street,
                House = address.House,
                Room = address.Room,
            }, 
            token);

        return new(addedAddress.Entity.AddressId);
    }

    private readonly ApplicationContext _context;
}
