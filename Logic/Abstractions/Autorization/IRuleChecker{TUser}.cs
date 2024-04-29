using Models.Autorization;
using Models.Requests.Operations;

namespace Logic.Abstractions.Autorization;

/// <summary>
/// Класс, проверяющий, достаточно ли прав у пользователя на совершение операции.
/// </summary>
/// /// <typeparam name="TUser">
/// Тип пользователя.
/// </typeparam>
public interface IRuleChecker<TUser>
{
    /// <summary>
    /// Проверить, достаточно ли прав.
    /// </summary>
    /// <param name="jwtToken">
    /// JWT-токен.
    /// </param>
    /// <param name="operationDescriprion">
    /// Описание операции.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns></returns>
    public Task CheckRuleAsync(
        JwtToken jwtToken,
        OperationDescriprion operationDescriprion,
        CancellationToken token);
}
