namespace Models.FullEntities;

/// <summary>
/// Покупатель.
/// </summary>
public sealed class Customer
{
    /// <summary>
    /// Идентификатор покупателя.
    /// </summary>
    public Id<FullEntities.Customer> CustomerId { get; set; }

    /// <summary>
    /// Полное имя.
    /// </summary>
    public FullName FullName { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public Phone? Phone { get; set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public Email Email { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Customer"/>
    /// </summary>
    /// <param name="customerId">
    /// Идентификатор работника.
    /// </param>
    /// <param name="fullName">
    /// Полное имя.
    /// </param>
    /// <param name="birthDate">
    /// Дата рождения.
    /// </param>
    /// <param name="phone">
    /// Номер телефона.
    /// </param>
    /// <param name="email">
    /// Электронная почта.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="customerId"/>, <paramref name="fullName"/> или
    /// <paramref name="email"/> равен <see langword="null"/>.
    /// </exception>
    public Customer(
        Id<FullEntities.Customer> customerId,
        FullName fullName,
        DateOnly? birthDate,
        Phone? phone,
        Email email)
    {
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        BirthDate = birthDate;
        Phone = phone;
        Email = email ?? throw new ArgumentNullException(nameof(email));
    }
}
