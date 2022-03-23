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

            var resources = new Dictionary<string, string>
            {
                // Menu 
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root", "Swiper Slider" },
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root.Configuration", "Configuration" },
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root.List", "Slider List" },
                
                // Notification Messages
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Added", "{0} has beed added."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.NotAdded", "Swiper slider could not be added."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Updated", "{0} has beed updated."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Deleted", "{0} has beed deleted."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.AllDeleted", "The selected sliders are deleted."},

                // General
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.NoCustomerRolesAvailable", "No customer roles available"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.NoStoresAvailable", "No stores available"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.BackToList", "back to list"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Cards.Info.Title", "Info"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Cards.Mappings.Title", "Mappings"},

                // Configuration
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Cards.General.Title", "General"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ContainerCssSelector", "Container Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ContainerCssSelector.Hint", "Swiper Slider kapsayan html elementine ait css selector girin. id için #selector, class için .selector!"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationCssSelector", "Pagination Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationCssSelector.Hint", "Pagination Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationNextCssSelector", "Navigation Next Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationNextCssSelector.Hint", "Navigation Next Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationPrevCssSelector", "Navigation Prev Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationPrevCssSelector.Hint", "Navigation Prev Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarCssSelector", "Scroll Bar Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarCssSelector.Hint", "Scroll Bar Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Direction", "Direction"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Direction.Hint", "Direction"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.InitialSlide", "Initial Slide"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.InitialSlide.Hint", "Initial Slide"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Speed", "Speed"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Speed.Hint", "Speed"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Loop", "Loop"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Loop.Hint", "Loop"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.LoopFillGroupWithBlankEnabled", "Loop Fill Group With Blank Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.LoopFillGroupWithBlankEnabled.Hint", "Loop Fill Group With Blank Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationEnabled", "Pagination Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationEnabled.Hint", "Pagination Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationClickableEnabled", "Pagination Clickable Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationClickableEnabled.Hint", "Pagination Clickable Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationEnabled", "Navigation Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationEnabled.Hint", "Navigation Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarEnabled", "Scroll Bar Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarEnabled.Hint", "Scroll Bar Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayEnabled", "Auto Play Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayEnabled.Hint", "Auto Play Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDelay", "Auto Play Delay"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDelay.Hint", "Auto Play Delay"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDisableOnInteraction", "Auto Play Disable On Interaction"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDisableOnInteraction.Hint", "Auto Play Disable On Interaction"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerGroup", "Slides Per Group"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerGroup.Hint", "Slides Per Group"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SpaceBetween", "Space Between"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SpaceBetween.Hint", "Space Between"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerView", "Slides Per View"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerView.Hint", "Slides Per View"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerColumn", "Slides Per Column"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerColumn.Hint", "Slides Per Column"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.FreeModeEnabled", "Free Mode Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.FreeModeEnabled.Hint", "Free Mode Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.DynamicBulletsEnabled", "Dynamic Bullets Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.DynamicBulletsEnabled.Hint", "Dynamic Bullets Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CenteredSlidesEnabled", "Centered Slides Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CenteredSlidesEnabled.Hint", "Centered Slides Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CustomCss", "Custom Css"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CustomCss.Hint", "Custom Css"},

                // Pages
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.PageTitle", "Configure Swiper Slider Plugin"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Title", "Configure Swiper Slider Plugin"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.List.PageTitle", "Swiper Sliders"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.List.Title", "Swiper Slider List"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Create.PageTitle", "Create Swiper Slider"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Create.Title", "Add a New Swiper Slider"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Edit.PageTitle", "Update {0} - Swiper Slider"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Edit.Title", "Update Swiper Slider - {0}"},

                // Field Names
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Name", "Slider Name"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Name.Hint", "Slider Name"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.DisplayOrder", "Display Order"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.DisplayOrder.Hint", "Display Order"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Published", "Published"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Published.Hint", "Published"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.AclCustomerRoles", "Customer Roles"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.AclCustomerRoles.Hint", "Customer Roles"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.LimitedToStores", "Limited To Stores"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.LimitedToStores.Hint", "Limited To Stores"}
            };

            await _localizationService.AddOrUpdateLocaleResourceAsync(resources);
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
