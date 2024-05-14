using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Presentation.Atributos;
using Presentation.Base;
using Presentation.Configurations.Extensions;
using Entity = Domain.Entities.Produto;

namespace Presentation.V1
{
    [ApiController]
    [RouterController("produto")]
    [ApiVersion(ApiConfig.V1)]
    public class ProdutoController : MainController
    {
        public ProdutoController(IServiceProvider serviceProvider) : base(serviceProvider) 
        { 
        }

        [HttpGet]
        public IEnumerable<Entity.Produto> GetAsync()
        {
            return new Entity.Produto[]
            {
                new()
                {
                    Id = 1,
                    Preco = 24.90M,
                    Nome = "Sanduiche",
                    Ativo = true,
                    CaminhoImagem = "C:\\teste\\teste01"
                },
                new()
                {
                    Id = 1,
                    Preco = 34.98M,
                    Nome = "Temaki",
                    Ativo = true,
                    CaminhoImagem = "C:\\teste\\teste01"
                }
            };
        }
    }
}
