using AutoMapper;
using codeislife.Widgets.CilSlider.Data.Domain;
using codeislife.Widgets.CilSlider.Models;
using Nop.Core.Infrastructure.Mapper;

namespace codeislife.Widgets.CilSlider.Infrastructure
{
    public class AutoMapperConfiguration : Profile, IOrderedMapperProfile
    {
        public AutoMapperConfiguration()
        {
            Initialize();
        }

        public void Initialize()
        {
            CreateMap<Slider, SliderModel>().ReverseMap();
        }

        public int Order => int.MaxValue;

    }
}
