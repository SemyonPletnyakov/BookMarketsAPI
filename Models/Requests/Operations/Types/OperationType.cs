namespace Models.Requests.Operations.Types;

/// <summary>
/// Тип операции.
/// </summary>
public enum OperationType
{
    /// <summary>
    /// Операция получения.
    /// </summary>
    Get = 1,

    /// <summary>
    /// Операция добавления.
    /// </summary>
    Add = 2,

    /// <summary>
    /// Операция обновления.
    /// </summary>
    Update = 4,

    /// <summary>
    /// Операция удаления.
    /// </summary>
    Delete = 8,

    /// <summary>
    /// Операция получения или добавления.
    /// </summary>
    GetOrAdd = Get | Add
}
