using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Plugin.Widgets.SwiperSlider.Factories;
using Nop.Web.Framework.Components;

namespace Nop.Plugin.Widgets.SwiperSlider.Components
{
    [ViewComponent(Name = "SwiperSlider")]
    public class SwiperSliderViewComponent : NopViewComponent
    {
        #region Fields
        private readonly ISwiperSliderViewModelFactory _swiperSliderViewModelFactory;
        #endregion

        #region Ctor
        public SwiperSliderViewComponent
        (
            ISwiperSliderViewModelFactory swiperSliderViewModelFactory
        )
        {
            _swiperSliderViewModelFactory = swiperSliderViewModelFactory;
        }
        #endregion

        public async Task<IViewComponentResult> InvokeAsync(string widgetZone, object additionalData)
        {
            var model = await _swiperSliderViewModelFactory.GetAllSliders();
            return View(model);
        }
    }
}
