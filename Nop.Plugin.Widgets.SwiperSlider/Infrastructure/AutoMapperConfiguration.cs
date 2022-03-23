using AutoMapper;
using Nop.Core.Infrastructure.Mapper;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
using Nop.Plugin.Widgets.SwiperSlider.Models;

namespace Nop.Plugin.Widgets.SwiperSlider.Infrastructure
{
    public class AutoMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public AutoMapperConfiguration()
        {
            Initialize();
        }

        public void Initialize()
        {
            CreateMap<Slider, SwiperSliderModel>().ReverseMap();
            CreateMap<SliderItem, SwiperSliderItemModel>().ReverseMap();
        }

        public int Order => int.MaxValue;

    }
}
