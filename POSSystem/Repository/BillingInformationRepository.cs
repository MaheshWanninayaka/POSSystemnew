using Microsoft.Data.SqlClient;
using POSSystem.Controllers;
using POSSystem.Models;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace POSSystem.Repository
{
    public class BillingInformationRepository : IBillingInformationRepository
    {
        private ILogger<BillingInformationController> _logger;
        public BillingInformationRepository(ILogger<BillingInformationController> logger)
        {
            _logger = logger;
        }

        public async Task<bool> SaveBillingInformation(BillingInformation billingInformation)
        {
            using (SqlConnection conn = new SqlConnection())
            {
                conn.ConnectionString = "Server=DESKTOP-1UD7PJE\\SQLEXPRESS;Database=POSdb;Integrated Security=true;TrustServerCertificate=True";
                conn.Open();

                SqlTransaction tran = conn.BeginTransaction();
                SqlDataAdapter da = new SqlDataAdapter();

                try
                {
                    if (billingInformation != null)
                    {
                        using (SqlCommand cmd = new SqlCommand("INSERT INTO dbo.BillingInformation (Discount, GrandTotal, SubTotal, Vat) VALUES (@Discount, @GrandTotal, @SubTotal, @Vat); SELECT SCOPE_IDENTITY();", conn, tran))
                        {
                            cmd.Parameters.AddWithValue("@Discount", billingInformation.Discount);
                            cmd.Parameters.AddWithValue("@GrandTotal", billingInformation.GrandTotal);
                            cmd.Parameters.AddWithValue("@SubTotal", billingInformation.SubTotal);
                            cmd.Parameters.AddWithValue("@Vat", billingInformation.Vat);

                            int billingInformationId = Convert.ToInt32(cmd.ExecuteScalar());

                            foreach (var item in billingInformation.Items)
                            {
                                using (SqlCommand itemCmd = new SqlCommand("INSERT INTO dbo.BillingItem (BillingInformationId, ItemId, Quantity, Price, Amount) VALUES (@BillingInformationId, @ItemId, @Quantity, @Price, @Amount)", conn, tran))
                                {
                                    itemCmd.Parameters.AddWithValue("@BillingInformationId", billingInformationId);
                                    itemCmd.Parameters.AddWithValue("@ItemId", item.ItemId);
                                    itemCmd.Parameters.AddWithValue("@Quantity", item.Quantity);
                                    itemCmd.Parameters.AddWithValue("@Price", item.Price);
                                    itemCmd.Parameters.AddWithValue("@Amount", item.Amount);

                                    await itemCmd.ExecuteNonQueryAsync();
                                }
                            }

                            tran.Commit();
                            return true;
                        }
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    _logger.Log(LogLevel.Error, ex.Message);
                    tran.Rollback();
                    throw;
                }
                finally
                {
                    conn.Close();
                }
            }
        }
    }
}
