using codeislife.Widgets.CilSlider.Data.Domain;
using codeislife.Widgets.CilSlider.Models;
using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace codeislife.Widgets.CilSlider.Validators
{
    public partial class SliderValidator : BaseNopValidator<SliderModel>
    {
        public SliderValidator(ILocalizationService localizationService, INopDataProvider dataProvider)
        {
            // Name boş geçilemez!
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessageAwait(localizationService.GetResourceAsync("codeislife.Widgets.CilSlider.Sliders.Fields.Name.Required"));

            SetDatabaseValidationRules<Slider>(dataProvider);
        }
    }
}
