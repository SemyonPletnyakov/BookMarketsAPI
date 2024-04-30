using Logic.Abstractions.Handlers;
using Models.Exceptions;
using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

using BookForUpdate = Models.ForUpdate.Book;

namespace Logic.Handler;

public sealed class BookUpdateHandler :
    IRequestHandler<RequestUpdateEntity<BookForUpdate>>
{
    /// <summary>
    /// Создаёт объект <see cref="BookUpdateHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public BookUpdateHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestUpdateEntity<BookForUpdate> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        if (request.Entity.AuthorId is not null)
        {
            await _unitOfWork.Books.UpdateAuthorForBookAsync(
                request.Entity.ProductId,
                request.Entity.AuthorId,
                token);
        }
        else
        {
            var book = await _unitOfWork.Books.GetBooksByProductIdAsync(
                request.Entity.ProductId,
                token);

            if (book is null)
            {
                throw new EntityNotFoundException("Книга не найдена");
            }
        }

        await _unitOfWork.Products.UpdateProductAsync(request.Entity, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
