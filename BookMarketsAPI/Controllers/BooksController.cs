using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Logic.Abstractions.Processors;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using BookWithoutId = Models.ForCreate.Book;
using BookForUpdate = Models.ForUpdate.Book;
using SimpleBook = Models.SimpleEntities.Book;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер книг.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class BooksController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="BooksController"/>.
    /// </summary>
    /// <param name="getBookByIdProcessor">
    /// Процессор получения книги по идентификатору.
    /// </param>
    /// <param name="getBooksProcessor">
    /// Процессор получения книг.
    /// </param>
    /// <param name="getBooksByAuthorIdProcessor">
    /// Процессор получения книг по идентификатору автора.
    /// </param>
    /// <param name="getBooksByNameProcessor">
    /// Процессор получения книг по названию.
    /// </param>
    /// <param name="getBooksByKeywordsProcessor">
    /// Процессор получения книг по ключевым словам.
    /// </param>
    /// <param name="addBookProcessor">
    /// Процессор добавления книги.
    /// </param>
    /// <param name="bookToProdoctProcessor">
    /// Процессор удаления товара из списка книг.
    /// </param>
    /// <param name="updateBookProcessor">
    /// Процессор обновления книги.
    /// </param>
    /// <param name="deleteBookProcessor">
    /// Процессор удаления книги.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public BooksController(
        IRequestProcessorWithoutAuthorize<RequestGetOneById<Product, Book>, Book> getBookByIdProcessor,
        IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<BookSorting>, IList<Book>> getBooksProcessor,
        IRequestProcessorWithoutAuthorize<RequestGetManyByIdWithPagination<Author, BookSorting>, IList<SimpleBook>> getBooksByAuthorIdProcessor,
        IRequestProcessorWithoutAuthorize<RequestGetManyByNameWithPagination<Product, BookSorting>, IList<SimpleBook>> getBooksByNameProcessor,
        IRequestProcessorWithoutAuthorize<RequestGetManyByKeyWordsWithPagination<BookSorting>, IList<SimpleBook>> getBooksByKeywordsProcessor,
        IRequestProcessorWithAuthorize<RequestAddEntity<BookWithoutId>> addBookProcessor,
        IRequestProcessorWithAuthorize<RequestUpdateEntity<BookForUpdate>> updateBookProcessor,
        IRequestProcessorWithAuthorize<RequestDeleteEntityById<Product, Book>> bookToProdoctProcessor,
        IRequestProcessorWithAuthorize<RequestDeleteEntityById<Product, Book>> deleteBookProcessor)
    {
        _getBookByIdProcessor = getBookByIdProcessor
            ?? throw new ArgumentNullException(nameof(getBookByIdProcessor));
        _getBooksProcessor = getBooksProcessor
            ?? throw new ArgumentNullException(nameof(getBooksProcessor));
        _getBooksByAuthorIdProcessor = getBooksByAuthorIdProcessor
            ?? throw new ArgumentNullException(nameof(getBooksByAuthorIdProcessor));
        _getBooksByNameProcessor = getBooksByNameProcessor
            ?? throw new ArgumentNullException(nameof(getBooksByNameProcessor));
        _getBooksByKeywordsProcessor = getBooksByKeywordsProcessor
            ?? throw new ArgumentNullException(nameof(getBooksByKeywordsProcessor));
        _addBookProcessor = addBookProcessor
            ?? throw new ArgumentNullException(nameof(addBookProcessor));
        _updateBookProcessor = updateBookProcessor
            ?? throw new ArgumentNullException(nameof(updateBookProcessor));
        _bookToProdoctProcessor = bookToProdoctProcessor
            ?? throw new ArgumentNullException(nameof(bookToProdoctProcessor));
        _deleteBookProcessor = deleteBookProcessor
            ?? throw new ArgumentNullException(nameof(deleteBookProcessor));
    }

    /// <summary>
    /// Получить книгу.
    /// </summary>
    /// <param name="bookId">
    /// Идентификатор книги.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Книга.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetBookByIdAsync(
        int bookId,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить книги.
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
    /// Книги.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetBooksAsync(
        int size,
        int number,
        BookSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить книги по идентификатору автора.
    /// </summary>
    /// <param name="authorId">
    /// Идентификатор автора.
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
    /// Книги.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetBooksByAuthorIdAsync(
        int authorId,
        int size,
        int number,
        BookSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить книги по названию.
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
    /// Книги.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetBooksByNameAsync(
        string name,
        int size,
        int number,
        BookSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Получить книги по ключевым словам.
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
    /// Книги.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetBooksByKeyWordsAsync(
        string[] keyWords,
        int size,
        int number,
        BookSorting order,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Добавить книгу.
    /// </summary>
    /// <param name="book">
    /// Книга.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPost]
    //авторизация
    public async Task<IActionResult> AddBookAsync(
        Transport.Models.ForCreate.Book book,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Изменить книгу.
    /// </summary>
    /// <param name="book">
    /// Книга.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateBookAsync(
        Transport.Models.ForUpdate.Book book,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Удаление товара из списка книг.
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
    [HttpPut("book_to_product")]
    //авторизация
    public async Task<IActionResult> BookToProductAsync(
        Transport.Models.Ids.Product productId,
        CancellationToken token)
    {

    }

    /// <summary>
    /// Удалить книгу.
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
    public async Task<IActionResult> DeleteBookAsync(
        Transport.Models.Ids.Product productId,
        CancellationToken token)
    {

    }

    private readonly IRequestProcessorWithoutAuthorize<RequestGetOneById<Product, Book>, Book> _getBookByIdProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<BookSorting>, IList<Book>> _getBooksProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByIdWithPagination<Author, BookSorting>, IList<SimpleBook>> _getBooksByAuthorIdProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByNameWithPagination<Product, BookSorting>, IList<SimpleBook>> _getBooksByNameProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByKeyWordsWithPagination<BookSorting>, IList<SimpleBook>> _getBooksByKeywordsProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<BookWithoutId>> _addBookProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<BookForUpdate>> _updateBookProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Product, Book>> _bookToProdoctProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Product, Book>> _deleteBookProcessor;
}
