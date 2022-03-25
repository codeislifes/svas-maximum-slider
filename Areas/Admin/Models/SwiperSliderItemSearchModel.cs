using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Models
{
    public record SwiperSliderItemSearchModel : BaseSearchModel, IPagingRequestModel
    {
        #region Properties
        public int SliderId { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.SliderItems.List.SearchSliderItemName")]
        public string SearchSliderItemName { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.SliderItems.List.SearchPublished")]
        public int SearchPublishedId { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.SliderItems.List.SearchStore")]
        public int SearchStoreId { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }

        public bool HideStoresList { get; set; }
        #endregion
    }
}
