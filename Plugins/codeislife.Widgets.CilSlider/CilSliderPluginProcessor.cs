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
        private readonly ISettingService _settingService;
        #endregion

        #region Ctor
        public CilSliderPluginProcessor(ISettingService settingService)
        {
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
                Speed = 300
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
