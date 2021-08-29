using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using FluentValidation;
using codeislife.Widgets.CilSlider.Models;
using codeislife.Widgets.CilSlider.Validators;

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
            services.AddTransient<IValidator<SliderModel>, SliderValidator>();
        }
    }
}
