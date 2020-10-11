using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;

namespace codeislife.Widgets.CilSlider.Controllers
{
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class CilSliderController : BasePluginController
    {
        public CilSliderController()
        {
        }

        public IActionResult Configure()
        {
            return View("~/Plugins/codeislife.Widgets.CilSlider/Views/Configure.cshtml");
        }
    }
}
