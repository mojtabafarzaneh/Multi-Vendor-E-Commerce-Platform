namespace Multi_VendorE_CommercePlatform.Contracts.Authentication;

public class AuthUserResponse
{
    public Guid UserId { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }
}