using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace codeislife.Widgets.CilSlider.Models
{
    public class CilSliderConfigureModel : BaseNopModel
    {
        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.ContainerCssSelector")]
        public string ContainerCssSelector { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.PaginationCssSelector")]
        public string PaginationCssSelector { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.NavigationNextCssSelector")]
        public string NavigationNextCssSelector { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.NavigationPrevCssSelector")]
        public string NavigationPrevCssSelector { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.ScrollBarCssSelector")]
        public string ScrollBarCssSelector { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.Direction")]
        public Direction Direction { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.InitialSlide")]
        public int InitialSlide { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.Speed")]
        public int Speed { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.Loop")]
        public bool Loop { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.PaginationEnabled")]
        public bool PaginationEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.NavigationEnabled")]
        public bool NavigationEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.ScrollBarEnabled")]
        public bool ScrollBarEnabled { get; set; }
    }
}
