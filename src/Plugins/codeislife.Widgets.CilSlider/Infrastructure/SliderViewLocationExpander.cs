using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Razor;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Themes;

namespace codeislife.Widgets.CilSlider.Infrastructure
{
    public class SliderViewLocationExpander : IViewLocationExpander
    {
        private const string THEME_KEY = "nop.themename";

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            ////no need to add the themeable view locations at all as the administration should not be themeable anyway
            //if (context.AreaName?.Equals(AreaNames.Admin) ?? false)
            //    return;

            var themeContext = EngineContext.Current.Resolve<IThemeContext>();
            context.Values[THEME_KEY] = themeContext.GetWorkingThemeNameAsync().Result;
        }

        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context, IEnumerable<string> viewLocations)
        {
            if (context.Values.TryGetValue(THEME_KEY, out string theme))
            {
                viewLocations = new[] {
                        $"/Plugins/codeislife.Widgets.CilSlider/Themes/{theme}/Views/{{0}}.cshtml",
                        $"/Plugins/codeislife.Widgets.CilSlider/Themes/{theme}/Views/{{1}}/{{0}}.cshtml",
                        $"/Plugins/codeislife.Widgets.CilSlider/Themes/{theme}/Views/Shared/{{0}}.cshtml",

                        $"/Plugins/codeislife.Widgets.CilSlider/Areas/{{2}}/Views/{{0}}.cshtml",
                        $"/Plugins/codeislife.Widgets.CilSlider/Areas/{{2}}/Views/{{1}}/{{0}}.cshtml",
                        $"/Plugins/codeislife.Widgets.CilSlider/Areas/{{2}}/Views/Shared/{{0}}.cshtml",

                        $"/Plugins/codeislife.Widgets.CilSlider/Views/{{0}}.cshtml",
                        $"/Plugins/codeislife.Widgets.CilSlider/Views/{{1}}/{{0}}.cshtml",
                        $"/Plugins/codeislife.Widgets.CilSlider/Views/Shared/{{0}}.cshtml",
                    }
                    .Concat(viewLocations);
            }


            return viewLocations;
        }


    }
}
