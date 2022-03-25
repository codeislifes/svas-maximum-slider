using System;
using System.Collections.Generic;
using Nop.Data.Mapping;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;

namespace Nop.Plugin.Widgets.SwiperSlider.Data
{
    /// <summary>
    /// Swiper instance of backward compatibility of table naming
    /// </summary>
    public partial class NameCompatibility : INameCompatibility
    {
        public Dictionary<Type, string> TableNames => new()
        {
            { typeof(Slider), "SwiperSlider" },
            { typeof(SliderItem), "SwiperSliderItem" },
        };

        public Dictionary<(Type, string), string> ColumnName => new();
    }
}
