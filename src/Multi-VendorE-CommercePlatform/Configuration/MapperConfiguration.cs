﻿using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Implementations;

namespace Multi_VendorE_CommercePlatform.Configuration;

public class MapperConfiguration: Profile
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
        
        //Vendor
        CreateMap<Vendor, CreateVendor>().ReverseMap();
        CreateMap<Vendor, VendorRegistrationRequest>().ReverseMap();
        CreateMap<Vendor, VendorResponse>().ReverseMap();
        
        //products
        CreateMap<Product, ProductResponse>().ReverseMap();
    }
    
}