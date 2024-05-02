using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Logic.Abstractions.Processors;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using ProductWithoutId = Models.ForCreate.Product;
using SimpleProduct = Models.SimpleEntities.Product;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер товаров.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="ProductsController"/>.
    /// </summary>
    /// <param name="getProductByIdProcessor">
    /// Процессор получения товара по идентификатору.
    /// </param>
    /// <param name="getProductsProcessor">
    /// Процессор получения товаров.
    /// </param>
    /// <param name="getProductsByNameProcessor">
    /// Процессор получения товаров по названию.
    /// </param>
    /// <param name="getProductsByKeywordsProcessor">
    /// Процессор получения товаров по ключевым словам.
    /// </param>
    /// <param name="addProductProcessor">
    /// Процессор добавления товара.
    /// </param>
    /// <param name="updateProductProcessor">
    /// Процессор обновления товара.
    /// </param>
    /// <param name="productToBookProcessor">
    /// Процессор добавления товара в список книг.
    /// </param>
    /// <param name="deleteProductProcessor">
    /// Процессор удаления товара.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public ProductsController(
        IRequestProcessorWithoutAuthorize<RequestGetOneById<Product, Product>, Product> getProductByIdProcessor, 
        IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<ProductSorting>, IList<SimpleProduct>> getProductsProcessor, 
        IRequestProcessorWithoutAuthorize<RequestGetManyByNameWithPagination<Product, ProductSorting>, IList<SimpleProduct>> getProductsByNameProcessor, 
        IRequestProcessorWithoutAuthorize<RequestGetManyByKeyWordsWithPagination<ProductSorting>, IList<SimpleProduct>> getProductsByKeywordsProcessor, 
        IRequestProcessorWithAuthorize<RequestAddEntity<ProductWithoutId>> addProductProcessor, 
        IRequestProcessorWithAuthorize<RequestUpdateEntity<Product>> updateProductProcessor, 
        IRequestProcessorWithAuthorize<RequestAddProductInBooks> productToBookProcessor, 
        IRequestProcessorWithAuthorize<RequestDeleteEntityById<Product, Product>> deleteProductProcessor)
    {
        _getProductByIdProcessor = getProductByIdProcessor 
            ?? throw new ArgumentNullException(nameof(getProductByIdProcessor));
        _getProductsProcessor = getProductsProcessor 
            ?? throw new ArgumentNullException(nameof(getProductsProcessor));
        _getProductsByNameProcessor = getProductsByNameProcessor 
            ?? throw new ArgumentNullException(nameof(getProductsByNameProcessor));
        _getProductsByKeywordsProcessor = getProductsByKeywordsProcessor 
            ?? throw new ArgumentNullException(nameof(getProductsByKeywordsProcessor));
        _addProductProcessor = addProductProcessor 
            ?? throw new ArgumentNullException(nameof(addProductProcessor));
        _updateProductProcessor = updateProductProcessor 
            ?? throw new ArgumentNullException(nameof(updateProductProcessor));
        _productToBookProcessor = productToBookProcessor 
            ?? throw new ArgumentNullException(nameof(productToBookProcessor));
        _deleteProductProcessor = deleteProductProcessor 
            ?? throw new ArgumentNullException(nameof(deleteProductProcessor));
    }

    /// <summary>
    /// Получить товар.
    /// </summary>
    /// <param name="productId">
    /// Идентификатор товара.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Товар.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetProductByIdAsync(
        int productId,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить товары.
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
    /// Товары.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetProductsAsync(
        int size,
        int number,
        ProductSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить товары по названию.
    /// </summary>
    /// <param name="name">
    /// Название.
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
    /// Товары.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetProductsByNameAsync(
        string name,
        int size,
        int number,
        ProductSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить товары по ключевым словам.
    /// </summary>
    /// <param name="keyWords">
    /// Ключевые слова.
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
    /// Товары.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetProductsByKeyWordsAsync(
        string[] keyWords,
        int size,
        int number,
        ProductSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Добавить товар.
    /// </summary>
    /// <param name="product">
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
    public async Task<IActionResult> AddProductAsync(
        Transport.Models.ForCreate.Product product,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Изменить товар.
    /// </summary>
    /// <param name="product">
    /// Товар.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateProductAsync(
        Transport.Models.FullModels.Product product,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Добавление товара в списка книг.
    /// </summary>
    /// <param name="productId">
    /// Идентификатор товара.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut("product_to_book")]
    //авторизация
    public async Task<IActionResult> ProductToBookAsync(
        Transport.Models.Ids.Product productId,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Удалить товар.
    /// </summary>
    /// <param name="productId">
    /// Идентификатор товара.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpDelete]
    //авторизация
    public async Task<IActionResult> DeleteProductAsync(
        Transport.Models.Ids.Product productId,
        CancellationToken token)
    {

    }

    private readonly IRequestProcessorWithoutAuthorize<RequestGetOneById<Product, Product>, Product> _getProductByIdProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<ProductSorting>, IList<SimpleProduct>> _getProductsProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByNameWithPagination<Product, ProductSorting>, IList<SimpleProduct>> _getProductsByNameProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByKeyWordsWithPagination<ProductSorting>, IList<SimpleProduct>> _getProductsByKeywordsProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<ProductWithoutId>> _addProductProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<Product>> _updateProductProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddProductInBooks> _productToBookProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Product, Product>> _deleteProductProcessor;
}
