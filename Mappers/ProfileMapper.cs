using AutoMapper;
using BackendStoreAPI.DTOs;
using BackendStoreAPI.Models;

namespace BackendStoreAPI.Mappers;

public class ProfileMapper : Profile
{
    public ProfileMapper()
    {
        CreateMap<Order, OrderDTO>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));
        
        CreateMap<Product, ProductDTO>();
        
        CreateMap<OrderDTO, Order>()
            .ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products));

        CreateMap<ProductDTO, Product>();
    }
}