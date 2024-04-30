namespace Transport.Models.ForCreate;

/// <summary>
/// Работник.
/// </summary>
public sealed class Employee
{
    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string LastName { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public required string FirstName { get; set; }

    /// <summary>
    /// Отчество.
    /// </summary>
    public string? Patronymic { get; set; }

    /// <summary>
    /// Дата рождения.
    /// </summary>
    public DateOnly? BirthDate { get; set; }

    /// <summary>
    /// Номер телефона.
    /// </summary>
    public string? Phone { get; set; }

    /// <summary>
    /// Электронная почта.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Должность работника.
    /// </summary>
    public required string JobTitle { get; set; }

    /// <summary>
    /// Логин от учётной записи.
    /// </summary>
    public required string Login { get; set; }

    /// <summary>
    /// Пароль от учётной записи.
    /// </summary>
    public required string Password { get; set; }
}
