using FluentValidation;
using Nop.Data;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Widgets.SwiperSlider.Validators
{
    public partial class SwiperSliderValidator : BaseNopValidator<SwiperSliderModel>
    {
        public SwiperSliderValidator(ILocalizationService localizationService, INopDataProvider dataProvider)
        {
            // Name boş geçilemez!
            RuleFor(m => m.Name)
                .NotEmpty()
                .WithMessageAwait(localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Sliders.Fields.Name.Required"));

            base.SetDatabaseValidationRules<Data.Domain.Slider>(dataProvider);
        }
    }
}
