using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Web.Infrastructure
{
    public class GetOrderQueryParams
    {
        [FromQuery(Name = "customer-guid")]
        public string CustomerGuid { get; set; }

        [FromQuery(Name = "checked-out")]
        public bool? CheckedOut { get; set; }
    }
}
