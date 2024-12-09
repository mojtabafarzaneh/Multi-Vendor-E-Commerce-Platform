using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Implementations;

namespace Multi_VendorE_CommercePlatform.Configuration;

public class MapperConfiguration: Profile
{
    public MapperConfiguration()
    {
        //registration
        CreateMap<User, RegistrationRequest>().ReverseMap();
        CreateMap<User, UserLoginRequest>().ReverseMap();
        CreateMap<User, AuthUserResponse>().ReverseMap();
        CreateMap<User, CreateUser>().ReverseMap();
        CreateMap<CreateUser, RegistrationRequest>().ReverseMap();
        
        //Costumer
        CreateMap<Customer, CreateCustomer>().ReverseMap();
        CreateMap<CreateCustomer, RegistrationRequest>().ReverseMap();
    }
    
}