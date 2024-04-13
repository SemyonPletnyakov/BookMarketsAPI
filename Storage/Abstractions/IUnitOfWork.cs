namespace Storage.Abstractions;

public interface IUnitOfWork
{
    /// <summary>
    /// Отправляет транзакцию изменения в репозиториях.
    /// </summary>
    /// <param name="token">
    /// Токен отмены.
    /// </param>
    /// <returns>
    /// Задача асинхронного выполнения.
    /// </returns>
    Task SaveChangesAsync(CancellationToken token);
}
