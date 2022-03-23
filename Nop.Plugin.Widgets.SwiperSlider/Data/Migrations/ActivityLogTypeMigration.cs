using System;
using System.Linq;
using FluentMigrator;
using Nop.Core.Domain.Logging;
using Nop.Data;
using Nop.Data.Migrations;

namespace Nop.Plugin.Widgets.SwiperSlider_dev.Data.Migrations
{
    [NopMigration("2022-03-23 04:11:12", "Nop.Plugin.Widgets.SwiperSlider Activity Log Types", MigrationProcessType.Installation)]
    public class ActivityLogTypeMigration : AutoReversingMigration
    {
        #region Fields
        private readonly INopDataProvider _dataProvider;
        #endregion

        #region Ctor
        public ActivityLogTypeMigration(INopDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }
        #endregion

        public override void Up()
        {
            if (!_dataProvider.GetTable<ActivityLogType>().Any(pr => string.Compare(pr.SystemKeyword, "AddNewSwiperSlider", StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                _dataProvider.InsertEntity(new ActivityLogType
                {
                    Name = "Add a new swiper slider",
                    SystemKeyword = "AddNewSwiperSlider",
                    Enabled = true
                });
            }

            if (!_dataProvider.GetTable<ActivityLogType>().Any(pr => string.Compare(pr.SystemKeyword, "EditSwiperSlider", StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                _dataProvider.InsertEntity(new ActivityLogType
                {
                    Name = "Edit swiper slider",
                    SystemKeyword = "EditSwiperSlider",
                    Enabled = true
                });
            }

            if (!_dataProvider.GetTable<ActivityLogType>().Any(pr => string.Compare(pr.SystemKeyword, "DeleteSwiperSlider", StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                _dataProvider.InsertEntity(new ActivityLogType
                {
                    Name = "Delete a swiper slider",
                    SystemKeyword = "DeleteSwiperSlider",
                    Enabled = true
                });
            }

            if (!_dataProvider.GetTable<ActivityLogType>().Any(pr => string.Compare(pr.SystemKeyword, "DeleteSwiperSliders", StringComparison.InvariantCultureIgnoreCase) == 0))
            {
                _dataProvider.InsertEntity(new ActivityLogType
                {
                    Name = "Delete multiple swiper sliders",
                    SystemKeyword = "DeleteSwiperSliders",
                    Enabled = true
                });
            }
        }
    }
}
