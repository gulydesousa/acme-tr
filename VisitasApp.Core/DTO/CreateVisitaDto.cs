using System.ComponentModel.DataAnnotations;

namespace VisitasApp.Core.DTO
{
    /// <summary>
    /// Clase para crear una nueva visita. 
    /// Se utiliza para enviar información de una nueva visita a través de la API.
    /// </summary>
    public class CreateVisitaDto
    {
        [Required(ErrorMessage = "El nombre del cliente es requerido")]
        public string NombreCliente { get; set; } = "";

        [Required(ErrorMessage = "La fecha de la visita es requerida")]
        public DateTime FechaVisita { get; set; }

        [Required(ErrorMessage = "El vendedor es requerido")]
        public string NombreVendedor { get; set; } = "";

        [StringLength(500)]
        public string Notas { get; set; } = "";
    }
}