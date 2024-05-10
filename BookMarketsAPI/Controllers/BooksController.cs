using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using BookMarketsAPI.Helpers;

using Logic.Abstractions.Processors;

using Models.Exceptions;
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
        IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<BookSorting>, IList<SimpleBook>> getBooksProcessor,
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
    [HttpGet("id")]
    public async Task<IActionResult> GetBookByIdAsync(
        int bookId,
        CancellationToken token)
    {
        try
        {
            var book = await _getBookByIdProcessor.ProcessAsync(new(new(bookId)), token);

            var transportBook = new Transport.Models.FullModels.Book
            {
                ProductId = book.ProductId.Value,
                Description = book.Description?.Value,
                Price = book.Price.Value,
                Name = book.Name.Value,
                KeyWords = book.KeyWords,
                Author = book.Author is null
                    ? null
                    : new Transport.Models.FullModels.Author
                    {
                        AuthorId = book.Author.AuthorId.Value,
                        FirstName = book.Author.FullName.FirstName,
                        LastName = book.Author.FullName.LastName,
                        BirthDate = book.Author.BirthDate,
                        Patronymic = book.Author.FullName.Patronymic,
                        Countries = book.Author.Countries
                    }
            };

            return Ok(transportBook);
        }
        catch (EntityNotFoundException)
        {
            return NotFound();
        }
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
        var books =
            (await _getBooksProcessor.ProcessAsync(new(new(size, number, order)), token))
                .Select(a =>
                    new Transport.Models.SimpleModels.Book 
                    { 
                        ProductId = a.ProductId.Value,
                        Price = a.Price.Value,
                        Name = a.Name.Value,
                        KeyWords = a.KeyWords,
                        Author = a.Author is null
                            ? null
                            : new Transport.Models.FullModels.Author
                            {
                                AuthorId = a.Author.AuthorId.Value,
                                FirstName = a.Author.FullName.FirstName,
                                LastName = a.Author.FullName.LastName,
                                BirthDate = a.Author.BirthDate,
                                Patronymic = a.Author.FullName.Patronymic,
                                Countries = a.Author.Countries
                            }
                    }).ToArray();

        return Ok(books);
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
    [HttpGet("author")]
    public async Task<IActionResult> GetBooksByAuthorIdAsync(
        int authorId,
        int size,
        int number,
        BookSorting order,
        CancellationToken token)
    {
        var books =
            (await _getBooksByAuthorIdProcessor
                .ProcessAsync(
                    new(new(authorId), 
                    new(size, number, order)), 
                    token))
                .Select(a =>
                    new Transport.Models.SimpleModels.Book
                    {
                        ProductId = a.ProductId.Value,
                        Price = a.Price.Value,
                        Name = a.Name.Value,
                        KeyWords = a.KeyWords,
                        Author = a.Author is null
                            ? null
                            : new Transport.Models.FullModels.Author
                            {
                                AuthorId = a.Author.AuthorId.Value,
                                FirstName = a.Author.FullName.FirstName,
                                LastName = a.Author.FullName.LastName,
                                BirthDate = a.Author.BirthDate,
                                Patronymic = a.Author.FullName.Patronymic,
                                Countries = a.Author.Countries
                            }
                    }).ToArray();

        return Ok(books);
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
    [HttpGet("name")]
    public async Task<IActionResult> GetBooksByNameAsync(
        string name,
        int size,
        int number,
        BookSorting order,
        CancellationToken token)
    {
        var books =
            (await _getBooksByNameProcessor
                .ProcessAsync(
                    new(new(name),
                    new(size, number, order)),
                    token))
                .Select(a =>
                    new Transport.Models.SimpleModels.Book
                    {
                        ProductId = a.ProductId.Value,
                        Price = a.Price.Value,
                        Name = a.Name.Value,
                        KeyWords = a.KeyWords,
                        Author = a.Author is null
                            ? null
                            : new Transport.Models.FullModels.Author
                            {
                                AuthorId = a.Author.AuthorId.Value,
                                FirstName = a.Author.FullName.FirstName,
                                LastName = a.Author.FullName.LastName,
                                BirthDate = a.Author.BirthDate,
                                Patronymic = a.Author.FullName.Patronymic,
                                Countries = a.Author.Countries
                            }
                    }).ToArray();

        return Ok(books);
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
    [HttpGet("key_words")]
    public async Task<IActionResult> GetBooksByKeyWordsAsync(
        string keyWords,
        int size,
        int number,
        BookSorting order,
        CancellationToken token)
    {
        var keyWordsArr = keyWords.Split(',');

        var books =
            (await _getBooksByKeywordsProcessor
                .ProcessAsync(
                    new(keyWordsArr,
                    new(size, number, order)),
                    token))
                .Select(a =>
                    new Transport.Models.SimpleModels.Book
                    {
                        ProductId = a.ProductId.Value,
                        Price = a.Price.Value,
                        Name = a.Name.Value,
                        KeyWords = a.KeyWords,
                        Author = a.Author is null
                            ? null
                            : new Transport.Models.FullModels.Author
                            {
                                AuthorId = a.Author.AuthorId.Value,
                                FirstName = a.Author.FullName.FirstName,
                                LastName = a.Author.FullName.LastName,
                                BirthDate = a.Author.BirthDate,
                                Patronymic = a.Author.FullName.Patronymic,
                                Countries = a.Author.Countries
                            }
                    }).ToArray();

        return Ok(books);
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
    [Authorize]
    public async Task<IActionResult> AddBookAsync(
        Transport.Models.ForCreate.Book book,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _addBookProcessor.ProcessAsync(
                new(new(new(book.Name),
                        book.Description is null
                            ? null
                            : new(book.Description),
                        new(book.Price),
                        book.KeyWords?.ToHashSet(),
                        book.AuthorId is null
                            ? null
                            : new(book.AuthorId.Value))),
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
    [Authorize]
    public async Task<IActionResult> UpdateBookAsync(
        Transport.Models.ForUpdate.Book book,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _updateBookProcessor.ProcessAsync(
                new(new(new(book.ProductId),
                        new(book.Name),
                        book.Description is null
                            ? null
                            : new(book.Description),
                        new(book.Price),
                        book.KeyWords?.ToHashSet(),
                        book.AuthorId is null
                            ? null
                            : new(book.AuthorId.Value))),
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
    [Authorize]
    public async Task<IActionResult> BookToProductAsync(
        Transport.Models.Ids.Product productId,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _bookToProdoctProcessor.ProcessAsync(
                new(new(productId.ProductId)),
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
    [Authorize]
    public async Task<IActionResult> DeleteBookAsync(
        Transport.Models.Ids.Product productId,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _deleteBookProcessor.ProcessAsync(
                new(new(productId.ProductId)),
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

    private readonly IRequestProcessorWithoutAuthorize<RequestGetOneById<Product, Book>, Book> _getBookByIdProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<BookSorting>, IList<SimpleBook>> _getBooksProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByIdWithPagination<Author, BookSorting>, IList<SimpleBook>> _getBooksByAuthorIdProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByNameWithPagination<Product, BookSorting>, IList<SimpleBook>> _getBooksByNameProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByKeyWordsWithPagination<BookSorting>, IList<SimpleBook>> _getBooksByKeywordsProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<BookWithoutId>> _addBookProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<BookForUpdate>> _updateBookProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Product, Book>> _bookToProdoctProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Product, Book>> _deleteBookProcessor;
}
