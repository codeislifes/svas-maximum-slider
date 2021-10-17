using codeislife.Widgets.CilSlider.Factories;
using codeislife.Widgets.CilSlider.Services;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;

namespace codeislife.Widgets.CilSlider.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => int.MaxValue;

        public void Register(IServiceCollection services, ITypeFinder typeFinder, AppSettings appSettings)
        {
            services.AddScoped<ISliderService, SliderService>();
            services.AddScoped<ISliderModelFactory, SliderModelFactory>();
        }
    }
}
