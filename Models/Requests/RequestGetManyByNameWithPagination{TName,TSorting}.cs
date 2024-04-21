using Models.Pagination;

namespace Models.Requests;

/// <summary>
/// Запрос на поиск по фамилии с учётом пагинации.
/// </summary>
/// <typeparam name="TName">
/// Тип, по названию которого будет происходить поиск.
/// </typeparam>
/// <typeparam name="TSorting">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record RequestGetManyByNameWithPagination<TName, TSorting> : RequestBase
    where TSorting : Enum
{
    /// <summary>
    /// Название.
    /// </summary>
    public Name<TName> Name { get; }

    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public PaginationInfo<TSorting> PaginationInfo { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestGetManyByNameWithPagination{TName, TSorting}"/>.
    /// </summary>
    /// <param name="name">
    /// Фамилия.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public RequestGetManyByNameWithPagination(
        Name<TName> name,
        PaginationInfo<TSorting> paginationInfo)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));

        PaginationInfo = paginationInfo 
            ?? throw new ArgumentNullException(nameof(paginationInfo));
    }
}
