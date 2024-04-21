using Models;
using Models.Requests;

namespace Logic.Abstractions.Processors;

/// <summary>
/// Процессор запросов с авторизацией.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
/// <typeparam name="TOutput">
/// Выходные данные.
/// </typeparam>
public interface IRequestProcessorWithAuthorize<TInput, TOutput> 
    where TInput : RequestBase
{
    /// <summary>
    /// Обрабатывает запрос.
    /// </summary>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <param name="jwtToken">
    /// Токен авторизации.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// <typeparamref name="TOutput"/>.
    /// </returns>
    public Task<TOutput> ProcessAsync(
        TInput request, 
        JwtToken jwtToken, 
        CancellationToken token);
}
