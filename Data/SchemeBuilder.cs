using FluentMigrator.Builders.Create.Table;
using Nop.Data.Extensions;
using Nop.Data.Mapping.Builders;

namespace Nop.Plugin.Widgets.SwiperSlider.Data.Domain
{
    public class SchemeBuilder : NopEntityBuilder<SliderItem>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table.WithColumn(nameof(SliderItem.SliderId)).AsInt32().ForeignKey<Slider>();
        }
    }
}
