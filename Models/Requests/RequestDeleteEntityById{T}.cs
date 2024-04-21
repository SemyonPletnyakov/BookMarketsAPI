namespace Models.Requests;

/// <summary>
/// Запрос на удаление сущности.
/// </summary>
/// <typeparam name="T">
/// Тип удаляемой сущности.
/// </typeparam>
public record RequestDeleteEntityById<T> : RequestBase
{
    /// <summary>
    /// Идентификатор сущности, которая должна быть удалена.
    /// </summary>
    public Id<T> EntityId { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestDeleteEntityById{T}"/>.
    /// </summary>
    /// <param name="entityId">
    /// Идентификатор сущности, которая должна быть удалена.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="entityId"/> равен <see langword="null"/>.
    /// </exception>
    public RequestDeleteEntityById(Id<T> entityId)
    {
        EntityId = entityId ?? throw new ArgumentNullException(nameof(entityId));
    }
}
