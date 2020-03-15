using AutoMapper;
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
    public class ServiceDetailService : IServiceDetailService
    {
        private readonly ISMDbContext _dbContext;
        private readonly IMapper _mapper;

        public ServiceDetailService(ISMDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<ServiceDetailModel>> GetAllServiceDetailsAsync()
        {
            var services = await _dbContext.Services
                .Include(x => x.ServiceCategory)
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ServiceDetailModel>>(services);
        }

        public async Task<IEnumerable<ServiceDetailModel>> GetAllServiceDetailsAsync(ServiceDetailParameters serviceDetailParameters)
        {
            IQueryable<Service> query = _dbContext.Services
                .Include(x => x.ServiceCategory)
                .Where(x => x.IsActive == true)
                .OrderBy(x => x.Name);

            if (!string.IsNullOrWhiteSpace(serviceDetailParameters.ServiceName))
            {
                var serviceName = serviceDetailParameters.ServiceName.Trim();

                query = query
                    .Where(x => x.Name == serviceName);
            }

            if (!string.IsNullOrWhiteSpace(serviceDetailParameters.SearchTerm))
            {
                var searchTerm = serviceDetailParameters.SearchTerm.Trim();

                query = query
                    .Where(x => x.Name.Contains(searchTerm)
                        || x.Description.Contains(searchTerm));
            }

            var services = await query.ToListAsync();

            return _mapper.Map<IEnumerable<ServiceDetailModel>>(services);
        }

        public async Task<ServiceDetailModel> GetServiceDetailAsync(int id)
        {
            var service = await _dbContext.Services
                .Include(x => x.ServiceCategory)
                .Where(x => x.IsActive == true && x.Id == id)
                .FirstOrDefaultAsync();

            return _mapper.Map<ServiceDetailModel>(service);
        }

        public async Task<ServiceDetailModel> GetServiceDetailByNameAsync(string name)
        {
            var service = await _dbContext.Services
                .Include(x => x.ServiceCategory)
                .Where(x => x.IsActive == true && x.Name == name)
                .FirstOrDefaultAsync();

            return _mapper.Map<ServiceDetailModel>(service);
        }

        public async Task<ServiceDetailModel> AddServiceDetailAsync(ServiceCreateModel serviceCreateModel)
        {
            var service = _mapper.Map<Service>(serviceCreateModel);

            _dbContext.Services.Add(service);

            await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());

            return await GetServiceDetailAsync(service.Id);
        }

        public async Task<ServiceDetailModel> UpdateServiceDetail(int id, ServiceUpdateModel serviceUpdateModel)
        {
            var existingService = _dbContext.Services
                .Where(x => x.Id == id)
                .SingleOrDefault();

            _mapper.Map(serviceUpdateModel, existingService);

            await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken());

            return await GetServiceDetailAsync(id);
        }

        public async Task<bool> DeleteServiceDetailAsync(int id)
        {
            var service = await _dbContext.Services
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (service != null)
            {
                _dbContext.Services.Remove(service);
                return (await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken()) > 0);
            }

            return false;
        }

        public async Task<bool> DisableServiceDetailAsync(int id)
        {
            var service = _dbContext.Services
                .Where(x => x.Id == id)
                .SingleOrDefault();

            if (service != null)
            {
                service.IsActive = false;
                return (await _dbContext.SaveChangesAsync(new System.Threading.CancellationToken()) > 0);
            }

            return false;
        }

        public async Task<IEnumerable<ServiceDetailModel>> SearchServiceDetailsByNameAsync(string name)
        {
            var services = await _dbContext.Services
                .Where(x => x.IsActive == true && x.Name.ToLowerInvariant().Contains(name.ToLowerInvariant()))
                .OrderBy(x => x.Name)
                .ToListAsync();

            return _mapper.Map<IEnumerable<ServiceDetailModel>>(services);
        }

        public async Task<bool> ServiceCategoryExistAsync(int serviceCategoryId)
        {
            return await _dbContext.ServiceCategories.Where(x => x.Id == serviceCategoryId).AnyAsync();
        }
    }
}
