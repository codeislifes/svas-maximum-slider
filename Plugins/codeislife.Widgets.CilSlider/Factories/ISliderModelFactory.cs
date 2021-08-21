using codeislife.Widgets.CilSlider.Data.Domain;
using codeislife.Widgets.CilSlider.Models;

namespace codeislife.Widgets.CilSlider.Factories
{
    public interface ISliderModelFactory
    {
        SliderModel PrepareSliderModel(SliderModel model, Slider entity);
    }
}
