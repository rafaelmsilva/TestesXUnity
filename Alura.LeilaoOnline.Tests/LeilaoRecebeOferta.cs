using Alura.LeilaoOnline.Core;
using System.Linq;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoRecebeOferta
    {
        [Fact]
        public void NaoAceitaProximoLanceDadoMesmoClienteRealizouUltimoLance()
        {
            //1 - Arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessado("Fulano", leilao);

            leilao.IniciaPregao();

            //2- Act
            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(fulano, 1000);

            //3- Assert
            var quantidadeEsperada = 1;
            var quantidadeObtida = leilao.Lances.Count();
            Assert.Equal(quantidadeEsperada, quantidadeObtida);

        }

        [Theory]
        [InlineData(2, new double[] { 800, 900 })]
        [InlineData(4, new double[] { 100, 1200, 1300, 1400 })]
        public void NaoPermiteNovosLancesDadoLeilaoFinalizado(int quantidadeEsperada, double[] ofertas)
        {
            //1 - Arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);
            var fulano = new Interessado("Fulano", leilao);
            var maria = new Interessado("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];

                if ((i % 2) == 0)
                    leilao.RecebeLance(fulano, valor);
                else
                    leilao.RecebeLance(maria, valor);
            }

            leilao.TerminaPregao();

            //2 - ACT - método sob teste
            leilao.RecebeLance(fulano, 1000);
            //Assert 
            var valorEsperado = quantidadeEsperada;
            var valorObtido = leilao.Lances.Count();

            Assert.Equal(valorEsperado, valorObtido);
        }
    }
}
