using System.Security.Cryptography;

namespace Models.Requests;

/// <summary>
/// Запрос на получение объекта по идентификатору.
/// </summary>
/// <typeparam name="T">
/// Тип, по идентификатору которого будет происходить поиск.
/// </typeparam>
public record RequestGetOneById<T> : RequestBase
{
    /// <summary>
    /// Идендификатор.
    /// </summary>
    public Id<T> Id { get; }

    /// <summary>
    /// Создаёт объект типа <see cref="RequestGetOneById{T}"/>
    /// </summary>
    /// <param name="id">
    /// Идендификатор.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="id"/> равен <see langword="null"/>.
    /// </exception>
    public RequestGetOneById(Id<T> id)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
}
