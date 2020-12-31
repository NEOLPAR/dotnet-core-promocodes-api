using Microsoft.AspNetCore.Mvc;
using PromocodesApp.Entities;
using PromocodesApp.Interfaces;
using PromocodesApp.Services;
using System.Threading.Tasks;

namespace PromocodesApp.Controllers
{
    public class ServicesController : ControllerAbstract<ServiceDTO>
    {
        public new IServiceService<ServiceDTO> _service;
        public ServicesController(IServiceService<ServiceDTO> service) :
            base(service)
        {
            _service = service;
        }

        // GET: api/services/:name
        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var response = await _service.GetByName(name);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }

        // GET: api/services/:page/:elements
        [HttpGet("{page:int}/{elements:int}")]
        public async Task<IActionResult> Get(int page, int elements)
        {
            var response = await _service.GetInfiniteScroll(page, elements);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(response);
        }
    }
}
