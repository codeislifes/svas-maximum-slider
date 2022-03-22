using System.Collections.Generic;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;

namespace Nop.Plugin.Widgets.SwiperSlider
{
    // Hello
    public class SwiperSliderPluginProcessor : BasePlugin, IWidgetPlugin
    {
        #region Fields
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Ctor
        public SwiperSliderPluginProcessor(
            IWebHelper webHelper,
            ISettingService settingService,
            ILocalizationService localizationService)
        {
            _webHelper = webHelper;
            _settingService = settingService;
            _localizationService = localizationService;
        }
        #endregion

        #region BasePlugin Overrides

        public override async Task InstallAsync()
        {
            var settings = new SwiperSliderSettings
            {
                ContainerCssSelector = ".swiper-container",
                PaginationCssSelector = ".swiper-pagination",
                NavigationNextCssSelector = ".swiper-button-next",
                NavigationPrevCssSelector = ".swiper-button-prev",
                ScrollBarCssSelector = ".swiper-scrollbar",
                Direction = Direction.Horizontal,
                InitialSlide = 0,
                Speed = 300,
                Loop = true,
                NavigationEnabled = true,
                PaginationEnabled = true,
                ScrollBarEnabled = false
            };

            await _settingService.SaveSettingAsync(settings);

            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.ContainerCssSelector", "Container Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.ContainerCssSelector.Hint", "Swiper Slider kapsayan html elementine ait css selector girin. id için #selector, class için .selector!");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.PaginationCssSelector", "Pagination Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.NavigationNextCssSelector", "Navigation Next Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.NavigationPrevCssSelector", "Navigation Prev Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.ScrollBarCssSelector", "Scroll Bar Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.Direction", "Direction");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.InitialSlide", "Initial Slide");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.Speed", "Speed");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.Loop", "Loop");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.LoopFillGroupWithBlankEnabled", "Loop Fill Group With Blank Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.PaginationEnabled", "Pagination Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.PaginationClickableEnabled", "Pagination Clickable Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.NavigationEnabled", "Navigation Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.ScrollBarEnabled", "Scroll Bar Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.AutoPlayEnabled", "Auto Play Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.AutoPlayDelay", "Auto Play Delay");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.AutoPlayDisableOnInteraction", "Auto Play Disable On Interaction");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.SlidesPerGroup", "Slides Per Group");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.SpaceBetween", "Space Between");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.SlidesPerView", "Slides Per View");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.SlidesPerColumn", "Slides Per Column");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.FreeModeEnabled", "Free Mode Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.DynamicBulletsEnabled", "Dynamic Bullets Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.CenteredSlidesEnabled", "Centered Slides Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Models.CustomCss", "Custom Css");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Configuration.SliderList", "Slider List");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.configuration.title", "Configuration");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders", "Sliders");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.AddNew", "Add a New Slider");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Edit", "Update {0}");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.BackToList", "back to list");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Info", "Info");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Mappings", "Mappings");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Fields.Name", "Name");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Fields.Published", "Published");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Fields.DisplayOrder", "Display Order");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Fields.AclCustomerRoles", "Customer Roles");
            await _localizationService.AddOrUpdateLocaleResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Fields.LimitedToStores", "Limited To Stores");
        }

        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<SwiperSliderSettings>();
            await _localizationService.DeleteLocaleResourcesAsync("Nop.Plugin.Widgets.SwiperSlider");
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/SwiperSlider/Configure";
        }
        #endregion

        #region IWidgetPlugin
        public async Task<IList<string>> GetWidgetZonesAsync()
        {
            return await Task.FromResult(new List<string> { PublicWidgetZones.HomepageTop, PublicWidgetZones.ProductDetailsTop });
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "SwiperSlider";
        }

        public bool HideInWidgetList => false;
        #endregion
    }
}
