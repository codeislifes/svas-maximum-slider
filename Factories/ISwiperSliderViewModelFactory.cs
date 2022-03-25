using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Plugin.Widgets.SwiperSlider.Models;

namespace Nop.Plugin.Widgets.SwiperSlider.Factories
{
    public interface ISwiperSliderViewModelFactory
    {
        Task<List<SwiperSliderViewModel>> GetAllSliders();
    }
}
