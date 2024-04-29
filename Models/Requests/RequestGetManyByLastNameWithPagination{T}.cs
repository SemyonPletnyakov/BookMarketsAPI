using Models.Pagination.Sorting;
using Models.Pagination;
using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос на поиск по фамилии с учётом пагинации.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record RequestGetManyByLastNameWithPagination<T> : RequestBase
    where T : Enum
{
    /// <summary>
    /// Фамилия.
    /// </summary>
    public LastName LastName { get; }

    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public PaginationInfo<T> PaginationInfo { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestGetManyByLastNameWithPagination{T}"/>.
    /// </summary>
    /// <param name="lastName">
    /// Фамилия.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public RequestGetManyByLastNameWithPagination(
        LastName lastName, 
        PaginationInfo<T> paginationInfo)
        : base(
            new OperationDescriprion(
                OperationType.Get,
                GetEntityTypeBySortingType<T>()))
    {
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));

        PaginationInfo = paginationInfo 
            ?? throw new ArgumentNullException(nameof(paginationInfo));
    }
}
