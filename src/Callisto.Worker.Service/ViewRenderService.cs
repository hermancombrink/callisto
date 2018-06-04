using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Callisto.Worker.Service
{
    /// <summary>
    /// Defines the <see cref="ViewRenderService" />
    /// </summary>
    public class ViewRenderService : IViewRenderService
    {
        /// <summary>
        /// Defines the _razorViewEngine
        /// </summary>
        private readonly IRazorViewEngine _razorViewEngine;

        /// <summary>
        /// Defines the _tempDataProvider
        /// </summary>
        private readonly ITempDataProvider _tempDataProvider;

        /// <summary>
        /// Defines the _serviceProvider
        /// </summary>
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Gets the TemplateOptions
        /// </summary>
        public TemplateOptions TemplateOptions { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewRenderService"/> class.
        /// </summary>
        /// <param name="razorViewEngine">The <see cref="IRazorViewEngine"/></param>
        /// <param name="tempDataProvider">The <see cref="ITempDataProvider"/></param>
        /// <param name="serviceProvider">The <see cref="IServiceProvider"/></param>
        public ViewRenderService(IRazorViewEngine razorViewEngine,
            ITempDataProvider tempDataProvider,
            IServiceProvider serviceProvider,
            IOptions<TemplateOptions> templateOptions)
        {
            _razorViewEngine = razorViewEngine;
            _tempDataProvider = tempDataProvider;
            _serviceProvider = serviceProvider;
            TemplateOptions = templateOptions.Value;
        }

        /// <summary>
        /// The RenderToStringAsync
        /// </summary>
        /// <param name="viewName">The <see cref="string"/></param>
        /// <param name="model">The <see cref="object"/></param>
        /// <returns>The <see cref="Task{string}"/></returns>
        public async Task<string> RenderToStringAsync(string viewName, object model)
        {
            var httpContext = new DefaultHttpContext { RequestServices = _serviceProvider };
            var actionContext = new ActionContext(httpContext, new RouteData(), new ActionDescriptor());

            using (var sw = new StringWriter())
            {
                var viewResult = _razorViewEngine.FindView(actionContext, viewName, false);

                if (viewResult.View == null)
                {
                    throw new ArgumentNullException($"{viewName} does not match any available view");
                }

                var viewDictionary = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
                {
                    Model = model
                };

                var viewContext = new ViewContext(
                    actionContext,
                    viewResult.View,
                    viewDictionary,
                    new TempDataDictionary(actionContext.HttpContext, _tempDataProvider),
                    sw,
                    new HtmlHelperOptions()
                );

                viewContext.ViewBag.Options = TemplateOptions;

                await viewResult.View.RenderAsync(viewContext);
                return sw.ToString();
            }
        }
    }
}
