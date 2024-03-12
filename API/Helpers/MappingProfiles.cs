using API.Dtos;
using API.Helpers;
using AutoMapper;
using Core;
using Core.Entities;
using Core.Entities.OrderAggregate;

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
        CreateMap<AddressDto, AddressOrder>();

        CreateMap<Order, OrderToReturnDto>()
        .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
        .ForMember(d => d.ShippingPrice, o => o.MapFrom(s => s.DeliveryMethod.Price))
        ;

        CreateMap<OrderItem, OrderItemDto>()
        .ForMember(d => d.ProductId, o => o.MapFrom(s => s.ItemOrdered.ProductItemId))
        .ForMember(d => d.ProductName, o => o.MapFrom(s => s.ItemOrdered.ProductName))
        .ForMember(d => d.PictureUrl, o => o.MapFrom(s => s.ItemOrdered.PictureUrl))
        .ForMember(d => d.PictureUrl, o => o.MapFrom<OrderItemPictureUrlResolver>());
        ;
    }
}
