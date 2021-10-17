using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace codeislife.Widgets.CilSlider
{
    public class CilSliderPluginProcessor : BasePlugin, IWidgetPlugin
    {
        #region Fields
        private readonly IWebHelper _webHelper;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Ctor
        public CilSliderPluginProcessor(
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
            var settings = new CilSliderSettings
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

            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.ContainerCssSelector", "Container Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.ContainerCssSelector.Hint", "Swiper Slider kapsayan html elementine ait css selector girin. id için #selector, class için .selector!");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.PaginationCssSelector", "Pagination Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.NavigationNextCssSelector", "Navigation Next Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.NavigationPrevCssSelector", "Navigation Prev Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.ScrollBarCssSelector", "Scroll Bar Css Selector");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.Direction", "Direction");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.InitialSlide", "Initial Slide");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.Speed", "Speed");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.Loop", "Loop");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.LoopFillGroupWithBlankEnabled", "Loop Fill Group With Blank Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.PaginationEnabled", "Pagination Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.PaginationClickableEnabled", "Pagination Clickable Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.NavigationEnabled", "Navigation Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.ScrollBarEnabled", "Scroll Bar Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.AutoPlayEnabled", "Auto Play Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.AutoPlayDelay", "Auto Play Delay");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.AutoPlayDisableOnInteraction", "Auto Play Disable On Interaction");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.SlidesPerGroup", "Slides Per Group");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.SpaceBetween", "Space Between");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.SlidesPerView", "Slides Per View");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.SlidesPerColumn", "Slides Per Column");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.FreeModeEnabled", "Free Mode Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.DynamicBulletsEnabled", "Dynamic Bullets Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.CenteredSlidesEnabled", "Centered Slides Enabled");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Models.CustomCss", "Custom Css");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Configuration.SliderList", "Slider List");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.widgets.cilslider.configuration.title", "Configuration");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders", "Sliders");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.AddNew", "Add a New Slider");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.Edit", "Update {0}");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.BackToList", "back to list");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.Info", "Info");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.Mappings", "Mappings");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.Fields.Name", "Name");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.Fields.Published", "Published");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.Fields.DisplayOrder", "Display Order");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.Fields.AclCustomerRoles", "Customer Roles");
            await _localizationService.AddOrUpdateLocaleResourceAsync("codeislife.Widgets.CilSlider.Sliders.Fields.LimitedToStores", "Limited To Stores");
        }

        public override async Task UninstallAsync()
        {
            await _settingService.DeleteSettingAsync<CilSliderSettings>();
            await _localizationService.DeleteLocaleResourcesAsync("codeislife.Widgets.CilSlider");
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/CilSlider/Configure";
        }
        #endregion

        #region IWidgetPlugin
        public async Task<IList<string>> GetWidgetZonesAsync()
        {
            return await Task.FromResult(new List<string> { PublicWidgetZones.HomepageTop, PublicWidgetZones.ProductDetailsTop });
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "CilSlider";
        }

        public bool HideInWidgetList => false;
        #endregion
    }
}
