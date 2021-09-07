using codeislife.Widgets.CilSlider.Models;
using codeislife.Widgets.CilSlider.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using System;

namespace codeislife.Widgets.CilSlider.Infrastructure
{
    public class CilSliderStartup : INopStartup
    {
        public int Order => Int32.MaxValue;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new SliderViewLocationExpander());
            });

            services.AddTransient<IValidator<SliderModel>, SliderValidator>();
        }
    }
}
