using Newtonsoft.Json.Serialization;

namespace Callisto.SharedKernel.ContractResolvers
{
    /// <summary>
    /// Defines the <see cref="SnakeCasePropertyNamesContractResolver" />
    /// </summary>
    public class SnakeCasePropertyNamesContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// The ResolvePropertyName
        /// </summary>
        /// <param name="propertyName">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            var startUnderscores = System.Text.RegularExpressions.Regex.Match(propertyName, @"^_+");
            return startUnderscores + System.Text.RegularExpressions.Regex
              .Replace(propertyName, @"([A-Z0-9])", "_$1").ToLower().TrimStart('_');
        }
    }
}
