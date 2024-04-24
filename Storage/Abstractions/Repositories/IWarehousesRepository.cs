using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using WarehouseWithoutId = Models.ForCreate.Warehouse;
using WarehouseForUpdate = Models.ForUpdate.Warehouse;
using ProductCount = Models.SimpleEntities.ProductCount;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория складов.
/// </summary>
public interface IWarehousesRepository
{
    /// <summary>
    /// Получить часть складов в соотвествии с пагинацией.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка складов.
    /// </returns>
    public Task<IList<Warehouse>> GetWarehousesAsync(
        PaginationInfo<WarehouseSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Получить количество товаров на складе.
    /// </summary>
    /// <param name="warehouseId">
    /// Идентификатор склада.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Количество товаров на складе.
    /// </returns>
    public Task<IList<ProductCount>> GetProductsCountInWarehouseByIdAsync(
        Id<Warehouse> warehouseId,
        PaginationInfo<ProductCountSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Добавление склада.
    /// </summary>
    /// <param name="warehouse">
    /// Склад.
    /// </param>
    public void AddWarehouse(WarehouseWithoutId warehouse);

    /// <summary>
    /// Изменение склада.
    /// </summary>
    /// <param name="warehouse">
    /// Склад.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateWarehouseAsync(
        WarehouseForUpdate warehouse, 
        CancellationToken token);

    /// <summary>
    /// Обновить количество товара на складе.
    /// </summary>
    /// <param name="warehouseId">
    /// Идентификатор склада.
    /// </param>
    /// <param name="productId">
    /// Идентификатор товара.
    /// </param>
    /// <param name="count">
    /// Количество товара.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Количество товаров на складе.
    /// </returns>
    public Task UpdateProductCountInWarehouseAsync(
        Id<Warehouse> warehouseId,
        Id<Product> productId,
        Count count,
        CancellationToken token);

    /// <summary>
    /// Удаление склада.
    /// </summary>
    /// <param name="warehouseId">
    /// Идентификатор склада.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task DeleteWarehouseAsync(Id<Warehouse> warehouseId, CancellationToken token);
}
