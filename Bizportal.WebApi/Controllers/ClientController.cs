using Bizportal.Api;
using Microsoft.AspNetCore.Mvc;

namespace Bizportal.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : BaseController<Client>
    {
        public ClientController(IBizportalManager<Client> bizportalManager) : base(bizportalManager)
        {
        }
    }
}