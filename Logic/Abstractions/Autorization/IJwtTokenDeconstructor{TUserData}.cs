using Models.Autorization;

namespace Logic.Abstractions.Autorization;

/// <summary>
/// Контракт сущности, которая достаёт из JWT-токена данные пользователя.
/// </summary>
public interface IJwtTokenDeconstructor
{
    /// <summary>
    /// Достаёт из JWT-токена данные пользователя.
    /// </summary>
    /// <param name="jwtToken">
    /// JWT-токен.
    /// </param>
    /// <returns>
    /// Данные пользователя.
    /// </returns>
    public AutorizationData Deconstruct(JwtToken jwtToken);
}
