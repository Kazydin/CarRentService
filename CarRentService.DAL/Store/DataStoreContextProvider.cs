using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Store;

public static class DataStoreContextProvider
{
    public static IDataStoreContext Current { get; set; } = null!;

    /// <summary>
    /// Инициализация DataStoreContextProvider.
    /// </summary>
    /// <param name="context">Экземпляр IDataStoreContext.</param>
    public static void Init(IDataStoreContext context)
    {
        if (Current != null)
        {
            throw new InvalidOperationException("DataStoreContextProvider уже инициализирован.");
        }

        Current = context ?? throw new ArgumentNullException(nameof(context));
    }
}