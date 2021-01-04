using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PromocodesApp.Entities;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;
using PromocodesApp.Services;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PromocodesApp.Controllers
{
    public class ServicesController : ControllerAbstract<Service, ServiceDTO>
    {
        public new IServiceService<Service> _service;
        public ServicesController(IServiceService<Service> service) :
            base(service)
        {
            _service = service;
        }

        // GET: api/services/:name
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var response = await _service.GetByName(name);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(ToDTO(response));
        }

        // GET: api/services/:page/:elements
        [HttpGet("{page:int}/{elements:int}")]
        public async Task<IActionResult> GetInfiniteScroll(int page, int elements)
        {
            var response = await _service.GetInfiniteScroll(page, elements);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(ToDTO(response));
        }

        // GET: api/services/:name/:page/:elements
        [HttpGet("filter/{name}/{page:int}/{elements:int}")]
        public async Task<IActionResult> FilterByNameInfiniteScroll(string name, int page, int elements)
        {
            var response = await _service.FilterByNameInfiniteScroll(name, page, elements);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(ToDTO(response));
        }

        // GET: api/services/:name
        [HttpGet("filter/{name}")]
        public async Task<IActionResult> FilterByName(string name)
        {
            var response = await _service.FilterByName(name);

            if (response == null)
            {
                return NoContent();
            }

            return Ok(ToDTO(response));
        }

        public override Service FromDTO(ServiceDTO itm)
        {
            return new Service(itm);
        }
        public override ServiceDTO ToDTO(Service itm)
        {
            return new ServiceDTO(itm, _service.CurrentUserName());
        }

        public override IList<ServiceDTO> ToDTO(IList<Service> itmList)
        {
            return itmList.Select(x => new ServiceDTO(x, _service.CurrentUserName())).ToList();
        }
    }
}
