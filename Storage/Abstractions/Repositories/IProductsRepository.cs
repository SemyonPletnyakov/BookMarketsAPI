using Models;
using Models.FullEntities;
using Models.Pagination;
using Models.Pagination.Sorting;

using ProductWithoutId = Models.ForCreate.Product;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория товаров.
/// </summary>
public interface IProductsRepository
{
    /// <summary>
    /// Получить часть товаров в соотвествии с пагинацией.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка товаров.
    /// </returns>
    public Task<IList<Product>> GetProductsAsync(
        PaginationInfo<ProductSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск товаров по совпадению названия в соотвествии с пагинацией.
    /// </summary>
    /// <param name="name">
    /// Фамилия.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка товаров.
    /// </returns>
    public Task<IList<Product>> GetProductsByNameAsync(
        Name<Product> name,
        PaginationInfo<ProductSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск товаров по ключевыми словам в соотвествии с пагинацией.
    /// </summary>
    /// <param name="keyWords">
    /// Ключевые слова.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка товаров.
    /// </returns>
    public Task<IList<Product>> GetProductsByKeyWordsOrderingByNameAsync(
        IReadOnlyCollection<string> keyWords,
        PaginationInfo<ProductSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Добавление товара.
    /// </summary>
    /// <param name="product">
    /// Товар.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task AddProductAsync(ProductWithoutId product, CancellationToken token);

    /// <summary>
    /// Изменение товара.
    /// </summary>
    /// <param name="product">
    /// Товар.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateProductAsync(Product product, CancellationToken token);

    /// <summary>
    /// Удаление товара.
    /// </summary>
    /// <param name="productId">
    /// Идентификатор товара.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task DeleteProductAsync(Id<Product> productId, CancellationToken token);
}
