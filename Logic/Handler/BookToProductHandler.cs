using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class BookToProductHandler :
    IRequestHandler<RequestRemoveProductFromBooks>
{
    /// <summary>
    /// Создаёт объект <see cref="BookToProductHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public BookToProductHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestRemoveProductFromBooks request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Books.RemoveProductFromBooksAsync(request.ProductId, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
