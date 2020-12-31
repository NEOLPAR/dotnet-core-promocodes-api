using PromocodesApp.Entities;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;
using PromocodesApp.Services;
using System.Collections.Generic;
using System.Linq;

namespace PromocodesApp.Controllers
{
    public class CodesController : ControllerAbstract<Code, CodeDTO>
    {
        public CodesController(IService<Code> service) :
            base(service) {}
        public override Code FromDTO(CodeDTO itm)
        {
            return new Code(itm);
        }
        public override CodeDTO ToDTO(Code itm)
        {
            return new CodeDTO(itm);
        }

        public override IList<CodeDTO> ToDTO(IList<Code> itmList)
        {
            return itmList.Select(x => new CodeDTO(x)).ToList();
        }
    }
}
