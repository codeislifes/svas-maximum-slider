using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;
using System.Collections.Generic;

namespace codeislife.Widgets.CilSlider.Models
{
    public class SliderModel : BaseNopEntityModel, IStoreMappingSupportedModel, IAclSupportedModel
    {
        public SliderModel()
        {
            AvailableCustomerRoles = new List<SelectListItem>();
            AvailableStores = new List<SelectListItem>();
        }

        public string Name { get; set; }
        public int DisplayOrder { get; set; }
        public bool Published { get; set; }

        //ACL (customer roles)
        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.Fields.AclCustomerRoles")]
        public IList<int> SelectedCustomerRoleIds { get; set; }
        public IList<SelectListItem> AvailableCustomerRoles { get; set; }


        //store mapping
        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Sliders.Fields.LimitedToStores")]
        public IList<int> SelectedStoreIds { get; set; }
        public IList<SelectListItem> AvailableStores { get; set; }
    }
}
