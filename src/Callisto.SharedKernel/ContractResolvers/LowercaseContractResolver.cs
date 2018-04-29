using Newtonsoft.Json.Serialization;
using System;

namespace Callisto.SharedKernel.ContractResolvers
{
    /// <summary>
    /// Defines the <see cref="LowercaseContractResolver" />
    /// </summary>
    public class LowercaseContractResolver : DefaultContractResolver
    {
        /// <summary>
        /// The ResolvePropertyName
        /// </summary>
        /// <param name="propertyName">The <see cref="string"/></param>
        /// <returns>The <see cref="string"/></returns>
        protected override string ResolvePropertyName(string propertyName)
        {
            return Char.ToLowerInvariant(propertyName[0]) + propertyName.Substring(1);
        }
    }
}
