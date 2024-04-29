namespace Logic.Autorization;

/// <summary>
/// Настройки авторизации.
/// </summary>
public sealed class AuthConfiguration
{
    /// <summary>
    /// Издатель.
    /// </summary>
    public required string Issuer { get; set; }

    /// <summary>
    /// Потребитель.
    /// </summary>
    public required string Audience { get; set; }

    /// <summary>
    /// Ключ шифровки.
    /// </summary>
    public required string Key { get; set; }

    /// <summary>
    /// Время жизни.
    /// </summary>
    public required int Lifetime { get; set; }
}
