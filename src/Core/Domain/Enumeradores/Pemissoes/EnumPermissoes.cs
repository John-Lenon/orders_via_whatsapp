using System.ComponentModel;

namespace Domain.Enumeradores.Pemissoes
{
    public enum EnumPermissoes
    {
        /// <summary>
        /// Permissão para criar outros usuarios.
        /// </summary>
        [Description("Permissao para criar outros usuarios.")]
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

        /// <summary>
        /// Permissão para atualizar outros usuarios.
        /// </summary>
        [Description("Permissao para atualizar outros usuarios.")]
        USU_000004 = 4,

        /// <summary>
        /// Permissão para deletar outros usuarios.
        /// </summary>
        [Description("Permissao para deletar outros usuarios.")]
        USU_000005 = 5,
    }
}
