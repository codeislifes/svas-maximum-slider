using System.Threading.Tasks;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
using Nop.Plugin.Widgets.SwiperSlider.Models;

namespace Nop.Plugin.Widgets.SwiperSlider.Factories
{
    public interface ISwiperSliderModelFactory
    {
        Task<SwiperSliderModel> PrepareSliderModelAsync(SwiperSliderModel model, Data.Domain.Slider entity);
    }
}
