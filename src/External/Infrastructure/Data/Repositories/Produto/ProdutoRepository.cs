﻿using Domain.Entities;
using Domain.Interfaces.Repositories;
using Infrastructure.Data.Context;
using Infrastructure.Data.Repository.Base;

namespace Infrastructure.Data.Repositories
{
    public class ProdutoRepository(IServiceProvider serviceProvider) :
        RepositorioBase<Produto, OrderViaWhatsAppContext>(serviceProvider),
            IProdutoRepository
    {
    }
}
