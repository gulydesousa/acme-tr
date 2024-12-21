using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
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


        [Route("[action]/{id}")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var visita = await _visitasService.VisitasGetByIdAsync(id);
            if(visita == null)
            {
                return RedirectToAction("Index", "Visitas");
            }
            return View(visita);
        }


        [Route("[action]/{id}")]
        [HttpPost]
        public async Task<IActionResult> Edit(Visita visitaUpdate)
        {
            Visita request = await _visitasService.VisitasGetByIdAsync(visitaUpdate.Id);

            if (request == null)
            {
                return RedirectToAction("Index", "Visitas");
            }
            if (ModelState.IsValid)
            {
                await _visitasService.VisitasUpdateAsync(visitaUpdate);
                return RedirectToAction("Index", "Visitas");
            }
            else
            {
                ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                return View(request);
            }
        }
    }
}
