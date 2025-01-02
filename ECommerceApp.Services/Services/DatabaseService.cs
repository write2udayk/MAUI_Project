using SQLite;
using ECommerceApp.Models;

namespace ECommerceApp.Services.Services
{

    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;
        public DatabaseService() 
        {
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "ECommerce.Db");
            _database= new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Product>().Wait();
        }

        public Task<List<Product>> GetProtuctAsync() 
        {
            return _database.Table<Product>().ToListAsync();
        }

        public Task<int> SaveproductAsync(Product product) 
        {
            if (product.Id != 0)
            {
                return _database.UpdateAsync(product);
            }
            else 
            {
                return _database.InsertAsync(product);
            }
        }

        public Task<int> DeleteProductAsync(Product product) 
        {

            return _database.DeleteAsync(product);
        }

    }
}
