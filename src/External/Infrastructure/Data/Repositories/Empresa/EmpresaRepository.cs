using Domain.Entities.Empresa;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository.Base;

namespace Infrastructure.Data.Repositories
{
    public class EmpresaRepository(IServiceProvider serviceProvider) :
       RepositorioBase<Empresa, OrderViaWhatsAppContext>(serviceProvider),
           IEmpresaRepository
    {
    }
}
