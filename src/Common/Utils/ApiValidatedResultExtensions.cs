using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hydrogen.General.Validation;

namespace Arvan.PowerShell.Common.Utils
{
    public static class ApiValidatedResultExtensions
    {
        public static T EnsureSuccess<T>(this Task<ApiValidatedResult<T>> resultTask)
        {
            if (resultTask == null)
                throw new ArgumentException("An API invocation has returned a NULL result instead of Task");
            
            var validatedResult = resultTask.GetAwaiter().GetResult();
            if (!validatedResult.Success)
                throw new InvalidOperationException($"An API invocation was not successful, {validatedResult.ToErrorString()}");

            return validatedResult.Result;
        }

        public static T ResultIfSuccess<T>(this Task<ApiValidatedResult<T>> resultTask)
        {
            if (resultTask == null)
                throw new ArgumentException("An API invocation has returned a NULL result instead of Task");
            
            var validatedResult = resultTask.GetAwaiter().GetResult();
            return validatedResult.Success ? validatedResult.Result : default(T);
        }

        public static string ToErrorString(this ApiValidationResult validationResult)
        {
            if (validationResult.Success)
                return string.Empty;
            
            var result = new StringBuilder();

            if (!string.IsNullOrWhiteSpace(validationResult.Message))
            {
                result.Append(validationResult.Message).Append("; ");
            }

            result.Append(string.Join("; ", validationResult.Errors.Select(e => e.ToErrorString())));
            return result.ToString();
        }

        public static string ToErrorString(this ApiValidationError error)
        {
            var result = $"[{error.ErrorKey ?? "-"}]";

            if (!string.IsNullOrWhiteSpace(error.PropertyPath))
                result += $" ({error.PropertyPath})";

            if (!string.IsNullOrWhiteSpace(error.LocalizedMessage))
                result += $" {error.LocalizedMessage}";
            
            return result;
        }
    }
}