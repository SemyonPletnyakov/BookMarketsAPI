using Models.Autorization;
using Models.Requests.Operations;

namespace Logic.Abstractions.Autorization;

/// <summary>
/// Контракт сущности, проверяющей, 
/// достаточно ли прав у пользователя на совершение операции.
/// </summary>
public interface IRuleChecker
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
    /// <returns>
    /// Задача для асинхронного выполнения.
    /// </returns>
    public Task CheckRuleAsync(
        JwtToken jwtToken,
        OperationDescriprion operationDescriprion,
        CancellationToken token);
}
