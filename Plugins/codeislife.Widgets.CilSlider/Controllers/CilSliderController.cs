using codeislife.Widgets.CilSlider.Models;
using Microsoft.AspNetCore.Mvc;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using System;

namespace codeislife.Widgets.CilSlider.Controllers
{
    [Area(AreaNames.Admin)]
    [AutoValidateAntiforgeryToken]
    public class CilSliderController : BasePluginController
    {
        #region Fields
        private readonly ISettingService _settingService;
        private readonly CilSliderSettings _cilSliderSettings;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        #endregion

        #region Ctor
        public CilSliderController(
            ISettingService settingService,
            CilSliderSettings cilSliderSettings,
            INotificationService notificationService,
            ILocalizationService localizationService)
        {
            _settingService = settingService;
            _cilSliderSettings = cilSliderSettings;
            _notificationService = notificationService;
            _localizationService = localizationService;
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
                CenteredSlidesEnabled = _cilSliderSettings.CenteredSlidesEnabled
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

            _settingService.SaveSetting(_cilSliderSettings);
            _settingService.ClearCache();


            _notificationService.SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }
        #endregion
    }
}
