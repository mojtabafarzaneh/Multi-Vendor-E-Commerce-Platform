using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface IVendorService
{
    public Task<VendorResponse> GetVendorByIdAsync();
    public Task UpdateVendorBusinessAsync(VendorUpdateEmail request);
    public Task DeleteVendorAsync();
}