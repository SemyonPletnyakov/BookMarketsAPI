using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using BookMarketsAPI.Helpers;

using Logic.Abstractions.Processors;

using Models.Exceptions;
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
        IRequestProcessorWithAuthorize<RequestGetManyWithPagination<WarehouseSorting>, IList<Warehouse>> getWarehousesProcessor,
        IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Warehouse, ProductCountSorting>, IList<ProductCount>> getProductCountsInWarehouseProcessor,
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
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var warehouses =
                (await _getWarehousesProcessor.ProcessAsync(new(new(size, number, order)), jwtToken, token))
                    .Select(warehouse =>
                        new Transport.Models.FullModels.Warehouse
                        {
                            WarehouseId = warehouse.WarehouseId.Value,
                            Name = warehouse.Name?.Value,
                            OpeningTime = warehouse.OpeningTime,
                            ClosingTime = warehouse.ClosingTime,
                            Address = new()
                            {
                                AddressId = warehouse.Address.AddressId.Value,
                                Country = warehouse.Address.Country,
                                RegionNumber = warehouse.Address.RegionNumber,
                                RegionName = warehouse.Address.RegionName,
                                City = warehouse.Address.City,
                                District = warehouse.Address.District,
                                Street = warehouse.Address.Street,
                                House = warehouse.Address.House,
                                Room = warehouse.Address.Room
                            }
                        }).ToArray();

            return Ok(warehouses);
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
    [HttpGet("product_count")]
    [Authorize]
    public async Task<IActionResult> GetProductCountsInWarehouseAsync(
        int warehouseId,
        int size,
        int number,
        ProductCountSorting order,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            var warehouses =
                (await _getProductCountsInWarehouseProcessor.ProcessAsync(
                        new(new(warehouseId), new(size, number, order)),
                        jwtToken,
                        token))
                    .Select(product =>
                        new Transport.Models.SimpleModels.ProductCount
                        {
                            ProductId = product.ProductId.Value,
                            Count = product.Count.Value
                        }).ToArray();

            return Ok(warehouses);
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
    [Authorize]
    public async Task<IActionResult> AddWarehouseAsync(
        Transport.Models.ForCreate.Warehouse warehouse,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _addWarehouseProcessor.ProcessAsync(
                new(new(
                        warehouse.Name is null
                            ? null
                            : new(warehouse.Name),
                        warehouse.OpeningTime,
                        warehouse.ClosingTime,
                        new(warehouse.AddressId))),
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
    [Authorize]
    public async Task<IActionResult> UpdateWarehouseAsync(
        Transport.Models.ForUpdate.Warehouse warehouse,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _updateWarehouseProcessor.ProcessAsync(
                new(new(new(warehouse.WarehouseId),
                        warehouse.Name is null
                            ? null
                            : new(warehouse.Name),
                        warehouse.OpeningTime,
                        warehouse.ClosingTime,
                        new(warehouse.AddressId))),
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
    [HttpPut("product_count")]
    [Authorize]
    public async Task<IActionResult> UpdateProductCountInWarehouseAsync(
        Transport.Models.ForUpdate.ProductCountInWarehouse productCountInWarehouse,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _updateProductCountInWarehouseProcessor.ProcessAsync(
                new(new(productCountInWarehouse.WarehouseId),
                    new(productCountInWarehouse.ProductId),
                    new(productCountInWarehouse.Count)),
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
    [Authorize]
    public async Task<IActionResult> DeleteWarehouseAsync(
        Transport.Models.Ids.Warehouse warehouseId,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _deleteWarehouseProcessor.ProcessAsync(
                new(new(warehouseId.WarehouseId)),
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

    private readonly IRequestProcessorWithAuthorize<RequestGetManyWithPagination<WarehouseSorting>, IList<Warehouse>> _getWarehousesProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestGetManyByIdWithPagination<Warehouse, ProductCountSorting>, IList<ProductCount>> _getProductCountsInWarehouseProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<WarehouseWithoutId>> _addWarehouseProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<WarehouseForUpdate>> _updateWarehouseProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateProductCountInEntity<Warehouse>> _updateProductCountInWarehouseProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Warehouse, Warehouse>> _deleteWarehouseProcessor;
}

