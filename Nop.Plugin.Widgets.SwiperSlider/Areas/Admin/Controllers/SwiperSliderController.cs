using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Factories;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Models;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Services;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
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
using Nop.Web.Framework.Models;
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
        private async Task SaveAclAsync<TEntity, TModel>(TEntity entity, TModel model)
            where TEntity : BaseEntity, IAclSupported
            where TModel : BaseNopEntityModel, IAclSupportedModel
        {
            var existingAclRecords = await _aclService.GetAclRecordsAsync(entity);
            var allCustomerRoles = await _customerService.GetAllCustomerRolesAsync(true);
            foreach (var customerRole in allCustomerRoles)
            {
                if (model.SelectedCustomerRoleIds.Contains(customerRole.Id))
                {
                    //new role
                    if (!existingAclRecords.Any(acl => acl.CustomerRoleId == customerRole.Id))
                        await _aclService.InsertAclRecordAsync(entity, customerRole.Id);
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
        private async Task SaveStoreMappingsAsync<TEntity, TModel>(TEntity entity, TModel model)
            where TEntity : BaseEntity, IStoreMappingSupported
            where TModel : BaseNopEntityModel, IStoreMappingSupportedModel
        {
            var existingStoreMappings = await _storeMappingService.GetStoreMappingsAsync(entity);
            var allStores = await _storeService.GetAllStoresAsync();
            foreach (var store in allStores)
            {
                if (model.SelectedStoreIds.Contains(store.Id))
                {
                    //new store
                    if (!existingStoreMappings.Any(sm => sm.StoreId == store.Id))
                        await _storeMappingService.InsertStoreMappingAsync(entity, store.Id);
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
        protected virtual async Task SaveSliderAclAsync(Slider slider, SwiperSliderModel model)
        {
            slider.SubjectToAcl = model.SelectedCustomerRoleIds.Any();
            await _swiperSliderService.UpdateSliderAsync(slider);
            await SaveAclAsync(slider, model);
        }
        protected virtual async Task SaveSliderStoreMappingsAsync(Slider slider, SwiperSliderModel model)
        {
            slider.LimitedToStores = model.SelectedStoreIds.Any();
            await _swiperSliderService.UpdateSliderAsync(slider);
            await SaveStoreMappingsAsync(slider, model);
        }
        protected virtual async Task SaveSliderItemAclAsync(SliderItem sliderItem, SwiperSliderItemModel model)
        {
            sliderItem.SubjectToAcl = model.SelectedCustomerRoleIds.Any();
            await _swiperSliderService.UpdateSliderItemAsync(sliderItem);
            await SaveAclAsync(sliderItem, model);
        }
        protected virtual async Task SaveSliderItemStoreMappingsAsync(SliderItem sliderItem, SwiperSliderItemModel model)
        {
            sliderItem.LimitedToStores = model.SelectedStoreIds.Any();
            await _swiperSliderService.UpdateSliderItemAsync(sliderItem);
            await SaveStoreMappingsAsync(sliderItem, model);
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
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = await _swiperSliderModelFactory.PrepareSliderSearchModelAsync(new SwiperSliderSearchModel());
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> List(SwiperSliderSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = await _swiperSliderModelFactory.PrepareSliderListModelAsync(searchModel);

            return Json(model);
        }

        #endregion

        #region CRUD
        public virtual async Task<IActionResult> Create()
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var model = await _swiperSliderModelFactory.PrepareSliderModelAsync(new SwiperSliderModel(), null);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> Create(SwiperSliderModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
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
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
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
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
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
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
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
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
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

        #region Slider Items
        [HttpPost]
        public virtual async Task<IActionResult> SliderItemList(SwiperSliderItemSearchModel searchModel)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return await AccessDeniedDataTablesJson();

            var model = await _swiperSliderModelFactory.PrepareSliderItemListModelAsync(searchModel);

            return Json(model);
        }

        public virtual async Task<IActionResult> SliderItemCreate(int sliderId)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (await _swiperSliderService.GetSliderByIdAsync(sliderId) == null)
            {
                _notificationService.ErrorNotification("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.Slider.NotFound");
                return RedirectToAction("List");
            }

            try
            {
                var model = await _swiperSliderModelFactory.PrepareSliderItemModelAsync(new SwiperSliderItemModel { SliderId = sliderId }, null);
                return View(model);

            }
            catch (Exception ex)
            {
                await _notificationService.ErrorNotificationAsync(ex);

                //select an appropriate card
                SaveSelectedCardName("swiper-slider-items");
                return RedirectToAction("Edit", new { id = sliderId });
            }
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> SliderItemCreate(SwiperSliderItemModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (ModelState.IsValid)
            {
                var sliderItem = model.ToEntity<SliderItem>();
                await _swiperSliderService.InsertSliderItemAsync(sliderItem);

                await SaveSliderItemAclAsync(sliderItem, model);

                await SaveSliderItemStoreMappingsAsync(sliderItem, model);

                if (sliderItem.Id > 0)
                {
                    //activity log
                    await _customerActivityService.InsertActivityAsync("AddNewSwiperSliderItem",
                        string.Format(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.AddNewSwiperSliderItem"), sliderItem.Name), sliderItem);

                    var message = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.Added");
                    _notificationService.SuccessNotification(string.Format(message, sliderItem.Name));

                    if (continueEditing)
                        return RedirectToAction("SliderItemEdit", new { id = sliderItem.Id, sliderId = sliderItem.SliderId });
                }
                else
                {
                    _notificationService.ErrorNotification(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.NotAdded"));
                }

                //select an appropriate card
                SaveSelectedCardName("swiper-slider-items");
                return RedirectToAction("Edit", new { id = model.SliderId });
            }

            model = await _swiperSliderModelFactory.PrepareSliderItemModelAsync(model, null);

            return View(model);
        }

        public virtual async Task<IActionResult> SliderItemEdit(int id, int sliderId)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var sliderItem = await _swiperSliderService.GetSliderItemByIdAsync(id);
            if (sliderItem == null)
                return RedirectToAction("Edit", new { id = sliderId });

            var model = await _swiperSliderModelFactory.PrepareSliderItemModelAsync(null, sliderItem);

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public virtual async Task<IActionResult> SliderItemEdit(SwiperSliderItemModel model, bool continueEditing)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            var sliderItem = await _swiperSliderService.GetSliderItemByIdAsync(model.Id);
            if (sliderItem == null)
                return RedirectToAction("Edit", new { id = model.SliderId });

            if (ModelState.IsValid)
            {
                sliderItem = model.ToEntity(sliderItem);
                await _swiperSliderService.UpdateSliderItemAsync(sliderItem);

                await SaveSliderItemAclAsync(sliderItem, model);
                await SaveSliderItemStoreMappingsAsync(sliderItem, model);


                await _customerActivityService.InsertActivityAsync("EditSwiperSliderItem",
                    string.Format(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.EditSwiperSliderItem"), sliderItem.Name), sliderItem);

                var message = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.Updated");
                _notificationService.SuccessNotification(string.Format(message, sliderItem.Name));

                if (continueEditing)
                    return RedirectToAction("SliderItemEdit", new { id = sliderItem.Id, sliderId = model.SliderId });

                //select an appropriate card
                SaveSelectedCardName("swiper-slider-items");
                return RedirectToAction("Edit", new { id = model.SliderId });
            }

            model = await _swiperSliderModelFactory.PrepareSliderItemModelAsync(model, null);

            return View(model);
        }

        [HttpPost]
        public virtual async Task<IActionResult> SliderItemDelete(int id)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            //try to get a category with the specified id
            var sliderItem = await _swiperSliderService.GetSliderItemByIdAsync(id);
            if (sliderItem == null)
                return RedirectToAction("List");

            await _swiperSliderService.DeleteSliderItemAsync(sliderItem);

            await _customerActivityService.InsertActivityAsync("DeleteSwiperSliderItem",
                string.Format(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.DeleteSwiperSliderItem"), sliderItem.Name), sliderItem);

            var message = await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.Deleted");
            _notificationService.SuccessNotification(string.Format(message, sliderItem.Name));

            //select an appropriate card
            SaveSelectedCardName("swiper-slider-items");
            return RedirectToAction("Edit", new { id = sliderItem.SliderId });
        }

        [HttpPost]
        public virtual async Task<IActionResult> SliderItemDeleteSelected(ICollection<int> selectedIds)
        {
            if (!await _permissionService.AuthorizeAsync(StandardPermissionProvider.ManageWidgets))
                return AccessDeniedView();

            if (selectedIds == null || selectedIds.Count == 0)
                return NoContent();

            var sliders = await _swiperSliderService.GetSliderItemByIdsAsync(selectedIds);
            await _swiperSliderService.DeleteSliderItemAsync(sliders);

            await _customerActivityService.InsertActivityAsync("DeleteSwiperSliderItems",
                string.Format(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.ActivityLog.DeleteSwiperSliderItems"), string.Join(',', sliders.Select(p => p.Id))));

            _notificationService.SuccessNotification(await _localizationService.GetResourceAsync("Nop.Plugin.Widgets.SwiperSlider.Admin.Notifications.SliderItems.AllDeleted"));

            return Json(new { Result = true });

        }
        #endregion
    }
}
