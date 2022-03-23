using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.SwiperSlider.Models
{
    public record SwiperSliderSearchModel : BaseSearchModel, IPagingRequestModel
    {
        #region Ctor

        public SwiperSliderSearchModel()
        {
            AvailableStores = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Sliders.List.SearchSliderName")]
        public string SearchSliderName { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Sliders.List.SearchPublished")]
        public int SearchPublishedId { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Sliders.List.SearchStore")]
        public int SearchStoreId { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }

        public bool HideStoresList { get; set; }

        #endregion
    }
}
