namespace Models.Exceptions;

/// <summary>
/// Исключение, выбрасываемое при ошибке авторизации.
/// </summary>
public class AuthorizationException : Exception
{
    /// <summary>
    /// Создаёт объект <see cref="AuthorizationException"/>.
    /// </summary>
    public AuthorizationException()
        : base()
    {
    }

    /// <summary>
    /// Создаёт объект <see cref="AuthorizationException"/>.
    /// </summary>
    /// <param name="message">
    /// Сообщение исключения.
    /// </param>
    public AuthorizationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Создаёт объект <see cref="AuthorizationException"/>.
    /// </summary>
    /// <param name="message">
    /// Сообщение исключения.
    /// </param>
    /// <param name="inner">
    /// Внутреннее исключение.
    /// </param>
    public AuthorizationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}
