using FluentValidation;
using Nop.Data;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Models;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Validators
{
    public partial class SwiperSliderValidator : BaseNopValidator<SwiperSliderModel>
    {
        public SwiperSliderValidator(ILocalizationService localizationService, INopDataProvider dataProvider)
        {
            // Name boş geçilemez!
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Fields.Name.Required"));

            base.SetDatabaseValidationRules<Slider>(dataProvider);
        }
    }
}
