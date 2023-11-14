using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSSystem.Models;
using POSSystem.Services;

namespace POSSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        public ItemController(IItemService itemService)
        {
            _itemService = itemService;
        }

        [HttpGet]
        [Route("GetAllItems")]
        public async Task<List<Item>> GetAllItems()
        {
            return await _itemService.GetAllItems();
        }
    }
}
