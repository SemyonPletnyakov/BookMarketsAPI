namespace Models.Requests;

/// <summary>
/// Запрос на получение объекта по логину.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому будет происходить поиск.
/// </typeparam>
public record RequestGetOneByLogin<T> : RequestBase
{
    /// <summary>
    /// Логин.
    /// </summary>
    public Login Login { get; }

    /// <summary>
    /// Создаёт объект типа <see cref="RequestGetOneByLogin{T}"/>
    /// </summary>
    /// <param name="login">
    /// Логин.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="login"/> равен <see langword="null"/>.
    /// </exception>
    public RequestGetOneByLogin(Login login)
    {
        Login = login ?? throw new ArgumentNullException(nameof(login));
    }
}
