using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Logic.Abstractions.Processors;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using WarehouseWithoutId = Models.ForCreate.Warehouse;
using WarehouseForUpdate = Models.ForUpdate.Warehouse;
using ProductCount = Models.SimpleEntities.ProductCount;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер складов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class WarehousesController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="WarehousesController"/>.
    /// </summary>
    /// <param name="getWarehousesProcessor">
    /// Процессор получения складов.
    /// </param>
    /// <param name="getProductCountsInWarehouseProcessor">
    /// Процессор получения количества товаров на складе.
    /// </param>
    /// <param name="addWarehouseProcessor">
    /// Процессор добавления склада.
    /// </param>
    /// <param name="updateWarehouseProcessor">
    /// Процессор обновления склада.
    /// </param>
    /// <param name="updateProductCountInWarehouseProcessor">
    /// Процессор обновления количества товара на складе.
    /// </param>
    /// <param name="deleteWarehouseProcessor">
    /// Процессор удаления склада.
    /// </param>
    /// <exception cref="ArgumentNullException"></exception>
    public WarehousesController(
        IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<WarehouseSorting>, IList<Warehouse>> getWarehousesProcessor,
        IRequestProcessorWithoutAuthorize<RequestGetManyByIdWithPagination<Warehouse, ProductCountSorting>, IList<ProductCount>> getProductCountsInWarehouseProcessor,
        IRequestProcessorWithAuthorize<RequestAddEntity<WarehouseWithoutId>> addWarehouseProcessor,
        IRequestProcessorWithAuthorize<RequestUpdateEntity<WarehouseForUpdate>> updateWarehouseProcessor,
        IRequestProcessorWithAuthorize<RequestUpdateProductCountInEntity<Warehouse>> updateProductCountInWarehouseProcessor,
        IRequestProcessorWithAuthorize<RequestDeleteEntityById<Warehouse, Warehouse>> deleteWarehouseProcessor)
    {
        _getWarehousesProcessor = getWarehousesProcessor
            ?? throw new ArgumentNullException(nameof(getWarehousesProcessor));
        _getProductCountsInWarehouseProcessor = getProductCountsInWarehouseProcessor
            ?? throw new ArgumentNullException(nameof(getProductCountsInWarehouseProcessor));
        _addWarehouseProcessor = addWarehouseProcessor
            ?? throw new ArgumentNullException(nameof(addWarehouseProcessor));
        _updateWarehouseProcessor = updateWarehouseProcessor
            ?? throw new ArgumentNullException(nameof(updateWarehouseProcessor));
        _updateProductCountInWarehouseProcessor = updateProductCountInWarehouseProcessor
            ?? throw new ArgumentNullException(nameof(updateProductCountInWarehouseProcessor));
        _deleteWarehouseProcessor = deleteWarehouseProcessor
            ?? throw new ArgumentNullException(nameof(deleteWarehouseProcessor));
    }

    /// <summary>
    /// Получить склады.
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
    /// Склады.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetWarehousesAsync(
        int size,
        int number,
        WarehouseSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить количество товаров на складе.
    /// </summary>
    /// <param name="warehouseId">
    /// Идентификатор склада.
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
    public async Task<IActionResult> GetProductCountsInWarehouseAsync(
        int warehouseId,
        int size,
        int number,
        ProductCountSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Добавить склад.
    /// </summary>
    /// <param name="warehouse">
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
    public async Task<IActionResult> AddWarehouseAsync(
        Transport.Models.ForCreate.Warehouse warehouse,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Изменить склад.
    /// </summary>
    /// <param name="warehouse">
    /// Склад.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateWarehouseAsync(
        Transport.Models.FullModels.Warehouse warehouse,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Изменить количество товара на складе.
    /// </summary>
    /// <param name="productCountInWarehouse">
    /// Количество товара на складе.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateProductCountInWarehouseAsync(
        Transport.Models.ForUpdate.ProductCountInWarehouse productCountInWarehouse,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Удалить склад.
    /// </summary>
    /// <param name="warehouseId">
    /// Идентификатор склада.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpDelete]
    //авторизация
    public async Task<IActionResult> DeleteWarehouseAsync(
        Transport.Models.Ids.Warehouse warehouseId,
        CancellationToken token)
    {

    }

    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<WarehouseSorting>, IList<Warehouse>> _getWarehousesProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByIdWithPagination<Warehouse, ProductCountSorting>, IList<ProductCount>> _getProductCountsInWarehouseProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<WarehouseWithoutId>> _addWarehouseProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<WarehouseForUpdate>> _updateWarehouseProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateProductCountInEntity<Warehouse>> _updateProductCountInWarehouseProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Warehouse, Warehouse>> _deleteWarehouseProcessor;
}

