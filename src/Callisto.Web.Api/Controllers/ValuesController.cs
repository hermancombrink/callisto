using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Callisto.Web.Api.Controllers
{
    /// <summary>
    /// Defines the <see cref="ValuesController" />
    /// </summary>
    [Route("[controller]")]
    [Authorize]
    public class ValuesController : Controller
    {
        // GET api/values
        /// <summary>
        /// The Get
        /// </summary>
        /// <returns>The <see cref="IEnumerable{string}"/></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        /// <summary>
        /// The Get
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <returns>The <see cref="string"/></returns>
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        /// <summary>
        /// The Post
        /// </summary>
        /// <param name="value">The <see cref="string"/></param>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        /// <summary>
        /// The Put
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        /// <param name="value">The <see cref="string"/></param>
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        /// <summary>
        /// The Delete
        /// </summary>
        /// <param name="id">The <see cref="int"/></param>
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
