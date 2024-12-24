using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;
using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Contracts.Order;


namespace Multi_VendorE_CommercePlatform.Configuration;

public class MapperConfiguration : Profile
{
    public MapperConfiguration()
    {
        //registration
        CreateMap<User, CustomerRegistrationRequest>().ReverseMap();
        CreateMap<User, UserLoginRequest>().ReverseMap();
        CreateMap<User, AuthUserResponse>().ReverseMap();
        CreateMap<User, CreateUser>().ReverseMap();
        CreateMap<CreateUser, CustomerRegistrationRequest>().ReverseMap();

        //Costumer
        CreateMap<Customer, CreateCustomer>().ReverseMap();
        CreateMap<CreateCustomer, CustomerRegistrationRequest>().ReverseMap();
        CreateMap<Customer, ProfileResponse>().ReverseMap();
        CreateMap<Customer, CustomerResponse>().ReverseMap();

        //Vendor
        CreateMap<Vendor, CreateVendor>().ReverseMap();
        CreateMap<Vendor, VendorRegistrationRequest>().ReverseMap();
        CreateMap<Vendor, VendorResponse>().ReverseMap();
        CreateMap<Vendor, UnApproveVendorResponse>().ReverseMap();

        //products
        CreateMap<Product, ProductResponse>().ReverseMap();
        CreateMap<Product, UnApproveProductResponse>().ReverseMap();
        CreateMap<Product, ProductRequest>().ReverseMap();
        
        //Category 
        CreateMap<Category, CategoryResponse>().ReverseMap();
        CreateMap<Category, UpdateCategoryRequest>().ReverseMap();
        
        //Card
        CreateMap<Card, CardResponse>().ReverseMap();
        CreateMap<CardItem, CardItemResponse>().ReverseMap();
        CreateMap<CardItem, CreateCardItemRequest>().ReverseMap();
        CreateMap<CardItem, UpdateCardItem>().ReverseMap();
        
        //CardItem
        CreateMap<Order, OrderResponse>().ReverseMap();
        CreateMap<OrderItem, OrderItemResponse>().ReverseMap();
    }
}