using Models;
using Models.Common;
using Models.FullEntities;

using AuthorWithoutId = Models.ForCreate.Author;

namespace Storage.Abstractions.Repositories;

public interface IAuthorsRepository
{
    public Task<IList<Author>> GetAuthorsAsync(PagginationInfo pagginationInfo, CancellationToken token);
    public Task AddAuthorAsync(AuthorWithoutId author, CancellationToken token);
    public Task UpdateAuthorAsync(Author author, CancellationToken token);
    public Task DeleteAuthorAsync(Id<Author>, CancellationToken token);
}
