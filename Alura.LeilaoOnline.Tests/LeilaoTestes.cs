using Alura.LeilaoOnline.Core;
using System;
using Xunit;

namespace Alura.LeilaoOnline.Tests
{
    public class LeilaoTestes
    {
        [Fact]
        public void LeilaoComVariosLances()
        {

            //1 - Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessado("Fulano", leilao);
            var maria = new Interessado("Maria", leilao);

            leilao.RecebeLance(fulano, 800);
            leilao.RecebeLance(maria, 900);
            leilao.RecebeLance(fulano, 1000);


            //2 - ACT - método sob teste
            leilao.TerminaPregao();

            var valorEsperado = 1000;

            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);


        }

        [Fact]
        public void LeilaoComUmLance()
        {
            //1 - Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessado("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);

            //2 - ACT - método sob teste
            leilao.TerminaPregao();

            var valorEsperado = 800;

            var valorObtido = leilao.Ganhador.Valor;

            Assert.Equal(valorEsperado, valorObtido);

        }
    }
}
