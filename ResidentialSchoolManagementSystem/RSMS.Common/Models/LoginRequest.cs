namespace RSMS.Common.Models
{
    public class ApiClient
    {
        public string ClientId { get; set; } = string.Empty;
    }

    public class LoginRequest
    {
        public string ClientId { get; set; } = string.Empty;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
