using System.Xml.Linq;

using Microsoft.EntityFrameworkCore;

using Models;
using Models.Exceptions;
using Models.FullEntities;
using Models.Pagination;
using Models.Pagination.Sorting;

using Storage.Abstractions.Repositories;
using Storage.Getters;

using BookWithoutId = Models.ForCreate.Book;
using SimpleBook = Models.SimpleEntities.Book;

namespace Storage.Repositories;

/// <summary>
/// Репозиторий книг.
/// </summary>
public sealed class BooksRepository : IBooksRepository
{
    /// <summary>
    /// Создаёт объект <see cref="BooksRepository"/>.
    /// </summary>
    /// <param name="context">
    /// Контекст приложения.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="context"/> равен <see langword="null"/>.
    /// </exception>
    public BooksRepository(ApplicationContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    /// <inheritdoc/>
    public async Task<IList<SimpleBook>> GetBooksAsync(
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Books.Include(b => b.Product)
            .Include(b => b.Author)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(b =>
                new SimpleBook(
                    new(b.ProductId),
                    new(b.Product.Name),
                    new(b.Product.Price),
                    b.Product.KeyWords?.ToHashSet(),
                    b.Author is null
                        ? null
                        : new(
                            new(b.Author.AuthorId),
                            new(b.Author.LastName, b.Author.FirstName, b.Author.Patronymic),
                            b.Author.BirthDate,
                            b.Author.Countries)))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<SimpleBook>> GetBooksByNameAsync(
        Name<Product> name,
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(name);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Books.Include(b => b.Product)
            .Include(b => b.Author)
            .Where(b => EF.Functions.Like(b.Product.Name, $"%{name.Value}%"))
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(b =>
                new SimpleBook(
                    new(b.ProductId),
                    new(b.Product.Name),
                    new(b.Product.Price),
                    b.Product.KeyWords?.ToHashSet(),
                    b.Author is null
                        ? null
                        : new(
                            new(b.Author.AuthorId),
                            new(b.Author.LastName, b.Author.FirstName, b.Author.Patronymic),
                            b.Author.BirthDate,
                            b.Author.Countries)))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<SimpleBook>> GetBooksByAuthorAsync(
        Id<Author> authorId,
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(authorId);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Books.Include(b => b.Product)
            .Include(b => b.Author)
            .Where(b => b.AuthorId == authorId.Value)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(b =>
                new SimpleBook(
                    new(b.ProductId),
                    new(b.Product.Name),
                    new(b.Product.Price),
                    b.Product.KeyWords?.ToHashSet(),
                    b.Author is null
                        ? null
                        : new(
                            new(b.Author.AuthorId),
                            new(b.Author.LastName, b.Author.FirstName, b.Author.Patronymic),
                            b.Author.BirthDate,
                            b.Author.Countries)))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<SimpleBook>> GetBooksByKeyWordsAsync(
        IReadOnlyCollection<string> keyWords,
        PaginationInfo<BookSorting> paginationInfo,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(keyWords);
        ArgumentNullException.ThrowIfNull(paginationInfo);
        token.ThrowIfCancellationRequested();

        return (await _context.Books.Include(b => b.Product)
            .Include(b => b.Author)
            .Where(b => b.Product.KeyWords != null
                && b.Product.KeyWords.Count(kw => keyWords.Contains(kw)) == keyWords.Count)
            .WithPaginationInfo(paginationInfo)
            .ToArrayAsync(token))
            .Select(b =>
                new SimpleBook(
                    new(b.ProductId),
                    new(b.Product.Name),
                    new(b.Product.Price),
                    b.Product.KeyWords?.ToHashSet(),
                    b.Author is null
                        ? null
                        : new(
                            new(b.Author.AuthorId),
                            new(b.Author.LastName, b.Author.FirstName, b.Author.Patronymic),
                            b.Author.BirthDate,
                            b.Author.Countries)))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<Book> GetBooksByProductIdAsync(
        Id<Product> productId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(productId);
        token.ThrowIfCancellationRequested();

        ArgumentNullException.ThrowIfNull(productId);
        token.ThrowIfCancellationRequested();

        var book = await _context.Books.Include(b => b.Product)
            .Include(b => b.Author)
            .SingleOrDefaultAsync(p => p.ProductId == productId.Value, token);

        if (book is null)
        {
            throw new EntityNotFoundException(
                $"Книга с Id = {productId.Value} не найден.");
        }

        return new Book(
            new(book.ProductId),
            new(book.Product.Name),
            book.Product.Description == null
                ? null
                : new(book.Product.Description),
            new(book.Product.Price),
            book.Product.KeyWords?.ToHashSet(),
            book.Author is null
                ? null
                : new(
                    new(book.Author.AuthorId),
                    new(book.Author.LastName, book.Author.FirstName, book.Author.Patronymic),
                    book.Author.BirthDate,
                    book.Author.Countries));
    }

    /// <inheritdoc/>
    public void AddBook(BookWithoutId book)
    {
        ArgumentNullException.ThrowIfNull(book);

        _context.Books.Add(
            new Models.Book
            {
                Product = new Models.Product
                {
                    Name = book.Name.Value,
                    Description = book.Description?.Value,
                    Price = book.Price.Value,
                    KeyWords = book.KeyWords?.ToList()
                },
                AuthorId = book.AuthorId?.Value
            });
    }

    /// <inheritdoc/>
    public async Task AddProductInBooksAsync(
        Id<Product> productId,
        Id<Author>? authorId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(productId);
        token.ThrowIfCancellationRequested();

        var book = await _context.Books
            .SingleOrDefaultAsync(p => p.ProductId == productId.Value, token);

        if (book is not null)
        {
            throw new EntityNotFoundException(
                $"Товар с Id = {productId.Value} уже является книгой.");
        }

        _context.Books.Add(
            new Models.Book
            {
                ProductId = productId.Value,
                AuthorId = authorId?.Value
            });
    }

    /// <inheritdoc/>
    public async Task UpdateAuthorForBookAsync(
        Id<Product> productId,
        Id<Author> authorId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(productId);
        ArgumentNullException.ThrowIfNull(authorId);
        token.ThrowIfCancellationRequested();

        var book = await _context.Books
            .SingleOrDefaultAsync(p => p.ProductId == productId.Value, token);

        if (book is null)
        {
            throw new EntityNotFoundException(
                $"Товар с Id = {productId.Value} не является книгой.");
        }

        book.AuthorId = authorId.Value;
    }

    /// <inheritdoc/>
    public async Task RemoveProductFromBooksAsync(
        Id<Product> productId,
        CancellationToken token)
    {
        ArgumentNullException.ThrowIfNull(productId);
        token.ThrowIfCancellationRequested();

        var book = await _context.Books
            .SingleOrDefaultAsync(p => p.ProductId == productId.Value, token);

        if (book is null)
        {
            throw new EntityNotFoundException(
                $"Товар с Id = {productId.Value} не является книгой.");
        }
    }

    private readonly ApplicationContext _context;
}
