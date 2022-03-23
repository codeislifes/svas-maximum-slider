using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Stores;
using Nop.Core.Events;
using Nop.Data;
using Nop.Plugin.Widgets.SwiperSlider.Data.Domain;
using Nop.Services.Customers;

namespace Nop.Plugin.Widgets.SwiperSlider.Areas.Admin.Services
{
    public class SwiperSliderService : ISwiperSliderService
    {
        #region Fields
        private readonly IWorkContext _workContext;
        private readonly IEventPublisher _eventPublisher;
        private readonly CatalogSettings _catalogSettings;
        private readonly ICustomerService _customerService;
        private readonly IRepository<AclRecord> _aclRepository;
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IRepository<SliderItem> _sliderItemRepository;
        private readonly IRepository<StoreMapping> _storeMappingRepository;
        #endregion

        #region Ctor
        public SwiperSliderService(
            IWorkContext workContext,
            IEventPublisher eventPublisher,
            CatalogSettings catalogSettings,
            ICustomerService customerService,
            IRepository<AclRecord> aclRepository,
            IRepository<Slider> sliderRepository,
            IRepository<SliderItem> sliderItemRepository,
            IRepository<StoreMapping> storeMappingRepository
        )
        {
            _workContext = workContext;
            _eventPublisher = eventPublisher;
            _catalogSettings = catalogSettings;
            _customerService = customerService;
            _aclRepository = aclRepository;
            _sliderRepository = sliderRepository;
            _sliderItemRepository = sliderItemRepository;
            _storeMappingRepository = storeMappingRepository;
        }


        #endregion

        #region Slider
        public async Task<IPagedList<Slider>> GetAllSlidersAsync(
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
                    var allowedCustomerRolesIds = await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync());
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
                            from acl in Enumerable.DefaultIfEmpty<AclRecord>(c_acl)
                            where !c.SubjectToAcl || allowedCustomerRolesIds.Contains((int)acl.CustomerRoleId)
                            select c;
                }

                if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
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
                            from sm in Enumerable.DefaultIfEmpty<StoreMapping>(c_sm)
                            where !c.LimitedToStores || storeId == sm.StoreId
                            select c;
                }
            }

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<Slider> GetSliderByIdAsync(int sliderId)
        {
            return await _sliderRepository.GetByIdAsync(sliderId);
        }

        public async Task InsertSliderAsync(Slider slider)
        {
            await _sliderRepository.InsertAsync(slider);
        }

        public async Task UpdateSliderAsync(Slider slider)
        {
            await _sliderRepository.UpdateAsync(slider);
        }

        public async Task DeleteSliderAsync(Slider slider)
        {
            await _sliderRepository.DeleteAsync(slider);
        }
        public async Task DeleteSliderAsync(IList<Slider> sliders)
        {
            await _sliderRepository.DeleteAsync(sliders);
        }
        public async Task<IList<Slider>> GetSliderByIdsAsync(ICollection<int> sliderIds)
        {
            var query = _sliderRepository.Table;

            query = query.Where(p => sliderIds.Contains(p.Id));

            return await query.ToListAsync();
        }
        #endregion

        #region SliderItem
        public async Task<IPagedList<SliderItem>> GetAllSliderItemsAsync(
            int sliderId = 0,
            int storeId = 0,
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            bool showHidden = false,
            bool? overridePublished = null)
        {
            var query = _sliderItemRepository.Table;

            if (!showHidden)
                query = query.Where(s => s.Published);

            query = query.OrderBy(p => p.DisplayOrder).ThenBy(p => p.Id);

            if ((storeId > 0 && !_catalogSettings.IgnoreStoreLimitations) || (!showHidden && !_catalogSettings.IgnoreAcl))
            {
                if (!showHidden && !_catalogSettings.IgnoreAcl)
                {
                    //ACL (access control list)
                    var allowedCustomerRolesIds = await _customerService.GetCustomerRoleIdsAsync(await _workContext.GetCurrentCustomerAsync());
                    query = from c in query
                            join acl in _aclRepository.Table
                                on new
                                {
                                    c1 = c.Id,
                                    c2 = nameof(SliderItem)
                                }
                                equals
                                new
                                {
                                    c1 = acl.EntityId,
                                    c2 = acl.EntityName
                                } into c_acl
                            from acl in Enumerable.DefaultIfEmpty<AclRecord>(c_acl)
                            where !c.SubjectToAcl || allowedCustomerRolesIds.Contains((int)acl.CustomerRoleId)
                            select c;
                }

                if (storeId > 0 && !_catalogSettings.IgnoreStoreLimitations)
                {
                    //Store mapping
                    query = from c in query
                            join sm in _storeMappingRepository.Table
                               on new
                               {
                                   c1 = c.Id,
                                   c2 = nameof(SliderItem)
                               }
                                equals
                                new
                                {
                                    c1 = sm.EntityId,
                                    c2 = sm.EntityName
                                } into c_sm
                            from sm in Enumerable.DefaultIfEmpty<StoreMapping>(c_sm)
                            where !c.LimitedToStores || storeId == sm.StoreId
                            select c;
                }
            }

            return await query.ToPagedListAsync(pageIndex, pageSize);
        }

        public async Task<SliderItem> GetSliderItemByIdAsync(int sliderItemId)
        {
            return await _sliderItemRepository.GetByIdAsync(sliderItemId);
        }

        public async Task InsertSliderItemAsync(SliderItem sliderItem)
        {
            await _sliderItemRepository.InsertAsync(sliderItem);
        }

        public async Task UpdateSliderItemAsync(SliderItem sliderItem)
        {
            await _sliderItemRepository.UpdateAsync(sliderItem);
        }

        public async Task DeleteSliderItemAsync(SliderItem sliderItem)
        {
            await _sliderItemRepository.DeleteAsync(sliderItem);
        }
        #endregion
    }
}
