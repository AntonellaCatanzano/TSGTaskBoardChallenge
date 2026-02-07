using TSGTaskBoard.Domain.Enums;

namespace TSGTaskBoard.Domain.Rules
{
    public static class EstadoTareaRules
    {
        private static readonly Dictionary<EstadoTareaEnum, EstadoTareaEnum[]> TransicionesValidas =
            new()
            {
                { EstadoTareaEnum.Backlog, new[] { EstadoTareaEnum.ToDo } },
                { EstadoTareaEnum.ToDo, new[] { EstadoTareaEnum.InProgress } },
                { EstadoTareaEnum.InProgress, new[] { EstadoTareaEnum.Done } },
                { EstadoTareaEnum.Done, Array.Empty<EstadoTareaEnum>() }
            };

        public static bool PuedeTransicionar(
            EstadoTareaEnum estadoActual,
            EstadoTareaEnum nuevoEstado)
        {
            return TransicionesValidas.TryGetValue(estadoActual, out var permitidos)
                   && permitidos.Contains(nuevoEstado);
        }

        public static EstadoTareaEnum[] GetTransiciones(EstadoTareaEnum estadoActual)
        {
            return TransicionesValidas.TryGetValue(estadoActual, out var permitidos)
                ? permitidos
                : Array.Empty<EstadoTareaEnum>();
        }
    }
}
