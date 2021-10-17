using codeislife.Widgets.CilSlider.Data.Domain;
using codeislife.Widgets.CilSlider.Factories;
using codeislife.Widgets.CilSlider.Models;
using codeislife.Widgets.CilSlider.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace codeislife.Widgets.CilSlider.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class CilSliderController : BasePluginController
    {
        #region Fields
        private readonly IAclService _aclService;
        private readonly IStoreService _storeService;
        private readonly ISliderService _sliderService;
        private readonly ISettingService _settingService;
        private readonly CatalogSettings _catalogSettings;
        private readonly ICustomerService _customerService;
        private readonly CilSliderSettings _cilSliderSettings;
        private readonly IPermissionService _permissionService;
        private readonly ISliderModelFactory _sliderModelFactory;
        private readonly INotificationService _notificationService;
        private readonly ILocalizationService _localizationService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        #endregion

        #region Ctor
        public CilSliderController(
            IAclService aclService,
            IStoreService storeService,
            ISliderService sliderService,
            ISettingService settingService,
            CatalogSettings catalogSettings,
            ICustomerService customerService,
            CilSliderSettings cilSliderSettings,
            IPermissionService permissionService,
            ISliderModelFactory sliderModelFactory,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IStoreMappingService storeMappingService,
            IBaseAdminModelFactory baseAdminModelFactory)
        {
            _aclService = aclService;
            _storeService = storeService;
            _sliderService = sliderService;
            _settingService = settingService;
            _catalogSettings = catalogSettings;
            _customerService = customerService;
            _cilSliderSettings = cilSliderSettings;
            _permissionService = permissionService;
            _sliderModelFactory = sliderModelFactory;
            _notificationService = notificationService;
            _localizationService = localizationService;
            _storeMappingService = storeMappingService;
            _baseAdminModelFactory = baseAdminModelFactory;
        }
        #endregion

        #region Utilities
        protected virtual async Task SaveSliderAclAsync(Slider slider, SliderModel model)
        {
            slider.SubjectToAcl = model.SelectedCustomerRoleIds.Any();
            _sliderService.UpdateSliderAsync(slider);

            var existingAclRecords = await _aclService.GetAclRecordsAsync(slider);
            var allCustomerRoles = await _customerService.GetAllCustomerRolesAsync(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (existingAclRecords.Count(acl => acl.CustomerRoleId == customerRole.Id) == 0)
                        await _aclService.InsertAclRecordAsync(slider, customerRole.Id);
                }
                else
                {
                    //remove role
                    var aclRecordToDelete = existingAclRecords.FirstOrDefault(acl => acl.CustomerRoleId == customerRole.Id);
                    if (aclRecordToDelete != null)
                        await _aclService.DeleteAclRecordAsync(aclRecordToDelete);
                }
            }
        }

        protected virtual async Task SaveSliderStoreMappingsAsync(Slider slider, SliderModel model)
        {
            slider.LimitedToStores = model.SelectedStoreIds.Any();
            _sliderService.UpdateSliderAsync(slider);

            var existingStoreMappings = await _storeMappingService.GetStoreMappingsAsync(slider);
            var allStores = await _storeService.GetAllStoresAsync();
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds.Contains(store.Id))
                {
                    //new store
                    if (existingStoreMappings.Count(sm => sm.StoreId == store.Id) == 0)
                        await _storeMappingService.InsertStoreMappingAsync(slider, store.Id);
                }
                else
                {
                    //remove store
                    var storeMappingToDelete = existingStoreMappings.FirstOrDefault(sm => sm.StoreId == store.Id);
                    if (storeMappingToDelete != null)
                        await _storeMappingService.DeleteStoreMappingAsync(storeMappingToDelete);
                }
            }
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

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(CilSliderConfigureModel model)
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

            await _settingService.SaveSettingAsync(_cilSliderSettings);
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return Configure();
        }
        #endregion

        #region List
        public async Task<IActionResult> List()
        {
            var searchModel = new SliderSearchModel();

            await _baseAdminModelFactory.PrepareStoresAsync(searchModel.AvailableStores);
            searchModel.HideStoresList = _catalogSettings.IgnoreStoreLimitations || searchModel.AvailableStores.SelectionIsNotPossible();

            //prepare "published" filter (0 - all; 1 - published only; 2 - unpublished only)
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "0",
                Text = await _localizationService.GetResourceAsync("Admin.Catalog.Categories.List.SearchPublished.All")
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "1",
                Text = await _localizationService.GetResourceAsync("Admin.Catalog.Categories.List.SearchPublished.PublishedOnly")
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "2",
                Text = await _localizationService.GetResourceAsync("Admin.Catalog.Categories.List.SearchPublished.UnpublishedOnly")
            });

            searchModel.SetGridPageSize();

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(SliderSearchModel searchModel)
        {
            var sliders = await _sliderService
                .GetAllSlidersAsync(
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
        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var model = await _sliderModelFactory.PrepareSliderModelAsync(new SliderModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Create(SliderModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var entity = model.ToEntity<Slider>();
                await _sliderService.InsertSliderAsync(entity);

                await SaveSliderAclAsync(entity, model);

                await SaveSliderStoreMappingsAsync(entity, model);

                if (entity.Id > 0)
                    _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Categories.Added"));
                else
                    _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Categories.NotAdded"));


                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = entity.Id });
            }

            model = await _sliderModelFactory.PrepareSliderModelAsync(model, null);

            return View(model);
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var slider = await _sliderService.GetSliderByIdAsync(id);
            if (slider == null)
                return RedirectToAction("List");

            var model = await _sliderModelFactory.PrepareSliderModelAsync(null, slider);

            return View(model);
        }


        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Edit(SliderModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var slider = await _sliderService.GetSliderByIdAsync(model.Id);
            if (slider == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                slider = model.ToEntity(slider);
                await _sliderService.UpdateSliderAsync(slider);

                await SaveSliderAclAsync(slider, model);
                await SaveSliderStoreMappingsAsync(slider, model);

                _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Catalog.Categories.Updated"));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = slider.Id });
            }

            model = await _sliderModelFactory.PrepareSliderModelAsync(model, slider);

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            if (selectedIds != null)
            {
                var sliders = await _sliderService.GetSliderByIdsAsync(selectedIds);
                await _sliderService.DeleteSliderAsync(sliders);
            }

            return Json(new { Result = true });

        }
        #endregion
    }
}
