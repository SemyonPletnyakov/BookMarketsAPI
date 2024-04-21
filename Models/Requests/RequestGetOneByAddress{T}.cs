using Models.ForCreate;

namespace Models.Requests;

/// <summary>
/// Запрос на получение объекта по адресу.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому будет происходить поиск.
/// </typeparam>
public record RequestGetOneByAddress<T> : RequestBase
{
    /// <summary>
    /// Адрес.
    /// </summary>
    public Address Address { get; }

    /// <summary>
    /// Создаёт объект типа <see cref="RequestGetOneByAddress{T}"/>
    /// </summary>
    /// <param name="login">
    /// Адрес.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="login"/> равен <see langword="null"/>.
    /// </exception>
    public RequestGetOneByAddress(Address login)
    {
        Address = login ?? throw new ArgumentNullException(nameof(login));
    }
}
