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
        public int DirectionId { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.Direction")]
        public Direction Direction { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.InitialSlide")]
        public int InitialSlide { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.Speed")]
        public int Speed { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.Loop")]
        public bool Loop { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.LoopFillGroupWithBlankEnabled")]
        public bool LoopFillGroupWithBlankEnabled { get; set; }


        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.PaginationEnabled")]
        public bool PaginationEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.PaginationClickableEnabled")]
        public bool PaginationClickableEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.NavigationEnabled")]
        public bool NavigationEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.ScrollBarEnabled")]
        public bool ScrollBarEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.AutoPlayEnabled")]
        public bool AutoPlayEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.AutoPlayDelay")]
        public int AutoPlayDelay { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.AutoPlayDisableOnInteraction")]
        public bool AutoPlayDisableOnInteraction { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.SlidesPerGroup")]
        public int SlidesPerGroup { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.SpaceBetween")]
        public int SpaceBetween { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.SlidesPerView")]
        public string SlidesPerView { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.SlidesPerColumn")]
        public int SlidesPerColumn { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.FreeModeEnabled")]
        public bool FreeModeEnabled { get; set; }


        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.DynamicBulletsEnabled")]
        public bool DynamicBulletsEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.CenteredSlidesEnabled")]
        public bool CenteredSlidesEnabled { get; set; }

        [NopResourceDisplayName("codeislife.Widgets.CilSlider.Models.CustomCss")]
        public string CustomCss { get; set; }
    }
}
