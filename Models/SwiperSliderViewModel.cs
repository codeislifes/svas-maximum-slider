using Nop.Web.Framework.Models;

namespace Nop.Plugin.Widgets.SwiperSlider.Models
{
    public record SwiperSliderViewModel : BaseNopModel
    {
        public string Url { get; set; }
        public string AlternateText { get; set; }
        public string PictureUrl { get; set; }
        public int DisplayOrder { get; set; }
    }
}
