namespace Models;

/// <summary>
/// Электронная почта.
/// </summary>
public sealed record Email
{
/// <summary>
/// Значение электронной почты.
/// </summary>
public string Value { get; }

/// <summary>
/// Создаёт объект <see cref="Email"/>
/// </summary>
/// <param name="value">
/// Значение электронной почты.
/// </param>
/// <exception cref="ArgumentNullException">
/// Если <paramref name="value"/> равен <see cref="null"/>.
/// </exception>
/// <exception cref="ArgumentNullException">
/// Если <paramref name="value"/> пустой или состоит из пробелов.
/// </exception>
public Email(string value)
{
    ArgumentException.ThrowIfNullOrWhiteSpace(value, nameof(value));

    Value = value;
}
}