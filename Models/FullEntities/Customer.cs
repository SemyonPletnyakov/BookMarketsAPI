namespace Models.FullEntities;

/// <summary>
/// Покупатель.
/// </summary>
public sealed class Customer
{
    /// <summary>
    /// Идентификатор покупателя.
    /// </summary>
    public Id<Employee> CustomerId { get; set; }

    /// <summary>
    /// Полное имя.
    /// </summary>
    public FullName? FullName { get; set; }

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
    /// Пароль от учётной записи.
    /// </summary>
    public Password Password { get; set; }

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
    /// <param name="password">
    /// Пароль от учётной записи.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="customerId"/>, <paramref name="email"/> 
    /// или <paramref name="password"/> равен <see langword="null"/>.
    /// </exception>
    public Customer(
        Id<Employee> customerId,
        FullName? fullName,
        DateOnly? birthDate,
        Phone? phone,
        Email email,
        Password password)
    {
        CustomerId = customerId ?? throw new ArgumentNullException(nameof(customerId));
        FullName = fullName;
        BirthDate = birthDate;
        Phone = phone;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}
