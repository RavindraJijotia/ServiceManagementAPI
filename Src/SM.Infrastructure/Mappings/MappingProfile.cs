using AutoMapper;
using SM.Application.Models;
using SM.Domain.Common;
using SM.Domain.Entities;
using System;
using System.Linq;
using System.Reflection;

namespace SM.Infrastructure.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BaseEntity, BaseEntityModel>();
            CreateMap<BaseEntityModel, BaseEntity>()
                .ForMember(dest => dest.Created, option => option.Ignore())
                .ForMember(dest => dest.CreatedBy, option => option.Ignore())
                .ForMember(dest => dest.LastModified, option => option.Ignore())
                .ForMember(dest => dest.LastModifiedBy, option => option.Ignore());

            CreateMap<ServiceCategory, ServiceCategoryModel>()
                .ReverseMap();
            CreateMap<ServiceCategory, ServiceCategoryDetailModel>()
                .ReverseMap();

            CreateMap<Service, ServiceModel>();
            CreateMap<Service, ServiceDetailModel>()
                .ForMember(dest => dest.ServiceCategoryName, opt => opt.MapFrom(src => src.ServiceCategory.Name))
                .ReverseMap();
            CreateMap<ServiceCreateModel, Service>();
            CreateMap<ServiceUpdateModel, Service>();

        }
    }
}
