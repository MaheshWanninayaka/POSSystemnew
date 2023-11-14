using POSSystem.Models;

namespace POSSystem.Services
{
    public interface IBillingInformationService
    {
        Task<bool> SaveBillingInformation(BillingInformation billingInformation);
    }
}
