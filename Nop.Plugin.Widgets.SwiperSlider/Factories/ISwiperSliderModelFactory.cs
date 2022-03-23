using System.Threading.Tasks;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
using Nop.Plugin.Widgets.SwiperSlider.Models;

namespace Nop.Plugin.Widgets.SwiperSlider.Factories
{
    public interface ISwiperSliderModelFactory
    {
        Task<SwiperSliderSearchModel> PrepareSliderSearchModelAsync(SwiperSliderSearchModel searchModel);
        Task<SwiperSliderListModel> PrepareSliderListModelAsync(SwiperSliderSearchModel searchModel);
        Task<SwiperSliderModel> PrepareSliderModelAsync(SwiperSliderModel model, Slider entity);

        Task<SwiperSliderItemSearchModel> PrepareSliderItemSearchModelAsync(SwiperSliderItemSearchModel searchModel, Slider slider);
        Task<SwiperSliderItemListModel> PrepareSliderItemListModelAsync(SwiperSliderItemSearchModel searchModel);
        Task<SwiperSliderItemModel> PrepareSliderItemModelAsync(SwiperSliderItemModel model, SliderItem sliderItem);
    }
}
