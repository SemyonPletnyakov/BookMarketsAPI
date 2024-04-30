using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class AuthorsGetHandler : 
    IRequestHandler<
        RequestGetManyWithPagination<AuthorSorting>, 
        IList<Author>>
{
    /// <summary>
    /// Создаёт объект <see cref="AuthorsGetHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public AuthorsGetHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Author>> HandleAsync(
        RequestGetManyWithPagination<AuthorSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Authors.GetAuthorsAsync(request.PaginationInfo, token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
