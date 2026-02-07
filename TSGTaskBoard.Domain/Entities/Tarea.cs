using TSGTaskBoard.Domain.Enums;

namespace TSGTaskBoard.Domain.Entities
{
    public class Tarea
    {
        public int Id { get; set; }

        public string Titulo { get; set; } = string.Empty;

        public string? Descripcion { get; set; }

        public string Categoria { get; set; } = string.Empty;

        public EstadoTareaEnum Estado { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime? FechaVencimiento { get; set; }
    }
}
