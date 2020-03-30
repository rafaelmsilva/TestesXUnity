using Alura.LeilaoOnline.Core;
using System;

namespace Alura.LeilaoOnline.ConsoleApp
{
    class Program
    {

        private static void Verifica(double esperado, double obtido)
        {
            if (esperado == obtido)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Teste OK");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Teste Falhou! Esperado: {esperado}, obtido: {obtido}");
            }
        }
        private static void LeilaoComVariosLances()
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

            Verifica(valorEsperado, valorObtido);

            //Assert 
            Console.WriteLine(leilao.Ganhador.Valor);
            Console.ReadKey();
        }

        private static void LeilaoComUmLance()
        {
            //1 - Arranje - cenário
            var leilao = new Leilao("Van Gogh");
            var fulano = new Interessado("Fulano", leilao);

            leilao.RecebeLance(fulano, 800);

            //2 - ACT - método sob teste
            leilao.TerminaPregao();

            var valorEsperado = 800;

            var valorObtido = leilao.Ganhador.Valor;

            Verifica(valorEsperado, valorObtido);

            //Assert 
            Console.WriteLine(leilao.Ganhador.Valor);
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            LeilaoComVariosLances();
            LeilaoComUmLance();
        }
    }
}
