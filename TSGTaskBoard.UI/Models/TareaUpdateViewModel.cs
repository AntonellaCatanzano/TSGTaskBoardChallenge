using System.ComponentModel.DataAnnotations;

namespace TSGTaskBoard.UI.Models
{
    public class TareaUpdateViewModel
    {
        [StringLength(150)]
        public string? Titulo { get; set; }

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [StringLength(50)]
        public string? Categoria { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }
    }
}
