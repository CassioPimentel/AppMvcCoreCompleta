using Dev.Business.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Dev.Business.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacao;

        public Notificador()       
        {
            _notificacao = new List<Notificacao>();
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacao.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacao()
        {
            return _notificacao;
        }

        public bool TemNotificacao()
        {
            return _notificacao.Any();
        }
    }
}