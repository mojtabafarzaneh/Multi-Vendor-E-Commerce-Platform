using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Impelementation;

public class VendorService: IVendorService
{
    private readonly ILogger<VendorService> _logger;
    private readonly RoleHelper _roleHelper;
    private readonly UserHelper _userHelper;
    private readonly IMapper _mapper;
    private readonly IVendorManager _vendorManager;

    public VendorService(ILogger<VendorService> logger,
        RoleHelper roleHelper,
        UserHelper userHelper,
        IVendorManager vendorManager,
        IMapper mapper)
    {
        _logger = logger;
        _roleHelper = roleHelper;
        _userHelper = userHelper;
        _vendorManager = vendorManager;
        _mapper = mapper;
    }

    public async Task<VendorResponse> GetVendorByIdAsync()
    {
        try
        {
            var isVendor = _roleHelper.IsVendorUser();
            var userId = _userHelper.UserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            if (!Guid.TryParse(userId, out var userGuid))
            {
                throw new ArgumentException("Invalid UserId format.");
            }

            if (!isVendor || !await _vendorManager.DoesVendorExist(userGuid) )
            {
                throw new UnauthorizedAccessException("User is not a Vendor.");
            }

            var vendor = await _vendorManager.GetVendorById(userGuid);
            if (vendor == null)
            {
                throw new UnauthorizedAccessException("User is not a Vendor.");
            }
            var vendorResponse = _mapper.Map<VendorResponse>(vendor);
            vendorResponse.Products = _mapper.Map<ICollection<ProductResponse>>(vendor.Products);
            
            return vendorResponse;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task UpdateVendorBusinessAsync(VendorUpdateEmail request)
    {
        try
        {
            var isVendor = _roleHelper.IsVendorUser();
            var userId = _userHelper.UserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }
            if (!Guid.TryParse(userId, out var userGuid))
            {
                throw new ArgumentException("Invalid UserId format.");
            }

            if (!isVendor || !await _vendorManager.DoesVendorExist(userGuid) )
            {
                throw new UnauthorizedAccessException("User is not a Vendor.");
            }

            await _vendorManager.UpdateEmail(userGuid, request.BusinessEmail);


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task DeleteVendorAsync()
    {
        try
        {
            var isVendor = _roleHelper.IsVendorUser();
            var userId = _userHelper.UserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!Guid.TryParse(userId, out var userGuid))
            {
                throw new ArgumentException("Invalid UserId format.");
            }

            if (!isVendor || !await _vendorManager.DoesVendorExist(userGuid))
            {
                throw new UnauthorizedAccessException("User is not a Vendor.");
            }

            var vendor = await _vendorManager.GetVendorById(userGuid);

            await _vendorManager.Delete(vendor);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
    }
}