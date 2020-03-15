using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SM.Application.Interfaces;
using SM.Application.Models;
using SM.Application.ResourceParameters;
using SM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SM.Infrastructure.Services
{
    public class ServiceCategoryService : IServiceCategoryService
    {
        private readonly ISMDbContext _dbContext;
        private readonly IMapper _mapper;

        public ServiceCategoryService(ISMDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }

        public async Task<IEnumerable<ServiceCategoryDetailModel>> GetAllServiceCategoriesAsync(bool includeServices)
        {
            IQueryable<ServiceCategory> query = _dbContext.ServiceCategories
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Name);

            if (includeServices)
                query = query.Include(x => x.Services);

            var serviceCategories = await query.ToListAsync();

            return _mapper.Map<IEnumerable<ServiceCategoryDetailModel>>(serviceCategories);
        }

        public async Task<IEnumerable<ServiceCategoryDetailModel>> GetAllServiceCategoriesAsync(ServiceCategoryParameters serviceCategoryParameters)
        {
            IQueryable<ServiceCategory> query = _dbContext.ServiceCategories
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Name);

            if (!string.IsNullOrWhiteSpace(serviceCategoryParameters.ServiceCategoryName))
            {
                var serviceCategoryName = serviceCategoryParameters.ServiceCategoryName.Trim();

                query = query
                    .Where(x => x.Name == serviceCategoryName);
            }

            if (!string.IsNullOrWhiteSpace(serviceCategoryParameters.SearchTerm))
            {
                var searchTerm = serviceCategoryParameters.SearchTerm.Trim();

                query = query
                    .Where(x => x.Name.Contains(searchTerm) 
                        || x.Description.Contains(searchTerm));
            }

            if (serviceCategoryParameters.IncludeServices)
                query = query.Include(x => x.Services);

            var serviceCategories = await query.ToListAsync();

            return _mapper.Map<IEnumerable<ServiceCategoryDetailModel>>(serviceCategories);
        }

        public async Task<ServiceCategoryDetailModel> GetServiceCategoryAsync(int id, bool includeServices)
        {
            var query = _dbContext.ServiceCategories
                .Where(x => x.IsActive == true && x.Id == id);

            if (includeServices)
                query = query.Include(x => x.Services);

            var serviceCategory = await query.FirstOrDefaultAsync();

            return _mapper.Map<ServiceCategoryDetailModel>(serviceCategory);
        }

        public async Task<ServiceCategoryDetailModel> GetServiceCategoryByNameAsync(string name, bool includeServices)
        {
            var query = _dbContext.ServiceCategories
                .Where(x => x.IsActive == true && x.Name == name);

            if (includeServices)
                query = query.Include(x => x.Services);

            var serviceCategory = await query.FirstOrDefaultAsync();

            return _mapper.Map<ServiceCategoryDetailModel>(serviceCategory);
        }

        public async Task<ServiceCategoryModel> AddServiceCategoryAsync(ServiceCategoryModel serviceCategoryModel)
        {            
            var serviceCategory = _mapper.Map<ServiceCategory>(serviceCategoryModel);

            _dbContext.ServiceCategories.Add(serviceCategory);

            await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());

            return _mapper.Map<ServiceCategoryModel>(serviceCategory);
        }

        public async Task<ServiceCategoryModel> UpdateServiceCategory(ServiceCategoryModel serviceCategoryModel)
        {
            var existingServiceCategory = _dbContext.ServiceCategories.Where(x => x.Id == serviceCategoryModel.Id)
                .SingleOrDefault();

            _mapper.Map(serviceCategoryModel, existingServiceCategory);

            await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());

            return _mapper.Map<ServiceCategoryModel>(existingServiceCategory);
        }

        public async Task<bool> DeleteServiceCategoryAsync(int id)
        {
            var serviceCategory = await _dbContext.ServiceCategories
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if(serviceCategory != null)
            {
                _dbContext.ServiceCategories.Remove(serviceCategory);
                return (await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken()) > 0);
            }

            return false;
        }

        public async Task<bool> DisableServiceCategoryAsync(int id)
        {
            var existingServiceCategory = _dbContext.ServiceCategories
                .Where(x => x.Id == id)
                .SingleOrDefault();

            if (existingServiceCategory != null)
            {
                existingServiceCategory.IsActive = false;
                return (await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken()) > 0);
            }

            return false;
        }

        public async Task<IEnumerable<ServiceCategoryDetailModel>> SearchServiceCategoriesByNameAsync(string name, bool includeServices)
        {
            IQueryable<ServiceCategory> query = _dbContext.ServiceCategories
                .Where(x => x.IsActive == true && x.Name.Contains(name))
                .OrderBy(x => x.Name);

            if (includeServices)
                query = query.Include(x => x.Services);

            var serviceCategories = await query.ToListAsync();

            return _mapper.Map<IEnumerable<ServiceCategoryDetailModel>>(serviceCategories);
        }
    }
}
