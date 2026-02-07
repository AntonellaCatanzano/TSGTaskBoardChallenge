using Microsoft.AspNetCore.Mvc;
using TSGTaskBoard.UI.Models;
using TSGTaskBoard.UI.Services.Interfaces;

namespace TSGTaskBoard.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITareaHttpService _tareaService;

        public HomeController(ITareaHttpService tareaService)
        {
            _tareaService = tareaService;
        }

        public async Task<IActionResult> Index()
        {
            // Obtener todas las tareas desde la API
            var tareasDto = await _tareaService.GetAllAsync();

            // Mapear DTO a ViewModel
            var tareasVm = tareasDto.Select(t => new TareaViewModel
            {
                Id = t.Id,
                Titulo = t.Titulo,
                Descripcion = t.Descripcion,
                Categoria = t.Categoria,
                Estado = t.Estado,
                FechaInicio = t.FechaInicio,
                FechaVencimiento = t.FechaVencimiento
            }).ToList();

            // Organizar por columnas del tablero Kanban
            var kanbanVm = new KanbanDashboardViewModel
            {
                Backlog = tareasVm.Where(t => t.Estado == "Backlog").ToList(),
                ToDo = tareasVm.Where(t => t.Estado == "ToDo").ToList(),
                InProgress = tareasVm.Where(t => t.Estado == "InProgress").ToList(),
                Done = tareasVm.Where(t => t.Estado == "Done").ToList()
            };

            return View(kanbanVm);
        }
    }
}


