using POSSystem.Models;
using POSSystem.Repository;

namespace POSSystem.Services
{

    public class ItemService : IItemService
    {
        private readonly IItemRepository _itemRepository;
        public ItemService(IItemRepository itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<List<Item>> GetAllItems()
        {
            return await _itemRepository.GetAllItems();
        }
    }
}
