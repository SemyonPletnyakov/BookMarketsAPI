using Logic.Abstractions.Handlers;

using Models.Requests;

using Storage.Abstractions;

using AuthorWithoutId = Models.ForCreate.Author;

namespace Logic.Handler;

public sealed class AuthorAddHandler :
    IRequestHandler<RequestAddEntity<AuthorWithoutId>>
{
    /// <summary>
    /// Создаёт объект <see cref="AuthorAddHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public AuthorAddHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task HandleAsync(
        RequestAddEntity<AuthorWithoutId> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        _unitOfWork.Authors.AddAuthor(request.Entity);

        return _unitOfWork.SaveChangesAsync(token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
