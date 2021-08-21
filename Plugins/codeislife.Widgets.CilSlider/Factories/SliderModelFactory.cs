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

        public SliderModel PrepareSliderModel(SliderModel model, Slider entity)
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
            _aclSupportedModelFactory.PrepareModelCustomerRoles(model, entity, false);

            //prepare model stores
            _storeMappingSupportedModelFactory.PrepareModelStores(model, entity, false);

            return model;
        }
    }
}
