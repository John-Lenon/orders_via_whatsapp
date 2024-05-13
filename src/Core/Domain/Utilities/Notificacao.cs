﻿using Domain.Enumeradores.Notificacao;

namespace Domain.Utilities
{
    public class Notificacao
    {
        public EnumTipoNotificacao Tipo { get; set; } = EnumTipoNotificacao.Informacao;
        public string Descricao { get; set; }

        public Notificacao(EnumTipoNotificacao tipo, string mensagem)
        {
            Tipo = tipo;
            Descricao = mensagem;
        }

        public Notificacao() { }
    }
}
