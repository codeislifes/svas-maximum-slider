using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.SwiperSlider.Models
{
    public partial record SwiperSliderModel :
        BaseNopEntityModel,
        IStoreMappingSupportedModel,
        IAclSupportedModel
    {
        public SwiperSliderModel()
        {
            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
            SwiperSliderItemSearchModel = new SwiperSliderItemSearchModel();
        }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Published")]
        public bool Published { get; set; }

        //ACL (customer roles)
        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.AclCustomerRoles")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }

        // Store mapping
        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.LimitedToStores")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }

        public SwiperSliderItemSearchModel SwiperSliderItemSearchModel { get; set; }
    }
}
