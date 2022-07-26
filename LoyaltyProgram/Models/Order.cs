namespace LoyaltyProgram.Models
{
    public class Order
    {
        public int EventSourceId { get; set; }
        public int AccountId { get; set; }

        public double TotalAmount { get; set; }

        public double TotalAmountAfterDiscount { get; set; }
        public int Discount { get; set; }

        public int ApplyCode { get; set; }
        public List<OrderDetail> Details { get; set; }
    }
}
