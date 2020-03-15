using SM.Application.Models;
using SM.Application.ResourceParameters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface IServiceCategoryService
    {
        Task<IEnumerable<ServiceCategoryDetailModel>> GetAllServiceCategoriesAsync(bool includeServices = false);

        Task<IEnumerable<ServiceCategoryDetailModel>> GetAllServiceCategoriesAsync(ServiceCategoryParameters serviceCategoryParameters);

        Task<ServiceCategoryDetailModel> GetServiceCategoryAsync(int id, bool includeServices = false);

        Task<ServiceCategoryDetailModel> GetServiceCategoryByNameAsync(string name, bool includeServices = false);

        Task<ServiceCategoryModel> AddServiceCategoryAsync(ServiceCategoryModel serviceCategoryCreateModel);

        Task<ServiceCategoryModel> UpdateServiceCategory(ServiceCategoryModel serviceCategoryModel);

        Task<bool> DeleteServiceCategoryAsync(int id);

        Task<bool> DisableServiceCategoryAsync(int id);

        Task<IEnumerable<ServiceCategoryDetailModel>> SearchServiceCategoriesByNameAsync(string name, bool includeServices = false);
    }
}
