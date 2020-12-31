using PromocodesApp.Entities;
using PromocodesApp.Interfaces;
using PromocodesApp.Services;

namespace PromocodesApp.Controllers
{
    public class CodesController : ControllerAbstract<CodeDTO>
    {
        public CodesController(IService<CodeDTO> service) :
            base(service) {}
    }
}
