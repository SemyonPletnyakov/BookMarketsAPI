using Models.Requests.Operations.Types;
using System.ComponentModel;

namespace Models.Requests.Operations;

/// <summary>
/// Описание операциии.
/// </summary>
public record OperationDescriprion
{
    /// <summary>
    /// Тип операции.
    /// </summary>
    public OperationType OperationType { get; }

    /// <summary>
    /// Тип сущности.
    /// </summary>
    public EntityType EntityType { get; }

    /// <summary>
    /// Создаёт объект <see cref="OperationDescriprion"/>.
    /// </summary>
    /// <param name="operationType">
    /// Тип операции.
    /// </param>
    /// <param name="entityType">
    /// Тип сущности.
    /// </param>
    /// <exception cref="InvalidEnumArgumentException">
    /// Если значения <paramref name="operationType"/> или <paramref name="entityType"/>
    /// не найдены в перечислениях.
    /// </exception>
    public OperationDescriprion(OperationType operationType, EntityType entityType)
    {
        if (!Enum.IsDefined<OperationType>(operationType))
        {
            throw new InvalidEnumArgumentException(
                nameof(operationType),
                (int)operationType,
                typeof(OperationType));
        }

        OperationType = operationType;

        if (!Enum.IsDefined<EntityType>(entityType))
        {
            throw new InvalidEnumArgumentException(
                nameof(entityType),
                (int)entityType,
                typeof(EntityType));
        }

        EntityType = entityType;
    }

}
