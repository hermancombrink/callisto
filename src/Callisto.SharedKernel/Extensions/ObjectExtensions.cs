using Callisto.SharedKernel.ContractResolvers;
using Callisto.SharedKernel.Enum;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

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
        /// The Validate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj">The <see cref="T"/></param>
        /// <param name="result">The <see cref="string"/></param>
        /// <returns>The <see cref="(bool isValid, IEnumerable{ValidationResult} results)"/></returns>
        public static (bool isValid, IEnumerable<ValidationResult> results) Validate<T>(this T obj, out string result)
        {
            var context = new ValidationContext(obj, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, context, results, true);
            result = results.FirstOrDefault()?.ErrorMessage ?? string.Empty;
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
        public static string ToJson<T>(this T source, bool includeNull = true, JsonContractResolver camelCase = JsonContractResolver.Default, bool enumToString = false, Formatting? formatting = null)
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

        /// <summary>
        /// The FromJson
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The <see cref="string"/></param>
        /// <param name="includeNull">The <see cref="bool"/></param>
        /// <param name="camelCase">The <see cref="JsonContractResolver"/></param>
        /// <param name="enumToString">The <see cref="bool"/></param>
        /// <param name="formatting">The <see cref="Formatting?"/></param>
        /// <returns>The <see cref="T"/></returns>
        public static T FromJson<T>(this string source, bool includeNull = true, JsonContractResolver camelCase = JsonContractResolver.Default, bool enumToString = false, Formatting? formatting = null)
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
            return JsonConvert.DeserializeObject<T>(source, settings);
        }

        /// <summary>
        /// The CopyProperties
        /// </summary>
        /// <param name="source">The <see cref="object"/></param>
        /// <param name="destination">The <see cref="object"/></param>
        public static void CopyProperties(this object source, object destination)
        {
            // If any this null throw an exception
            if (source == null || destination == null)
                return;
            // Getting the Types of the objects
            var typeDest = destination.GetType();
            var typeSrc = source.GetType();
            foreach (var srcProp in typeSrc.GetProperties())
            {
                foreach (var trgProp in typeDest.GetProperties().Where(c => c.Name == srcProp.Name).ToList())
                {
                    if (!srcProp.CanRead)
                        continue;
                    if (trgProp.GetSetMethod(true) == null)
                        continue;
                    if (trgProp.GetSetMethod(true).IsPrivate)
                        continue;
                    if (!trgProp.PropertyType.IsAssignableFrom(Nullable.GetUnderlyingType(srcProp.PropertyType) ?? srcProp.PropertyType))
                        continue;
                    if ((trgProp.GetSetMethod().Attributes & MethodAttributes.Static) != 0)
                        continue;

                    var value = srcProp.GetValue(source, null);

                    if (Nullable.GetUnderlyingType(srcProp.PropertyType) != null &&
                    Nullable.GetUnderlyingType(trgProp.PropertyType) == null && value == null)
                        continue;

                    try
                    {
                        trgProp.SetValue(destination, value);
                    }
                    catch (Exception)
                    {
                    }

                }
            }
        }
    }
}
