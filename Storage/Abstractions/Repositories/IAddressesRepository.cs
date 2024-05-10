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
    /// Получить идентификатор адреса.
    /// </summary>
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Идентификатор адреса, если адрес существует.
    /// </returns>
    public Task<Id<Address>?> TryGetIdAddressAsync(
        AddressWithoutId address, 
        CancellationToken token);

    /// <summary>
    /// Добавить адрес.
    /// </summary>
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного выполнения.
    /// </returns>
    public Task AddAddressAsync(
        AddressWithoutId address,
        CancellationToken token);
}
