using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

using SimpleBook = Models.SimpleEntities.Book;

namespace Logic.Handler;

public sealed class BooksGetByNameHandler :
    IRequestHandler<
        RequestGetManyByNameWithPagination<Product, BookSorting>,
        Task<IList<SimpleBook>>>
{
    /// <summary>
    /// Создаёт объект <see cref="BooksGetByNameHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public BooksGetByNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<SimpleBook>> HandleAsync(
        RequestGetManyByNameWithPagination<Product, BookSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Books.GetBooksByNameAsync(
            request.Name,
            request.PaginationInfo,
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
