using Microsoft.AspNetCore.Mvc;

namespace E_commerce.Controllers;

public static class ControllerExtensions
{
    // TempData keys shared by controllers and the layout
    public const string SkipVectorUpdate = "SkipVectorUpdate";
    public const string OpenCart = "OpenCart";

    // cookie that stores the "show weights" setting
    public const string ShowWeightsCookie = "ShowWeights";

    // redirect to the page the user came from (only inside this site)
    public static IActionResult RedirectBack(this Controller controller, string fallback = "/")
    {
        var referer = controller.Request.Headers.Referer.ToString();
        if (Uri.TryCreate(referer, UriKind.Absolute, out var uri) && uri.Host == controller.Request.Host.Host)
        {
            return controller.LocalRedirect(uri.PathAndQuery);
        }
        return controller.LocalRedirect(fallback);
    }
}
