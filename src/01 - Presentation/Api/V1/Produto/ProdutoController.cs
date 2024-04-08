using Api.Base;
using Api.Extensions.Atributos;
using Microsoft.AspNetCore.Mvc;
using Entity = Domain.Entities.Produto;

namespace Api.V1.Produto
{
    [RouterController("produto")]
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
                new Entity.Produto
                {
                    Id = 1, 
                    Preco = 24.90M,
                    Nome = "Sanduiche", 
                    Ativo = true, 
                    CaminhoImagem = "C:\\teste\\teste01"
                },
                new Entity.Produto
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
