using Models;
using Models.Pagination;
using Models.FullEntities;
using Models.Pagination.Sorting;

using AuthorWithoutId = Models.ForCreate.Author;

namespace Storage.Abstractions.Repositories;

/// <summary>
/// Интерфейс репозитория авторов.
/// </summary>
public interface IAuthorsRepository
{
    /// <summary>
    /// Получить часть авторов в соотвествии с пагинацией.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка авторов.
    /// </returns>
    public Task<IList<Author>> GetAuthorsAsync(
        PaginationInfo<AuthorSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Поиск авторов по фамилии в соотвествии с пагинацией.
    /// </summary>
    /// <param name="lastName">
    /// Фамилия.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Часть списка авторов.
    /// </returns>
    public Task<IList<Author>> GetAuthorsByLastNameAsync(
        LastName lastName,
        PaginationInfo<AuthorSorting> paginationInfo,
        CancellationToken token);

    /// <summary>
    /// Добавление автора.
    /// </summary>
    /// <param name="authorId">
    /// Автор.
    /// </param>
    public void AddAuthor(AuthorWithoutId authorId);

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
