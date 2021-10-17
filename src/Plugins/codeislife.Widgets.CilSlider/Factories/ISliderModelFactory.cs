using System.Threading.Tasks;
using codeislife.Widgets.CilSlider.Data.Domain;
using codeislife.Widgets.CilSlider.Models;

namespace codeislife.Widgets.CilSlider.Factories
{
    public interface ISliderModelFactory
    {
        Task<SliderModel> PrepareSliderModelAsync(SliderModel model, Slider entity);
    }
}
