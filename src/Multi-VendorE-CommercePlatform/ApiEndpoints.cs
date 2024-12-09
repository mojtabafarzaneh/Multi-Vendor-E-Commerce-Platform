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
        public const string Register = $"{Base}/manage";
    }
}