using Domain.Enumeradores.Notificacao;
using Domain.Interfaces.Utilities;
using Domain.Utilities;

namespace Application.Utilities
{
    public class Notificador : INotificador
    {
        public List<Notificacao> ListNotificacoes { get; } = new List<Notificacao>();

        public void Notify(EnumTipoNotificacao tipo, string mensagem) =>
            ListNotificacoes.Add(new Notificacao(tipo, mensagem));

        public bool HasNotifications(EnumTipoNotificacao tipo, out Notificacao[] notifications)
        {
            notifications = ListNotificacoes.Where(notificacao => notificacao.Tipo == tipo).ToArray();
            return notifications.Length > 0;
        }

        public void Clear()
        {
            ListNotificacoes.Clear();
        }
    }
}
