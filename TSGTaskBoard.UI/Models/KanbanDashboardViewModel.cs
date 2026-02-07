using TSGTaskBoard.Domain.DTO;

namespace TSGTaskBoard.UI.Models
{
    public class KanbanDashboardViewModel
    {
        public List<TareaViewModel> Backlog { get; set; } = new();
        public List<TareaViewModel> ToDo { get; set; } = new();
        public List<TareaViewModel> InProgress { get; set; } = new();
        public List<TareaViewModel> Done { get; set; } = new();
    }
}
