using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Routing;
using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Plugins;
using Nop.Web.Framework;
using Nop.Web.Framework.Infrastructure;
using Nop.Web.Framework.Menu;

namespace Nop.Plugin.Widgets.SwiperSlider
{
    // Hello
    public class SwiperSliderPluginProcessor : BasePlugin, IWidgetPlugin, IAdminMenuPlugin
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

        public Task ManageSiteMapAsync(SiteMapNode rootNode)
        {
            var menuItem = new SiteMapNode()
            {
                SystemName = "Nop.Plugin.Widgets.SwiperSlider.Root",
                Title = _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root").Result,
                IconClass = "far fa-dot-circle",
                Visible = true,
                ChildNodes = new List<SiteMapNode>()
                {
                    new SiteMapNode()
                    {
                        SystemName = "Nop.Plugin.Widgets.SwiperSlider.Configuration",
                        Title = _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root.Configuration").Result,
                        ControllerName = "SwiperSlider",
                        ActionName = "Configure",
                        IconClass = "far fa-dot-circle",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } },
                    },
                    new SiteMapNode()
                    {
                        SystemName = "Nop.Plugin.Widgets.SwiperSlider.List",
                        Title = _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root.List").Result,
                        ControllerName = "SwiperSlider",
                        ActionName = "List",
                        IconClass = "far fa-dot-circle",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", AreaNames.Admin } },
                    }
                }
            };

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Third party plugins");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(menuItem);
            else
                rootNode.ChildNodes.Add(menuItem);

            return Task.CompletedTask;
        }

        public bool HideInWidgetList => false;
        #endregion
    }
}
