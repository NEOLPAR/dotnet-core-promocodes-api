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
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class CodeServiceUsersController : ControllerBase
    {
        protected ICodeServiceUserService _service;
        public CodeServiceUsersController(ICodeServiceUserService service)
        {
            _service = service;
        }

        // GET: api/CodeServiceUsers
        [HttpGet]
        public async Task<IActionResult> Get([FromHeader] string authorization)
        {
            var response = await _service.Get(authorization);

            if (response == null) return BadRequest();

            return Ok(ToDTO(response));
        }

        // GET: api/CodeServiceUsers/5/8/9245fe4a-d402-451c-b9ed-9c1a04247482
        [HttpGet("{codeId}/{serviceId}")]
        public async Task<IActionResult> Get
            (int codeId, int serviceId, [FromHeader] string authorization)
        {
            var response = await _service.Get(codeId, serviceId, authorization);

            if (response == null)
            {
                return NotFound();
            }

            return Ok(ToDTO(response));
        }

        // POST: api/CodeServiceUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> Post(CodeServiceUserDTO itm, [FromHeader] string authorization)
        {
            var response = await _service.Post(fromDTO(itm), authorization);

            if (response == null) return BadRequest();

            return Ok(ToDTO(response));
        }

        // DELETE: api/CodeServiceUsers/5/8/9245fe4a-d402-451c-b9ed-9c1a04247482
        [HttpDelete("{codeId}/{serviceId}")]
        public async Task<IActionResult> Delete
            (int codeId, int serviceId, [FromHeader] string authorization)
        {
            var response = await _service.Delete(codeId, serviceId, authorization);

            if (!response)
            {
                return NotFound();
            }

            return NoContent();
        }

        [NonAction]
        public CodeServiceUser fromDTO(CodeServiceUserDTO itm)
        {
            return new CodeServiceUser(itm);
        }

        [NonAction]
        public CodeServiceUserDTO ToDTO(CodeServiceUser itm)
        {
            return new CodeServiceUserDTO(itm);
        }

        [NonAction]
        public IList<CodeServiceUserDTO> ToDTO(IList<CodeServiceUser> itmList)
        {
            return itmList.Select(x => new CodeServiceUserDTO(x)).ToList();
        }
    }
}
