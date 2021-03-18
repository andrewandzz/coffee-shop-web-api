using Microsoft.AspNetCore.Mvc;

namespace CoffeeShop.Web.Infrastructure
{
    public class GetCoffeeQueryParams
    {
        [FromQuery]
        public string Name { get; set; }

        [FromQuery]
        public string Fields { get; set; }
    }
}
