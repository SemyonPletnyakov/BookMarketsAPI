﻿namespace Transport.Models.ForUpdate;

/// <summary>
/// Покупатель.
/// </summary>
public sealed class Customer
{
    /// <summary>
    /// Идентификатор.
    /// </summary>
    public required int CustomerId { get; set; }

    /// <summary>
    /// Фамилия.
    /// </summary>
    public required string? LastName { get; set; }

    /// <summary>
    /// Имя.
    /// </summary>
    public required string? FirstName { get; set; }

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
    public required string Email { get; set; }
}
