namespace Models.ForCreate;

/// <summary>
/// Покупатель.
/// </summary>
public sealed class Customer
{
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
    /// Пароль от учётной записи.
    /// </summary>
    public Password Password { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Customer"/>
    /// </summary>
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
    /// Если <paramref name="fullName"/>, <paramref name="email"/> 
    /// или <paramref name="password"/> равен <see cref="null"/>.
    /// </exception>
    public Customer(
        FullName fullName, 
        DateOnly? birthDate, 
        Phone? phone, 
        Email email, 
        Password password)
    {
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        BirthDate = birthDate;
        Phone = phone;
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}
