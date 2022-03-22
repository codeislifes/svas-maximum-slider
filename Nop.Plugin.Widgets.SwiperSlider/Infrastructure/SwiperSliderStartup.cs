using System;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Widgets.SwiperSlider.Factories;
using Nop.Plugin.Widgets.SwiperSlider.Models;
using Nop.Plugin.Widgets.SwiperSlider.Services;
using Nop.Plugin.Widgets.SwiperSlider.Validators;

namespace Nop.Plugin.Widgets.SwiperSlider.Infrastructure
{
    public class SwiperSliderStartup : INopStartup
    {
        public int Order => int.MaxValue;

        public void Configure(IApplicationBuilder application)
        {
        }

        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISwiperSliderService, SwiperSliderService>();
            services.AddScoped<ISwiperSliderModelFactory, SwiperSliderModelFactory>();

            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new SwiperSliderViewLocationExpander());
            });

            services.AddTransient<IValidator<SwiperSliderModel>, SwiperSliderValidator>();
        }
    }
}
