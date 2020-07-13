using Bizportal.Api;
using Microsoft.AspNetCore.Mvc;

namespace Bizportal.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController<Product>
    {
        public ProductController(IBizportalManager<Product> bizportalManager) : base(bizportalManager)
        {
        }
    }
}
