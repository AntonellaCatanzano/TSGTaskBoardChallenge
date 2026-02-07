using System.ComponentModel.DataAnnotations;
using TSGTaskBoard.Domain.Enums;

namespace TSGTaskBoard.Domain.DTO
{
    public class TareaUpdateDTO
    {
        public string? Titulo { get; set; }
        public string? Descripcion { get; set; }
        public string? Categoria { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaVencimiento { get; set; }
    }
}
