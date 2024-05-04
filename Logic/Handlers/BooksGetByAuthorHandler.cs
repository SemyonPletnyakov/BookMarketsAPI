using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using SimpleBook = Models.SimpleEntities.Book;

namespace Logic.Handlers;

public sealed class BooksGetByAuthorHandler :
    IRequestHandler<
        RequestGetManyByIdWithPagination<Author, BookSorting>,
        IList<SimpleBook>>
{
    /// <summary>
    /// Создаёт объект <see cref="BooksGetByAuthorHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public BooksGetByAuthorHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<SimpleBook>> HandleAsync(
        RequestGetManyByIdWithPagination<Author, BookSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Books.GetBooksByAuthorAsync(
            request.Id,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
