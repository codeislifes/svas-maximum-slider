using codeislife.Widgets.CilSlider.Factories;
using codeislife.Widgets.CilSlider.Models;
using codeislife.Widgets.CilSlider.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Models.Extensions;
using System;
using System.Linq;

namespace codeislife.Widgets.CilSlider.Controllers
{
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class CilSliderController : BasePluginController
    {
        #region Fields
        private readonly ISliderService _sliderService;
        private readonly ISettingService _settingService;
        private readonly CatalogSettings _catalogSettings;
        private readonly CilSliderSettings _cilSliderSettings;
        private readonly IPermissionService _permissionService;
        private readonly ISliderModelFactory _sliderModelFactory;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public CilSliderController(
            ISliderService sliderService,
            ISettingService settingService,
            CatalogSettings catalogSettings,
            CilSliderSettings cilSliderSettings,
            IPermissionService permissionService,
            ISliderModelFactory sliderModelFactory,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IBaseAdminModelFactory baseAdminModelFactory)
        {
            _sliderService = sliderService;
            _settingService = settingService;
            _catalogSettings = catalogSettings;
            _cilSliderSettings = cilSliderSettings;
            _permissionService = permissionService;
            _sliderModelFactory = sliderModelFactory;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Configure
        public IActionResult Configure()
        {
            var model = new CilSliderConfigureModel
            {
                ContainerCssSelector = _cilSliderSettings.ContainerCssSelector,
                PaginationCssSelector = _cilSliderSettings.PaginationCssSelector,
                NavigationNextCssSelector = _cilSliderSettings.NavigationNextCssSelector,
                NavigationPrevCssSelector = _cilSliderSettings.NavigationPrevCssSelector,
                ScrollBarCssSelector = _cilSliderSettings.ScrollBarCssSelector,
                Direction = _cilSliderSettings.Direction,
                DirectionId = Convert.ToInt32(_cilSliderSettings.Direction),
                InitialSlide = _cilSliderSettings.InitialSlide,
                Speed = _cilSliderSettings.Speed,
                Loop = _cilSliderSettings.Loop,
                LoopFillGroupWithBlankEnabled = _cilSliderSettings.LoopFillGroupWithBlankEnabled,
                PaginationEnabled = _cilSliderSettings.PaginationEnabled,
                PaginationClickableEnabled = _cilSliderSettings.PaginationClickableEnabled,
                NavigationEnabled = _cilSliderSettings.NavigationEnabled,
                ScrollBarEnabled = _cilSliderSettings.ScrollBarEnabled,

                AutoPlayEnabled = _cilSliderSettings.AutoPlayEnabled,
                AutoPlayDelay = _cilSliderSettings.AutoPlayDelay,
                AutoPlayDisableOnInteraction = _cilSliderSettings.AutoPlayDisableOnInteraction,
                SlidesPerGroup = _cilSliderSettings.SlidesPerGroup,
                SpaceBetween = _cilSliderSettings.SpaceBetween,
                SlidesPerView = _cilSliderSettings.SlidesPerView,
                FreeModeEnabled = _cilSliderSettings.FreeModeEnabled,
                DynamicBulletsEnabled = _cilSliderSettings.DynamicBulletsEnabled,
                CenteredSlidesEnabled = _cilSliderSettings.CenteredSlidesEnabled,
                CustomCss = _cilSliderSettings.CustomCss
            };
            return View("~/Plugins/codeislife.Widgets.CilSlider/Views/Configure.cshtml", model);
        }

        [HttpPost]
        public IActionResult Configure(CilSliderConfigureModel model)
        {
            _cilSliderSettings.ContainerCssSelector = model.ContainerCssSelector;
            _cilSliderSettings.PaginationCssSelector = model.PaginationCssSelector;
            _cilSliderSettings.NavigationNextCssSelector = model.NavigationNextCssSelector;
            _cilSliderSettings.NavigationPrevCssSelector = model.NavigationPrevCssSelector;
            _cilSliderSettings.ScrollBarCssSelector = model.ScrollBarCssSelector;
            _cilSliderSettings.Direction = (Direction)Enum.Parse(typeof(Direction), model.DirectionId.ToString());
            _cilSliderSettings.InitialSlide = model.InitialSlide;
            _cilSliderSettings.Speed = model.Speed;
            _cilSliderSettings.Loop = model.Loop;
            _cilSliderSettings.LoopFillGroupWithBlankEnabled = model.LoopFillGroupWithBlankEnabled;
            _cilSliderSettings.PaginationEnabled = model.PaginationEnabled;
            _cilSliderSettings.PaginationClickableEnabled = model.PaginationClickableEnabled;
            _cilSliderSettings.NavigationEnabled = model.NavigationEnabled;
            _cilSliderSettings.ScrollBarEnabled = model.ScrollBarEnabled;
            _cilSliderSettings.AutoPlayEnabled = model.AutoPlayEnabled;
            _cilSliderSettings.AutoPlayDelay = model.AutoPlayDelay;
            _cilSliderSettings.AutoPlayDisableOnInteraction = model.AutoPlayDisableOnInteraction;
            _cilSliderSettings.SlidesPerGroup = model.SlidesPerGroup;
            _cilSliderSettings.SpaceBetween = model.SpaceBetween;
            _cilSliderSettings.SlidesPerView = model.SlidesPerView;
            _cilSliderSettings.FreeModeEnabled = model.FreeModeEnabled;
            _cilSliderSettings.DynamicBulletsEnabled = model.DynamicBulletsEnabled;
            _cilSliderSettings.CenteredSlidesEnabled = model.CenteredSlidesEnabled;
            _cilSliderSettings.CustomCss = model.CustomCss;

            _settingService.SaveSetting(_cilSliderSettings);
            _settingService.ClearCache();

            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
        #endregion

        #region List
        public IActionResult List()
        {
            var searchModel = new SliderSearchModel();

            _baseAdminModelFactory.PrepareStores(searchModel.AvailableStores);
            searchModel.HideStoresList = _catalogSettings.IgnoreStoreLimitations || searchModel.AvailableStores.SelectionIsNotPossible();

            //prepare "published" filter (0 - all; 1 - published only; 2 - unpublished only)
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "0",
                Text = _localizationService.GetResource("Admin.Catalog.Categories.List.SearchPublished.All")
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "1",
                Text = _localizationService.GetResource("Admin.Catalog.Categories.List.SearchPublished.PublishedOnly")
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "2",
                Text = _localizationService.GetResource("Admin.Catalog.Categories.List.SearchPublished.UnpublishedOnly")
            });

            searchModel.SetGridPageSize();

            return View("~/Plugins/codeislife.Widgets.CilSlider/Views/Slider/List.cshtml", searchModel);
        }

        [HttpPost]
        public IActionResult List(SliderSearchModel searchModel)
        {
            var sliders = _sliderService
                .GetAllSliders(
                name: searchModel.SearchSliderName,
                storeId: searchModel.SearchStoreId,
                pageIndex: (searchModel.Page - 1),
                pageSize: searchModel.PageSize,
                showHidden: true,
                overridePublished: (searchModel.SearchPublishedId == 0 ? null : (bool?)(searchModel.SearchPublishedId == 1)));

            var model = new SliderListModel().PrepareToGrid(searchModel, sliders, () =>
            {
                return sliders.Select(slider =>
                {
                    return slider.ToModel<SliderModel>();
                });
            });

            return Json(model);
        }

        #endregion

        #region Create
        public virtual IActionResult Create()
        {
            if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
                return AccessDeniedView();

            var model = _sliderModelFactory.PrepareSliderModel(new SliderModel(), null);

            return View("~/Plugins/codeislife.Widgets.CilSlider/Views/Slider/Create.cshtml", model);
        }
        #endregion
    }
}
