using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Services
{
    public interface ISwiperSliderService
    {
        #region Slider
        Task<IPagedList<Slider>> GetAllSlidersAsync(
            string name = null,
            int storeId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false,
            bool? overridePublished = null);

        Task<Slider> GetSliderByIdAsync(int sliderId);
        Task<IList<Slider>> GetSliderByIdsAsync(ICollection<int> sliderIds);
        Task InsertSliderAsync(Slider slider);
        Task UpdateSliderAsync(Slider slider);
        Task DeleteSliderAsync(Slider slider);
        Task DeleteSliderAsync(IList<Slider> sliders);
        #endregion

        #region Slider Items
        Task<IPagedList<SliderItem>> GetAllSliderItemsAsync(
            int[] sliderIds = null,
            int storeId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false,
            bool? overridePublished = null);
        Task<SliderItem> GetSliderItemByIdAsync(int sliderItemId);
        Task<IList<SliderItem>> GetSliderItemByIdsAsync(ICollection<int> sliderItemIds);
        Task InsertSliderItemAsync(SliderItem sliderItem);
        Task UpdateSliderItemAsync(SliderItem sliderItem);
        Task DeleteSliderItemAsync(SliderItem sliderItem);
        Task DeleteSliderItemAsync(IList<SliderItem> sliderItems);
        #endregion

    }
}
