using TSGTaskBoard.Domain.Enums;

namespace TSGTaskBoard.Domain.DTO
{
    public class TareaDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
        public string? Descripcion { get; set; }
        public string Categoria { get; set; } = default!;
        public string Estado { get; set; } = default!; 
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaVencimiento { get; set; }

    }
}
