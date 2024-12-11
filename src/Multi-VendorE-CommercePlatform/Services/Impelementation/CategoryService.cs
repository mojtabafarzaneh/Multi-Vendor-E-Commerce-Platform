using AutoMapper;
using Multi_VendorE_CommercePlatform.Contracts.Project;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Models;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryManager _categoryManager;
    private readonly ILogger<CategoryService> _logger;
    private readonly IMapper _mapper;
    private readonly IAdminManager _adminManager;
    private readonly RoleHelper _roleHelper;
    private readonly UserHelper _userHelper;


    public CategoryService(ICategoryManager categoryManager, 
        ILogger<CategoryService> logger, 
        RoleHelper roleHelper,
        UserHelper userHelper, 
        IMapper mapper, 
        IAdminManager adminManager)
    {
        _categoryManager = categoryManager;
        _logger = logger;
        _roleHelper = roleHelper;
        _userHelper = userHelper;
        _mapper = mapper;
        _adminManager = adminManager;
    }

    public async Task Create(CategoryRequest request)
    {
        try
        {
            var isAdmin = _roleHelper.IsAdminUser();
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid)) throw new ArgumentException("Invalid UserId format.");

            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");
            
            var requestMapper = _mapper.Map<Category>(request);

            await _categoryManager.Create(requestMapper);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task Update(UpdateCategoryRequest request, Guid id)
    {
        try
        {
            var isAdmin = _roleHelper.IsAdminUser();
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid)) throw new ArgumentException("Invalid UserId format.");

            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");

            if (await _categoryManager.DoesExist(id))
            {
                throw new ArgumentException("the category you are trying to update does not exist.");
            }
            
            var requestMapper = _mapper.Map<Category>(request);
            
            await _categoryManager.Update(requestMapper);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }    
    }

    public async Task Delete(Guid id)
    {
        try
        {
            var isAdmin = _roleHelper.IsAdminUser();
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid)) throw new ArgumentException("Invalid UserId format.");

            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");

            if (await _categoryManager.DoesExist(id))
            {
                throw new ArgumentException("the category you are trying to update does not exist.");
            }
            
            await _categoryManager.Delete(id);

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }    
        
    }

    public async Task<CategoryResponse> GetCategoryByIdAsync(Guid id)
    {
        try
        {
            var isAdmin = _roleHelper.IsAdminUser();
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid)) throw new ArgumentException("Invalid UserId format.");

            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");
            var category = await _categoryManager.GetCategory(id);
            var response = _mapper.Map<CategoryResponse>(category);
        
            return response;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<List<CategoryResponse>> GetCategoriesAsync()
    {
        try
        {
            var isAdmin = _roleHelper.IsAdminUser();
            var userId = _userHelper.UserId();
            if (userId == null) throw new UnauthorizedAccessException();
            if (!Guid.TryParse(userId, out var userGuid)) throw new ArgumentException("Invalid UserId format.");

            if (!isAdmin || !await _adminManager.DoesAdminExist(userGuid))
                throw new UnauthorizedAccessException("you are not administrator.");
            var category = await _categoryManager.GetAllCategories();
            if (category == null) throw new NullReferenceException();
            var response = _mapper.Map<List<CategoryResponse>>(category);
            return response;

        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
        
    }
}