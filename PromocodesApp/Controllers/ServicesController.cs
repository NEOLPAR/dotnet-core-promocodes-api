using PromocodesApp.Entities;
using PromocodesApp.Interfaces;

namespace PromocodesApp.Controllers
{
    public class ServicesController : ControllerAbstract<ServiceDTO>
    {
        public ServicesController(IService<ServiceDTO> service) :
            base(service)
        { }
    }
}
