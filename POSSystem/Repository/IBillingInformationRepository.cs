using POSSystem.Models;

namespace POSSystem.Repository
{
    public interface IBillingInformationRepository
    {
        Task<bool> SaveBillingInformation(BillingInformation billingInformation);
    }
}
