namespace Storage.Models;

/// <summary>
/// Сущность, показывающая магазин, в котором работает сотрудник.
/// </summary>
public sealed class LinksEmployeeAndShop
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
    /// Индентификатор магазина.
    /// </summary>
    public int ShopId { get; set; }

    /// <summary>
    /// Магазин, в котором работает сотрудник.
    /// </summary>
    public Shop Shop { get; set; }
}
