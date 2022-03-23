using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Models
{
    public record SwiperSliderConfigurationModel : BaseNopModel
    {
        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ContainerCssSelector")]
        public string ContainerCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationCssSelector")]
        public string PaginationCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationNextCssSelector")]
        public string NavigationNextCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationPrevCssSelector")]
        public string NavigationPrevCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarCssSelector")]
        public string ScrollBarCssSelector { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Direction")]
        public int DirectionId { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Direction")]
        public Direction Direction { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.InitialSlide")]
        public int InitialSlide { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Speed")]
        public int Speed { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Loop")]
        public bool Loop { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.LoopFillGroupWithBlankEnabled")]
        public bool LoopFillGroupWithBlankEnabled { get; set; }


        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationEnabled")]
        public bool PaginationEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationClickableEnabled")]
        public bool PaginationClickableEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationEnabled")]
        public bool NavigationEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarEnabled")]
        public bool ScrollBarEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayEnabled")]
        public bool AutoPlayEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDelay")]
        public int AutoPlayDelay { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDisableOnInteraction")]
        public bool AutoPlayDisableOnInteraction { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerGroup")]
        public int SlidesPerGroup { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SpaceBetween")]
        public int SpaceBetween { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerView")]
        public string SlidesPerView { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerColumn")]
        public int SlidesPerColumn { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.FreeModeEnabled")]
        public bool FreeModeEnabled { get; set; }


        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.DynamicBulletsEnabled")]
        public bool DynamicBulletsEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CenteredSlidesEnabled")]
        public bool CenteredSlidesEnabled { get; set; }

        [NopResourceDisplayName("Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CustomCss")]
        public string CustomCss { get; set; }
    }
}
