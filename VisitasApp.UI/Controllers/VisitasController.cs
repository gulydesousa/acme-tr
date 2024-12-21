using Microsoft.AspNetCore.Mvc;
using VisitasApp.Core.ServicesContract;


namespace VisitasApp.UI.Controllers
{    
    public class VisitasController : Controller
    {   
        private readonly IVisitasService _visitasService;

        public VisitasController(IVisitasService visitasService)
        {
            _visitasService = visitasService;
        }


        [Route("visitas/index")]
        [Route("/")]
        public async Task<IActionResult> Index()
        {
            var visitas = await _visitasService.VisitasGetAllAsync();
            return View(visitas);
        }


    }
}
