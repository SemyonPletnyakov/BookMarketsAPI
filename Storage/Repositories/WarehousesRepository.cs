using Microsoft.EntityFrameworkCore;

using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using Storage.Abstractions.Repositories;
using Storage.Getters;
using Models.Exceptions;

using WarehouseWithoutId = Models.ForCreate.Warehouse;
using WarehouseForUpdate = Models.ForUpdate.Warehouse;
using ProductCount = Models.SimpleEntities.ProductCount;

namespace Storage.Repositories;

/// <summary>
/// Репозиторий складов.
/// </summary>
public sealed class WarehousesRepository : IWarehousesRepository
{
    /// <summary>
    /// Создаёт объект <see cref="WarehousesRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public WarehousesRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IList<Warehouse>> GetWarehousesAsync(
        PaginationInfo<WarehouseSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Warehouses.Include(w => w.Address)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(w =>
                new Warehouse(
                    new(w.WarehouseId),
                    w.Name == null
                        ? null
                        : new(w.Name),
                    w.OpeningTime,
                    w.ClosingTime,
                    new(
                        new(w.AddressId),
                        w.Address.Country,
                        w.Address.RegionNumber,
                        w.Address.RegionName,
                        w.Address.City,
                        w.Address.District,
                        w.Address.Street,
                        w.Address.House,
                        w.Address.Room)))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<ProductCount>> GetProductsCountInWarehouseByIdAsync(
        Id<Warehouse> warehouseId,
        PaginationInfo<ProductCountSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(warehouseId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.ProductsInWarehouses.Where(piw => piw.WarehouseId == warehouseId.Value)
            .Include(piw => piw.Product)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(piw =>
                new ProductCount(
                    new(
                        new(piw.ProductId),
                        new(piw.Product.Name),
                        new(piw.Product.Price),
                        piw.Product.KeyWords?.ToHashSet()),
                    new(piw.Count)))
            .ToList();
    }

    /// <inheritdoc/>
    public void AddWarehouse(WarehouseWithoutId warehouse)
    {
        ArgumentNullException.ThrowIfNull(warehouse);

        _context.Warehouses.Add(
            new Models.Warehouse
            {
                AddressId = warehouse.AddressId.Value,
                Name = warehouse.Name?.Value,
                OpeningTime = warehouse.OpeningTime,
                ClosingTime = warehouse.ClosingTime
            });
    }

    /// <inheritdoc/>
    public async Task UpdateWarehouseAsync(
        WarehouseForUpdate warehouse, 
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(warehouse);
        token.ThrowIfCancellationRequested();

        var contextWarehouse = await _context.Warehouses
            .SingleOrDefaultAsync(
                w => w.WarehouseId == warehouse.WarehouseId.Value, 
                token);

        if (contextWarehouse == null)
        {
            throw new EntityNotFoundException(
                $"Склад с Id = {warehouse.WarehouseId.Value} не найден.");
        }

        contextWarehouse.AddressId = warehouse.AddressId.Value;
        contextWarehouse.Name = warehouse.Name?.Value;
        contextWarehouse.OpeningTime = warehouse.OpeningTime;
        contextWarehouse.ClosingTime = warehouse.ClosingTime;
    }

    /// <inheritdoc/>
    public async Task UpdateProductCountInWarehouseAsync(
        Id<Warehouse> warehouseId,
        Id<Product> productId,
        Count count,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(warehouseId);
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentNullException.ThrowIfNull(count);
        token.ThrowIfCancellationRequested();

        var productsInWarehouses = await _context.ProductsInWarehouses
            .SingleOrDefaultAsync(
                piw => piw.WarehouseId == warehouseId.Value 
                    && piw.ProductId == productId.Value,
                token);

        if (productsInWarehouses is not null) 
        {
            productsInWarehouses.Count = count.Value;
        }
        
        var warehouse = await _context.Warehouses
            .SingleOrDefaultAsync(
                w => w.WarehouseId == warehouseId.Value,
                token);

        if (warehouse == null)
        {
            throw new EntityNotFoundException(
                $"Склад с Id = {warehouseId.Value} не найден.");
        }

        var product = await _context.Products
            .SingleOrDefaultAsync(
                piw => piw.ProductId == productId.Value,
                token);

        if (product == null)
        {
            throw new EntityNotFoundException(
                $"Продукт с Id = {productId.Value} не найден.");
        }

        _context.ProductsInWarehouses.Add(
            new Models.ProductsInWarehouse
            {
                WarehouseId = warehouseId.Value,
                ProductId = productId.Value,
                Count = count.Value
            });
    }

    /// <inheritdoc/>
    public async Task DeleteWarehouseAsync(Id<Warehouse> warehouseId, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(warehouseId);
        token.ThrowIfCancellationRequested();

        var contextWarehouse = await _context.Warehouses
            .SingleOrDefaultAsync(
                w => w.WarehouseId == warehouseId.Value,
                token);

        if (contextWarehouse == null)
        {
            throw new EntityNotFoundException(
                $"Склад с Id = {warehouseId.Value} не найден.");
        }

        _context.Warehouses.Remove(contextWarehouse);
    }

    private readonly ApplicationContext _context;
}
