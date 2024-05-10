using Models.Pagination;
using Models.Pagination.Sorting;

namespace Storage.Getters;

internal static class BooksWithPaginationGetter
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
    public static IQueryable<Models.Book> WithPaginationInfo(
        this IQueryable<Models.Book> queryable,
        PaginationInfo<BookSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            BookSorting.NameAsc =>
                queryable.OrderBy(q => q.Product.Name)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            BookSorting.NameDesc =>
                queryable.OrderByDescending(q => q.Product.Name)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            BookSorting.PriceAsc =>
                queryable.OrderBy(q => q.Product.Price)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            BookSorting.PriceDesc =>
                queryable.OrderByDescending(q => q.Product.Price)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            BookSorting.AuthorFullNameAsc =>
                queryable.OrderBy(q => q.Author.LastName)
                    .ThenBy(q => q.Author.FirstName)
                    .ThenBy(q => q.Author.Patronymic)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            BookSorting.AuthorFullNameDesc =>
                queryable.OrderByDescending(q => q.Author.LastName)
                    .ThenBy(q => q.Author.FirstName)
                    .ThenBy(q => q.Author.Patronymic)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };
}
