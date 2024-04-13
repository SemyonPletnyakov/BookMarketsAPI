using Models;
using Models.Pagination;
using Models.FullEntities;

using AuthorWithoutId = Models.ForCreate.Author;
using Models.Pagination.Sorting;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория авторов.
/// </summary>
public interface IAuthorsRepository
{
    /// <summary>
    /// Получить часть авторов в соотвествии с пагинацией.
    /// </summary>
    /// <param name="pagginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка авторов.
    /// </returns>
    public Task<IList<Author>> GetAuthorsAsync(
        PaginationInfo<AuthorSorting> pagginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск авторов по фамилии в соотвествии с пагинацией.
    /// </summary>
    /// <param name="lastName">
    /// Фамилия.
    /// </param>
    /// <param name="pagginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка авторов.
    /// </returns>
    public Task<IList<Author>> GetAuthorsByLastNameAsync(
        string lastName,
        PaginationInfo<AuthorSorting> pagginationInfo,
        CancellationToken token);

    /// <summary>
    /// Добавление автора.
    /// </summary>
    /// <param name="author">
    /// Автор.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task AddAuthorAsync(AuthorWithoutId author, CancellationToken token);

    /// <summary>
    /// Изменение автора.
    /// </summary>
    /// <param name="author">
    /// Автор.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task UpdateAuthorAsync(Author author, CancellationToken token);

    /// <summary>
    /// Удаление автора.
    /// </summary>
    /// <param name="authorId">
    /// Идентификатор автора.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача для асинхронного ожидания.
    /// </returns>
    public Task DeleteAuthorAsync(Id<Author> authorId, CancellationToken token);
}
