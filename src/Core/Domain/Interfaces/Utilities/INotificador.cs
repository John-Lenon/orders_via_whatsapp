using Domain.Enumeradores.Notificacao;
using Domain.Utilities;

namespace Domain.Interfaces.Utilities
{
    public interface INotificador
    {
        List<Notificacao> ListNotificacoes { get; }
        void Notify(EnumTipoNotificacao tipo, string message);
        bool HasNotifications(EnumTipoNotificacao tipo, out Notificacao[] notifications);
        void Clear();
    }
}
