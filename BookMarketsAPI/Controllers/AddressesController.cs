using Microsoft.AspNetCore.Mvc;

using BookMarketsAPI.Helpers;

using Logic.Abstractions.Processors;

using Models;
using Models.FullEntities;
using Models.Exceptions;
using Models.Requests;

using AddressWithoutId = Models.ForCreate.Address;

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
    public async Task<IActionResult> GetIdOrAddAddressAsync(Transport.Models.ForCreate.Address address, CancellationToken cancellationToken)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var id = await _getOrAddAddressProcessor.ProcessAsync(
                new(new(
                    address.Country,
                    address.RegionNumber,
                    address.RegionName,
                    address.City,
                    address.District,
                    address.Street,
                    address.House,
                    address.Room)),
                jwtToken,
                cancellationToken);

            var response = new Transport.Models.Ids.Address 
            { 
                AddressId = id.Value 
            };

            return Ok(response);
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    private readonly IRequestProcessorWithAuthorize<RequestGetOneByAddress<Id<Address>>, Id<Address>> _getOrAddAddressProcessor;
}
