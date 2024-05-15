using Microsoft.Extensions.Configuration;

namespace Application.Configurations
{
    public class AppSettings
    {
        public static string[] AllowedOrigins { get; private set; }
        public static CompanyConnectionStrings CompanyConnectionStrings { get; private set; }
        public static AuthenticationTokenConfig JwtConfigs { get; private set; }

        public static void CarregarDados(IConfiguration config)
        {
            AllowedOrigins = config.GetSection("AllowedOrigins")?.Get<string[]>();
            CompanyConnectionStrings = config.GetSection("CompanyConnectionStrings")?.Get<CompanyConnectionStrings>();
            JwtConfigs = config.GetSection("JwtConfigs")?.Get<AuthenticationTokenConfig>();
        }
    }

    public class AuthenticationTokenConfig
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
    }

    public class CompanyConnectionStrings
    {
        public CompanyInfo[] List { get; set; }
        public string GetDefaultString()
        {
            return List.First(x => x.NomeDominio == "default").ConnnectionString;
        }
    }

    public class CompanyInfo
    {
        public string NomeDominio { get; set; }
        public string ConnnectionString { get; set; }
    }
}
