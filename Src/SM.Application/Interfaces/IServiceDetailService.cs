using SM.Application.Models;
using SM.Application.ResourceParameters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SM.Application.Interfaces
{
    public interface IServiceDetailService
    {
        Task<IEnumerable<ServiceDetailModel>> GetAllServiceDetailsAsync();

        Task<IEnumerable<ServiceDetailModel>> GetAllServiceDetailsAsync(ServiceDetailParameters serviceDetailParameters);

        Task<ServiceDetailModel> GetServiceDetailAsync(int id);

        Task<ServiceDetailModel> GetServiceDetailByNameAsync(string name);

        Task<ServiceDetailModel> AddServiceDetailAsync(ServiceCreateModel serviceCreateModel);

        Task<ServiceDetailModel> UpdateServiceDetail(int id, ServiceUpdateModel serviceUpdateModel);

        Task<bool> DeleteServiceDetailAsync(int id);

        Task<bool> DisableServiceDetailAsync(int id);

        Task<bool> ServiceCategoryExistAsync(int serviceCategoryId);

        Task<IEnumerable<ServiceDetailModel>> SearchServiceDetailsByNameAsync(string name);
    }
}
