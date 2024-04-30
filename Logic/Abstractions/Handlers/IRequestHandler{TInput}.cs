using Models.Requests.BaseRequests;

namespace Logic.Abstractions.Handlers;

/// <summary>
/// Контракт обработчика запросов без выходных параметров.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
public interface IRequestHandler<TInput>
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
    /// Задача для ожидания.
    /// </returns>
    public Task HandleAsync(
        TInput request,
        CancellationToken token);
}
