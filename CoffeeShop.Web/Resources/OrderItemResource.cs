namespace CoffeeShop.Web.Resources
{
    public class OrderItemResource
    {
        public int Id { get; set; }
        public CoffeeResource Coffee { get; set; }
        public OrderResource Order { get; set; }
        public int Sugar { get; set; }
        public bool CupCap { get; set; }
    }
}
