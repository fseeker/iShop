namespace iShopServerSide.Model
{
    public class CartItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public float Price { get; set; } //always take price related fields to double
        public float Discount { get; set; }
        public int Quantity { get; set; }
    }
}
