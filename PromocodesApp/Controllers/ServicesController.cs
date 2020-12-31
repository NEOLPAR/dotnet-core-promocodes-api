using Microsoft.AspNetCore.Mvc;
using PromocodesApp.Entities;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;
using PromocodesApp.Services;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> Get(string name)
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
        public async Task<IActionResult> Get(int page, int elements)
        {
            var response = await _service.GetInfiniteScroll(page, elements);

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
            return new ServiceDTO(itm);
        }

        public override IList<ServiceDTO> ToDTO(IList<Service> itmList)
        {
            return itmList.Select(x => new ServiceDTO(x)).ToList();
        }
    }
}
