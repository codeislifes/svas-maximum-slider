using System.Threading.Tasks;
using codeislife.Widgets.CilSlider.Data.Domain;
using codeislife.Widgets.CilSlider.Models;
using Nop.Web.Areas.Admin.Infrastructure.Mapper.Extensions;
using Nop.Web.Framework.Factories;

namespace codeislife.Widgets.CilSlider.Factories
{
    public class SliderModelFactory : ISliderModelFactory
    {
        #region Fields
        private readonly IAclSupportedModelFactory _aclSupportedModelFactory;
        private readonly IStoreMappingSupportedModelFactory _storeMappingSupportedModelFactory;
        #endregion

        #region Ctor
        public SliderModelFactory(
            IAclSupportedModelFactory aclSupportedModelFactory,
            IStoreMappingSupportedModelFactory storeMappingSupportedModelFactory)
        {
            _aclSupportedModelFactory = aclSupportedModelFactory;
            _storeMappingSupportedModelFactory = storeMappingSupportedModelFactory;
        }
        #endregion

        public async Task<SliderModel> PrepareSliderModelAsync(SliderModel model, Slider entity)
        {
            if(entity != null)
            {
                if(model == null)
                {
                    model = entity.ToModel<SliderModel>();
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
