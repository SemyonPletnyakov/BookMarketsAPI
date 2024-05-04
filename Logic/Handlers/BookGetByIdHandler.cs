using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handlers;

public sealed class BookGetByIdHandler :
    IRequestHandler<
        RequestGetOneById<Product, Book>,
        Book>
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
    public BookGetByIdHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<Book> HandleAsync(
        RequestGetOneById<Product, Book> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Books.GetBooksByProductIdAsync(request.Id, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
