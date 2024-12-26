using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Cards;
using Multi_VendorE_CommercePlatform.Contracts.Order;
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

    public async Task<PagedOrderResponse> GetAllOrders(
        int page, int pageSize)
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

            decimal totalPrice = 0;
            int totalQuantity = 0;
            
            var orders = await _adminManager
                .GetOrders(page, pageSize);
            var listOrders = new List<SingleOrderResponse>();
            foreach (var order in orders)
            {
                var mappedOrder = _mapper.Map<SingleOrderResponse>(order);
                var mappedOrderItems = _mapper.Map<List<OrderItemResponse>>(order.OrderItems);
                foreach (var mappedOrderItem in mappedOrderItems)
                {
                    mappedOrderItem.TotalPrice = mappedOrderItem.Price * mappedOrderItem.Quantity;
                    totalPrice += mappedOrderItem.TotalPrice;
                    totalQuantity += mappedOrderItem.Quantity;
                }
                mappedOrder.Items = mappedOrderItems;
                listOrders.Add(mappedOrder);
            }

            return new PagedOrderResponse
            {
                Order = listOrders,
                TotalPrice = totalPrice,
                TotalQuantity = totalQuantity,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling((double)totalQuantity / pageSize)
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
    public async Task<PagedProductResponse> GetAllProducts(
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

            var (product,totalCount) = await _adminManager.GetProducts(
                page, pageSize, search);
            var mappedProduct= _mapper.Map<List<ProductResponse>>(product);
            return new PagedProductResponse
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

    public async Task<List<VendorResponse>> GetAllVendors(
        int page, int pageSize)
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

            var vendors = await _adminManager
                .GetVendors(page, pageSize);
            var listVendors = new List<VendorResponse>();
            foreach (var vendor in vendors)
            {
                var mappedVendor = _mapper.Map<VendorResponse>(vendor);
                var mappedProduct = _mapper.Map<List<ProductResponse>>(vendor.Products);
                mappedVendor.Products = mappedProduct;
                listVendors.Add(mappedVendor);
            }
            return listVendors;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<List<CustomerResponse>> GetAllCustomers(
        int page, int pageSize)
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
            var customers = await _adminManager.GetCustomers(
                page, pageSize);
            var listCustomers = new List<CustomerResponse>();

            foreach (var customer in customers)
            {
                var mappedCustomer = _mapper.Map<CustomerResponse>(customer);
                var mappedCards = _mapper.Map<List<CardResponse>>(customer.Cards);
                var mappedOrders = _mapper.Map<List<OrderResponse>>(customer.Orders);
                mappedCustomer.Card = mappedCards;
                mappedCustomer.Order = mappedOrders;
                listCustomers.Add(mappedCustomer);
            }

            return listCustomers;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}