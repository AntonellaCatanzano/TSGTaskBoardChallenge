using System.ComponentModel.DataAnnotations;
using TSGTaskBoard.Domain.Enums;

namespace TSGTaskBoard.Domain.DTO
{
    public class CambiarEstadoTareaDTO
    {
        [Required]
        public string Estado { get; set; } = string.Empty;
    }
}
