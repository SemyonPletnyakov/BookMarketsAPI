namespace Models.FullEntities;

/// <summary>
/// Магазин.
/// </summary>
public sealed class Shop
{
    /// <summary>
    /// Идентификатор магазина.
    /// </summary>
    public Id<Shop> ShopId { get; set; }

    /// <summary>
    /// Название магазина.
    /// </summary>
    public Name<Shop>? Name { get; set; }

    /// <summary>
    /// Время открытия.
    /// </summary>
    public TimeOnly? OpeningTime { get; set; }

    /// <summary>
    /// Время закрытия.
    /// </summary>
    public TimeOnly? ClosingTime { get; set; }

    /// <summary>
    /// Адрес.
    /// </summary>
    public Address Address { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Shop"/>.
    /// </summary>
    /// <param name="shopId">
    /// Идентификатор магазина.
    /// </param>
    /// <param name="name">
    /// Название магазина.
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
    public Shop(
        Id<Shop> shopId,
        Name<Shop>? name,
        TimeOnly? openingTime,
        TimeOnly? closingTime,
        Address address)
    {
        ShopId = shopId ?? throw new ArgumentNullException(nameof(shopId));
        Name = name;
        OpeningTime = openingTime;
        ClosingTime = closingTime;
        Address = address ?? throw new ArgumentNullException(nameof(address));
    }
}
