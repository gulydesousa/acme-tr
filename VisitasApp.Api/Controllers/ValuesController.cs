using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using VisitasApp.Core.Domain.Entities;
using VisitasApp.Core.Domain.RepositoryContracts;
using VisitasApp.Core.DTO;

namespace VisitasApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitasController : ControllerBase
    {
        private readonly IVisitaRepository _visitaRepository;

        public VisitasController(IVisitaRepository visitaRepository)
        {
            _visitaRepository = visitaRepository;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Agregar una nueva visita", Description = "Crea una nueva visita y devuelve la visita creada.")]
        public async Task<IActionResult> AddVisita(CreateVisitaDto createVisitaDto)
        {
            var createdVisita = await _visitaRepository.VisitasCreateAsync(createVisitaDto);

            return CreatedAtAction(nameof(GetVisita), new { id = createdVisita.Id }, createdVisita);
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Obtener todas las visitas", Description = "Devuelve una lista de todas las visitas.")]
        public async Task<ActionResult<IEnumerable<Visita>>> GetVisitas()
        {
            var visitas = await _visitaRepository.VisitasGetAllAsync();
            return Ok(visitas);
        }


        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Obtener una visita por ID", Description = "Devuelve una visita por su ID.")]
        public async Task<ActionResult<Visita>> GetVisita(int id)
        {
            var visita = await _visitaRepository.VisitasGetByIdAsync(id);

            if (visita == null)
            {
                return NotFound();
            }

            return Ok(visita);
        }       

        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Actualizar una visita", Description = "Actualiza una visita existente por su ID.")]
        public async Task<IActionResult> UpdateVisita(int id, Visita visita)
        {
            if (id != visita.Id)
            {
                return BadRequest();
            }

            await _visitaRepository.VisitasUpdateAsync(visita);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Eliminar una visita", Description = "Elimina una visita por su ID.")]
        public async Task<IActionResult> DeleteVisita(int id)
        {
            await _visitaRepository.VisitasDeleteAsync(id);
            return NoContent();
        }
    }
}
