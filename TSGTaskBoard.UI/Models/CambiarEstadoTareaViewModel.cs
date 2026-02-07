using System.ComponentModel.DataAnnotations;
using TSGTaskBoard.Domain.DTO;

namespace TSGTaskBoard.UI.Models
{
    public class CambiarEstadoTareaViewModel
    {
        [Required]
        public string Estado { get; set; } = string.Empty;
    }
}
