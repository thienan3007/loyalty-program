namespace LoyaltyProgram.Models
{
    public class OrderDetail
    {
        public int ProductId { get; set; }

        public int Quantity
        {
            get; set;

        }
        public double TotalAmount { get; set; }
    }
}
