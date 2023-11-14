using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POSSystem.Models;
using POSSystem.Services;

namespace POSSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillingInformationController : ControllerBase
    {
        private readonly IBillingInformationService _billingInformationService;
        public BillingInformationController(IBillingInformationService billingInformationService)
        {
            _billingInformationService = billingInformationService;
        }

        [HttpPost]
        [Route("SaveBillingInformation")]

        public async Task<bool> SaveBillingInformation(BillingInformation billingInformation)
        {
           return await _billingInformationService.SaveBillingInformation(billingInformation);
        }
    }
}
