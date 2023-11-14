using POSSystem.Models;

namespace POSSystem.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetAllItems();
    }
}
