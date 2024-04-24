using Models.Pagination;
using Models.Pagination.Sorting;

namespace Storage.Getters;

internal static class AuthorsWithPaginationGetter
{
    /// <summary>
    /// Применить правила пагинации.
    /// </summary>
    /// <param name="queryable">
    /// Запрос к БД.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <returns>
    /// Запрос с правилами пагинации.
    /// </returns>
    /// <exception cref="NotSupportedException">
    /// Если тип сортировки не поддерживается.
    /// </exception>
    public static IQueryable<Models.Author> WithPaginationInfo(
        this IQueryable<Models.Author> queryable,
        PaginationInfo<AuthorSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            AuthorSorting.BirthDateAsc =>
                queryable.OrderBy(q => q.BirthDate)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            AuthorSorting.BirthDateDesc =>
                queryable.OrderByDescending(q => q.BirthDate)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            AuthorSorting.FullNameAsc =>
                queryable.OrderBy(q => q.LastName)
                    .ThenBy(q => q.FirstName)
                    .ThenBy(q => q.Patronymic)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            AuthorSorting.FullNameDesc =>
                queryable.OrderByDescending(q => q.LastName)
                    .ThenBy(q => q.FirstName)
                    .ThenBy(q => q.Patronymic)
                    .Skip(paginationInfo.PageSize * paginationInfo.PageNumber)
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };
}
