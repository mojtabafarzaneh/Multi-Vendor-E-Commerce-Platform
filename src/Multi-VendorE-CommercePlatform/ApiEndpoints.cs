namespace Multi_VendorE_CommercePlatform;

public class ApiEndpoints
{
    private const string ApiBase = "api";

    public static class Registration
    {
        private const string Base = $"{ApiBase}/register";

        public const string Signup = Base;
        public const string VendorRegister = $"{Base}/vendor";
        public const string Login = $"{Base}/login";
        public const string AdminRegister = $"{Base}/admin/register";
        public const string RefreshToken = $"{Base}/refresh";
    }

    public static class Vendor
    {
        private const string Base = $"{ApiBase}";
        public const string Me = $"{Base}/vendor/me";
        public const string Update = $"{Base}/vendor/update";
    }

    public static class Admin
    {
        private const string Base = $"{ApiBase}/admin";
        public const string UnApproveVendors = $"{Base}/vendor";
        public const string ChangeVendorStatus = $"{Base}/vendor/status/{{id:guid}}";
        public const string ChangeProductStatus = $"{Base}/vendor/product/status/{{id:guid}}";
        public const string UnApproveProducts = $"{Base}/vendor/products";
    }

    public static class Category
    {
        private const string Base = $"{ApiBase}/category";
        public const string GetById = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Profile
    {
        private const string Base = $"{ApiBase}/profile";
        public const string ProfilePage = $"{Base}/";
    }

    public static class Product
    {
        private const string Base = $"{ApiBase}/product";
        public const string GetById = $"{Base}/{{id:guid}}";
        public const string GetAll = $"{Base}";
        public const string Create = $"{Base}";
        public const string Update = $"{Base}/{{id:guid}}";
        public const string Delete = $"{Base}/{{id:guid}}";
    }

    public static class Card
    {
        private const string Base = $"{ApiBase}/card";
        public const string Create = $"{Base}";
        public const string CustomerCard = $"{Base}";
        public const string CreateCardItem = $"{Base}/cardItem";
        public const string Update = $"{Base}/cardItem";
        public const string Delete = $"{Base}";
        public const string GetCardItemById = $"{Base}/cardItem/{{id:guid}}";
        public const string DeleteCardItem = $"{Base}/cardItem/{{id:guid}}";
        public const string GetAll = $"{Base}";
        public const string Checkout = $"{Base}/checkout";
    }
}