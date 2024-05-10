using Models.Pagination;
using Models.Pagination.Sorting;

namespace Storage.Getters;

public static class CustomersWithPaginationGetter
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
    public static IQueryable<Models.Customer> WithPaginationInfo(
        this IQueryable<Models.Customer> queryable,
        PaginationInfo<CustomerSorting> paginationInfo)
        => paginationInfo.OrderBy switch
        {
            CustomerSorting.EmailAsc =>
                queryable.OrderBy(q => q.Email)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            CustomerSorting.EmailDesc =>
                queryable.OrderByDescending(q => q.Email)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            CustomerSorting.PhoneAsc =>
                queryable.OrderBy(q => q.Phone)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            CustomerSorting.PhoneDesc =>
                queryable.OrderByDescending(q => q.Phone)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            CustomerSorting.BirthDateAsc =>
                queryable.OrderBy(q => q.BirthDate)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            CustomerSorting.BirthDateDesc =>
                queryable.OrderByDescending(q => q.BirthDate)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            CustomerSorting.FullNameAsc =>
                queryable.OrderBy(q => q.LastName)
                    .ThenBy(q => q.FirstName)
                    .ThenBy(q => q.Patronymic)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            CustomerSorting.FullNameDesc =>
                queryable.OrderByDescending(q => q.LastName)
                    .ThenBy(q => q.FirstName)
                    .ThenBy(q => q.Patronymic)
                    .Skip(paginationInfo.PageSize * (paginationInfo.PageNumber - 1))
                    .Take(paginationInfo.PageSize),

            _ => throw new NotSupportedException()
        };
}
