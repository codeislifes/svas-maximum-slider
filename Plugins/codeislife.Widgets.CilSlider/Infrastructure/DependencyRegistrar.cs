using Autofac;
using codeislife.Widgets.CilSlider.Factories;
using codeislife.Widgets.CilSlider.Services;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using System;

namespace codeislife.Widgets.CilSlider.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public int Order => Int16.MaxValue;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, NopConfig config)
        {
            builder.RegisterType<SliderService>().As<ISliderService>().InstancePerLifetimeScope();
            builder.RegisterType<SliderModelFactory>().As<ISliderModelFactory>().InstancePerLifetimeScope();
        }

    }
}
