using System;
using System.Collections.Generic;
using System.Linq;

namespace Alura.LeilaoOnline.Core
{
    public enum EstadoLeilao
    {
        LeilaoAntesDoPregao,
        LeilaoEmAndamento,
        LeilaoFinalizado
    }
    public class Leilao
    {
        private IList<Lance> _lances;
        public IEnumerable<Lance> Lances => _lances;
        public string Peca { get; }
        public Lance Ganhador { get; private set; }
        public EstadoLeilao Estado { get; private set; }
        private Interessado _ultimoCliente;


        public Leilao(string peca)
        {
            Peca = peca;
            _lances = new List<Lance>();
            Estado = EstadoLeilao.LeilaoAntesDoPregao;
        }

        private bool AceitaLance(Interessado cliente, double valor)
        {
            return (Estado == EstadoLeilao.LeilaoEmAndamento && cliente != _ultimoCliente);
        }

        public void RecebeLance(Interessado cliente, double valor)
        {
            if (AceitaLance(cliente,valor))
            {
                _lances.Add(new Lance(cliente, valor));
                _ultimoCliente = cliente;
            }

        }

        public void IniciaPregao()
        {
            Estado = EstadoLeilao.LeilaoEmAndamento;

        }

        public void TerminaPregao()
        {
            if(Estado != EstadoLeilao.LeilaoEmAndamento)
            {
                throw new InvalidOperationException("Não é possível terminar o pregão sem que ele tenha começado. Utilize o método IniciaPregao()"); 
            }
            Ganhador = Lances.
                DefaultIfEmpty(new Lance(null, 0)).OrderBy(l => l.Valor).LastOrDefault();
            Estado = EstadoLeilao.LeilaoFinalizado;

        }
    }
}