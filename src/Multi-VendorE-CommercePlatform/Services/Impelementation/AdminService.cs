using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class AdminService : IAdminService
{
    private readonly IAdminManager _adminManager;
    private readonly ILogger<AdminService> _logger;
    private readonly IMapper _mapper;
    private readonly RoleHelper _roleHelper;
    private readonly UserHelper _userHelper;


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

    public async Task<PagedUnApproveVendorResponse> GetAllUnapprovedVendors(
        int page, int pageSize, string? search)
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");

            var isAdmin = _roleHelper.IsAdminUser();
            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");

            var (vendors, totalCount) = await _adminManager
                .UnapprovedVendors(page, pageSize, search);
            var mappedVendors = _mapper.Map<List<UnApproveVendorResponse>>(vendors);

            return new PagedUnApproveVendorResponse
            {
                Items = mappedVendors,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int) Math.Ceiling((double) totalCount / pageSize)
            };
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
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");

            var isAdmin = _roleHelper.IsAdminUser();
            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");

            await _adminManager.ChangeApprovedVendorsStatus(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<PagedUnapprovedProductResponse> GetAllUnapprovedProducts(
        int page, int pageSize, string? search)
    {
        try
        {
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");

            var isAdmin = _roleHelper.IsAdminUser();
            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");

            var (products,totalCount) = await _adminManager
                .UnapprovedProducts(page, pageSize, search);
            var mappedProduct = _mapper
                .Map<List<UnApproveProductResponse>>(products);

            return new PagedUnapprovedProductResponse
            {
                Items = mappedProduct,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int) Math.Ceiling((double) totalCount / pageSize)
            };
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
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");

            var isAdmin = _roleHelper.IsAdminUser();
            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");

            await _adminManager.ChangeApprovedProductsStatus(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}