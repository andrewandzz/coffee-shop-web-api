namespace CoffeeShop.Logics.Dtos
{
    public class OrderItemDto
    {
        public int Id { get; set; }
        public CoffeeDto Coffee { get; set; }
        public OrderDto Order { get; set; }
        public int Sugar { get; set; }
        public bool CupCap { get; set; }
    }
}
