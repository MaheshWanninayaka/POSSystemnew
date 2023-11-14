namespace POSSystem.Models
{
    public class BillingItem
    {
        public int BillingItemId { get;set; }
        public int ItemId { get;set; }
        public int? BillingInformationId { get;set; }
        public decimal Quantity { get;set; }    
        public decimal Price { get;set;}
        public decimal Amount { get;set;}

    }
}
