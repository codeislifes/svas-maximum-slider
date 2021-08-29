using codeislife.Widgets.CilSlider.Data.Domain;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Services.Customers;
using Nop.Services.Security;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;

namespace codeislife.Widgets.CilSlider.Services
{
    public class SliderService : ISliderService
    {
        #region Fields
        private readonly IWorkContext _workContext;
        private readonly CatalogSettings _catalogSettings;
        private readonly ICustomerService _customerService;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IRepository<SliderItem> _sliderItemRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        #endregion

        #region Ctor
        public SliderService(
            IWorkContext workContext,
            CatalogSettings catalogSettings,
            ICustomerService customerService,
            IRepository<AclRecord> aclRepository,
            IRepository<Slider> sliderRepository,
            IRepository<SliderItem> sliderItemRepository,
            IRepository<StoreMapping> storeMappingRepository)
        {
            _workContext = workContext;
            _catalogSettings = catalogSettings;
            _customerService = customerService;
            _aclRepository = aclRepository;
            _sliderRepository = sliderRepository;
            _sliderItemRepository = sliderItemRepository;
            _storeMappingRepository = storeMappingRepository;
        }


        #endregion

        #region Slider
        public IPagedList<Slider> GetAllSliders(
            string name,
            int storeId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false,
            bool? overridePublished = null)
        {
            var query = _sliderRepository.Table;

            if (!showHidden)
                query = query.Where(s => s.Published);

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(s => s.Name.Contains(name));

            query = query.OrderBy(p => p.DisplayOrder).ThenBy(p => p.Id);

            if ((storeId > 0 && !_catalogSettings.IgnoreStoreLimitations) || (!showHidden && !_catalogSettings.IgnoreAcl))
            {
                if (!showHidden && !_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    // TODO : Mehmet -> Burayı açıkla.
                    var allowedCustomerRolesIds = _customerService.GetCustomerRoleIds(_workContext.CurrentCustomer);
                    query = from c in query
                            join acl in _aclRepository.Table
                                on new
                                {
                                    c1 = c.Id,
                                    c2 = nameof(Slider)
                                }
                                equals
                                new
                                {
                                    c1 = acl.EntityId,
                                    c2 = acl.EntityName
                                } into c_acl
                            from acl in c_acl.DefaultIfEmpty()
                            where !c.SubjectToAcl || allowedCustomerRolesIds.Contains(acl.CustomerRoleId)
                            select c;
                }

                if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    // TODO : Mehmet -> Burayı açıkla.
                    query = from c in query
                            join sm in _storeMappingRepository.Table
                               on new
                               {
                                   c1 = c.Id,
                                   c2 = nameof(Slider)
                               }
                                equals
                                new
                                {
                                    c1 = sm.EntityId,
                                    c2 = sm.EntityName
                                } into c_sm
                            from sm in c_sm.DefaultIfEmpty()
                            where !c.LimitedToStores || storeId == sm.StoreId
                            select c;
                }
            }

            return new PagedList<Slider>(query, pageIndex, pageSize);
        }

        public Slider GetSliderById(int sliderId)
        {
            return _sliderRepository.GetById(sliderId);
        }

        public void InsertSlider(Slider slider)
        {
            _sliderRepository.Insert(slider);
        }

        public void UpdateSlider(Slider slider)
        {
            _sliderRepository.Update(slider);
        }

        public void DeleteSlider(Slider slider)
        {
            _sliderRepository.Delete(slider);
        }
        public void DeleteSlider(IList<Slider> sliders)
        {
            _sliderRepository.Delete(sliders);
        }
        public IList<Slider> GetSliderByIds(ICollection<int> sliderIds)
        {
            var query = _sliderRepository.Table;

            query = query.Where(p => sliderIds.Contains(p.Id));

            return query.ToList();
        }
        #endregion

        #region SliderItem
        public IList<SliderItem> GetAllSliderItemsBySliderId(int sliderId)
        {
            throw new System.NotImplementedException();
        }

        public SliderItem GetSliderItemById(int sliderItemId)
        {
            throw new System.NotImplementedException();
        }

        public void InsertSliderItem(SliderItem sliderItem)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateSliderItem(SliderItem sliderItem)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteSliderItem(SliderItem sliderItem)
        {
            throw new System.NotImplementedException();
        }

     
        #endregion
    }
}
