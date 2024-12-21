using Microsoft.AspNetCore.Mvc;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.DTO;
using VisitasApp.Core.ServicesContract;


namespace VisitasApp.UI.Controllers
{
    [Route("[controller]")]
    public class VisitasController : Controller
    {   
        private readonly IVisitasService _visitasService;

        public VisitasController(IVisitasService visitasService)
        {
            _visitasService = visitasService;
        }


        [Route("[action]")]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var visitas = await _visitasService.VisitasGetAllAsync();
            return View(visitas);
        }

        [Route("[action]")]
        [HttpGet]
        public IActionResult Create()
        {
            CreateVisitaDto createVisitaDto = new CreateVisitaDto();
            createVisitaDto.FechaVisita = DateTime.Now;
            return View(createVisitaDto);
        }


        [Route("[action]")]
        [HttpPost]
        public IActionResult Create(CreateVisitaDto visita)
        {
            if(ModelState.IsValid)
            {
                _visitasService.VisitasCreateAsync(visita);
                return RedirectToAction("Index", "Visitas");
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(visita);
            }
        }



    }
}
