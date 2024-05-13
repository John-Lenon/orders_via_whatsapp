using Domain.Utilities;

namespace Domain.Interfaces.Utilities
{
    public interface INotificador
    {
        List<Notificacao> ListNotificacoes { get; }
        void Add(Notificacao notificacao);
        void Clear();
    }
}
