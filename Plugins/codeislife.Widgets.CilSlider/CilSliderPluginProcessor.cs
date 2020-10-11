using Nop.Core;
using Nop.Services.Cms;
using Nop.Services.Configuration;
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
        #endregion

        #region Ctor
        public CilSliderPluginProcessor(
            IWebHelper webHelper,
            ISettingService settingService)
        {
            _webHelper = webHelper;
            _settingService = settingService;
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

        }

        public override void Uninstall()
        {
            _settingService.DeleteSetting<CilSliderSettings>();
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
