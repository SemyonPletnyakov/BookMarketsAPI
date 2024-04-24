using Models;
using Models.FullEntities;

using AddressWithoutId = Models.ForCreate.Address;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория адресов.
/// </summary>
public interface IAddressesRepository
{
    /// <summary>
    /// Получить идентификатор адреса и добавить адрес при его отуствуии.
    /// </summary>
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Идентификатор адреса.
    /// </returns>
    public Task<Id<Address>> GetIdOrAddAddressAsync(
        AddressWithoutId address, 
        CancellationToken token);
}
