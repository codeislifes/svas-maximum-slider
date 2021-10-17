using System.Collections.Generic;
using System.Threading.Tasks;
using codeislife.Widgets.CilSlider.Data.Domain;
using Nop.Core;

namespace codeislife.Widgets.CilSlider.Services
{
    public interface ISliderService
    {
        #region Slider
        Task<IPagedList<Slider>> GetAllSlidersAsync(string name,
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
        Task<IList<SliderItem>> GetAllSliderItemsBySliderIdAsync(int sliderId);
        Task<SliderItem> GetSliderItemByIdAsync(int sliderItemId);
        Task InsertSliderItemAsync(SliderItem sliderItem);
        Task UpdateSliderItemAsync(SliderItem sliderItem);
        Task DeleteSliderItemAsync(SliderItem sliderItem);
        #endregion

    }
}
