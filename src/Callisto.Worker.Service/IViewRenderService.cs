using System.Threading.Tasks;

namespace Callisto.Worker.Service
{
    /// <summary>
    /// Defines the <see cref="IViewRenderService" />
    /// </summary>
    public interface IViewRenderService
    {
        /// <summary>
        /// The RenderToStringAsync
        /// </summary>
        /// <param name="viewName">The <see cref="string"/></param>
        /// <param name="model">The <see cref="object"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        Task<string> RenderToStringAsync(string viewName, object model);
    }
}
