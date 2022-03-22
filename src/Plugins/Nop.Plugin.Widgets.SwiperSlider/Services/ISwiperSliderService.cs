using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;

namespace Nop.Plugin.Widgets.SwiperSlider.Services
{
    public interface ISwiperSliderService
    {
        #region Slider
        Task<IPagedList<Data.Domain.Slider>> GetAllSlidersAsync(string name,
            int storeId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false,
            bool? overridePublished = null);

        Task<Data.Domain.Slider> GetSliderByIdAsync(int sliderId);

        Task<IList<Data.Domain.Slider>> GetSliderByIdsAsync(ICollection<int> sliderIds);
        Task InsertSliderAsync(Data.Domain.Slider slider);
        Task UpdateSliderAsync(Data.Domain.Slider slider);
        Task DeleteSliderAsync(Data.Domain.Slider slider);
        Task DeleteSliderAsync(IList<Data.Domain.Slider> sliders);
        #endregion

        #region Slider Items
        Task<IList<SliderItem>> GetAllSliderItemsBySliderIdAsync(int sliderId);
        Task<SliderItem> GetSliderItemByIdAsync(int sliderItemId);
        Task InsertSliderItemAsync(SliderItem sliderItem);
        Task UpdateSliderItemAsync(SliderItem sliderItem);
        Task DeleteSliderItemAsync(SliderItem sliderItem);
        #endregion

    }
}
