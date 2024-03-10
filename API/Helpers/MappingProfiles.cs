﻿using AutoMapper;
using Core;
using Core.Entities;

namespace API;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Product, ProductDto>()
        .ForMember(d => d.ProductType, o => o.MapFrom(p => p.ProductType.Name))
        .ForMember(d => d.ProductBrand, o => o.MapFrom(p => p.ProductBrand.Name))
        .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductUrlResolver>())
        ;

        CreateMap<Address, AddressDto>().ReverseMap();
        CreateMap<BasketItemDto, BasketItem>();
        CreateMap<CustomerBasketDto, CustomerBasket>();
    }
}
