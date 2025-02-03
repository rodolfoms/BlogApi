using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace BlogApi.Extensions
{
    public static class ModelStateExtension
    {
        public static List<string> GetErrors(this ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var item in modelState.Values)
            //{
                //foreach (var error in item.Errors)
                //{
                //    errors.Add(error.ErrorMessage);
                //}
                errors.AddRange(item.Errors.Select(x => x.ErrorMessage));
            //}
            return errors;

        }

    }
}
