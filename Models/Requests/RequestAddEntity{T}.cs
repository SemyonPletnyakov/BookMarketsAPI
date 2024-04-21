namespace Models.Requests;

/// <summary>
/// Запрос на добавление сущности.
/// </summary>
/// <typeparam name="T">
/// Тип добавляемой сущности.
/// </typeparam>
public record RequestAddEntity<T> : RequestBase
    where T : class
{
    /// <summary>
    /// Сущность, которая должна быть добавлена.
    /// </summary>
    public T Entity { get; }

    /// <summary>
    /// Создаёт объект <see cref="RequestAddEntity{T}"/>.
    /// </summary>
    /// <param name="entity">
    /// Сущность, которая должна быть добавлена.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="entity"/> равен <see langword="null"/>.
    /// </exception>
    public RequestAddEntity(T entity)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }
}
