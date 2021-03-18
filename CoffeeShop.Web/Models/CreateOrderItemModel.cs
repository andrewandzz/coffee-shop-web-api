namespace CoffeeShop.Web.Models
{
    public class CreateOrderItemModel
    {
        public int? OrderId { get; set; }
        public string CustomerGuid { get; set; }
        public int? CoffeeId { get; set; }
        public int? Sugar { get; set; }
        public bool? CupCap { get; set; }
    }
}
