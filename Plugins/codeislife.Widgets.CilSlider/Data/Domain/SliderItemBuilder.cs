using FluentMigrator.Builders.Create.Table;
using Nop.Data.Mapping.Builders;
using Nop.Data.Extensions;

namespace codeislife.Widgets.CilSlider.Data.Domain
{
    public class SliderItemBuilder : NopEntityBuilder<SliderItem>
    {
        public override void MapEntity(CreateTableExpressionBuilder table)
        {
            table
                .WithColumn(nameof(SliderItem.SliderId)).AsInt32().ForeignKey<Slider>();
        }
    }
}
