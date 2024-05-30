using Api.Configurations.Extensions.ManageDependencies;
using Infrastructure.Data.Configurations;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureApplication();
builder.SetDatabaseProvider();
builder.AddServicesDependencyInjectionSystem();

var app = builder.Build();

app.AddCorsPolicy(builder);
await app.ConfigurarBancoDeDadosAsync();
app.MapControllers();
app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<IdentificadorDataBaseMiddleware>();
app.Run();