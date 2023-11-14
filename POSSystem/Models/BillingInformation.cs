namespace POSSystem.Models
{
    public class BillingInformation
    {
        public int BillInginformationId {  get; set; }
        public int ItemId { get; set; }
        public decimal SubTotal {  get; set; }
        public decimal Discount { get; set; }
        public decimal Vat { get; set; }
        public decimal GrandTotal { get; set; }
        public List<BillingItem> Items { get; set; }
    }
}
