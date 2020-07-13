using Bizportal.Api;
using Microsoft.AspNetCore.Mvc;

namespace Bizportal.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : BaseController<Order>
    {
        public OrderController(IBizportalManager<Order> bizportalManager) : base(bizportalManager)
        {
        }
    }
}
