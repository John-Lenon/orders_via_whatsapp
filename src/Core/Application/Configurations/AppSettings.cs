using Microsoft.Extensions.Configuration;

namespace Application.Configurations
{
    public class AppSettings
    {
        public static string[] AllowedOrigins { get; private set; }
        public static CompanyConnectionStrings CompanyConnectionStrings { get; private set; }
        public static AuthenticationTokenConfig JwtConfigs { get; private set; }
        public static string CompanyFilePaths { get; private set; }

        public static void CarregarDados(IConfiguration config)
        {
            AllowedOrigins = config.GetSection("AllowedOrigins")?.Get<string[]>();

            JwtConfigs = config.GetSection("JwtConfigs")?.Get<AuthenticationTokenConfig>();

            CompanyFilePaths = config["CompanyFilePaths"];

            CompanyConnectionStrings = config
                .GetSection("CompanyConnectionStrings")?
                .Get<CompanyConnectionStrings>();
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
            var defaultConnectionString = List.FirstOrDefault(x =>
                x.NomeDominio == "default"
            )?.ConnnectionString;

            if (string.IsNullOrEmpty(defaultConnectionString))
                throw new Exception(
                    "Não foi definido uma string de conexão padrão no arquivo appsettings"
                );

            return defaultConnectionString;
        }
    }

    public class CompanyInfo
    {
        public string NomeDominio { get; set; }
        public string ConnnectionString { get; set; }
    }
}
