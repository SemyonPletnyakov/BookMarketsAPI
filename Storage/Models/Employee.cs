namespace Storage.Models;

/// <summary>
/// Работник.
/// </summary>
public sealed class Employee
{
    /// <summary>
    /// Идентификатор работника.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public string LastName { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public string FirstName { get; set; }

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
    public string JobTitle { get; set; }

    /// <summary>
    /// Логин от учётной записи.
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Пароль от учётной записи.
    /// </summary>
    public string Password { get; set; }
}
