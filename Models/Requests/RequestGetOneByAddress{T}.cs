using Models.ForCreate;
using Models.Requests.BaseRequests;
using Models.Requests.Operations;
using Models.Requests.Operations.Types;

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
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="address"/> равен <see langword="null"/>.
    /// </exception>
    public RequestGetOneByAddress(Address address)
        : base(
            new OperationDescriprion(
                typeof(T) == typeof(Address)
                    ? OperationType.GetOrAdd
                    : OperationType.Get,
                GetEntityTypeByEntity<T>()))
    {
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }
}
