using System.ComponentModel.DataAnnotations;

namespace TSGTaskBoard.UI.Models
{
    public class TareaCreateViewModel
    {
        [Required]
        [StringLength(150)]
        public string Titulo { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Descripcion { get; set; }

        [Required]
        [StringLength(50)]
        public string Categoria { get; set; } = string.Empty;

        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }
    }
}
