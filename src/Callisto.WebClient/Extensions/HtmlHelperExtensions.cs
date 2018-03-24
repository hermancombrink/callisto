using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Callisto.WebClient.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static string PageClass(this IHtmlHelper htmlHelper)
        {
            string currentAction = (string)htmlHelper.ViewContext.RouteData.Values["action"];
            return currentAction;
        }

        public static string IsSelected(this IHtmlHelper html, string controller = null, string action = null, string cssClass = null)
        {
            if (String.IsNullOrEmpty(cssClass))
                cssClass = "active";

            string currentAction = (string)html.ViewContext.RouteData.Values["action"];
            string currentController = (string)html.ViewContext.RouteData.Values["controller"];

            if (String.IsNullOrEmpty(controller))
                controller = currentController;

            if (String.IsNullOrEmpty(action))
                action = currentAction;

            return controller.ToLower() == currentController.ToLower() && action.ToLower() == currentAction.ToLower() ?
                cssClass : String.Empty;
        }
    }
}
