using TSGTaskBoard.Domain.Enums;

public static class EstadoTareaParser
{
    public static EstadoTareaEnum ToEnum(string estado)
    {
        if (!Enum.TryParse<EstadoTareaEnum>(
                estado,
                ignoreCase: true,
                out var result))
        {
            throw new ArgumentException(
                $"Estado inválido. Valores permitidos: {string.Join(", ", Enum.GetNames(typeof(EstadoTareaEnum)))}");
        }

        return result;
    }
}
