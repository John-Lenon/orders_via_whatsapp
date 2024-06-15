using Domain.Entities.Empresa;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository.Base;

namespace Infrastructure.Data.Repositories
{
    public class HorarioFuncionamentoRepository(IServiceProvider serviceProvider) :
       RepositorioBase<HorarioFuncionamento, OrderViaWhatsAppContext>(serviceProvider),
           IHorarioFuncionamentoRepository
    {
    }
}
