using Models.Pagination;
using Models.Pagination.Sorting;

namespace Models.Requests;

/// <summary>
/// Запрос на получение значений с учётом пагинации.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record RequestGetManyWithPagination<T> : RequestBase
{
    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public PaginationInfo<AuthorSorting> PaginationInfo { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestGetManyWithPagination{T}"/>.
    /// </summary>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="paginationInfo"/> равено <see langword="null"/>.
    /// </exception>
    public RequestGetManyWithPagination(PaginationInfo<AuthorSorting> paginationInfo)
    {
        PaginationInfo = paginationInfo 
            ?? throw new ArgumentNullException(nameof(paginationInfo));
    }    
}
