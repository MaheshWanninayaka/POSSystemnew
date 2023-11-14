using POSSystem.Models;
using POSSystem.Repository;

namespace POSSystem.Services
{
    public class BillingInformationService : IBillingInformationService
    {
        private readonly IBillingInformationRepository _billingInformationRepository;
        public BillingInformationService(IBillingInformationRepository billingInformationRepository)
        {
            _billingInformationRepository = billingInformationRepository;   
        }

        public async  Task<bool> SaveBillingInformation(BillingInformation billingInformation)
        {
            return await _billingInformationRepository.SaveBillingInformation(billingInformation);
        }
    }
}
