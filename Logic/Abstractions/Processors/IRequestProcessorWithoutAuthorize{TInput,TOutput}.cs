using Models.Requests;

namespace Logic.Abstractions.Processors;

/// <summary>
/// Процессор запросов без авторизации.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
/// <typeparam name="TOutput">
/// Выходные данные.
/// </typeparam>
public interface IRequestProcessorWithoutAuthorize<TInput, TOutput> 
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
    /// <returns>
    /// <typeparamref name="TOutput"/>.
    /// </returns>
    public Task<TOutput> ProcessAsync(
        TInput request,
        CancellationToken token);
}
