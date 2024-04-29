using Models.Requests.BaseRequests;
using Models.Requests.Operations.Types;
using Models.Requests.Operations;

namespace Models.Requests;

/// <summary>
/// Запрос на получение объекта по электронной почте.
/// </summary>
/// <typeparam name="T">
/// Тип, по которому будет происходить поиск.
/// </typeparam>
public record RequestGetOneByEmail<T> : RequestBase
{
    /// <summary>
    /// Электронная почта.
    /// </summary>
    public Email Email { get; }

    /// <summary>
    /// Создаёт объект типа <see cref="RequestGetOneByEmail{T}"/>
    /// </summary>
    /// <param name="email">
    /// Электронная почта.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="email"/> равен <see langword="null"/>.
    /// </exception>
    public RequestGetOneByEmail(Email email)
        : base(
            new OperationDescriprion(
                OperationType.Get,
                GetEntityTypeByEntity<T>()))
    {
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }
}
