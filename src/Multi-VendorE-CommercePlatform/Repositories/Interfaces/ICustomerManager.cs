using Multi_VendorE_CommercePlatform.Models;

namespace Multi_VendorE_CommercePlatform.Repositories.Interfaces;

public interface ICustomerManager
{
    public Task Create(Customer customer);

}