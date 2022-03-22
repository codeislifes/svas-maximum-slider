using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.SwiperSlider.Models
{
    public record SwiperSliderConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.ContainerCssSelector")]
        public string ContainerCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.PaginationCssSelector")]
        public string PaginationCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.NavigationNextCssSelector")]
        public string NavigationNextCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.NavigationPrevCssSelector")]
        public string NavigationPrevCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.ScrollBarCssSelector")]
        public string ScrollBarCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.Direction")]
        public int DirectionId { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.Direction")]
        public Direction Direction { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.InitialSlide")]
        public int InitialSlide { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.Speed")]
        public int Speed { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.Loop")]
        public bool Loop { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.LoopFillGroupWithBlankEnabled")]
        public bool LoopFillGroupWithBlankEnabled { get; set; }


        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.PaginationEnabled")]
        public bool PaginationEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.PaginationClickableEnabled")]
        public bool PaginationClickableEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.NavigationEnabled")]
        public bool NavigationEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.ScrollBarEnabled")]
        public bool ScrollBarEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.AutoPlayEnabled")]
        public bool AutoPlayEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.AutoPlayDelay")]
        public int AutoPlayDelay { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.AutoPlayDisableOnInteraction")]
        public bool AutoPlayDisableOnInteraction { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.SlidesPerGroup")]
        public int SlidesPerGroup { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.SpaceBetween")]
        public int SpaceBetween { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.SlidesPerView")]
        public string SlidesPerView { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.SlidesPerColumn")]
        public int SlidesPerColumn { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.FreeModeEnabled")]
        public bool FreeModeEnabled { get; set; }


        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.DynamicBulletsEnabled")]
        public bool DynamicBulletsEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.CenteredSlidesEnabled")]
        public bool CenteredSlidesEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Models.CustomCss")]
        public string CustomCss { get; set; }
    }
}
