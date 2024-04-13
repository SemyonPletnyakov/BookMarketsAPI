namespace Storage.Models;

/// <summary>
/// Сущность, показывающая склад, на котором работает сотрудник.
/// </summary>
public sealed class LinksEmployeeAndWarehouse
{
    /// <summary>
    /// Идентификатор работника.
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// Работник.
    /// </summary>
    public Employee Employee { get; set; }

    /// <summary>
    /// Индентификатор склада.
    /// </summary>
    public int WarehouseId { get; set; }

    /// <summary>
    /// Склад, на котором работает сотрудник.
    /// </summary>
    public Warehouse Warehouse { get; set; }
}
