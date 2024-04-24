namespace Storage.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при ненахождении сущности.
/// </summary>
public class EntityNotFoundException : Exception
{
    /// <summary>
    /// Создаёт объект <see cref="EntityNotFoundException"/>.
    /// </summary>
    public EntityNotFoundException()
        : base() 
    { 
    }

    /// <summary>
    /// Создаёт объект <see cref="EntityNotFoundException"/>.
    /// </summary>
    /// <param name="message">
    /// Сообщение исключения.
    /// </param>
    public EntityNotFoundException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Создаёт объект <see cref="EntityNotFoundException"/>.
    /// </summary>
    /// <param name="message">
    /// Сообщение исключения.
    /// </param>
    /// <param name="inner">
    /// Внутреннее исключение.
    /// </param>
    public EntityNotFoundException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
