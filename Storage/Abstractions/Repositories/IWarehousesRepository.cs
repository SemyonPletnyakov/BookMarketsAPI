using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using WarehouseWithoutId = Models.ForCreate.Warehouse;

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
    /// Поиск склада по адресу в соотвествии с пагинацией.
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
    /// Склад.
    /// </returns>
    public Task<Warehouse> GetWarehouseByAddressAsync(
        Address address,
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
    public Task<IReadOnlyCollection<ProductCount>> GetProductsCountInWarehouseByIdAsync(
        Id<Warehouse> warehouseId,
        PaginationInfo<ProductCountSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Добавление склада.
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
    public Task AddWarehouseAsync(WarehouseWithoutId warehouse, CancellationToken token);

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
    public Task UpdateWarehouseAsync(Warehouse warehouse, CancellationToken token);

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
