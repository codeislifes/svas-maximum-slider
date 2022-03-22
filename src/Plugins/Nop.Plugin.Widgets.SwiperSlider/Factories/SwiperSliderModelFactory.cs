using System.Threading.Tasks;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
using Nop.Plugin.Widgets.SwiperSlider.Models;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Factories;

namespace Nop.Plugin.Widgets.SwiperSlider.Factories
{
    public class SwiperSliderModelFactory : ISwiperSliderModelFactory
    {
        #region Fields
        private readonly IAclSupportedModelFactory _aclSupportedModelFactory;
        private readonly IStoreMappingSupportedModelFactory _storeMappingSupportedModelFactory;
        #endregion

        #region Ctor
        public SwiperSliderModelFactory(
            IAclSupportedModelFactory aclSupportedModelFactory,
            IStoreMappingSupportedModelFactory storeMappingSupportedModelFactory)
        {
            _aclSupportedModelFactory = aclSupportedModelFactory;
            _storeMappingSupportedModelFactory = storeMappingSupportedModelFactory;
        }
        #endregion

        public async Task<SwiperSliderModel> PrepareSliderModelAsync(SwiperSliderModel model, Data.Domain.Slider entity)
        {
            if(entity != null)
            {
                if(model == null)
                {
                    model = entity.ToModel<SwiperSliderModel>();
                }
            }

            if(entity == null)
            {
                model.Published = true;
            }

            //prepare model customer roles
            await _aclSupportedModelFactory.PrepareModelCustomerRolesAsync(model, entity, false);

            //prepare model stores
            await _storeMappingSupportedModelFactory.PrepareModelStoresAsync(model, entity, false);

            return model;
        }
    }
}
