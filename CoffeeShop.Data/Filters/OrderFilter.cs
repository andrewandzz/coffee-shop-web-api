namespace CoffeeShop.Data.Filters
{
    public class OrderFilter
    {
        public string CustomerGuid { get; set; }
        public bool? CheckedOut { get; set; }
    }
}
