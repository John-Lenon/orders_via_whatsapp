using Microsoft.AspNetCore.Mvc;

namespace Presentation.Atributos
{
    public class RouterControllerAttribute(string template) : RouteAttribute("api/v{version:ApiVersion}/" + template)
    {
    }
}
