using Microsoft.EntityFrameworkCore;

using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using Storage.Abstractions.Repositories;
using Storage.Getters;
using Models.Exceptions;

using ShopWithoutId = Models.ForCreate.Shop;
using ShopForUpdate = Models.ForUpdate.Shop;
using ProductCount = Models.SimpleEntities.ProductCount;

namespace Storage.Repositories;

/// <summary>
/// Репозиторий магазинов.
/// </summary>
public sealed class ShopsRepository : IShopsRepository
{
    /// <summary>
    /// Создаёт объект <see cref="ShopsRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public ShopsRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IList<Shop>> GetShopsAsync(
        PaginationInfo<ShopSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Shops.Include(w => w.Address)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(w =>
                new Shop(
                    new(w.ShopId),
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
    public async Task<IReadOnlyCollection<ProductCount>> GetProductsCountInShopByIdAsync(
        Id<Shop> shopId,
        PaginationInfo<ProductCountSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shopId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.ProductsInShops.Where(piw => piw.ShopId == shopId.Value)
            .Include(piw => piw.Product)
            .WhithPaginationInfo(paginationInfo)
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
    public void AddShop(ShopWithoutId shop)
    {
        ArgumentNullException.ThrowIfNull(shop);

        _context.Shops.Add(
            new Models.Shop
            {
                AddressId = shop.AddressId.Value,
                Name = shop.Name?.Value,
                OpeningTime = shop.OpeningTime,
                ClosingTime = shop.ClosingTime
            });
    }

    /// <inheritdoc/>
    public async Task UpdateShopAsync(ShopForUpdate shop, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shop);
        token.ThrowIfCancellationRequested();

        var contextShop = await _context.Shops
            .SingleOrDefaultAsync(
                w => w.ShopId == shop.ShopId.Value,
                token);

        if (contextShop == null)
        {
            throw new EntityNotFoundException(
                $"Магазин с Id = {shop.ShopId.Value} не найден.");
        }

        contextShop.AddressId = shop.AddressId.Value;
        contextShop.Name = shop.Name?.Value;
        contextShop.OpeningTime = shop.OpeningTime;
        contextShop.ClosingTime = shop.ClosingTime;
    }

    /// <inheritdoc/>
    public async Task UpdateProductCountInShopAsync(
        Id<Shop> shopId,
        Id<Product> productId,
        Count count,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shopId);
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentNullException.ThrowIfNull(count);
        token.ThrowIfCancellationRequested();

        var productsInShops = await _context.ProductsInShops
            .SingleOrDefaultAsync(
                piw => piw.ShopId == shopId.Value
                    && piw.ProductId == productId.Value,
                token);

        if (productsInShops is not null)
        {
            productsInShops.Count = count.Value;
        }

        var shop = await _context.Shops
            .SingleOrDefaultAsync(
                w => w.ShopId == shopId.Value,
                token);

        if (shop == null)
        {
            throw new EntityNotFoundException(
                $"Магазин с Id = {shopId.Value} не найден.");
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

        _context.ProductsInShops.Add(
            new Models.ProductsInShop
            {
                ShopId = shopId.Value,
                ProductId = productId.Value,
                Count = count.Value
            });
    }

    /// <inheritdoc/>
    public async Task DeleteShopAsync(Id<Shop> shopId, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(shopId);
        token.ThrowIfCancellationRequested();

        var contextShop = await _context.Shops
            .SingleOrDefaultAsync(
                w => w.ShopId == shopId.Value,
                token);

        if (contextShop == null)
        {
            throw new EntityNotFoundException(
                $"Магазин с Id = {shopId.Value} не найден.");
        }

        _context.Shops.Remove(contextShop);
    }

    private readonly ApplicationContext _context;
}
