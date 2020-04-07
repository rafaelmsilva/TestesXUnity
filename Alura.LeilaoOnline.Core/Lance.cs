﻿using System;

namespace Alura.LeilaoOnline.Core
{
    public class Lance
    {
        public Interessado Cliente { get; }
        public double Valor { get; }

        public Lance(Interessado cliente, double valor)
        {
            if (valor < 0)
                throw new ArgumentException("Valor do lance não deve ser negativo. Valor do lance tem que ser maior ou igual a 0");
            Cliente = cliente;
            Valor = valor;
        }
    }
}