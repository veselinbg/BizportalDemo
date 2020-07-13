using Bizportal.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Bizportal.WebApi.Controllers
{
    public class BaseController<T> : ControllerBase where T : BaseEntity
    {
        protected readonly IBizportalManager<T> _bizportalManager;
        public BaseController(IBizportalManager<T> bizportalManager)
        {
            _bizportalManager = bizportalManager ?? throw new ArgumentNullException(nameof(bizportalManager));
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _bizportalManager.GetAll().ToListAsync();

            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            if (id.Equals(Guid.Empty))
            {
                return BadRequest();
            }
            var result = await _bizportalManager.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] T entity)
        {
            var result = await _bizportalManager.Add(entity);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _bizportalManager.Delete(id);

            return Ok();
        }
    }
}
