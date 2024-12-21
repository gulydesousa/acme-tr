using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VisitasApp.Core.Domain.Entities
{

    /// <summary>
    /// La clase Visita está diseñada para almacenar información sobre visitas comerciales, incluyendo el cliente, la fecha de la visita, el vendedor y cualquier nota adicional.
    /// </summary>
    public class Visita
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

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