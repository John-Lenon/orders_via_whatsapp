using Microsoft.AspNetCore.Mvc;

namespace Api.Configurations.Atributos
{
    public class RouterControllerAttribute(string template) : RouteAttribute("api/v1/" + template)
    {
    }
}
