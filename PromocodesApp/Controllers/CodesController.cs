using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PromocodesApp.Entities;
using PromocodesApp.Interfaces;
using PromocodesApp.Models;

namespace PromocodesApp.Controllers
{
    public class CodesController : ControllerAbstract<CodeDTO>
    {
        private readonly IService<CodeDTO> _codeService;

        public CodesController(IService<CodeDTO> codeService) : base(codeService) => _codeService = codeService;
    }
}
