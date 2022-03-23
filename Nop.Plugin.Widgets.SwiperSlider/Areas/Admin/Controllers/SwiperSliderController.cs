using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.SwiperSlider.Factories;
using Nop.Plugin.Widgets.SwiperSlider.Models;
using Nop.Plugin.Widgets.SwiperSlider.Services;
using Nop.Services.Configuration;
using Nop.Services.Customers;
using Nop.Services.Localization;
using Nop.Services.Logging;
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

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Controllers
{
    [Area(AreaNames.Admin)]
    [AuthorizeAdmin]
    [AutoValidateAntiforgeryToken]
    public class SwiperSliderController : BasePluginController
    {
        #region Fields
        private readonly IAclService _aclService;
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly CatalogSettings _catalogSettings;
        private readonly ICustomerService _customerService;
        private readonly IPermissionService _permissionService;
        private readonly ISwiperSliderService _swiperSliderService;
        private readonly INotificationService _notificationService;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ILocalizationService _localizationService;
        private readonly SwiperSliderSettings _swiperSliderSettings;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly ISwiperSliderModelFactory _swiperSliderModelFactory;
        #endregion

        #region Ctor
        public SwiperSliderController(
            IAclService aclService,
            IStoreService storeService,
            ISettingService settingService,
            CatalogSettings catalogSettings,
            ICustomerService customerService,
            IPermissionService permissionService,
            ISwiperSliderService swiperSliderService,
            INotificationService notificationService,
            ILocalizationService localizationService,
            IStoreMappingService storeMappingService,
            SwiperSliderSettings swiperSliderSettings,
            IBaseAdminModelFactory baseAdminModelFactory,
            ICustomerActivityService customerActivityService,
            ISwiperSliderModelFactory swiperSliderModelFactory
        )
        {
            _aclService = aclService;
            _storeService = storeService;
            _settingService = settingService;
            _catalogSettings = catalogSettings;
            _customerService = customerService;
            _permissionService = permissionService;
            _swiperSliderService = swiperSliderService;
            _notificationService = notificationService;
            _storeMappingService = storeMappingService;
            _localizationService = localizationService;
            _swiperSliderSettings = swiperSliderSettings;
            _baseAdminModelFactory = baseAdminModelFactory;
            _customerActivityService = customerActivityService;
            _swiperSliderModelFactory = swiperSliderModelFactory;
        }
        #endregion

        #region Utilities
        protected virtual async Task SaveSliderAclAsync(Data.Domain.Slider slider, SwiperSliderModel model)
        {
            slider.SubjectToAcl = model.SelectedCustomerRoleIds.Any();
            await _swiperSliderService.UpdateSliderAsync(slider);

            var existingAclRecords = await _aclService.GetAclRecordsAsync(slider);
            var allCustomerRoles = await _customerService.GetAllCustomerRolesAsync(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (!existingAclRecords.Any(acl => acl.CustomerRoleId == customerRole.Id))
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

        protected virtual async Task SaveSliderStoreMappingsAsync(Data.Domain.Slider slider, SwiperSliderModel model)
        {
            slider.LimitedToStores = model.SelectedStoreIds.Any();
            await _swiperSliderService.UpdateSliderAsync(slider);

            var existingStoreMappings = await _storeMappingService.GetStoreMappingsAsync(slider);
            var allStores = await _storeService.GetAllStoresAsync();
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds.Contains(store.Id))
                {
                    //new store
                    if (!existingStoreMappings.Any(sm => sm.StoreId == store.Id))
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
            var model = new SwiperSliderConfigurationModel
            {
                ContainerCssSelector = _swiperSliderSettings.ContainerCssSelector,
                PaginationCssSelector = _swiperSliderSettings.PaginationCssSelector,
                NavigationNextCssSelector = _swiperSliderSettings.NavigationNextCssSelector,
                NavigationPrevCssSelector = _swiperSliderSettings.NavigationPrevCssSelector,
                ScrollBarCssSelector = _swiperSliderSettings.ScrollBarCssSelector,
                Direction = _swiperSliderSettings.Direction,
                DirectionId = Convert.ToInt32(_swiperSliderSettings.Direction),
                InitialSlide = _swiperSliderSettings.InitialSlide,
                Speed = _swiperSliderSettings.Speed,
                Loop = _swiperSliderSettings.Loop,
                LoopFillGroupWithBlankEnabled = _swiperSliderSettings.LoopFillGroupWithBlankEnabled,
                PaginationEnabled = _swiperSliderSettings.PaginationEnabled,
                PaginationClickableEnabled = _swiperSliderSettings.PaginationClickableEnabled,
                NavigationEnabled = _swiperSliderSettings.NavigationEnabled,
                ScrollBarEnabled = _swiperSliderSettings.ScrollBarEnabled,
                AutoPlayEnabled = _swiperSliderSettings.AutoPlayEnabled,
                AutoPlayDelay = _swiperSliderSettings.AutoPlayDelay,
                AutoPlayDisableOnInteraction = _swiperSliderSettings.AutoPlayDisableOnInteraction,
                SlidesPerGroup = _swiperSliderSettings.SlidesPerGroup,
                SpaceBetween = _swiperSliderSettings.SpaceBetween,
                SlidesPerView = _swiperSliderSettings.SlidesPerView,
                FreeModeEnabled = _swiperSliderSettings.FreeModeEnabled,
                DynamicBulletsEnabled = _swiperSliderSettings.DynamicBulletsEnabled,
                CenteredSlidesEnabled = _swiperSliderSettings.CenteredSlidesEnabled,
                CustomCss = _swiperSliderSettings.CustomCss
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Configure(SwiperSliderConfigurationModel model)
        {
            _swiperSliderSettings.ContainerCssSelector = model.ContainerCssSelector;
            _swiperSliderSettings.PaginationCssSelector = model.PaginationCssSelector;
            _swiperSliderSettings.NavigationNextCssSelector = model.NavigationNextCssSelector;
            _swiperSliderSettings.NavigationPrevCssSelector = model.NavigationPrevCssSelector;
            _swiperSliderSettings.ScrollBarCssSelector = model.ScrollBarCssSelector;
            _swiperSliderSettings.Direction = (Direction)Enum.Parse(typeof(Direction), model.DirectionId.ToString());
            _swiperSliderSettings.InitialSlide = model.InitialSlide;
            _swiperSliderSettings.Speed = model.Speed;
            _swiperSliderSettings.Loop = model.Loop;
            _swiperSliderSettings.LoopFillGroupWithBlankEnabled = model.LoopFillGroupWithBlankEnabled;
            _swiperSliderSettings.PaginationEnabled = model.PaginationEnabled;
            _swiperSliderSettings.PaginationClickableEnabled = model.PaginationClickableEnabled;
            _swiperSliderSettings.NavigationEnabled = model.NavigationEnabled;
            _swiperSliderSettings.ScrollBarEnabled = model.ScrollBarEnabled;
            _swiperSliderSettings.AutoPlayEnabled = model.AutoPlayEnabled;
            _swiperSliderSettings.AutoPlayDelay = model.AutoPlayDelay;
            _swiperSliderSettings.AutoPlayDisableOnInteraction = model.AutoPlayDisableOnInteraction;
            _swiperSliderSettings.SlidesPerGroup = model.SlidesPerGroup;
            _swiperSliderSettings.SpaceBetween = model.SpaceBetween;
            _swiperSliderSettings.SlidesPerView = model.SlidesPerView;
            _swiperSliderSettings.FreeModeEnabled = model.FreeModeEnabled;
            _swiperSliderSettings.DynamicBulletsEnabled = model.DynamicBulletsEnabled;
            _swiperSliderSettings.CenteredSlidesEnabled = model.CenteredSlidesEnabled;
            _swiperSliderSettings.CustomCss = model.CustomCss;

            await _settingService.SaveSettingAsync(_swiperSliderSettings);
            await _settingService.ClearCacheAsync();

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Admin.Plugins.Saved"));

            return Configure();
        }
        #endregion

        #region List
        public async Task<IActionResult> Index()
        {
            return await Task.FromResult(RedirectToAction("List"));
        }

        public async Task<IActionResult> List()
        {
            var searchModel = new SwiperSliderSearchModel();

            await _baseAdminModelFactory.PrepareStoresAsync(searchModel.AvailableStores);
            searchModel.HideStoresList = _catalogSettings.IgnoreStoreLimitations || searchModel.AvailableStores.SelectionIsNotPossible();

            //prepare "published" filter (0 - all; 1 - published only; 2 - unpublished only)
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "0",
                Text = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.List.SearchPublished.All")
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "1",
                Text = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.List.SearchPublished.PublishedOnly")
            });
            searchModel.AvailablePublishedOptions.Add(new SelectListItem
            {
                Value = "2",
                Text = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Sliders.List.SearchPublished.UnpublishedOnly")
            });

            searchModel.SetGridPageSize();

            return View(searchModel);
        }

        [HttpPost]
        public async Task<IActionResult> List(SwiperSliderSearchModel searchModel)
        {
            var sliders = await _swiperSliderService.GetAllSlidersAsync(
                name: searchModel.SearchSliderName,
                storeId: searchModel.SearchStoreId,
                pageIndex: (searchModel.Page - 1),
                pageSize: searchModel.PageSize,
                showHidden: true,
                overridePublished: (searchModel.SearchPublishedId == 0 ? null : searchModel.SearchPublishedId == 1));

            var model = new SwiperSliderListModel().PrepareToGrid(searchModel, sliders, () =>
            {
                return sliders.Select(slider =>
                {
                    return slider.ToModel<SwiperSliderModel>();
                });
            });

            return Json(model);
        }

        #endregion

        #region CRUD
        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var model = await _swiperSliderModelFactory.PrepareSliderModelAsync(new SwiperSliderModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Create(SwiperSliderModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var slider = model.ToEntity<Data.Domain.Slider>();
                await _swiperSliderService.InsertSliderAsync(slider);

                await SaveSliderAclAsync(slider, model);

                await SaveSliderStoreMappingsAsync(slider, model);

                if (slider.Id > 0)
                {
                    //activity log
                    await _customerActivityService.InsertActivityAsync("AddNewSwiperSlider",
                        string.Format(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.AddNewSwiperSlider"), slider.Name), slider);

                    var message = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Added");
                    _notificationService.SuccessNotification(string.Format(message, slider.Name));
                }
                else
                    _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.NotAdded"));


                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = slider.Id });
            }

            model = await _swiperSliderModelFactory.PrepareSliderModelAsync(model, null);

            return View(model);
        }

        public virtual async Task<IActionResult> Edit(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var slider = await _swiperSliderService.GetSliderByIdAsync(id);
            if (slider == null)
                return RedirectToAction("List");

            var model = await _swiperSliderModelFactory.PrepareSliderModelAsync(null, slider);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Edit(SwiperSliderModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            var slider = await _swiperSliderService.GetSliderByIdAsync(model.Id);
            if (slider == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                slider = model.ToEntity(slider);
                await _swiperSliderService.UpdateSliderAsync(slider);

                await SaveSliderAclAsync(slider, model);
                await SaveSliderStoreMappingsAsync(slider, model);

                await _customerActivityService.InsertActivityAsync("EditSwiperSlider",
                    string.Format(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.EditSwiperSlider"), slider.Name), slider);

                var message = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Updated");
                _notificationService.SuccessNotification(string.Format(message, slider.Name));

                if (!continueEditing)
                    return RedirectToAction("List");

                return RedirectToAction("Edit", new { id = slider.Id });
            }

            model = await _swiperSliderModelFactory.PrepareSliderModelAsync(model, slider);

            return View(model);
        }


        [HttpPost]
        public virtual async Task<IActionResult> Delete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            //try to get a category with the specified id
            var slider = await _swiperSliderService.GetSliderByIdAsync(id);
            if (slider == null)
                return RedirectToAction("List");

            await _swiperSliderService.DeleteSliderAsync(slider);

            await _customerActivityService.InsertActivityAsync("DeleteSwiperSlider",
                string.Format(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.DeleteSwiperSlider"), slider.Name), slider);

            var message = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.Deleted");
            _notificationService.SuccessNotification(string.Format(message, slider.Name));

            return RedirectToAction("List");
        }

        [HttpPost]
        public virtual async Task<IActionResult> DeleteSelected(ICollection<int> selectedIds)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageSettings))
                return AccessDeniedView();

            if (selectedIds == null || selectedIds.Count == 0)
                return NoContent();

            var sliders = await _swiperSliderService.GetSliderByIdsAsync(selectedIds);
            await _swiperSliderService.DeleteSliderAsync(sliders);

            await _customerActivityService.InsertActivityAsync("DeleteSwiperSliders",
                string.Format(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.DeleteSwiperSliders"), string.Join(',', sliders.Select(p => p.Id))));

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Sliders.AllDeleted"));

            return Json(new { Result = true });

        }
        #endregion
    }
}
