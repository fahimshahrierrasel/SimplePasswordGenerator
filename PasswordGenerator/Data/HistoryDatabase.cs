using SQLite;
using System.Linq;
using System.Linq.Expressions;

namespace PasswordGenerator.Data
{
    public class HistoryDatabase : IAsyncDisposable
    {
        SQLiteAsyncConnection _db;
        SQLiteAsyncConnection DB =>
            _db ??= new SQLiteAsyncConnection(Constants.DatabasePath, Constants.DatabaseFlags);

        async Task CreateTableAsync<TTable>() where TTable : class, new()
        {
            await DB.CreateTableAsync<TTable>();
        }

        private async Task<TResult> Execute<TTable, TResult>(Func<Task<TResult>> action) where TTable : class, new()
        {
            await CreateTableAsync<TTable>();
            return await action();
        }

        public async Task<AsyncTableQuery<TTable>> GetTableAsync<TTable>() where TTable : class, new()
        {
            await CreateTableAsync<TTable>();
            return DB.Table<TTable>();
        }

        public async Task<IEnumerable<TTable>> GetAllAsync<TTable>() where TTable : class, new()
        {
            var table = await GetTableAsync<TTable>();
            return await table.ToListAsync();
        }

        public async Task<bool> InsertItemAsync<TTable>(TTable item) where TTable : class, new()
        {
            return await Execute<TTable, bool>(async () => await DB.InsertAsync(item) > 0);
        }

        public async ValueTask DisposeAsync() => await _db?.CloseAsync();
    }
}
