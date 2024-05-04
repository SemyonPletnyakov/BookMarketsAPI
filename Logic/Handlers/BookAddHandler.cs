using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using BookWithoutId = Models.ForCreate.Book;

namespace Logic.Handlers;

public sealed class BookAddHandler :
    IRequestHandler<RequestAddEntity<BookWithoutId>>
{
    /// <summary>
    /// Создаёт объект <see cref="BookAddHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public BookAddHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestAddEntity<BookWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        var productId = 
            await _unitOfWork.Products.AddProductAsync(request.Entity, token);

        await _unitOfWork.Books.AddProductInBooksAsync(
            productId, 
            request.Entity.AuthorId, 
            token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
