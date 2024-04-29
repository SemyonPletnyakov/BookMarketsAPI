using Models.Pagination;
using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос поиск по идентификатору с учётом пагинации.
/// </summary>
/// <typeparam name="TId">
/// Тип, по идендификатору которого будет происходить поиск.
/// </typeparam>
/// <typeparam name="TSorting">
/// Тип, по которому происходит сортировка.
/// </typeparam>
public record RequestGetManyByIdWithPagination<TId, TSorting> : RequestBase
    where TSorting : Enum
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public Id<TId> Id { get; }

    /// <summary>
    /// Информация о пагинации.
    /// </summary>
    public PaginationInfo<TSorting> PaginationInfo { get; }

    /// <summary>
    /// Создаёт объект 
    /// <see cref="RequestGetManyByIdWithPagination{TId, TSorting}"/>.
    /// </summary>
    /// <param name="id">
    /// Идентификатор.
    /// </param>
    /// <param name="paginationInfo">
    /// Информация о пагинации.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public RequestGetManyByIdWithPagination(
        Id<TId> id,
        PaginationInfo<TSorting> paginationInfo)
        : base(
            new OperationDescriprion(
                OperationType.Get,
                GetEntityTypeBySortingType<TSorting>()))
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));

        PaginationInfo = paginationInfo
            ?? throw new ArgumentNullException(nameof(paginationInfo));
    }
}
