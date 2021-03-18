namespace CoffeeShop.Data.Filters
{
    public class OrderItemFilter
    {
        public int? OrderId { get; set; }
        public string CustomerGuid { get; set; }
        public bool? CheckedOut { get; set; }
    }
}
