namespace moneyboss_transaction_service.Models
{
    public class Income
    {
        public int Id { get; set; }
        public string PaymentFrom { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public string Category { get; set; } = String.Empty;
        public int UserId { get; set; }
        public double Amount { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
