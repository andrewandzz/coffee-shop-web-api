using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CoffeeShop.Data.Entities
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(36)]
        public string CustomerGuid { get; set; }

        [MaxLength(30)]
        public string CustomerName { get; set; }

        [MaxLength(12)]
        public string CustomerPhone { get; set; }

        public double? TotalPrice { get; set; }

        public DateTime CreationDate { get; set; }

        public bool CheckedOut { get; set; }

        public IEnumerable<Coffee> Coffees { get; set; }

        public IEnumerable<OrderItem> OrderItems { get; set; }
    }
}
