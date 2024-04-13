namespace Models.ForCreate;

/// <summary>
/// Товар.
/// </summary>
public class Product
{
    /// <summary>
    /// Название товара.
    /// </summary>
    public Name<Product> Name { get; set; }

    /// <summary>
    /// Описание.
    /// </summary>
    public Description? Description { get; set; }

    /// <summary>
    /// Цена.
    /// </summary>
    public Price Price { get; set; }

    /// <summary>
    /// Ключевые слова, применимые к товару.
    /// </summary>
    public ICollection<string>? KeyWords { get; set; }

    /// <summary>
    /// Создаёт объект <see cref="Product"/>
    /// </summary>
    /// <param name="name">
    /// Название товара.
    /// </param>
    /// <param name="description">
    /// Описание.
    /// </param>
    /// <param name="price">
    /// Цена.
    /// </param>
    /// <param name="keyWords">
    /// Ключевые слова, применимые к товару.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если <paramref name="name"/> или <paramref name="price"/> 
    /// равен <see cref="null"/>.
    /// </exception>
    public Product(
        Name<Product> name, 
        Description? description, 
        Price price,
        ICollection<string>? keyWords)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Description = description;
        Price = price ?? throw new ArgumentNullException(nameof(price));
        KeyWords = keyWords;
    }
}
