namespace  BlogApi;

public static class Configuration
{
    public static string JwtKey = "gerarumachavesegura124!gerarumachavesegura124!gerarumachavesegura124!";
    public static string ApiKeyName = "api_key";
    public static string ApiKey = "Blog_API_eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bm";

    public static SmtpConfiguration Smtp = new();

    public class SmtpConfiguration
    {
        public string Host { get; set; }
        public int Port { get; set; } = 25;
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
