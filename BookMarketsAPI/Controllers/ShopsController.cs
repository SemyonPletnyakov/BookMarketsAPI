using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Logic.Abstractions.Processors;

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
        IRequestProcessorWithoutAuthorize<RequestGetManyByIdWithPagination<Shop, ProductCountSorting>, IList<ProductCount>> getProductCountsInShopProcessor, 
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
    [HttpGet]
    //авторизация
    public async Task<IActionResult> GetProductCountsInShopAsync(
        int shopId,
        int size,
        int number,
        ProductCountSorting order,
        CancellationToken token)
    {

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
    //авторизация
    public async Task<IActionResult> AddShopAsync(
        Transport.Models.ForCreate.Shop shop,
        CancellationToken token)
    {

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
    //авторизация
    public async Task<IActionResult> UpdateShopAsync(
        Transport.Models.FullModels.Shop shop,
        CancellationToken token)
    {

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
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateProductCountInShopAsync(
        Transport.Models.ForUpdate.ProductCountInShop productCountInShop,
        CancellationToken token)
    {

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
    //авторизация
    public async Task<IActionResult> DeleteShopAsync(
        Transport.Models.Ids.Shop shopId,
        CancellationToken token)
    {

    }

    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<ShopSorting>, IList<Shop>> _getShopsProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByIdWithPagination<Shop, ProductCountSorting>, IList<ProductCount>> _getProductCountsInShopProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<ShopWithoutId>> _addShopProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<ShopForUpdate>> _updateShopProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateProductCountInEntity<Shop>> _updateProductCountInShopProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Shop, Shop>> _deleteShopProcessor;
}
