using Models.Requests.BaseRequests;

namespace Logic.Abstractions.Processors;

/// <summary>
/// Процессор запросов без авторизации.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
public interface IRequestProcessorWithoutAuthorize<TInput> 
    where TInput : RequestBase
{
    /// <summary>
    /// Обрабатывает запрос.
    /// </summary>
    /// <param name="request">
    /// Запрос.
    /// </param>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    public Task ProcessAsync(
        TInput request,
        CancellationToken token);
}
