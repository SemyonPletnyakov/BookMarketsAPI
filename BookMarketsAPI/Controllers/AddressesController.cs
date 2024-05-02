using Microsoft.AspNetCore.Mvc;

using Logic.Abstractions.Processors;

using Models.Requests;
using Models;

using Transport.Models.ForCreate;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер адресов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AddressesController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="AddressesController"/>.
    /// </summary>
    /// <param name="getOrAddAddressProcessor">
    /// Процессор получения или добавления адреса.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="getOrAddAddressProcessor"/> равен <see langword="null"/>.
    /// </exception>
    public AddressesController(
        IRequestProcessorWithAuthorize<RequestGetOneByAddress<Id<Address>>, Id<Address>> getOrAddAddressProcessor)
    {
        _getOrAddAddressProcessor = getOrAddAddressProcessor 
            ?? throw new ArgumentNullException(nameof(getOrAddAddressProcessor));
    }

    /// <summary>
    /// /// Получить идентификатор адреса 
    /// или добавить адрес.
    /// </summary>
    /// <param name="address">
    /// Адресю
    /// </param>
    /// <param name="cancellationToken">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Идентификатор адреса.
    /// </returns>
    [HttpPost]
    //авторизация
    public async Task<IActionResult> GetIdOrAddAddressAsync(Address address, CancellationToken cancellationToken)
    {

    }

    private readonly IRequestProcessorWithAuthorize<RequestGetOneByAddress<Id<Address>>, Id<Address>> _getOrAddAddressProcessor;
}
