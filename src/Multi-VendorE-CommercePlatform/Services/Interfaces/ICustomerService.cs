using Multi_VendorE_CommercePlatform.Contracts.Profiles;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface ICustomerService
{
    public Task<ProfileResponse> Profile();
}