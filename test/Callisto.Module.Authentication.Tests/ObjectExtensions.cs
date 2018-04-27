using Callisto.SharedKernel;
using Callisto.SharedKernel.Extensions;
using System.Net.Http;
using System.Text;

namespace Callisto.Module.Authentication.Tests
{
    /// <summary>
    /// Defines the <see cref="ObjectExtensions" />
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// The ToContent
        /// </summary>
        /// <param name="Json">The <see cref="string"/></param>
        /// <returns>The <see cref="StringContent"/></returns>
        public static StringContent ToContent(this string Json)
        {
            return new StringContent(Json, Encoding.UTF8, "application/json");
        }

        /// <summary>
        /// The ToRequestResult
        /// </summary>
        /// <param name="response">The <see cref="HttpResponseMessage"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult ToRequestResult(this HttpResponseMessage response)
        {
            var body = response.Content.ReadAsStringAsync().Result;
            return body.FromJson<RequestResult>();
        }

        /// <summary>
        /// The ToRequestResult
        /// </summary>
        /// <param name="response">The <see cref="HttpResponseMessage"/></param>
        /// <returns>The <see cref="RequestResult"/></returns>
        public static RequestResult<T> ToRequestResult<T>(this HttpResponseMessage response)
        {
            var body = response.Content.ReadAsStringAsync().Result;
            return body.FromJson<RequestResult<T>>();
        }
    }
}
