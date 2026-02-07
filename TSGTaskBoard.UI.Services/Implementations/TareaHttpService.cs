using System.Net.Http;
using System.Net.Http.Json;
using TSGTaskBoard.Domain.DTO;
using TSGTaskBoard.UI.Services.Interfaces;

namespace TSGTaskBoard.UI.Services.Implementations
{
    public class TareaHttpService : ITareaHttpService
    {
        private readonly HttpClient _http;

        public TareaHttpService(HttpClient http) => _http = http;

        // Obtener todas las tareas
        public async Task<List<TareaDTO>> GetAllAsync()
            => await _http.GetFromJsonAsync<List<TareaDTO>>("api/tareas") ?? new List<TareaDTO>();

        // Obtener tarea por ID
        public async Task<TareaDTO?> GetByIdAsync(int id)
            => await _http.GetFromJsonAsync<TareaDTO>($"api/tareas/{id}");

        // Crear nueva tarea
        public async Task<TareaDTO> CreateAsync(TareaCreateDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/tareas/create", dto);
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadFromJsonAsync<TareaDTO>())!;
        }

        // Actualizar tarea existente
        public async Task<TareaDTO?> UpdateAsync(int id, TareaUpdateDTO dto)
        {
            var response = await _http.PutAsJsonAsync($"api/tareas/update/{id}", dto);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<TareaDTO>();
        }

        // Eliminar tarea
        public async Task<bool> DeleteAsync(int id)
        {
            var response = await _http.DeleteAsync($"api/tareas/delete/{id}");
            return response.IsSuccessStatusCode;
        }

        // Cambiar estado de la tarea
        public async Task CambiarEstadoAsync(int id, CambiarEstadoTareaDTO dto)
        {
            var response = await _http.PatchAsJsonAsync(
                $"api/tareas/{id}/estados-tarea",
                dto
            );
            response.EnsureSuccessStatusCode();
        }
    }
}
