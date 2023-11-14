using POSSystem.Models;

namespace POSSystem.Repository
{
    public interface IItemRepository
    {
        Task<List<Item>> GetAllItems();
    }
}
