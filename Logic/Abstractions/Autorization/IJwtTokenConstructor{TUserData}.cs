using Models.Autorization;

namespace Logic.Abstractions.Autorization;

/// <summary>
/// Контракт сущности, которая создаёт JWT-токен.
/// </summary>
/// <typeparam name="TUserData">
/// Тип данных пользователя.
/// </typeparam>
public interface IJwtTokenConstructor<TUserData>
{
    /// <summary>
    /// Создаёт JWT-токен.
    /// </summary>
    /// <param name="userData">
    /// Данные пользователя.
    /// </param>
    /// <returns>
    /// JWT-токен.
    /// </returns>
    public JwtToken Construct(TUserData userData);
}
