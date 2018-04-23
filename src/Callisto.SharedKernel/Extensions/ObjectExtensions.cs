using Callisto.SharedKernel.ContractResolvers;
using Callisto.SharedKernel.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
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

        /// <summary>
        /// The ToJson
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The <see cref="T"/></param>
        /// <param name="includeNull">The <see cref="bool"/></param>
        /// <param name="camelCase">The <see cref="JsonContractResolver"/></param>
        /// <param name="enumToString">The <see cref="bool"/></param>
        /// <param name="formatting">The <see cref="Newtonsoft.Json.Formatting?"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string ToJson<T>(this T source, bool includeNull = true, JsonContractResolver camelCase = JsonContractResolver.Default, bool enumToString = false, Newtonsoft.Json.Formatting? formatting = null)
        {
            var converter = new JsonConverter[] { };
            if (enumToString)
                converter = new JsonConverter[] { new StringEnumConverter() };
            IContractResolver resolver = new DefaultContractResolver();
            switch (camelCase)
            {
                case JsonContractResolver.CamelCase:
                    {
                        resolver = new CamelCasePropertyNamesContractResolver();
                        break;
                    }
                case JsonContractResolver.SnakeCase:
                    {
                        resolver = new SnakeCasePropertyNamesContractResolver();
                        break;
                    }
                case JsonContractResolver.LowerCase:
                    {
                        resolver = new LowercaseContractResolver();
                        break;
                    }
            }

            var settings = new JsonSerializerSettings()
            {
                ContractResolver = resolver,
                Converters = converter,
                NullValueHandling = includeNull ? NullValueHandling.Include : NullValueHandling.Ignore
            };
            if (formatting == null)
                return JsonConvert.SerializeObject(source, settings);
            else
                return JsonConvert.SerializeObject(source, formatting.Value, settings);
        }
    }
}
