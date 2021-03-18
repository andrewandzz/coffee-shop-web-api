using System;

namespace CoffeeShop.Logics.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerGuid { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public double? TotalPrice { get; set; }
        public DateTime CreationDate { get; set; }
        public bool CheckedOut { get; set; }
    }
}
