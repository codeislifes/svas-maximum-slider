using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Models
{
    public partial record SwiperSliderItemModel :
        BaseNopEntityModel,
        IStoreMappingSupportedModel,
        IAclSupportedModel
    {
        public SwiperSliderItemModel()
        {
            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
        }

        public int SliderId { get; set; }
        public string PictureThumbnailUrl { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Slider")]
        public string SliderName { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Url")]
        public string Url { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.AlternateText")]
        public string AlternateText { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Picture")]
        [UIHint("Picture")]
        public int PictureId { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Published")]
        public bool Published { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.AclCustomerRoles")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.LimitedToStores")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
    }
}
