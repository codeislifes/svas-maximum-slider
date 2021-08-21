using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace codeislife.Widgets.CilSlider.Components
{
    [ViewComponent(Name = "CilSlider")]
    public class CilSliderViewComponent : NopViewComponent
    {
        #region Fields
        private readonly CilSliderSettings _cilSliderSettings;
        #endregion

        #region Ctor
        public CilSliderViewComponent(CilSliderSettings cilSliderSettings)
        {
            _cilSliderSettings = cilSliderSettings;
        }
        #endregion

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View("~/Plugins/codeislife.Widgets.CilSlider/Views/PublicInfo.cshtml", _cilSliderSettings);
        }
    }
}
