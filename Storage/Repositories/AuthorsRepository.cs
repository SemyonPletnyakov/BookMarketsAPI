using Microsoft.EntityFrameworkCore;

using Models;
using Models.FullEntities;
using Models.Pagination;
using Models.Pagination.Sorting;

using Storage.Abstractions.Repositories;
using Storage.Exceptions;
using Storage.Getters;

using AuthorWithoutId = Models.ForCreate.Author;

namespace Storage.Repositories;

/// <summary>
/// Репозиторий авторов.
/// </summary>
public sealed class AuthorsRepository : IAuthorsRepository
{
    /// <summary>
    /// Создаёт объект <see cref="AuthorsRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public AuthorsRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IList<Author>> GetAuthorsAsync(
        PaginationInfo<AuthorSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Authors.WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(author =>
                new Author(
                    new(author.AuthorId),
                    new(author.LastName, author.FirstName, author.Patronymic),
                    author.BirthDate,
                    author.Countries))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<Author>> GetAuthorsByLastNameAsync(
        LastName lastName,
        PaginationInfo<AuthorSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(lastName);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Authors
            .Where(author => EF.Functions.Like(author.LastName, $"%{lastName.Value}%"))
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(author =>
                new Author(
                    new(author.AuthorId),
                    new(author.LastName, author.FirstName, author.Patronymic),
                    author.BirthDate,
                    author.Countries))
            .ToList();
    }

    /// <inheritdoc/>
    public void AddAuthor(AuthorWithoutId authorId)
    {
        ArgumentNullException.ThrowIfNull(authorId);

        _context.Authors.Add(
            new Models.Author
            {
                LastName = authorId.FullName.LastName,
                FirstName = authorId.FullName.FirstName,
                Patronymic = authorId.FullName.Patronymic,
                BirthDate = authorId.BirthDate,
                Countries = authorId.Countries?.ToList()
            });
    }

    /// <inheritdoc/>
    public async Task UpdateAuthorAsync(Author author, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(author);
        token.ThrowIfCancellationRequested();

        var contextAuthor = await _context.Authors
            .SingleOrDefaultAsync(a => a.AuthorId == author.AuthorId.Value, token);

        if (contextAuthor == null)
        {
            throw new EntityNotFoundException(
                $"Автор с Id = {author.AuthorId.Value} не найден.");
        }

        contextAuthor.LastName = author.FullName.LastName;
        contextAuthor.FirstName = author.FullName.FirstName;
        contextAuthor.Patronymic = author.FullName.Patronymic;
        contextAuthor.BirthDate = author.BirthDate;
        contextAuthor.Countries = author.Countries?.ToList();
    }

    /// <inheritdoc/>
    public async Task DeleteAuthorAsync(Id<Author> authorId, CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(authorId);
        token.ThrowIfCancellationRequested();

        var contextAuthor = await _context.Authors
            .SingleOrDefaultAsync(a => a.AuthorId == authorId.Value, token);

        if (contextAuthor == null)
        {
            throw new EntityNotFoundException(
                $"Автор с Id = {authorId.Value} не найден.");
        }

        _context.Authors.Remove(contextAuthor);
    }

    private readonly ApplicationContext _context;
}
