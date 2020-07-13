using Bizportal.Api;
using Microsoft.AspNetCore.Mvc;

namespace Bizportal.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController<Category>
    {
        public CategoryController(IBizportalManager<Category> bizportalManager) : base(bizportalManager)
        {
        }
    }
}
