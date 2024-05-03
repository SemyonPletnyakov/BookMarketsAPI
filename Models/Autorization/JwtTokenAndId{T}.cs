
namespace Models.Autorization;

/// <summary>
/// JWT-токен и идентификатор авторизованного пользователя.
/// </summary>
/// <typeparam name="T">
/// Тип пользователя.
/// </typeparam>
public class JwtTokenAndId<T>
{
    /// <summary>
    /// JWT-токен.
    /// </summary>
    public JwtToken JwtToken { get; }

    /// <summary>
    /// Идентификатор пользователя.
    /// </summary>
    public Id<T> Id { get; set; }

    /// <summary>
    /// Создать объект <see cref="JwtTokenAndId"/>.
    /// </summary>
    /// <param name="jwtToken">
    /// JWT-токен.
    /// </param>
    /// <param name="id">
    /// Идентификатор пользователя.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// Если один из параметров равен <see langword="null"/>.
    /// </exception>
    public JwtTokenAndId(JwtToken jwtToken, Id<T> id)
    {
        JwtToken = jwtToken ?? throw new ArgumentNullException(nameof(jwtToken));
        Id = id ?? throw new ArgumentNullException(nameof(id));
    }
}
