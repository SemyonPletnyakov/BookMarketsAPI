using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос на удаление сущности.
/// </summary>
/// <typeparam name="TId">
/// Тип удаляемой сущности.
/// </typeparam>
/// <typeparam name="TRemovedEntity">
/// Тип удаляемой сущности.
/// </typeparam>
public record RequestDeleteEntityById<TId, TRemovedEntity> : RequestBase
{
    /// <summary>
    /// Идентификатор сущности, которая должна быть удалена.
    /// </summary>
    public Id<TId> EntityId { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestDeleteEntityById{T}"/>.
    /// </summary>
    /// <param name="entityId">
    /// Идентификатор сущности, которая должна быть удалена.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="entityId"/> равен <see langword="null"/>.
    /// </exception>
    public RequestDeleteEntityById(Id<TId> entityId)
        : base(
            new OperationDescriprion(
                OperationType.Delete,
                GetEntityTypeByEntity<TRemovedEntity>()))
    {
        EntityId = entityId ?? throw new ArgumentNullException(nameof(entityId));
    }
}
