using Api.Configurations.Extensions.ManageDependencies;
using Api.Configurations.Extensions.Swagger;
using Infrastructure.Data.Configurations;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.ConfigureApplication();

builder.SetDatabaseProvider();

builder.AddServicesDependencyInjectionSystem();

var app = builder.Build();

app.UseStaticFiles();

app.ConfigureSwaggerUI();

app.AddCorsPolicy(builder);

app.UseHttpsRedirection();

app.UseRouting();

await app.ConfigurarBancoDeDadosAsync();

app.UseMiddleware<ExceptionMiddleware>();

app.UseMiddleware<IdentificadorDataBaseMiddleware>();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet(
    "/{*path}",
    async context =>
    {
        context.Response.Redirect("/doc");
        await Task.CompletedTask;
    }
);

app.Run();