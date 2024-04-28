using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class AuthorDeleteHandler :
    IRequestHandler<RequestDeleteEntityById<Author, Author>, Task>
{
    /// <summary>
    /// Создаёт объект <see cref="AuthorDeleteHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public AuthorDeleteHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public async Task HandleAsync(
        RequestDeleteEntityById<Author, Author> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        await _unitOfWork.Authors.DeleteAuthorAsync(request.EntityId, token);

        await _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
