using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Web.Infrastructure
{
    public class GetOrderItemQueryParams
    {
        [FromQuery(Name = "order-id")]
        public int? OrderId { get; set; }

        [FromQuery(Name = "customer-guid")]
        public string CustomerGuid { get; set; }

        [FromQuery(Name = "checked-out")]
        public bool? CheckedOut { get; set; }
    }
}
