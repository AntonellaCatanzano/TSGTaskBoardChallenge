using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace TSGTaskBoard.API.Support.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            return modelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.Exception?.Message ?? e.ErrorMessage)
                .ToList();
        }

        public static Dictionary<string, string[]> GetErrorsWithFields(this ModelStateDictionary modelState)
        {
            return modelState
                .Where(ms => ms.Value.Errors.Any())
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors
                        .Select(e => e.Exception?.Message ?? e.ErrorMessage)
                        .ToArray()
                );
        }
    }
}
