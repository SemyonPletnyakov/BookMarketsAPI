using Logic.Abstractions.Handlers;

using Models.Pagination.Sorting;
using Models.Requests;
using Models.SimpleEntities;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class BooksGetHandler :
    IRequestHandler<
        RequestGetManyWithPagination<BookSorting>,
        Task<IList<Book>>>
{
    /// <summary>
    /// Создаёт объект <see cref="BooksGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public BooksGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Book>> HandleAsync(
        RequestGetManyWithPagination<BookSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Books.GetBooksAsync(request.PaginationInfo, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
