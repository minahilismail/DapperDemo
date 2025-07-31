namespace DapperDemoData.Data
{
    public interface IDataAccess
    {
        Task<IEnumerable<T>> GetData<T, P>(string query, P parameters, string connectionId = "DefaultSQLConnection");
        Task SaveData<P>(string query, P parameters, string connectionId = "DefaultSQLConnection");
    }
}