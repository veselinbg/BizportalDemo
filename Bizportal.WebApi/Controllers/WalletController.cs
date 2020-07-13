using Bizportal.Api;
using Microsoft.AspNetCore.Mvc;

namespace Bizportal.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WalletController : BaseController<Wallet>
    {
        public WalletController(IBizportalManager<Wallet> bizportalManager) : base(bizportalManager)
        {
        }
    }
}
