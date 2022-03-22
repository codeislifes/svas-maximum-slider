using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.SwiperSlider.Components
{
    [ViewComponent(Name = "SwiperSlider")]
    public class SwiperSliderViewComponent : NopViewComponent
    {
        #region Fields
        private readonly SwiperSliderSettings _cilSliderSettings;
        #endregion

        #region Ctor
        public SwiperSliderViewComponent(SwiperSliderSettings cilSliderSettings)
        {
            _cilSliderSettings = cilSliderSettings;
        }
        #endregion

        public IViewComponentResult Invoke(string widgetZone, object additionalData)
        {
            return View(_cilSliderSettings);
        }
    }
}
