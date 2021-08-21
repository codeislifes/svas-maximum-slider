using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace codeislife.Widgets.CilSlider.Models
{
    public partial class SliderModel : 
        BaseNopEntityModel, 
        IStoreMappingSupportedModel, 
        IAclSupportedModel
    {
        public SliderModel()
        {
            SelectedCustomerRoleIds = new List<int>();
            AvailableCustomerRoles = new List<SelectListItem>();

            SelectedStoreIds = new List<int>();
            AvailableStores = new List<SelectListItem>();
        }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.Fields.Name")]
        public string Name { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.Fields.Published")]
        public bool Published { get; set; }

        //ACL (customer roles)
        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.Fields.AclCustomerRoles")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }


        // Store mapping
        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.Fields.LimitedToStores")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
    }
}
