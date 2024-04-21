namespace Models.FullEntities;

/// <summary>
/// Склад.
/// </summary>
public sealed class Warehouse
{
    /// <summary>
    /// Идентификатор склада.
    /// </summary>
    public Id<Warehouse> WarehouseId { get; set; }

    /// <summary>
    /// Название склада.
    /// </summary>
    public Name<Warehouse>? Name { get; set; }

    /// <summary>
    /// Время открытия.
    /// </summary>
    public TimeOnly? OpeningTime { get; set; }

    /// <summary>
    /// Время закрытия.
    /// </summary>
    public TimeOnly? ClosingTime { get; set; }

    /// <summary>
    /// Идентификатор адреса.
    /// </summary>
    public int AddressId { get; set; }

    /// <summary>
    /// Адрес.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Warehouse"/>.
    /// </summary>
    /// <param name="warehouseId">
    /// Идентификатор склада.
    /// </param>
    /// <param name="name">
    /// Название склада.
    /// </param>
    /// <param name="openingTime">
    /// Время открытия.
    /// </param>
    /// <param name="closingTime">
    /// Время закрытия.
    /// </param>
    /// <param name="address">
    /// Адрес.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="shopId"/> или <paramref name="address"/> 
    /// равен <see langword="null"/>.
    /// </exception>
    public Warehouse(
        Id<Warehouse> warehouseId,
        Name<Warehouse>? name,
        TimeOnly? openingTime,
        TimeOnly? closingTime,
        Address address)
    {
        WarehouseId = warehouseId
            ?? throw new ArgumentNullException(nameof(warehouseId));

        Name = name;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }
}
