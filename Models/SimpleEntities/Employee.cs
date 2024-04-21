namespace Models.SimpleEntities;

/// <summary>
/// Работник.
/// </summary>
public sealed class Employee
{
    /// <summary>
    /// Идентификатор работника.
    /// </summary>
    public Id<FullEntities.Employee> EmployeeId { get; set; }

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
    public Email? Email { get; set; }

    /// <summary>
    /// Должность работника.
    /// </summary>
    public JobTitle JobTitle { get; set; }

    /// <summary>
    /// Логин от учётной записи.
    /// </summary>
    public Login Login { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Employee"/>.
    /// </summary>
    /// <param name="employeeId">
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
    /// <param name="jobTitle">
    /// Должность работника.
    /// </param>
    /// <param name="login">
    /// Логин от учётной записи.
    /// </param>
    /// <param name="password">
    /// Пароль от учётной записи.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="employeeId"/>, <paramref name="fullName"/>, 
    /// <paramref name="jobTitle"/>, <paramref name="login"/> 
    /// или <paramref name="password"/> равен <see langword="null"/>.
    /// </exception>
    public Employee(
        Id<FullEntities.Employee> employeeId,
        FullName fullName,
        DateOnly? birthDate,
        Phone? phone,
        Email? email,
        JobTitle jobTitle,
        Login login)
    {
        EmployeeId = employeeId ?? throw new ArgumentNullException(nameof(employeeId));
        FullName = fullName ?? throw new ArgumentNullException(nameof(fullName));
        BirthDate = birthDate;
        Phone = phone;
        Email = email;
        JobTitle = jobTitle ?? throw new ArgumentNullException(nameof(jobTitle));
        Login = login ?? throw new ArgumentNullException(nameof(login));
    }
}
