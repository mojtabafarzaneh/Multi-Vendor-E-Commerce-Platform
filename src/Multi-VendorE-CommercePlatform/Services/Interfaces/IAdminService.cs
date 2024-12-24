﻿using Multi_VendorE_CommercePlatform.Contracts.Profiles;
using Multi_VendorE_CommercePlatform.Contracts.Project;

namespace Multi_VendorE_CommercePlatform.Services.Interfaces;

public interface IAdminService
{
    //TODO:ALL Customers, ALL Vendors, ALL Products, ALL Orders
    public Task<PagedUnApproveVendorResponse> GetAllUnapprovedVendors(
        int page, int pageSize, string? search);
    public Task ApproveVendors(Guid id);
    public Task<PagedUnapprovedProductResponse> GetAllUnapprovedProducts(
        int page, int pageSize, string? search);
    public Task ApproveProducts(Guid id);
}