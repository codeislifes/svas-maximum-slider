using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Models;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Services;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Web.Areas.Admin.Factories;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Extensions;
using Nop.Web.Framework.Factories;
using Nop.Web.Framework.Models.Extensions;

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Factories
{
    public class SwiperSliderModelFactory : ISwiperSliderModelFactory
    {
        #region Fields

        private readonly IPictureService _pictureService;
        private readonly CatalogSettings _catalogSettings;
        private readonly ILocalizationService _localizationService;
        private readonly ISwiperSliderService _swiperSliderService;
        private readonly IBaseAdminModelFactory _baseAdminModelFactory;
        private readonly IAclSupportedModelFactory _aclSupportedModelFactory;
        private readonly IStoreMappingSupportedModelFactory _storeMappingSupportedModelFactory;

        #endregion

        #region Ctor
        public SwiperSliderModelFactory
        (
            IPictureService pictureService,
            CatalogSettings catalogSettings,
            ILocalizationService localizationService,
            ISwiperSliderService swiperSliderService,
            IBaseAdminModelFactory baseAdminModelFactory,
            IAclSupportedModelFactory aclSupportedModelFactory,
            IStoreMappingSupportedModelFactory storeMappingSupportedModelFactory
        )
        {
            _pictureService = pictureService;
            _catalogSettings = catalogSettings;
            _localizationService = localizationService;
            _swiperSliderService = swiperSliderService;
            _baseAdminModelFactory = baseAdminModelFactory;
            _aclSupportedModelFactory = aclSupportedModelFactory;
            _storeMappingSupportedModelFactory = storeMappingSupportedModelFactory;
        }
        #endregion

        #region Slider
        public virtual async Task<SwiperSliderSearchModel> PrepareSliderSearchModelAsync(SwiperSliderSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

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

            return searchModel;
        }

        public virtual async Task<SwiperSliderListModel> PrepareSliderListModelAsync(SwiperSliderSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var sliders = await _swiperSliderService.GetAllSlidersAsync(
                name: searchModel.SearchSliderName,
                storeId: searchModel.SearchStoreId,
                pageIndex: (searchModel.Page - 1),
                pageSize: searchModel.PageSize,
                showHidden: true,
                overridePublished: (searchModel.SearchPublishedId == 0 ? null : searchModel.SearchPublishedId == 1));

            var model = await new SwiperSliderListModel().PrepareToGridAsync(searchModel, sliders, () =>
            {
                return sliders.SelectAwait(async slider =>
                {
                    return await PrepareSliderModelAsync(null, slider);
                });
            });

            return model;
        }

        public virtual async Task<SwiperSliderModel> PrepareSliderModelAsync(SwiperSliderModel model, Slider slider)
        {
            if (slider != null)
            {
                if (model == null)
                {
                    model = slider.ToModel<SwiperSliderModel>();
                }
                await PrepareSliderItemSearchModelAsync(model.SwiperSliderItemSearchModel, slider);
            }


            if (slider == null)
                model.Published = true;

            //prepare model customer roles
            await _aclSupportedModelFactory.PrepareModelCustomerRolesAsync(model, slider, false);

            //prepare model stores
            await _storeMappingSupportedModelFactory.PrepareModelStoresAsync(model, slider, false);

            return model;
        }
        #endregion

        #region Slider Item
        public virtual async Task<SwiperSliderItemSearchModel> PrepareSliderItemSearchModelAsync(SwiperSliderItemSearchModel searchModel, Slider slider)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            if (slider == null)
                throw new ArgumentNullException(nameof(slider));

            searchModel.SliderId = slider.Id;

            searchModel.SetGridPageSize();

            return await Task.FromResult(searchModel);
        }

        public virtual async Task<SwiperSliderItemListModel> PrepareSliderItemListModelAsync(SwiperSliderItemSearchModel searchModel)
        {
            if (searchModel == null)
                throw new ArgumentNullException(nameof(searchModel));

            var sliderItems = await _swiperSliderService.GetAllSliderItemsAsync(
                sliderIds: new int[] { searchModel.SliderId },
                storeId: searchModel.SearchStoreId,
                pageIndex: (searchModel.Page - 1),
                pageSize: searchModel.PageSize,
                showHidden: true,
                overridePublished: (searchModel.SearchPublishedId == 0 ? null : searchModel.SearchPublishedId == 1));

            var model = await new SwiperSliderItemListModel().PrepareToGridAsync(searchModel, sliderItems, () =>
            {
                return sliderItems.SelectAwait(async sliderItem =>
                {
                    return await PrepareSliderItemModelAsync(null, sliderItem);
                });
            });

            return model;
        }

        public virtual async Task<SwiperSliderItemModel> PrepareSliderItemModelAsync(SwiperSliderItemModel model, SliderItem sliderItem)
        {
            if (sliderItem != null)
            {
                if (model == null)
                {
                    model = sliderItem.ToModel<SwiperSliderItemModel>();
                }

                var slider = await _swiperSliderService.GetSliderByIdAsync(sliderId: sliderItem.SliderId);
                model.SliderName = slider.Name;

                var picture = await _pictureService.GetPictureByIdAsync(sliderItem.PictureId);
                (model.PictureThumbnailUrl, _) = await _pictureService.GetPictureUrlAsync(picture, 120);
            }

            if (sliderItem == null)
                model.Published = true;

            //prepare model customer roles
            await _aclSupportedModelFactory.PrepareModelCustomerRolesAsync(model, sliderItem, false);

            //prepare model stores
            await _storeMappingSupportedModelFactory.PrepareModelStoresAsync(model, sliderItem, false);

            return model;
        }
        #endregion
    }
}
