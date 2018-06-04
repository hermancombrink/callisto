using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Callisto.Worker.Service.Controllers
{
    /// <summary>
    /// Defines the <see cref="RenderController" />
    /// </summary>
    [Route("render")]
    public class RenderController : Controller
    {
        /// <summary>
        /// Defines the _viewRenderService
        /// </summary>
        private readonly IViewRenderService _viewRenderService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenderController"/> class.
        /// </summary>
        /// <param name="viewRenderService">The <see cref="IViewRenderService"/></param>
        public RenderController(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }

        /// <summary>
        /// The RenderTemplate
        /// </summary>
        /// <returns>The <see cref="Task{IActionResult}"/></returns>
        [Route("email/{view}")]
        public async Task<IActionResult> RenderTemplate(string view, Dictionary<string, string> tokens = null)
        {
            var result = await _viewRenderService.RenderToStringAsync($"email/{view}", tokens);
            return Content(result, "text/html");
        }
    }
}
