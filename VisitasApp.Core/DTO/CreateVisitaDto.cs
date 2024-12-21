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
        [CustomValidation(typeof(CreateVisitaDto), "ValidateFechaVisita")]
        public DateTime FechaVisita { get; set; }

        [Required(ErrorMessage = "El vendedor es requerido")]
        public string NombreVendedor { get; set; } = "";

        [StringLength(500)]
        private string _notas = "";
        public string? Notas
        {
            get
            {
                return _notas ?? "";
            }
            set
            {
                _notas = value ?? "";
            }
        }

        public static ValidationResult ValidateFechaVisita(DateTime fechaVisita, ValidationContext context)
        {
            if (fechaVisita > DateTime.Now.AddMinutes(1))
            {
                return new ValidationResult("La fecha de la visita no puede ser posterior a la fecha actual.");
            }
            return ValidationResult.Success!;
        }
    }
}