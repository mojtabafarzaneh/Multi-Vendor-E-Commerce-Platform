using AutoMapper;
using Microsoft.AspNetCore.Identity.Data;
using Multi_VendorE_CommercePlatform.Contracts.Authentication;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Configuration;

public class MapperConfiguration: Profile
{
    public MapperConfiguration()
    {
        //registration
        CreateMap<User, RegistrationRequest>().ReverseMap();
        CreateMap<User, UserLoginRequest>().ReverseMap();
        CreateMap<User, AuthUserResponse>().ReverseMap();
    }
    
}