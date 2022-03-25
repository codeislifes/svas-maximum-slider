using System.Threading.Tasks;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Models;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Factories
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
