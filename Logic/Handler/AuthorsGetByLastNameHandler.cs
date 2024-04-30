using Logic.Abstractions.Handlers;

using Models.FullEntities;
using Models.Pagination.Sorting;
using Models.Requests;

using Storage.Abstractions;

namespace Logic.Handler;

public sealed class AuthorsGetByLastNameHandler :
    IRequestHandler<
        RequestGetManyByLastNameWithPagination<AuthorSorting>, 
        IList<Author>>
{
    /// <summary>
    /// Создаёт объект <see cref="AuthorsGetByLastNameHandler"/>.
    /// </summary>
    /// <param name="unitOfWork">
    /// Единица работы с репозиторием.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="unitOfWork"/> равен <see langword="null"/>
    /// </exception>
    public AuthorsGetByLastNameHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    /// <inheritdoc/>
    public Task<IList<Author>> HandleAsync(
        RequestGetManyByLastNameWithPagination<AuthorSorting> request,
        CancellationToken token)
    {
        token.ThrowIfCancellationRequested();
        ArgumentNullException.ThrowIfNull(request);

        return _unitOfWork.Authors.GetAuthorsByLastNameAsync(
            request.LastName,
            request.PaginationInfo, 
            token);
    }

    private readonly IUnitOfWork _unitOfWork;
}
