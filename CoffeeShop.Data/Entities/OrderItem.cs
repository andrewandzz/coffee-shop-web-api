using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Data.Entities
{
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        public int CoffeeId { get; set; }
        public Coffee Coffee { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; }

        public int Sugar { get; set; }

        public bool CupCap { get; set; }
    }
}
