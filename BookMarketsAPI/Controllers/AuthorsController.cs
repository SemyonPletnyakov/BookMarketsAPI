using Microsoft.AspNetCore.Mvc;

using BookMarketsAPI.Helpers;

using Logic.Abstractions.Processors;

using Models.Exceptions;
using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using AuthorWithoutId = Models.ForCreate.Author;

namespace BookMarketsAPI.Controllers;

/// <summary>
/// Контроллер авторов.
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class AuthorsController : ControllerBase
{
    /// <summary>
    /// Создаёт объект <see cref="AuthorsController"/>.
    /// </summary>
    /// <param name="getAuthorsProcessor">
    /// Процессор получения авторов.
    /// </param>
    /// <param name="getAuthorsByLastNameProcessor">
    /// Процессор получения авторов по фамилии.
    /// </param>
    /// <param name="addAuthorProcessor">
    /// Процессор добавления автора.
    /// </param>
    /// <param name="deleteAuthorProcessor">
    /// Процессор удаления автора.
    /// </param>
    /// <param name="updateAuthorProcessor">
    /// Процессор обновления автора.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public AuthorsController(
        IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<AuthorSorting>, IList<Author>> getAuthorsProcessor,
        IRequestProcessorWithoutAuthorize<RequestGetManyByLastNameWithPagination<AuthorSorting>, IList<Author>> getAuthorsByLastNameProcessor,
        IRequestProcessorWithAuthorize<RequestAddEntity<AuthorWithoutId>> addAuthorProcessor,
        IRequestProcessorWithAuthorize<RequestDeleteEntityById<Author, Author>> deleteAuthorProcessor,
        IRequestProcessorWithAuthorize<RequestUpdateEntity<Author>> updateAuthorProcessor)
    {
        _getAuthorsProcessor = getAuthorsProcessor 
            ?? throw new ArgumentNullException(nameof(getAuthorsProcessor));
        _getAuthorsByLastNameProcessor = getAuthorsByLastNameProcessor 
            ?? throw new ArgumentNullException(nameof(getAuthorsByLastNameProcessor));
        _addAuthorProcessor = addAuthorProcessor 
            ?? throw new ArgumentNullException(nameof(addAuthorProcessor));
        _deleteAuthorProcessor = deleteAuthorProcessor 
            ?? throw new ArgumentNullException(nameof(deleteAuthorProcessor));
        _updateAuthorProcessor = updateAuthorProcessor 
            ?? throw new ArgumentNullException(nameof(updateAuthorProcessor));
    }

    /// <summary>
    /// Получить авторов.
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
    /// Авторы.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetAuthorsAsync(
        int size, 
        int number, 
        AuthorSorting order, 
        CancellationToken token)
    {
        var authors =
            (await _getAuthorsProcessor.ProcessAsync(new(new(size, number, order)), token))
                .Select(a =>
                    new Transport.Models.FullModels.Author
                    {
                        AuthorId = a.AuthorId.Value,
                        FirstName = a.FullName.FirstName,
                        LastName = a.FullName.LastName,
                        BirthDate = a.BirthDate,
                        Patronymic = a.FullName.Patronymic,
                        Countries = a.Countries
                    }).ToArray();

        return Ok(authors);
    }

    /// <summary>
    /// Получить атворов по фамилии.
    /// </summary>
    /// <param name="lname">
    /// Фамилия.
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
    /// Авторы.
    /// </returns>
    [HttpGet]
    public async Task<IActionResult> GetAuthorsByLastNameAsync(
        string lname, 
        int size, 
        int number, 
        AuthorSorting order, 
        CancellationToken token)
    {
        var authors =
            (await _getAuthorsByLastNameProcessor
                .ProcessAsync(new(new(lname), new(size, number, order)), token))
                    .Select(a =>
                        new Transport.Models.FullModels.Author
                        {
                            AuthorId = a.AuthorId.Value,
                            FirstName = a.FullName.FirstName,
                            LastName = a.FullName.LastName,
                            BirthDate = a.BirthDate,
                            Patronymic = a.FullName.Patronymic,
                            Countries = a.Countries
                        }).ToArray();

        return Ok(authors);
    }

    /// <summary>
    /// Добавить автора.
    /// </summary>
    /// <param name="author">
    /// Автор.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPost]
    //авторизация
    public async Task<IActionResult> AddAuthorAsync(
        Transport.Models.ForCreate.Author author, 
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _addAuthorProcessor.ProcessAsync(
                new(new(new(author.LastName, author.FirstName, author.Patronymic), 
                        author.BirthDate, 
                        author.Countries)),
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
    /// Изменить автора.
    /// </summary>
    /// <param name="author">
    /// Автор.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpPut]
    //авторизация
    public async Task<IActionResult> UpdateAuthorAsync(
        Transport.Models.FullModels.Author author,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _updateAuthorProcessor.ProcessAsync(
                new(new(new(author.AuthorId),
                        new(author.LastName, author.FirstName, author.Patronymic),
                        author.BirthDate,
                        author.Countries)),
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
    /// Удалить автора.
    /// </summary>
    /// <param name="authorId">
    /// Идентификатор автора.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Статус выполнения операции.
    /// </returns>
    [HttpDelete]
    //авторизация
    public async Task<IActionResult> DeleteAuthorAsync(
        Transport.Models.Ids.Author authorId,
        CancellationToken token)
    {
        try
        {
            var jwtToken = AuthorizationHelper.GetJwtTokenFromHandlers(Request.Headers);

            await _deleteAuthorProcessor.ProcessAsync(
                new(new(authorId.AuthorId)),
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

    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyWithPagination<AuthorSorting>, IList<Author>> _getAuthorsProcessor;
    private readonly IRequestProcessorWithoutAuthorize<RequestGetManyByLastNameWithPagination<AuthorSorting>, IList<Author>> _getAuthorsByLastNameProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestAddEntity<AuthorWithoutId>> _addAuthorProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestDeleteEntityById<Author, Author>> _deleteAuthorProcessor;
    private readonly IRequestProcessorWithAuthorize<RequestUpdateEntity<Author>> _updateAuthorProcessor;
}
