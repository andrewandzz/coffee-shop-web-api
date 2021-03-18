using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Data.Entities
{
    public class Coffee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        public string Volume { get; set; }

        public double Price { get; set; }

        [Required]
        [MaxLength(30)]
        public string ImageFileName { get; set; }

        public IEnumerable<Order> Orders { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
