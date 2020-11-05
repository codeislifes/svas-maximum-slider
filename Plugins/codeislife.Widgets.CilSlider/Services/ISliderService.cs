using codeislife.Widgets.CilSlider.Data.Domain;
using Nop.Core;
using System.Collections.Generic;

namespace codeislife.Widgets.CilSlider.Services
{
    public interface ISliderService
    {
        #region Slider
        IPagedList<Slider> GetAllSliders(string name, int storeId = 0, int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false, bool? overridePublished = null);
        Slider GetSliderById(int sliderId);
        void InsertSlider(Slider slider);
        void UpdateSlider(Slider slider);
        void DeleteSlider(Slider slider);
        #endregion

        #region Slider Items
        IList<SliderItem> GetAllSliderItemsBySliderId(int sliderId);
        SliderItem GetSliderItemById(int sliderItemId);
        void InsertSliderItem(SliderItem sliderItem);
        void UpdateSliderItem(SliderItem sliderItem);
        void DeleteSliderItem(SliderItem sliderItem);
        #endregion

    }
}
