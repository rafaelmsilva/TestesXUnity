using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTerminaPregao
    {
        [Theory]
        [InlineData(1200, 1250, new double[] { 800, 1150, 1400, 1250 })]
        public void RetornaValorSuperiorMaisProximoDadoLeilaoNessaModalidade(double valorDestino, double valorEsperado, double[] ofertas)
        {
            //1 - Arranje - cenário
            IModalidadeAvaliacao modalidade = new OfertaSuperiorMaisProxima(valorDestino);
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessado("Fulano", leilao);
            var maria = new Interessado("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);

                }
                else
                    leilao.RecebeLance(maria, valor);
            }

            //2 - ACT - método sob teste
            leilao.TerminaPregao();

            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }

        [Theory]
        [InlineData(1000, new double[] { 800, 900, 1000 })]
        [InlineData(1000, new double[] { 800, 900, 1000, 990 })]
        [InlineData(800, new double[] { 800 })]
        public void RetornaMaiorValorDadoLeilaopComPeloMenosUmLance(double valorEsperado, double[] ofertas)
        {

            //1 - Arranje - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh", modalidade);
            var fulano = new Interessado("Fulano", leilao);
            var maria = new Interessado("Maria", leilao);

            leilao.IniciaPregao();

            for (int i = 0; i < ofertas.Length; i++)
            {
                var valor = ofertas[i];
                if ((i % 2) == 0)
                {
                    leilao.RecebeLance(fulano, valor);

                }
                else
                    leilao.RecebeLance(maria, valor);
            }

            //2 - ACT - método sob teste
            leilao.TerminaPregao();

            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);
        }


        [Fact]
        public void LancaInvalidOperationExceptionDadoPregaoNaoIniciado()
        {
            //1 - Arranjo - cenário
            var modalidade = new MaiorValor();
            var leilao = new Leilao("Van Gogh",modalidade);

            //Assert

            var excecaoObtida = Assert.Throws<InvalidOperationException>(
                 //Act - método sob teste
                 () => leilao.TerminaPregao());

            var msgEsperada = "Não é possível terminar o pregão sem que ele tenha começado. Utilize o método IniciaPregao()";
            Assert.Equal(msgEsperada, excecaoObtida.Message);

        }

        [Fact]
        public void RetornaZeroDadoLeilaoSemLance()
        {
            //1 - Arranje - cenário
            var modalidade = new MaiorValor();

            var leilao = new Leilao("Van Gogh", modalidade);

            leilao.IniciaPregao();

            leilao.TerminaPregao();

            var valorEsperado = 0;


            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }
    }
}
