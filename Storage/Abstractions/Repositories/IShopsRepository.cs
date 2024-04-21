using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using ShopWithoutId = Models.ForCreate.Shop;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория магазинов.
/// </summary>
public interface IShopsRepository
{
    /// <summary>
    /// Получить часть магазинов в соотвествии с пагинацией.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка магазинов.
    /// </returns>
    public Task<IList<Shop>> GetShopsAsync(
        PaginationInfo<ShopSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск магазина по адресу в соотвествии с пагинацией.
    /// </summary>
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Магазин.
    /// </returns>
    public Task<Shop> GetShopByAddressAsync(
        Address address,
        PaginationInfo<ShopSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Получить количество товаров в магазине.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Количество товаров в магазине.
    /// </returns>
    public Task<IReadOnlyCollection<ProductCount>> GetProductsCountInShopByIdAsync(
        Id<Shop> shopId,
        PaginationInfo<ProductCountSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Добавление магазина.
    /// </summary>
    /// <param name="shop">
    /// Магазин.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task AddShopAsync(ShopWithoutId shop, CancellationToken token);

    /// <summary>
    /// Изменение магазина.
    /// </summary>
    /// <param name="shop">
    /// Магазин.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateShopAsync(Shop shop, CancellationToken token);

    /// <summary>
    /// Удаление магазина.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task DeleteShopAsync(Id<Shop> shopId, CancellationToken token);
}
