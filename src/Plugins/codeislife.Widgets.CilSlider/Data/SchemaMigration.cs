using codeislife.Widgets.CilSlider.Data.Domain;
using FluentMigrator;
using Nop.Data.Migrations;

namespace codeislife.Widgets.CilSlider.Data
{
    [NopMigration("2020/10/23 10:56:00","codeislife.cilslider base schema")]
    public class SchemaMigration : AutoReversingMigration
    {
        protected IMigrationManager _migrationManager;

        public SchemaMigration(IMigrationManager migrationManager)
        {
            _migrationManager = migrationManager;
        }

        public override void Up()
        {
            _migrationManager.BuildTable<Slider>(Create);
            _migrationManager.BuildTable<SliderItem>(Create);
        }
    }
}
