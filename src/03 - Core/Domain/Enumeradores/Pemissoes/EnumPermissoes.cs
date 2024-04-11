﻿using System.ComponentModel;

namespace Domain.Enumeradores.Pemissoes
{
    public enum EnumPermissoes
    {
        /// <summary>
        /// Permissão para criar.
        /// </summary>
        [Description("Permissao para criar.")]
        USU_000001 = 1,

        /// <summary>
        /// Permissão para atualizar.
        /// </summary>
        [Description("Permissao para atualizar.")]
        USU_000002 = 2,

        /// <summary>
        /// Permissão para deletar.
        /// </summary>
        [Description("Permissao para deletar.")]
        USU_000003 = 3,
    }
}
