namespace iShopServerSide.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
        public string Category { get; set; }
        public float Price { get; set; }
        public float Discount { get; set; }
        public int Stock { get; set; }
    }
}
