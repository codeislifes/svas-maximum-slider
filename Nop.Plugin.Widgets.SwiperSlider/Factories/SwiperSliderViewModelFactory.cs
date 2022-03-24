using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Media;
using Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Services;
using Nop.Plugin.Widgets.SwiperSlider.Models;
using Nop.Services.Media;

namespace Nop.Plugin.Widgets.SwiperSlider.Factories
{
    public class SwiperSliderViewModelFactory : ISwiperSliderViewModelFactory
    {

        #region Fields

        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly MediaSettings _mediaSettings;
        private readonly IPictureService _pictureService;
        private readonly ISwiperSliderService _swiperSliderService;

        #endregion

        #region Ctor
        public SwiperSliderViewModelFactory
        (
            IWorkContext workContext,
            IStoreContext storeContext,
            MediaSettings mediaSettings,
            IPictureService pictureService,
            ISwiperSliderService swiperSliderService
        )
        {
            _workContext = workContext;
            _storeContext = storeContext;
            _mediaSettings = mediaSettings;
            _pictureService = pictureService;
            _swiperSliderService = swiperSliderService;
        }
        #endregion

        #region Methods
        public async Task<List<SwiperSliderViewModel>> GetAllSliders()
        {
            var customer = await _workContext.GetCurrentCustomerAsync();
            var store = await _storeContext.GetCurrentStoreAsync();

            var sliders = await _swiperSliderService.GetAllSlidersAsync(storeId: store.Id);

            var model = new List<SwiperSliderViewModel>();
            if (sliders.Any())
            {
                var sliderIds = sliders.Select(x => x.Id)?.ToArray();
                var sliderItems = await _swiperSliderService.GetAllSliderItemsAsync(sliderIds, store.Id);
                foreach (var sliderItem in sliderItems)
                {
                    var viewModel = new SwiperSliderViewModel
                    {
                        AlternateText = sliderItem.AlternateText,
                        DisplayOrder = sliderItem.DisplayOrder,
                        Url = sliderItem.Url
                    };

                    var picture = await _pictureService.GetPictureByIdAsync(sliderItem.PictureId);
                    (viewModel.PictureUrl, _) = await _pictureService.GetPictureUrlAsync(picture, _mediaSettings.MaximumImageSize);
                    model.Add(viewModel);
                }
            }

            return model;
        }
        #endregion
    }
}
