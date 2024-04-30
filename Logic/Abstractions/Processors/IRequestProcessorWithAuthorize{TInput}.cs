using Models.Autorization;
using Models.Requests.BaseRequests;

namespace Logic.Abstractions.Processors;

/// <summary>
/// Контракт процессора запросов с авторизацией без возвращаемых данных.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
public interface IRequestProcessorWithAuthorize<TInput> 
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
    /// Задача для ожидания.
    /// </returns>
    public Task ProcessAsync(
        TInput request, 
        JwtToken jwtToken, 
        CancellationToken token);
}
