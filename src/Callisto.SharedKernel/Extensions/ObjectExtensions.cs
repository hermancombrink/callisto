using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Callisto.SharedKernel.Extensions
{
    /// <summary>
    /// Defines the <see cref="ObjectExtensions" />
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// The IsValid
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The <see cref="T"/></param>
        /// <returns>The <see cref="(bool isValid, IEnumerable{ValidationResult} results)"/></returns>
        public static (bool isValid, IEnumerable<ValidationResult> results) Validate<T>(this T obj)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, context, results, true);
            return (isValid, results);
        }
    }
}
