using Models.Autorization;

namespace Logic.Abstractions.Autorization;

/// <summary>
/// Контракт сущности, которая достаёт из JWT-токена данные пользователя.
/// </summary>
/// <typeparam name="TUserData">
/// Тип данных пользователя.
/// </typeparam>
public interface IJwtTokenDeconstructor<TUserData>
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
    public TUserData Deconstruct(JwtToken jwtToken);
}
