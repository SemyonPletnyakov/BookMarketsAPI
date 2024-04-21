namespace Logic.Abstractions.Handlers;

/// <summary>
/// Обработчик запросов.
/// </summary>
/// <typeparam name="TInput">
/// Запрос.
/// </typeparam>
/// <typeparam name="TOutput">
/// Выходные данные.
/// </typeparam>
public interface IRequestHandler<TInput, TOutput>
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
    public Task<TOutput> HandleAsync(
        TInput request,
        CancellationToken token);
}
