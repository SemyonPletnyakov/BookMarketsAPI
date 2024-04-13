namespace Models.ForCreate;

/// <summary>
/// Адрес.
/// </summary>
public sealed record Address
{
    /// <summary>
    /// Страна.
    /// </summary>
    public string Country { get; }

    /// <summary>
    /// Номер региона внутри страны.
    /// </summary>
    public int RegionNumber { get; }

    /// <summary>
    /// Название региона.
    /// </summary>
    public string RegionName { get; }

    /// <summary>
    /// Город.
    /// </summary>
    public string City { get; }

    /// <summary>
    /// Район.
    /// </summary>
    public string? District { get; }

    /// <summary>
    /// Улица.
    /// </summary>
    public string Street { get; }

    /// <summary>
    /// Дом.
    /// </summary>
    public string House { get; }

    /// <summary>
    /// Помещение внутри дома.
    /// </summary>
    public string Room { get; }

    /// <summary>
    /// Создаёт объект типа Address.
    /// </summary>
    /// <param name="country">
    /// Страна.
    /// </param>
    /// <param name="regionNumber">
    /// Номер региона внутри страны.
    /// </param>
    /// <param name="regionName">
    /// Название региона.
    /// </param>
    /// <param name="city">
    /// Город.
    /// </param>
    /// <param name="district">
    /// Район.
    /// </param>
    /// <param name="street">
    /// Улица.
    /// </param>
    /// <param name="house">
    /// Дом.
    /// </param>
    /// <param name="room">
    /// Помещение внутри дома.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="country"/>,  <paramref name="regionName"/>, 
    /// <paramref name="city"/>, <paramref name="street"/>, 
    /// <paramref name="house"/> или <paramref name="room"/> 
    /// </exception>
    /// <exception cref="ArgumentException">
    /// Если <paramref name="country"/>,  <paramref name="regionName"/>, 
    /// <paramref name="city"/>, <paramref name="street"/>, 
    /// <paramref name="house"/> или <paramref name="room"/> 
    /// пустой или состоит из пробелов..
    /// </exception>
    public Address(
        string country, 
        int regionNumber, 
        string regionName, 
        string city, 
        string? district, 
        string street, 
        string house, 
        string room)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(country, nameof(country));
        ArgumentException.ThrowIfNullOrWhiteSpace(regionName, nameof(regionName));
        ArgumentException.ThrowIfNullOrWhiteSpace(city, nameof(city));
        ArgumentException.ThrowIfNullOrWhiteSpace(street, nameof(street));
        ArgumentException.ThrowIfNullOrWhiteSpace(house, nameof(house));
        ArgumentException.ThrowIfNullOrWhiteSpace(room, nameof(room));

        Country = country;
        RegionNumber = regionNumber;
        RegionName = regionName;
        City = city;
        District = district;
        Street = street;
        House = house;
        Room = room;
    }
}
