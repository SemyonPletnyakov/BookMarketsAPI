namespace Models.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при недостаточных правах для операции.
/// </summary>
public class NotEnoughRightsException : Exception
{
    /// <summary>
    /// Создаёт объект <see cref="NotEnoughRightsException"/>.
    /// </summary>
    public NotEnoughRightsException()
        : base()
    {
    }

    /// <summary>
    /// Создаёт объект <see cref="NotEnoughRightsException"/>.
    /// </summary>
    /// <param name="message">
    /// Сообщение исключения.
    /// </param>
    public NotEnoughRightsException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Создаёт объект <see cref="NotEnoughRightsException"/>.
    /// </summary>
    /// <param name="message">
    /// Сообщение исключения.
    /// </param>
    /// <param name="inner">
    /// Внутреннее исключение.
    /// </param>
    public NotEnoughRightsException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
