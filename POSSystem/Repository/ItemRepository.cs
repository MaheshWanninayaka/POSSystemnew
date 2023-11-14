using Microsoft.Data.SqlClient;
using POSSystem.Controllers;
using POSSystem.Models;
using System.Data;

namespace POSSystem.Repository
{
    public class ItemRepository : IItemRepository
    {
        private ILogger<ItemController> _logger;
        public ItemRepository(ILogger<ItemController> logger)
        {
            _logger = logger;
        }

        public async Task<List<Item>> GetAllItems()
        {
            using (SqlConnection conn = new SqlConnection())
            {
                try
                {
                    
                    conn.ConnectionString = "Server=DESKTOP-1UD7PJE\\SQLEXPRESS;Database=POSdb;Integrated Security=true;TrustServerCertificate=True";

                    conn.Open();

                    if (conn.State == ConnectionState.Open)
                    {
                        using (SqlCommand cmd = new SqlCommand("SELECT * FROM dbo.Item", conn))
                        {
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                List<Item> items = new List<Item>();

                                while (reader.Read())
                                {
                                    Item item = new Item
                                    {
                                        ItemId = (int)reader["ItemId"], 
                                        ItemName = (string)reader["ItemName"],
                                        Price= (decimal?)reader["Price"],
                                        Quantity= (decimal?)reader["Quantity"]
                                    };

                                    items.Add(item);
                                }

                                return items;
                            }
                        }
                    }
                    else
                    {
                        throw new InvalidOperationException("Connection is not open.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex.Message);
                    throw;
                }
                finally
                {
                    if (conn.State == ConnectionState.Open)
                    {
                        conn.Close();
                    }
                }
            }
        }
    }


}
