using AutoMapper;
using Multi_VendorE_CommercePlatform.Helpers;
using Multi_VendorE_CommercePlatform.Repositories.Interfaces;
using Multi_VendorE_CommercePlatform.Services.Interfaces;

namespace Multi_VendorE_CommercePlatform.Services.Implenetations;

public class CategoryService : ICategoryService
{
    private readonly ICategoryManager _categoryManager;
    private readonly ILogger<CategoryService> _logger;
    private readonly IMapper _mapper;
    private readonly RoleHelper _roleHelper;
    private readonly UserHelper _userHelper;


    public CategoryService(ICategoryManager categoryManager, ILogger<CategoryService> logger, RoleHelper roleHelper,
        UserHelper userHelper, IMapper mapper)
    {
        _categoryManager = categoryManager;
        _logger = logger;
        _roleHelper = roleHelper;
        _userHelper = userHelper;
        _mapper = mapper;
    }

    public Task CreateCategory()
    {
        throw new NotImplementedException();
    }
}