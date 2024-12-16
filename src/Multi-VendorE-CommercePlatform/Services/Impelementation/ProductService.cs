using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class ProductService : IProductService
{
    private readonly ILogger<ProductService> _logger;
    private readonly RoleHelper _roleHelper;
    private readonly UserHelper _userHelper;
    private readonly IProductManager _productManager;
    private readonly IMapper _mapper;

    public ProductService(ILogger<ProductService> logger,
        RoleHelper roleHelper,
        UserHelper userHelper,
        IProductManager productManager,
        IMapper mapper)
    {
        _logger = logger;
        _roleHelper = roleHelper;
        _userHelper = userHelper;
        _productManager = productManager;
        _mapper = mapper;
    }

    public async Task AddProduct(ProductRequest request)
    {
        try
        {
            var userId = _userHelper.UserId();
            var isVendor = _roleHelper.IsVendorUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid)) 
                throw new ArgumentException("Invalid UserId format.");

            if (!isVendor || !await _productManager.DoesVendorExist(userGuid))
                throw new UnauthorizedAccessException("User is not a Vendor.");
            
            var mappedProduct = _mapper.Map<Product>(request);
            var vendorId = await _productManager.VendorId(userGuid);
            if (vendorId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("you are not the Vendor.");
            }

            mappedProduct.VendorId = vendorId;
            if (!await _productManager.DoesCategoryExist(mappedProduct.CategoryId))
            {
                throw new ArgumentException("Invalid Category ID.");
            }

            if (await _productManager.DoesExist(mappedProduct.Name))
            {
                throw new ArgumentException("product already exists.");
            }

            await _productManager.Create(mappedProduct);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<PagedProductResponse> GetAllProducts(
        int page, int pageSize, string? search, bool isApproved)
    {
        try
        {
            var userId = _userHelper.UserId();
            var isVendor = _roleHelper.IsVendorUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");

            if (!isVendor || !await _productManager.DoesVendorExist(userGuid))
                throw new UnauthorizedAccessException("User is not a Vendor.");

            var vendorId = await _productManager.VendorId(userGuid);

            var (products, totalCount) = await _productManager
                .GetAll(vendorId, page, pageSize, search, isApproved);
            var response = _mapper.Map<List<ProductResponse>>(products);
            return new PagedProductResponse
            {
                Items = response,
                TotalCount = totalCount,
                CurrentPage = page,
                PageSize = pageSize,
                TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize)
            };

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<ProductResponse> GetProductById(Guid id)
    {
        try
        {
            var userId = _userHelper.UserId();
            var isVendor = _roleHelper.IsVendorUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");

            if (!isVendor || !await _productManager.DoesVendorExist(userGuid))
                throw new UnauthorizedAccessException("User is not a Vendor.");
            if (!await _productManager.DoesVendorExist(userGuid))
            {
                throw new ArgumentException("you are not the vendor.");
            }

            if (!await _productManager.IsApproved(id))
            {
                throw new ArgumentException("product is not approved.");
            }
            
            var product = await _productManager.GetById(id);
            var response = _mapper.Map<ProductResponse>(product);
            return response;


        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task UpdateNameAndDescription(UpdateProductNameAndDescription request, Guid id)
    {
        try
        {
            var userId = _userHelper.UserId();
            var isVendor = _roleHelper.IsVendorUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");

            if (!isVendor || !await _productManager.DoesVendorExist(userGuid))
                throw new UnauthorizedAccessException("User is not a Vendor.");
            
            if (!await _productManager.DoesVendorExist(userGuid))
            {
                throw new ArgumentException("you are not the vendor.");
            }

            if (id == Guid.Empty)
            {
                throw new ArgumentException("You have to enter the product ID.");
            }

            if (request.Name == null || request.Description == null)
            {
                throw new ArgumentException("You have to provide a name and a description.");
            }
            request.Id = id;
            await _productManager.UpdateNameAndDescription(request);

            
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task RemoveProduct(Guid id)
    {
        try
        {
            var userId = _userHelper.UserId();
            var isVendor = _roleHelper.IsVendorUser();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid))
                throw new ArgumentException("Invalid UserId format.");

            if (!isVendor || !await _productManager.DoesVendorExist(userGuid))
                throw new UnauthorizedAccessException("User is not a Vendor.");
            
            if (!await _productManager.DoesVendorExist(userGuid))
            {
                throw new ArgumentException("you are not the vendor.");
            }

            await _productManager.Delete(id);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }
}
