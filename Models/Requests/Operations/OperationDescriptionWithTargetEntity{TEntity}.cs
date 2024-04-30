using Models.Requests.Operations.Types;

namespace Models.Requests.Operations;

/// <summary>
/// Описание операциии с идентификатором.
/// </summary>
/// <typeparam name="TEntity">
/// Тип сущности.
/// </typeparam>
public sealed record OperationDescriptionWithTargetEntity<TEntity> : OperationDescriprion
{
    /// <summary>
    /// Сущность.
    /// </summary>
    public TEntity Entity { get; }

    /// <summary>
    /// Создаёт объект <see cref="OperationDescriptionWithTargetEntity{TEntity}"/>.
    /// </summary>
    /// <param name="operationType">
    /// Тип операции.
    /// </param>
    /// <param name="entityType">
    /// Тип сущности.
    /// </param>
    /// <param name="entity">
    /// Сущность.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="entity"/> равен <see langword="null"/>.
    /// </exception>
    public OperationDescriptionWithTargetEntity(
        OperationType operationType, 
        EntityType entityType, 
        TEntity entity)
        : base(operationType, entityType)
    {
        Entity = entity ?? throw new ArgumentNullException(nameof(entity));
    }
}
