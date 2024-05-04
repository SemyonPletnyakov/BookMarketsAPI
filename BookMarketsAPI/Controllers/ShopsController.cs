using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookMarketsAPI.Helpers;

using Logic.Abstractions.Processors;

using Models.Exceptions;
using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using ShopWithoutId = Models.ForCreate.Shop;
using ShopForUpdate = Models.ForUpdate.Shop;
using ProductCount = Models.SimpleEntities.ProductCount;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер магазинов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ShopsController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="ShopsController"/>.
    /// </summary>
    /// <param name="getShopsProcessor">
    /// Процессор получения магазинов.
    /// </param>
    /// <param name="getProductCountsInShopProcessor">
    /// Процессор получения количества товаров в магазине.
    /// </param>
    /// <param name="addShopProcessor">
    /// Процессор добавления магазина.
    /// </param>
    /// <param name="updateShopProcessor">
    /// Процессор обновления магазина.
    /// </param>
    /// <param name="updateProductCountInShopProcessor">
    /// Процессор обновления количества товара в магазине.
    /// </param>
    /// <param name="deleteShopProcessor">
    /// Процессор удаления магазина.
    /// </param>
    /// <exception cref="ArgumentNullException"></exception>
    public ShopsController(
        IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<ShopSorting>, IList<Shop>> getShopsProcessor, 
        IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Shop, ProductCountSorting>, IList<ProductCount>> getProductCountsInShopProcessor, 
        IRequestProcessorWithAuthorize<RequestAddEntity<ShopWithoutId>> addShopProcessor, 
        IRequestProcessorWithAuthorize<RequestUpdateEntity<ShopForUpdate>> updateShopProcessor, 
        IRequestProcessorWithAuthorize<RequestUpdateProductCountInEntity<Shop>> updateProductCountInShopProcessor, 
        IRequestProcessorWithAuthorize<RequestDeleteEntityById<Shop, Shop>> deleteShopProcessor)
    {
        _getShopsProcessor = getShopsProcessor 
            ?? throw new ArgumentNullException(nameof(getShopsProcessor));
        _getProductCountsInShopProcessor = getProductCountsInShopProcessor 
            ?? throw new ArgumentNullException(nameof(getProductCountsInShopProcessor));
        _addShopProcessor = addShopProcessor 
            ?? throw new ArgumentNullException(nameof(addShopProcessor));
        _updateShopProcessor = updateShopProcessor 
            ?? throw new ArgumentNullException(nameof(updateShopProcessor));
        _updateProductCountInShopProcessor = updateProductCountInShopProcessor
            ?? throw new ArgumentNullException(nameof(updateProductCountInShopProcessor));
        _deleteShopProcessor = deleteShopProcessor 
            ?? throw new ArgumentNullException(nameof(deleteShopProcessor));
    }

    /// <summary>
    /// Получить магазины.
    /// </summary>
    /// <param name="size">
    /// Размер страницы.
    /// </param>
    /// <param name="number">
    /// Номер страницы.
    /// </param>
    /// <param name="order">
    /// Сортировка.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Магазины.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetShopsAsync(
        int size,
        int number,
        ShopSorting order,
        CancellationToken token)
    {
        var shops =
            (await _getShopsProcessor.ProcessAsync(new(new(size, number, order)), token))
                .Select(shop =>
                    new Transport.Models.FullModels.Shop
                    {
                        ShopId = shop.ShopId.Value,
                        Name = shop.Name?.Value,
                        OpeningTime = shop.OpeningTime,
                        ClosingTime = shop.ClosingTime,
                        Address = new()
                        {
                            AddressId = shop.Address.AddressId.Value,
                            Country = shop.Address.Country,
                            RegionNumber = shop.Address.RegionNumber,
                            RegionName = shop.Address.RegionName,
                            City = shop.Address.City,
                            District = shop.Address.District,
                            Street = shop.Address.Street,
                            House = shop.Address.House,
                            Room = shop.Address.Room
                        }
                    }).ToArray();

        return Ok(shops);
    }

    /// <summary>
    /// Получить количество товаров в магазине.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="size">
    /// Размер страницы.
    /// </param>
    /// <param name="number">
    /// Номер страницы.
    /// </param>
    /// <param name="order">
    /// Сортировка.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Покупатели.
    /// </returns>
    [HttpGet("product_count")]
    [Authorize]
    public async Task<IActionResult> GetProductCountsInShopAsync(
        int shopId,
        int size,
        int number,
        ProductCountSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var products =
                (await _getProductCountsInShopProcessor.ProcessAsync(
                        new(new(shopId), new(size, number, order)), 
                        jwtToken, 
                        token))
                    .Select(product =>
                        new Transport.Models.SimpleModels.ProductCount
                        {
                            ProductId = product.ProductId.Value,
                            Count = product.Count.Value
                        }).ToArray();

            return Ok(products);
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Добавить магазин.
    /// </summary>
    /// <param name="shop">
    /// Товар.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPost]
    [Authorize]
    public async Task<IActionResult> AddShopAsync(
        Transport.Models.ForCreate.Shop shop,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _addShopProcessor.ProcessAsync(
                new(new(
                        shop.Name is null
                            ? null
                            : new(shop.Name),
                        shop.OpeningTime,
                        shop.ClosingTime,
                        new(shop.AddressId))),
                jwtToken,
                token);

            return Created();
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

    /// <summary>
    /// Изменить магазин.
    /// </summary>
    /// <param name="shop">
    /// Магазин.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateShopAsync(
        Transport.Models.ForUpdate.Shop shop,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _updateShopProcessor.ProcessAsync(
                new(new(new(shop.ShopId),
                        shop.Name is null
                            ? null
                            : new(shop.Name),
                        shop.OpeningTime,
                        shop.ClosingTime,
                        new(shop.AddressId))),
                jwtToken,
                token);

            return Accepted();
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Изменить количество товара в магазине.
    /// </summary>
    /// <param name="productCountInShop">
    /// Количество товара в магазине.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut("product_count")]
    [Authorize]
    public async Task<IActionResult> UpdateProductCountInShopAsync(
        Transport.Models.ForUpdate.ProductCountInShop productCountInShop,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _updateProductCountInShopProcessor.ProcessAsync(
                new(new(productCountInShop.ShopId), 
                    new(productCountInShop.ProductId), 
                    new(productCountInShop.Count)),
                jwtToken,
                token);

            return Accepted();
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    /// <summary>
    /// Удалить магазин.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteShopAsync(
        Transport.Models.Ids.Shop shopId,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _deleteShopProcessor.ProcessAsync(
                new(new(shopId.ShopId)),
                jwtToken,
                token);

            return Accepted();
        }
        catch (AuthorizationException ex)
        {
            return Unauthorized(ex.Message);
        }
        catch (NotEnoughRightsException ex)
        {
            return Forbid(ex.Message);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
        catch (ArgumentException)
        {
            return BadRequest();
        }
    }

    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<ShopSorting>, IList<Shop>> _getShopsProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Shop, ProductCountSorting>, IList<ProductCount>> _getProductCountsInShopProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<ShopWithoutId>> _addShopProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<ShopForUpdate>> _updateShopProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateProductCountInEntity<Shop>> _updateProductCountInShopProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Shop, Shop>> _deleteShopProcessor;
}
