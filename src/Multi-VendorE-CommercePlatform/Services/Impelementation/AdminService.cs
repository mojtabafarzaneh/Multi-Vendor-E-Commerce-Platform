using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;


namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class AdminService: IAdminService
{
    private readonly RoleHelper _roleHelper;
    private readonly UserHelper _userHelper;
    private readonly IAdminManager _adminManager;
    private readonly ILogger<AdminService> _logger;
    private readonly IMapper _mapper;
    

    public AdminService(RoleHelper roleHelper, 
        UserHelper userHelper,
        IAdminManager adminManager, 
        ILogger<AdminService> logger,
        IMapper mapper)
    {
        _roleHelper = roleHelper;
        _userHelper = userHelper;
        _adminManager = adminManager;
        _logger = logger;
        _mapper = mapper;
    }


    private Guid UserId()
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null)
            {
                throw new UnauthorizedAccessException();
            }

            if (!Guid.TryParse(userId, out var userGuid))
            {
                throw new ArgumentException("Invalid UserId format.");
            }

            return userGuid;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }

    }
    private async Task<bool> IsAdmin(Guid userId)
    {
        try
        {
            var isAdmin = _roleHelper.IsAdminUser();

            if (!isAdmin || !await _adminManager.DoesAdminExist(userId))
            {
                throw new UnauthorizedAccessException("User is not a Vendor.");
            }

            return isAdmin;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
    }
    public async Task<List<UnApproveVendorResponse>> GetAllUnapprovedVendors()
    {
        try
        {
            var userId = UserId();
            if (!await IsAdmin(userId))
            {
                throw new UnauthorizedAccessException("User is not a Admin.");
            }
            var vendors = await _adminManager.UnapprovedVendors();
            return _mapper.Map<List<UnApproveVendorResponse>>(vendors);
            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task ApproveVendors(Guid id)
    {
        try
        {
            var userId = UserId();
            if (!await IsAdmin(userId))
            {
                throw new UnauthorizedAccessException("User is not a Admin.");
            }
            await _adminManager.ChangeApprovedVendorsStatus(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<ICollection<UnApproveProductResponse>> GetAllUnapprovedProducts()
    {
        try
        {
            var userId = UserId();
            if (!await IsAdmin(userId))
            {
                throw new UnauthorizedAccessException("User is not a Admin.");
            }

            var products = await _adminManager.UnapprovedProducts();
            var response = _mapper
                .Map<ICollection<UnApproveProductResponse>>(products);
            
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task ApproveProducts(Guid id)
    {
        try
        {
            var userId = UserId();
            if (!await IsAdmin(userId))
            {
                throw new UnauthorizedAccessException("User is not a Admin.");
            }

            await _adminManager.ChangeApprovedProductsStatus(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }

    }
}