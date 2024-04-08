using Application.Configurations;
using Data.Configurations;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.ConfigurarBancoDeDados();
builder.Services.ConfigurarBancoDeDados();
builder.Services.AddDependencyInjectionsApp();

CarregarAppSettings(builder.Services, builder.Configuration);

var app = builder.Build();

var companyConnections = app.Services.GetRequiredService<CompanyConnectionStrings>();
app.Services.ConfigurarBancoDeDados(companyConnections);

app.MapControllers();

app.Run();

void CarregarAppSettings(IServiceCollection services, IConfiguration configuration)
{
    var appSettingsSection = configuration.GetSection(nameof(CompanyConnectionStrings));
    var appSettings = appSettingsSection.Get<CompanyConnectionStrings>();
    services.AddSingleton(appSettings);
}