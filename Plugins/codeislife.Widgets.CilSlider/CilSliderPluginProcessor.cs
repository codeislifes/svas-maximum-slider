using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;

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

        public override void Install()
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

            _settingService.SaveSetting(settings);

            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.ContainerCssSelector", "Container Css Selector");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.ContainerCssSelector.Hint", "Swiper Slider kapsayan html elementine ait css selector girin. id için #selector, class için .selector!");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.PaginationCssSelector", "Pagination Css Selector");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.NavigationNextCssSelector", "Navigation Next Css Selector");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.NavigationPrevCssSelector", "Navigation Prev Css Selector");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.ScrollBarCssSelector", "Scroll Bar Css Selector");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.Direction", "Direction");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.InitialSlide", "Initial Slide");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.Speed", "Speed");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.Loop", "Loop");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.LoopFillGroupWithBlankEnabled", "Loop Fill Group With Blank Enabled");

            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.PaginationEnabled", "Pagination Enabled");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.PaginationClickableEnabled", "Pagination Clickable Enabled");

            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.NavigationEnabled", "Navigation Enabled");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.ScrollBarEnabled", "Scroll Bar Enabled");

            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.AutoPlayEnabled", "Auto Play Enabled");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.AutoPlayDelay", "Auto Play Delay");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.AutoPlayDisableOnInteraction", "Auto Play Disable On Interaction");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.SlidesPerGroup", "Slides Per Group");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.SpaceBetween", "Space Between");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.SlidesPerView", "Slides Per View");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.SlidesPerColumn", "Slides Per Column");

            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.FreeModeEnabled", "Free Mode Enabled");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.DynamicBulletsEnabled", "Dynamic Bullets Enabled");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.CenteredSlidesEnabled", "Centered Slides Enabled");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Models.CustomCss", "Custom Css");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Configuration.SliderList", "Slider List");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.widgets.cilslider.configuration.title", "Configuration");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders", "Sliders");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.AddNew", "Add a New Slider");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.Edit", "Update {0}");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.BackToList", "back to list");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.Info", "Info");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.Mappings", "Mappings");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.Fields.Name", "Name");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.Fields.Published", "Published");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.Fields.DisplayOrder", "Display Order");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.Fields.AclCustomerRoles", "Customer Roles");
            _localizationService.AddOrUpdatePluginLocaleResource("codeislife.Widgets.CilSlider.Sliders.Fields.LimitedToStores", "Limited To Stores");
        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<CilSliderSettings>();
            _localizationService.DeletePluginLocaleResources("codeislife.Widgets.CilSlider");
        }

        public override void PreparePluginToUninstall()
        {
            base.PreparePluginToUninstall();
        }

        public override string GetConfigurationPageUrl()
        {
            return $"{_webHelper.GetStoreLocation()}Admin/CilSlider/Configure";
        }
        #endregion

        #region IWidgetPlugin
        public IList<string> GetWidgetZones()
        {
            return new List<string> { PublicWidgetZones.HomepageTop, PublicWidgetZones.ProductDetailsTop };
        }

        public string GetWidgetViewComponentName(string widgetZone)
        {
            return "CilSlider";
        }

        public bool HideInWidgetList => false;
        #endregion
    }
}
