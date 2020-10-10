using Nop.Services.Cms;
using Nop.Services.Plugins;
using Nop.Web.Framework.Infrastructure;
using System.Collections.Generic;

namespace codeislife.Widgets.CilSlider
{
    public class CilSliderPluginProcessor : BasePlugin, IWidgetPlugin
    {

        #region BasePlugin Overrides

        public override void Install()
        {
            base.Install();
        }

        public override void Uninstall()
        {
            base.Uninstall();
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
