using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace codeislife.Widgets.CilSlider.Models
{
    public class SliderSearchModel : BaseSearchModel, IPagingRequestModel
    {
        #region Ctor

        public SliderSearchModel()
        {
            AvailableStores = new List<SelectListItem>();
            AvailablePublishedOptions = new List<SelectListItem>();
        }

        #endregion

        #region Properties

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.List.SearchCategoryName")]
        public string SearchSliderName { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.List.SearchPublished")]
        public int SearchPublishedId { get; set; }

        public IList<SelectListItem> AvailablePublishedOptions { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.List.SearchStore")]
        public int SearchStoreId { get; set; }

        public IList<SelectListItem> AvailableStores { get; set; }

        public bool HideStoresList { get; set; }

        #endregion
    }
}
