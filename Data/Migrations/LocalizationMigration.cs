using System.Collections.Generic;
using FluentMigrator;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Data.Migrations;
using Nop.Services.Localization;

namespace Nop.Plugin.Widgets.SwiperSlider_dev.Data.Migrations
{
    [NopMigration("2022-03-23 04:12:13", "Nop.Plugin.Widgets.SwiperSlider", UpdateMigrationType.Localization, MigrationProcessType.Installation)]
    public class LocalizationMigration : Migration
    {

        public override void Down()
        {
        }

        public override void Up()
        {
            if (!DataSettingsManager.IsDatabaseInstalled())
                return;

            var resources = new Dictionary<string, string>
            {
                // Menu 
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root", "Swiper Slider" },
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root.Configuration", "Configuration" },
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Menu.Root.List", "Slider List" },

                // Activity Logs
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.AddNewSwiperSlider", "Added a new swiper slider ({0})"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.EditSwiperSlider", "Added a new swiper slider ({0})"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.DeleteSwiperSlider", "Added a new swiper slider ({0})"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.DeleteSwiperSliders", "Multiple swiper slider were deleted. (IDs : {0})"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.AddNewSwiperSliderItem", "Added a new swiper slider item ({0})"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.EditSwiperSliderItem", "Added a new swiper slider item ({0})"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.DeleteSwiperSliderItem", "Added a new swiper slider item ({0})"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.DeleteSwiperSliderItems", "Multiple swiper slider items were deleted. (IDs : {0})"},
                
                // Notification Messages
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Added", "{0} has beed added."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.NotAdded", "Swiper slider could not be added."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Updated", "{0} has beed updated."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Deleted", "{0} has beed deleted."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.AllDeleted", "The selected sliders are deleted."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Slider.NotFound", "No swiper slider found with the specified id"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.Added", "{0} has beed added."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.NotAdded", "Swiper slider item could not be added."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.Updated", "{0} has beed updated."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.Deleted", "{0} has beed deleted."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.AllDeleted", "The selected slider items are deleted."},

                // General
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.NoCustomerRolesAvailable", "No customer roles available"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.NoStoresAvailable", "No stores available"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.BackToList", "back to list"},

                // Configuration
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Cards.General.Title", "General"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ContainerCssSelector", "Container Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ContainerCssSelector.Hint", "Swiper Slider kapsayan html elementine ait css selector girin. id için #selector, class için .selector!"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationCssSelector", "Pagination Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationCssSelector.Hint", "Pagination Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationNextCssSelector", "Navigation Next Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationNextCssSelector.Hint", "Navigation Next Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationPrevCssSelector", "Navigation Prev Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationPrevCssSelector.Hint", "Navigation Prev Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarCssSelector", "Scroll Bar Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarCssSelector.Hint", "Scroll Bar Css Selector"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Direction", "Direction"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Direction.Hint", "Direction"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.InitialSlide", "Initial Slide"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.InitialSlide.Hint", "Initial Slide"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Speed", "Speed"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Speed.Hint", "Speed"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Loop", "Loop"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.Loop.Hint", "Loop"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.LoopFillGroupWithBlankEnabled", "Loop Fill Group With Blank Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.LoopFillGroupWithBlankEnabled.Hint", "Loop Fill Group With Blank Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationEnabled", "Pagination Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationEnabled.Hint", "Pagination Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationClickableEnabled", "Pagination Clickable Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.PaginationClickableEnabled.Hint", "Pagination Clickable Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationEnabled", "Navigation Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.NavigationEnabled.Hint", "Navigation Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarEnabled", "Scroll Bar Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.ScrollBarEnabled.Hint", "Scroll Bar Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayEnabled", "Auto Play Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayEnabled.Hint", "Auto Play Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDelay", "Auto Play Delay"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDelay.Hint", "Auto Play Delay"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDisableOnInteraction", "Auto Play Disable On Interaction"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.AutoPlayDisableOnInteraction.Hint", "Auto Play Disable On Interaction"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerGroup", "Slides Per Group"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerGroup.Hint", "Slides Per Group"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SpaceBetween", "Space Between"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SpaceBetween.Hint", "Space Between"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerView", "Slides Per View"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerView.Hint", "Slides Per View"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerColumn", "Slides Per Column"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.SlidesPerColumn.Hint", "Slides Per Column"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.FreeModeEnabled", "Free Mode Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.FreeModeEnabled.Hint", "Free Mode Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.DynamicBulletsEnabled", "Dynamic Bullets Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.DynamicBulletsEnabled.Hint", "Dynamic Bullets Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CenteredSlidesEnabled", "Centered Slides Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CenteredSlidesEnabled.Hint", "Centered Slides Enabled"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CustomCss", "Custom Css"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Fields.CustomCss.Hint", "Custom Css"},

                // Pages
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.PageTitle", "Configure Swiper Slider Plugin"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Configuration.Title", "Configure Swiper Slider Plugin"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.List.PageTitle", "Swiper Sliders"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.List.Title", "Swiper Slider List"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Create.PageTitle", "Create Swiper Slider"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Create.Title", "Add a New Swiper Slider"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Edit.PageTitle", "Update {0} - Swiper Slider"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Edit.Title", "Update Swiper Slider - {0}"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Create.PageTitle", "Create Swiper Slider Item"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Create.Title", "Add a New Swiper Slider  Item"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Edit.PageTitle", "Update {0} - Swiper Slider  Item"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Edit.Title", "Update Swiper Slider  Item- {0}"},

                // Cards
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Cards.Info.Title", "Info"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Cards.Mappings.Title", "Mappings"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Cards.Items.Title", "Slider Items"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Cards.Info.Title", "Info"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Cards.Mappings.Title", "Mappings"},

                // Slider Field Names
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Name", "Slider Name"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Name.Hint", "Slider Name"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.DisplayOrder", "Display Order"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.DisplayOrder.Hint", "Display Order"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Published", "Published"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.Published.Hint", "Published"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.AclCustomerRoles", "Customer Roles"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.AclCustomerRoles.Hint", "Customer Roles"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.LimitedToStores", "Limited To Stores"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.Fields.LimitedToStores.Hint", "Limited To Stores"},
                
                // Slider Item Field Names
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Slider", "Slider Name"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Slider.Hint", "Slider Name"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Name", "Slider Item Name"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Name.Hint", "Slider Item Name"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Picture", "Picture"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Picture.Hint", "Picture"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Url", "Url"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Url.Hint", "Url"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.AlternateText", "Alternate Text"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.AlternateText.Hint", "Alternate Text"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.DisplayOrder", "Display Order"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.DisplayOrder.Hint", "Display Order"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Published", "Published"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.Published.Hint", "Published"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.AclCustomerRoles", "Customer Roles"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.AclCustomerRoles.Hint", "Customer Roles"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.LimitedToStores", "Limited To Stores"},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.Fields.LimitedToStores.Hint", "Limited To Stores"},

                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.BackToSlider", "back to edit slider" },
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.SaveBeforeEdit", "You need to save the slider before you can add slider items for this slider page."},
                {"Nop.Plugin.Widgets.SwiperSlider.Admin.SliderItems.CreateButton", "Add New Slider Item" },
            };

            var localizationService = EngineContext.Current.Resolve<ILocalizationService>();
            localizationService.AddOrUpdateLocaleResourceAsync(resources).Wait();
        }
    }
}
