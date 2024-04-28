using Logic.Abstractions.Handlers;

using Models.Pagination.Sorting;
using Models.Requests;
using Models.SimpleEntities;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class BooksGetByKeyWordsHandler :
    IRequestHandler<
        RequestGetManyByKeyWordsWithPagination<BookSorting>,
        Task<IList<Book>>>
{
    /// <summary>
    /// Создаёт объект <see cref="BooksGetByKeyWordsHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public BooksGetByKeyWordsHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Book>> HandleAsync(
        RequestGetManyByKeyWordsWithPagination<BookSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Books.GetBooksByKeyWordsAsync(
            request.KeyWords,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
