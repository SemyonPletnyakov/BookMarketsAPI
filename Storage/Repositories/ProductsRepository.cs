using Microsoft.EntityFrameworkCore;

using Models;
using Models.Pagination;
using Models.Pagination.Sorting;
using Models.FullEntities;

using Storage.Abstractions.Repositories;
using Storage.Getters;
using Storage.Exceptions;

using ProductWithoutId = Models.ForCreate.Product;
using SimpleProduct = Models.SimpleEntities.Product;

namespace Storage.Repositories;

/// <summary>
/// Репозиторий товаров.
/// </summary>
public sealed class ProductsRepository : IProductsRepository
{
    /// <summary>
    /// Создаёт объект <see cref="ProductsRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public ProductsRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IList<SimpleProduct>> GetProductsAsync(
        PaginationInfo<ProductSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Products
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(p =>
                new SimpleProduct(
                    new(p.ProductId),
                    new(p.Name),
                    new(p.Price),
                    p.KeyWords?.ToHashSet()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<SimpleProduct>> GetProductsByNameAsync(
        Name<Product> name,
        PaginationInfo<ProductSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        ArgumentNullException.ThrowIfNull(name);
        token.ThrowIfCancellationRequested();

        return (await _context.Products
            .Where(p => EF.Functions.Like(p.Name, $"%{name.Value}%"))
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(p =>
                new SimpleProduct(
                    new(p.ProductId),
                    new(p.Name),
                    new(p.Price),
                    p.KeyWords?.ToHashSet()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<SimpleProduct>> GetProductsByKeyWordsOrderingByNameAsync(
        IReadOnlyCollection<string> keyWords,
        PaginationInfo<ProductSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(keyWords);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Products
            .Where(p => 
                p.KeyWords != null 
                && p.KeyWords.Count(kw => keyWords.Contains(kw)) == keyWords.Count)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(p =>
                new SimpleProduct(
                    new(p.ProductId),
                    new(p.Name),
                    new(p.Price),
                    p.KeyWords?.ToHashSet()))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<Product> GetProductByIdAsync(
        Id<Product> productId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(productId);
        token.ThrowIfCancellationRequested();

        var product = await _context.Products
            .SingleOrDefaultAsync(p => p.ProductId == productId.Value, token);

        if (product is null)
        {
            throw new EntityNotFoundException(
                $"Товар с Id = {productId.Value} не найден.");
        }

        return new(
            new(product.ProductId),
            new(product.Name),
            product.Description == null
                ? null
                : new(product.Description),
            new(product.Price),
            product.KeyWords?.ToHashSet());
    }

    /// <inheritdoc/>
    public async Task<Id<Product>> AddProduct(
        ProductWithoutId product,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);

        var contextProduct = await _context.Products.AddAsync(
            new Models.Product
            {
                Name = product.Name.Value,
                Description = product.Description?.Value,
                Price = product.Price.Value,
                KeyWords = product.KeyWords?.ToList()
            },
            token);

        return new(contextProduct.Entity.ProductId);
    }

    /// <inheritdoc/>
    public async Task UpdateProductAsync(Product product, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(product);
        token.ThrowIfCancellationRequested();

        var contextProduct = await _context.Products
            .SingleOrDefaultAsync(p => p.ProductId == product.ProductId.Value, token);

        if (contextProduct is null)
        {
            throw new EntityNotFoundException(
                $"Товар с Id = {product.ProductId.Value} не найден.");
        }

        contextProduct.Name = product.Name.Value;
        contextProduct.Description = product.Description?.Value;
        contextProduct.Price = product.Price.Value;
        contextProduct.KeyWords = product.KeyWords?.ToList();
    }

    /// <inheritdoc/>
    public async Task DeleteProductAsync(Id<Product> productId, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(productId);
        token.ThrowIfCancellationRequested();

        var contextProduct = await _context.Products
            .SingleOrDefaultAsync(p => p.ProductId == productId.Value, token);

        if (contextProduct is null)
        {
            throw new EntityNotFoundException(
                $"Товар с Id = {productId.Value} не найден.");
        }

        _context.Products.Remove(contextProduct);
    }

    private readonly ApplicationContext _context;
}
