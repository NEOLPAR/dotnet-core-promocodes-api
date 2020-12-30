using Microsoft.AspNetCore.Mvc;
using PromocodesApp.Interfaces;
using System.Threading.Tasks;

namespace PromocodesApp.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ControllerAbstract<T> : ControllerBase
    {
        protected IService<T> _service;

        public ControllerAbstract(IService<T> service)
        {
            _service = service;
        }

        // GET: api/T
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var response = await _service.Get();

            if (response == null) return BadRequest();

            return Ok(response);
        }

        // GET: api/T/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var response = await _service.Get(id);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }

        // PUT: api/T/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, T itm)
        {
            var response = await _service.Put(id, itm);

            if (response == null) return BadRequest();

            return NoContent();
        }

        // POST: api/T
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(T itm)
        {
            var response = await _service.Post(itm);

            if (response == null) return BadRequest();

            return Ok(response);
        }

        // DELETE: api/T/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _service.Delete(id);

            if (!response)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
