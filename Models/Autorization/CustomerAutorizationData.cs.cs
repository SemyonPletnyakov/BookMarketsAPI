using Models.FullEntities;

namespace Models.Autorization;

/// <summary>
/// Данные покупателя для JWT-токена.
/// </summary>
public sealed record CustomerAutorizationData
{
    /// <summary>
    /// Идентификатор покупателя.
    /// </summary>
    public Id<Customer> Id { get; }

    /// <summary>
    /// Электронная почта покупателя.
    /// </summary>
    public Email Email { get; }

    /// <summary>
    /// Создаёт объект <see cref="CustomerAutorizationData"/>.
    /// </summary>
    /// <param name="id">
    /// Идентификатор покупателя.
    /// </param>
    /// <param name="email">
    /// Электронная почта покупателя.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен null.
    /// </exception>
    public CustomerAutorizationData(Id<Customer> id, Email email)
    {
        Id = id ?? throw new ArgumentNullException(nameof(id));
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }
}
