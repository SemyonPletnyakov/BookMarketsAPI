using Models;
using Models.Autorization;

namespace Logic.Abstractions.Processors;

/// <summary>
/// Контракт процессора запросов авторизации.
/// </summary>
public interface IAuthorizeRequestProcessor
{
    /// <summary>
    /// Обрабатывает запрос авторизации покупателя.
    /// </summary>
    /// <param name="login">
    /// Логин.
    /// </param>
    /// <param name="password">
    /// Пароль.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// JWT токен.
    /// </returns>
    public Task<JwtToken> ProcessAutorizeEmployeeAsync(
        Login login,
        Password password,
        CancellationToken token);

    /// <summary>
    /// Обрабатывает запрос авторизации сотрудника.
    /// </summary>
    /// <param name="email">
    /// Электронная почта.
    /// </param>
    /// <param name="password">
    /// Пароль.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// JWT токен.
    /// </returns>
    public Task<JwtToken> ProcessAutorizeCustomerAsync(
        Email email,
        Password password,
        CancellationToken token);
}
